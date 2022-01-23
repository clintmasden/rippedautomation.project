using System;

namespace RippedAutomation.Generation.Events.Keyboard.Models
{
    /// <summary>
    ///     Keyboard Event Types Enumeration
    /// </summary>
    /// <remarks>
    ///     Translates values for readability
    /// </remarks>
    [Flags]
    public enum KeyboardEventTypes : uint
    {
        KEYDOWN = 0x0000,
        EXTENDEDKEY = 0x0001,
        KEYUP = 0x0002
    }
}