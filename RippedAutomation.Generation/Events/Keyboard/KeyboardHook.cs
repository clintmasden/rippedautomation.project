using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using RippedAutomation.Generation.Events.Hook;
using RippedAutomation.Generation.Events.Keyboard.Extensions;
using RippedAutomation.Generation.Events.Keyboard.Models;
using RippedAutomation.Generation.Events.Native;
using RippedAutomation.Generation.Events.Native.Models;

namespace RippedAutomation.Generation.Events.Keyboard
{
    /// <summary>
    ///     Keyboard Hook Mechanism + Spyware
    /// </summary>
    internal static class KeyboardHook
    {
        /// <summary>
        ///     THIS NEEDS TO STAY SEQUENTIAL!
        ///     This struct is necessary for the logging of keystrokes. It WILL break if the layout is altered.
        ///     Be careful when using ReSharper or any other code cleaner; it may break the layout and cause tons of bugs.
        ///     Original order: vkCode -> scanCode -> flags -> time -> dwExtraInfo
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private class KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public uint dwExtraInfo;
        }

        private static HookProcedure _keyboardHookProcedure;
        private static IntPtr _keyboardHook = IntPtr.Zero;

        internal static event EventHandler<EventArgs> HookProcedureEventHandler;

        /// <summary>
        ///     Initializes static classes and hooks
        /// </summary>
        internal static void Initialize()
        {
            Terminate();

            _keyboardHookProcedure = KeyboardHookProcedure;

            using (var process = Process.GetCurrentProcess())
            using (var module = process.MainModule)
            {
                var hModule = NativeMethods.GetModuleHandle(module.ModuleName);

                _keyboardHook = NativeMethods.InitializeWindowHook(WindowHookTypes.WH_KEYBOARD_LL,
                    _keyboardHookProcedure, hModule, 0);
            }
        }

        /// <summary>
        ///     Defaults CAPS + NUMLOCK + SCROLL
        /// </summary>
        /// <remarks>
        ///     This is used to properly record the entire process
        ///     A hack would be required to know if CAPS + NUMLOCK + SCROLL otherwise
        /// </remarks>
        internal static void DefaultControls()
        {
            var hasCapitalKeyState = (KeyboardMethods.GetKeyState((int)KeyboardVirtualKeys.VK_CAPITAL) & 0x0001) != 0;

            if (hasCapitalKeyState) KeyboardEventExtensions.SendVirtualKey(KeyboardVirtualKeys.VK_CAPITAL);

            var hasNumLockKeyState = (KeyboardMethods.GetKeyState((int)KeyboardVirtualKeys.VK_NUMLOCK) & 0x0001) != 0;

            if (hasNumLockKeyState) KeyboardEventExtensions.SendVirtualKey(KeyboardVirtualKeys.VK_NUMLOCK);

            var hasScrollKeyState = (KeyboardMethods.GetKeyState((int)KeyboardVirtualKeys.VK_SCROLL) & 0x0001) != 0;

            if (hasScrollKeyState) KeyboardEventExtensions.SendVirtualKey(KeyboardVirtualKeys.VK_SCROLL);
        }

        /// <summary>
        ///     Disposes static properties
        /// </summary>
        internal static void Terminate()
        {
            if (_keyboardHook != IntPtr.Zero)
            {
                NativeMethods.TerminateWindowHook(_keyboardHook);
                _keyboardHook = IntPtr.Zero;
            }

            _keyboardHookProcedure = null;
        }

        /// <summary>
        ///     Keyboard Hook Method
        /// </summary>
        /// <remarks>
        ///     1. Checks the code coming back from the hook
        ///     2. Translates to a virtual keyboard key
        ///     3. Checks that the client is not paused - IsHookPreocedurePaused
        ///     4. Sends virtual keyboard key to HookEventHandler for processing
        /// </remarks>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private static IntPtr KeyboardHookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0) return NativeMethods.CallNextWindowHook(IntPtr.Zero, nCode, wParam, lParam);

            var keyboardEvent = (KeyboardHookTypes)wParam.ToInt32();
            var keyboardHook = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

            var scanCode = (int)keyboardHook.scanCode;
            var keyboarVirtualKey = (KeyboardVirtualKeys)keyboardHook.vkCode;

            if (keyboarVirtualKey == KeyboardVirtualKeys.VK_PAUSE && keyboardEvent == KeyboardHookTypes.KeyDown)
            {
                HookConstants.IsHookProcedurePaused = !HookConstants.IsHookProcedurePaused;

                HookProcedureEventHandler?.Invoke(typeof(KeyboardHook), new EventArgs());
            }

            if (keyboarVirtualKey != KeyboardVirtualKeys.VK_PAUSE && !HookConstants.IsHookProcedurePaused)
                HookEventHandler.KeyboardRecordInput(keyboardEvent, keyboarVirtualKey);

            return NativeMethods.CallNextWindowHook(IntPtr.Zero, nCode, wParam, lParam);
        }
    }
}