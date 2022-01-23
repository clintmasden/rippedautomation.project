using System.Collections.Generic;

namespace RippedAutomation.Generation.Events.Keyboard.Models
{
    /// <summary>
    ///     Leveraged in Hook Event Handler and is the precursor to KeyboardEvent
    /// </summary>
    /// <remarks>
    ///     Separation of concern here between State and Event for better maintainability
    /// </remarks>
    public class KeyboardState
    {
        public KeyboardState()
        {
            Tick = 0;

            LastVirtualKey = KeyboardVirtualKeys.DEFAULT;

            IsVirtualKeyDown = false;

            KeyPresses = new List<KeyboardKeyPress>();
        }

        public int Tick { get; set; }

        public KeyboardVirtualKeys LastVirtualKey { get; set; }

        public bool IsVirtualKeyDown { get; set; }

        public List<KeyboardKeyPress> KeyPresses { get; set; }
    }
}