using System.Runtime.InteropServices;

namespace RippedAutomation.Generation.Events.Keyboard
{
    /// <summary>
    ///     Native Keyboard Method Calls
    /// </summary>
    internal static class KeyboardMethods
    {
        /// <summary>
        ///     Gets Virtual Key Value
        /// </summary>
        /// <param name="nVirtKey"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern short GetKeyState(int nVirtKey);

        /// <summary>
        ///     Sets Virtual Key Value
        /// </summary>
        /// <param name="bVk"></param>
        /// <param name="bScan"></param>
        /// <param name="dwFlags"></param>
        /// <param name="dwExtraInfo"></param>
        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        internal static extern void KeyboardEvent(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
    }
}