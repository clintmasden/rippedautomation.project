using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using RippedAutomation.Generation.Events.Hook;
using RippedAutomation.Generation.Events.Mouse.Models;
using RippedAutomation.Generation.Events.Native;
using RippedAutomation.Generation.Events.Native.Models;

namespace RippedAutomation.Generation.Events.Mouse
{
    /// <summary>
    ///     Mouse Hook Mechanism + Spyware
    /// </summary>
    internal static class MouseHook
    {
        /// <summary>
        ///     Be careful when using ReSharper or any other code cleaner; it may break the layout and cause tons of bugs.
        ///     Original order: X -> Y
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal class POINT
        {
            public int X;
            public int Y;
        }

        /// <summary>
        ///     Be careful when using ReSharper or any other code cleaner; it may break the layout and cause tons of bugs.
        ///     Original order: pt -> mouseData -> flags -> time -> dwExtraInfo
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal class MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public uint dwExtraInfo;
        }

        private static HookProcedure _mouseHookProcedure;
        private static IntPtr _mouseHook = IntPtr.Zero;
        private static int _ignoreProcessId;

        /// <summary>
        ///     Initializes static classes and hooks
        /// </summary>
        internal static void Initialize(int ignoreProcessId)
        {
            Terminate();

            _mouseHookProcedure = new HookProcedure(MouseHookProcedure);
            _ignoreProcessId = ignoreProcessId;

            using (var process = Process.GetCurrentProcess())
            using (var module = process.MainModule)
            {
                var hModule = NativeMethods.GetModuleHandle(module.ModuleName);

                _mouseHook =
                    NativeMethods.InitializeWindowHook(WindowHookTypes.WH_MOUSE_LL, _mouseHookProcedure, hModule, 0);
            }
        }

        /// <summary>
        ///     Disposes static properties
        /// </summary>
        internal static void Terminate()
        {
            if (_mouseHook != IntPtr.Zero)
            {
                NativeMethods.TerminateWindowHook(_mouseHook);
                _mouseHook = IntPtr.Zero;
            }

            _mouseHookProcedure = null;
        }

        /// <summary>
        ///     Mouse Hook Method
        /// </summary>
        /// <remarks>
        ///     1. Checks the code coming back from the hook
        ///     2. Checks that the current element handle is not part of the ignorable Process
        ///     3. Sends mouse state to HookEventHandler for processing
        /// </remarks>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private static IntPtr MouseHookProcedure(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0 || HookConstants.IsHookProcedurePaused)
                return NativeMethods.CallNextWindowHook(IntPtr.Zero, nCode, wParam, lParam);

            var mhs = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
            var x = mhs.pt.X;
            var y = mhs.pt.Y;

            // Skip if on an ignorable Process | Thread
            if (IsIgnoreWindowProcess(x, y))
                return NativeMethods.CallNextWindowHook(IntPtr.Zero, nCode, wParam, lParam);

            switch (wParam.ToInt32())
            {
                case (int)MouseHookTypes.WM_MOUSEMOVE:
                    HookEventHandler.MouseMove(x, y);
                    break;

                case (int)MouseHookTypes.WM_MOUSEWHEEL:
                    var delta = (short)(mhs.mouseData >> 16);
                    HookEventHandler.MouseWheel(x, y, delta);
                    break;

                case (int)MouseHookTypes.WM_LBUTTONDOWN:
                    HookEventHandler.MouseLeftDown(x, y);
                    break;

                case (int)MouseHookTypes.WM_LBUTTONUP:
                    HookEventHandler.MouseLeftUp(x, y);
                    break;

                case (int)MouseHookTypes.WM_RBUTTONDOWN:
                    HookEventHandler.MouseRightDown(x, y);
                    break;

                case (int)MouseHookTypes.WM_RBUTTONUP:
                    HookEventHandler.MouseRightUp(x, y);
                    break;
            }

            return NativeMethods.CallNextWindowHook(IntPtr.Zero, nCode, wParam, lParam);
        }

        /// <summary>
        ///     Returns if the Window Handle is part of the Thread / Process
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool IsIgnoreWindowProcess(int x, int y)
        {
            var currentWindowHandle = NativeMethods.GetWindowFromPoint(x, y);
            var currentWindowProcessId =
                NativeMethods.GetWindowThreadProcessId(currentWindowHandle, out var currentMainWindowProcessId);

            if (_ignoreProcessId == currentMainWindowProcessId) return true;

            return false;
        }
    }
}