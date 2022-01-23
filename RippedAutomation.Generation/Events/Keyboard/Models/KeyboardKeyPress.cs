
namespace RippedAutomation.Generation.Events.Keyboard.Models
{
    /// <summary>
    ///     Supporting class for KeyboardEvent
    /// </summary>
    /// <remarks>
    ///     Let's us know the state of the virtual key and what the sequence of keys pressed
    /// </remarks>
    public class KeyboardKeyPress
    {
        public KeyboardVirtualKeys VirtualKey { get; set; }

        public bool IsDown { get; set; }

        public int Sequence { get; set; }

        public override string ToString()
        {
            return $"{VirtualKey}";
        }
    }
}