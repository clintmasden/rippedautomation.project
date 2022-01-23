using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RippedAutomation.Generation.Events.Keyboard.Models
{
    /// <summary>
    ///     Leveraged in UiEvent and is the main supporting class for sending keys
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class KeyboardEvent
    {
        private List<KeyboardKeyPress> _keyPresses = new List<KeyboardKeyPress>();

        public KeyboardEvent()
        {
        }

        public KeyboardEvent(KeyboardState keyboardState)
        {
            KeyPresses = new List<KeyboardKeyPress>(keyboardState.KeyPresses);
        }

        /// <summary>
        ///     List of KeyPresses retrieved from KeyboardState + Generation
        /// </summary>
        /// <remarks>
        ///     Order by is necessary when the object is initialized from a database
        /// </remarks>
        public List<KeyboardKeyPress> KeyPresses
        {
            get
            {
                _keyPresses = _keyPresses.OrderBy(k => k.Sequence).ToList();

                return _keyPresses;
            }
            set
            {
                var keyPressList = value;

                // Reorder if any new sequences exist
                if (keyPressList.Any(k => k.Sequence == 0))
                    for (var keyPressIndex = 0; keyPressIndex < keyPressList.Count; keyPressIndex++)
                    {
                        var keyPress = keyPressList[keyPressIndex];
                        keyPress.Sequence = keyPressIndex + 1;
                    }

                _keyPresses = keyPressList;
            }
        }

        /// <summary>
        ///     Generates a description of the keys pressed
        /// </summary>
        public string Description => KeyboardConstants.GetKeyboardDescription(KeyPresses);
    }
}