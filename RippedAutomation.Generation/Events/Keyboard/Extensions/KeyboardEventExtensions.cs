using System.Threading;
using RippedAutomation.Generation.Events.Keyboard.Models;

namespace RippedAutomation.Generation.Events.Keyboard.Extensions
{
    /// <summary>
    ///     Entry point for keyboard native methods
    /// </summary>
    public static class KeyboardEventExtensions
    {
        /// <summary>
        ///     Sends KeyboardEvent -> SendVirtualKey
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="keyboardEvent"></param>
        internal static void SendKeys(KeyboardEvent keyboardEvent)
        {
            var lastControlKey = KeyboardVirtualKeys.DEFAULT;

            foreach (var keyPress in keyboardEvent.KeyPresses)
            {
                var isControlKey = KeyboardConstants.IsControlSendKey(keyPress.VirtualKey);

                if (isControlKey && keyPress.IsDown)
                {
                    lastControlKey = keyPress.VirtualKey;
                }
                else if (isControlKey)
                {
                    lastControlKey = KeyboardVirtualKeys.DEFAULT;
                }
                else if (keyPress.IsDown)
                {
                    SendVirtualKey(keyPress, lastControlKey);
                    VirtualKeyDelay(keyPress);
                }

                Thread.Sleep(30);
            }
        }

        /// <summary>
        ///     Sends KeyboardKeyPress -> Native Method
        /// </summary>
        /// <remarks>
        ///     <code>
        ///     KeyboardMethods.KeyboardEvent(0x43, 0, (int) KeyboardEventTypes.KEYDOWN, 0); // presses c
        ///     KeyboardMethods.KeyboardEvent(0x43, 0, (int) KeyboardEventTypes.KEYUP, 0); //releases c
        /// </code>
        /// </remarks>
        /// <param name="key"></param>
        /// <param name="lastControlKey"></param>
        internal static void SendVirtualKey(KeyboardKeyPress key, KeyboardVirtualKeys lastControlKey)
        {
            switch (lastControlKey)
            {
                case KeyboardVirtualKeys.VK_LSHIFT:
                case KeyboardVirtualKeys.VK_RSHIFT:
                case KeyboardVirtualKeys.VK_SHIFT:

                    KeyboardMethods.KeyboardEvent((byte) KeyboardVirtualKeys.VK_LSHIFT, 0,
                        (int) KeyboardEventTypes.KEYDOWN, 0);

                    KeyboardMethods.KeyboardEvent((byte) key.VirtualKey, 0, (int) KeyboardEventTypes.KEYDOWN, 0);
                    KeyboardMethods.KeyboardEvent((byte) key.VirtualKey, 0, (int) KeyboardEventTypes.KEYUP, 0);

                    KeyboardMethods.KeyboardEvent((byte) KeyboardVirtualKeys.VK_LSHIFT, 0,
                        (int) KeyboardEventTypes.KEYUP, 0);
                    break;

                case KeyboardVirtualKeys.VK_LCONTROL:
                case KeyboardVirtualKeys.VK_RCONTROL:
                case KeyboardVirtualKeys.VK_CONTROL:

                    KeyboardMethods.KeyboardEvent((byte) KeyboardVirtualKeys.VK_LCONTROL, 0,
                        (int) KeyboardEventTypes.KEYDOWN, 0);

                    KeyboardMethods.KeyboardEvent((byte) key.VirtualKey, 0, (int) KeyboardEventTypes.KEYDOWN, 0);
                    KeyboardMethods.KeyboardEvent((byte) key.VirtualKey, 0, (int) KeyboardEventTypes.KEYUP, 0);

                    KeyboardMethods.KeyboardEvent((byte) KeyboardVirtualKeys.VK_LCONTROL, 0,
                        (int) KeyboardEventTypes.KEYUP, 0);
                    break;

                default:

                    KeyboardMethods.KeyboardEvent((byte) key.VirtualKey, 0, (int) KeyboardEventTypes.KEYDOWN, 0);
                    KeyboardMethods.KeyboardEvent((byte) key.VirtualKey, 0, (int) KeyboardEventTypes.KEYUP, 0);
                    break;
            }
        }

        /// <summary>
        ///     Adds delays between keyboard send keys since external applications can't keep up
        /// </summary>
        /// <param name="key"></param>
        private static void VirtualKeyDelay(KeyboardKeyPress key)
        {
            switch (key.VirtualKey)
            {
                case KeyboardVirtualKeys.VK_TAB:

                    Thread.Sleep(300);
                    break;
            }
        }

        /// <summary>
        ///     Direct access to sending a virtual key without any top level classes
        /// </summary>
        /// <param name="virtualKey"></param>
        internal static void SendVirtualKey(KeyboardVirtualKeys virtualKey)
        {
            KeyboardMethods.KeyboardEvent((byte) virtualKey, 0, (int) KeyboardEventTypes.KEYDOWN, 0);
            KeyboardMethods.KeyboardEvent((byte) virtualKey, 0, (int) KeyboardEventTypes.KEYUP, 0);
        }
    }
}