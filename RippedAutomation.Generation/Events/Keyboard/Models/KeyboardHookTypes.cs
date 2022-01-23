namespace RippedAutomation.Generation.Events.Keyboard.Models
{
    /// <summary>
    ///     Keyboard Hook Types Enumeration
    /// </summary>
    /// <remarks>
    ///     Translates values for readability
    /// </remarks>
    public enum KeyboardHookTypes
    {
        KeyDown = 0x0100,
        KeyUp = 0x0101,
        SystemKeyDown = 0x0104,
        SystemKeyUp = 0x0105
    }
}