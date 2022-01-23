namespace RippedAutomation.Generation.Events.Keyboard.Models
{
    /// <summary>
    ///     Supporting class for virtual key, KeyPairs
    /// </summary>
    /// <remarks>
    ///     Examples:
    ///     1 = !
    ///     2 = @
    ///     a = A
    /// </remarks>
    public class KeyboardKeyPair
    {
        public string Default;
        public string Special;

        public KeyboardKeyPair(string @default, string special)
        {
            Default = @default;
            Special = special;
        }
    }
}