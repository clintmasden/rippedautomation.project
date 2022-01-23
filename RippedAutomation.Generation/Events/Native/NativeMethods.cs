using System;
using System.Runtime.InteropServices;
using RippedAutomation.Generation.Events.Hook;
using RippedAutomation.Generation.Events.Native.Models;

namespace RippedAutomation.Generation.Events.Native
{
    internal class NativeMethods
    {
        /// <summary>
        ///     Sets DPI Awareness [Windows 7]
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool SetProcessDPIAware();

        /// <summary>
        ///     Sets DPI Awareness [Windows 8+]
        /// </summary>
        /// <param name="awareness"></param>
        /// <returns></returns>
        [DllImport("SHCore.dll", SetLastError = true)]
        internal static extern bool SetProcessDpiAwareness(DpiAwarenessProcessTypes awareness);

        /// <summary>
        ///     Sets DPI Awareness [Windows 10]
        /// </summary>
        /// <param name="dpiFlag"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool SetProcessDpiAwarenessContext(int dpiFlag);

        /// <summary>
        ///     Returns Process Id of Window Handle
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpdwProcessId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        /// <summary>
        ///     Returns Window Handle from position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]
        internal static extern IntPtr GetWindowFromPoint(int x, int y);

        /// <summary>
        ///     Gets Ancestor Process Id from Window Handle
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="ancestorType"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetAncestor", ExactSpelling = true)]
        internal static extern IntPtr GetAncestorFromHandle(IntPtr hWnd, AncestorTypes ancestorType);

        /// <summary>
        ///     Gets Window Handle From Window Handle
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="windowTypes"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetWindow", SetLastError = true)]
        internal static extern IntPtr GetWindowFromHandle(IntPtr hWnd, WindowTypes windowTypes);

        /// <summary>
        ///     Gets Foreground Window Handle [Gets Active Window]
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        internal static extern IntPtr GetForegroundWindowHandle();

        /// <summary>
        ///     Sets Foreground Window Handle [Sets Active Window]
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        internal static extern long SetForegroundWindowFromHandle(int hWnd);

        [DllImport("user32.dll")]
        internal static extern bool UpdateWindow(IntPtr hWnd);

        /// <summary>
        ///     Refreshes a window based on the Window Handle
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lprcUpdate"></param>
        /// <param name="hrgnUpdate"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate,
            RedrawWindowTypes flags);

        /// <summary>
        ///     Refreshes a Rectangle by specifying the coordinates of the portion of a window that you wish to update
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpRect"></param>
        /// <param name="bErase"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "InvalidateRect")]
        internal static extern bool SetInvalidateRectangle(IntPtr hWnd, IntPtr lpRect, bool bErase);

        /// <summary>
        ///     Passes the hook information to the next hook procedure in the current hook chain
        /// </summary>
        /// <param name="hhk"></param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "CallNextHookEx")]
        internal static extern IntPtr CallNextWindowHook(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        ///     Gets Module Handle
        /// </summary>
        /// <param name="lpModuleName"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        ///     Initializes Window Hooks [Mouse & Keyboard]
        /// </summary>
        /// <param name="windowHookType"></param>
        /// <param name="lpfn"></param>
        /// <param name="hMod"></param>
        /// <param name="dwThreadId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowsHookEx", SetLastError = true)]
        internal static extern IntPtr InitializeWindowHook(WindowHookTypes windowHookType, HookProcedure lpfn,
            IntPtr hMod, uint dwThreadId);

        /// <summary>
        ///     Terminates WIndow Hooks [Mouse & Keyboard]
        /// </summary>
        /// <param name="hhk"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "UnhookWindowsHookEx", SetLastError = true)]
        internal static extern bool TerminateWindowHook(IntPtr hhk);
    }
}