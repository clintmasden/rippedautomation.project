using System.Drawing;
using System.Runtime.InteropServices;

namespace RippedAutomation.Generation.Events.Mouse
{
    /// <summary>
    ///     Native Mouse Method Calls
    /// </summary>
    internal static class MouseMethods
    {
        /// <summary>
        ///     Gets Mouse Value
        /// </summary>
        /// <param name="dwFlags"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="cButtons"></param>
        /// <param name="dwExtraInfo"></param>
        [DllImport("user32.dll", EntryPoint = "mouse_event", CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        internal static extern void MouseEvent(uint dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        /// <summary>
        ///     Gets Mouse physical location
        /// </summary>
        /// <remarks>
        ///     Review Physical vs Logical mouse location for more information
        /// </remarks>
        /// <param name="point"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetPhysicalCursorPos")]
        internal static extern bool GetPhysicalCursorPosition(out Point point);

        /// <summary>
        ///     Sets Mouse physical location
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetPhysicalCursorPosition(int x, int y);
    }
}