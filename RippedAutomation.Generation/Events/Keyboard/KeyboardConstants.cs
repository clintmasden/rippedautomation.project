using System.Collections.Generic;
using RippedAutomation.Generation.Events.Keyboard.Models;

namespace RippedAutomation.Generation.Events.Keyboard
{
    /// <summary>
    ///     Keyboard support methods in regards to virtual keys
    /// </summary>
    public static class KeyboardConstants
    {
        internal static Dictionary<int, KeyboardKeyPair> VirtualKeyPairs = new Dictionary<int, KeyboardKeyPair>();

        /// <summary>
        ///     Initializes Virtual Key Pairs
        /// </summary>
        /// <remarks>
        ///     Binds Virtual Keys to corresponding English characters
        /// </remarks>
        internal static void Initialize()
        {
            if (VirtualKeyPairs.Count > 0) return;
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_SPACE, new KeyboardKeyPair(" ", " "));

            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_NUMPAD0, new KeyboardKeyPair("0", "0"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_NUMPAD1, new KeyboardKeyPair("1", "1"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_NUMPAD2, new KeyboardKeyPair("2", "2"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_NUMPAD3, new KeyboardKeyPair("3", "3"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_NUMPAD4, new KeyboardKeyPair("4", "4"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_NUMPAD5, new KeyboardKeyPair("5", "5"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_NUMPAD6, new KeyboardKeyPair("6", "6"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_NUMPAD7, new KeyboardKeyPair("7", "7"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_NUMPAD8, new KeyboardKeyPair("8", "8"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_NUMPAD9, new KeyboardKeyPair("9", "9"));

            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_0, new KeyboardKeyPair("0", ")"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_1, new KeyboardKeyPair("1", "!"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_2, new KeyboardKeyPair("2", "@"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_3, new KeyboardKeyPair("3", "#"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_4, new KeyboardKeyPair("4", "$"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_5, new KeyboardKeyPair("5", "%"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_6, new KeyboardKeyPair("6", "^"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_7, new KeyboardKeyPair("7", "&"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_8, new KeyboardKeyPair("8", "*"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_9, new KeyboardKeyPair("9", "("));

            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_OEM_3, new KeyboardKeyPair("`", "~"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_OEM_MINUS, new KeyboardKeyPair("-", "_"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_OEM_PLUS, new KeyboardKeyPair("=", "+"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_OEM_4, new KeyboardKeyPair("[", "{"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_OEM_6, new KeyboardKeyPair("]", "}"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_OEM_5, new KeyboardKeyPair("\\", "|"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_OEM_1, new KeyboardKeyPair(";", ":"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_OEM_7, new KeyboardKeyPair("'", "\""));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_OEM_PERIOD, new KeyboardKeyPair(".", ">"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_OEM_2, new KeyboardKeyPair("/", "?"));

            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_A, new KeyboardKeyPair("a", "A"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_B, new KeyboardKeyPair("b", "B"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_C, new KeyboardKeyPair("c", "C"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_D, new KeyboardKeyPair("d", "D"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_E, new KeyboardKeyPair("e", "E"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_F, new KeyboardKeyPair("f", "F"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_G, new KeyboardKeyPair("g", "G"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_H, new KeyboardKeyPair("h", "H"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_I, new KeyboardKeyPair("i", "I"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_J, new KeyboardKeyPair("j", "J"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_K, new KeyboardKeyPair("k", "K"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_L, new KeyboardKeyPair("l", "L"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_M, new KeyboardKeyPair("m", "M"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_N, new KeyboardKeyPair("n", "N"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_O, new KeyboardKeyPair("o", "O"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_P, new KeyboardKeyPair("p", "P"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_Q, new KeyboardKeyPair("q", "Q"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_R, new KeyboardKeyPair("r", "R"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_S, new KeyboardKeyPair("s", "S"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_T, new KeyboardKeyPair("t", "T"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_U, new KeyboardKeyPair("u", "U"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_V, new KeyboardKeyPair("v", "V"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_W, new KeyboardKeyPair("w", "W"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_X, new KeyboardKeyPair("x", "X"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_Y, new KeyboardKeyPair("y", "Y"));
            VirtualKeyPairs.Add((int) KeyboardVirtualKeys.VK_Z, new KeyboardKeyPair("z", "Z"));
        }

        /// <summary>
        ///     Leveraged in HookEventHandler for the purpose of adding IsDown = True|False
        /// </summary>
        /// <param name="virtualKey"></param>
        /// <returns></returns>
        internal static bool IsControlSendKey(KeyboardVirtualKeys virtualKey)
        {
            switch (virtualKey)
            {
                case KeyboardVirtualKeys.VK_LSHIFT:
                case KeyboardVirtualKeys.VK_RSHIFT:
                case KeyboardVirtualKeys.VK_SHIFT:
                case KeyboardVirtualKeys.VK_LCONTROL:
                case KeyboardVirtualKeys.VK_RCONTROL:
                case KeyboardVirtualKeys.VK_CONTROL:

                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Leveraged in GetKeyboardDescription for the purpose of creating a description to the end user
        /// </summary>
        /// <param name="virtualKey"></param>
        /// <returns></returns>
        internal static bool IsControlDescriptionKey(KeyboardVirtualKeys virtualKey)
        {
            switch (virtualKey)
            {
                case KeyboardVirtualKeys.VK_LSHIFT:
                case KeyboardVirtualKeys.VK_RSHIFT:
                case KeyboardVirtualKeys.VK_SHIFT:
                case KeyboardVirtualKeys.VK_CAPITAL:

                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Creates an English description of the Keyboard KeyPresses
        /// </summary>
        /// <remarks>
        ///     This generates successfully in most scenarios but does not cover all cases
        /// </remarks>
        /// <param name="keyPresses"></param>
        /// <returns></returns>
        public static string GetKeyboardDescription(List<KeyboardKeyPress> keyPresses)
        {
            var buildDescription = string.Empty;
            var lastControlKey = KeyboardVirtualKeys.DEFAULT;

            foreach (var keyPress in keyPresses)
            {
                var isControlKey = IsControlDescriptionKey(keyPress.VirtualKey);

                if (isControlKey && keyPress.IsDown)
                {
                    if (lastControlKey != keyPress.VirtualKey)
                        lastControlKey = keyPress.VirtualKey;
                    else
                        lastControlKey = KeyboardVirtualKeys.DEFAULT;
                }
                else if (isControlKey)
                {
                    lastControlKey = KeyboardVirtualKeys.DEFAULT;
                }
                else if (keyPress.IsDown)
                {
                    switch (lastControlKey)
                    {
                        case KeyboardVirtualKeys.VK_LSHIFT:
                        case KeyboardVirtualKeys.VK_RSHIFT:
                        case KeyboardVirtualKeys.VK_SHIFT:

                            if (VirtualKeyPairs.ContainsKey((int) keyPress.VirtualKey))
                            {
                                var keyPair = VirtualKeyPairs[(int) keyPress.VirtualKey];
                                buildDescription += $"{keyPair.Special}";
                            }

                            break;

                        case KeyboardVirtualKeys.VK_CAPITAL:

                            if (VirtualKeyPairs.ContainsKey((int) keyPress.VirtualKey))
                            {
                                var keyPair = VirtualKeyPairs[(int) keyPress.VirtualKey];

                                if (keyPress.VirtualKey >= KeyboardVirtualKeys.VK_A &&
                                    keyPress.VirtualKey <= KeyboardVirtualKeys.VK_Z)
                                    buildDescription += keyPair.Special;
                                else
                                    buildDescription += keyPair.Default;
                            }

                            break;

                        default:

                            if (VirtualKeyPairs.ContainsKey((int) keyPress.VirtualKey))
                            {
                                var keyPair = VirtualKeyPairs[(int) keyPress.VirtualKey];
                                buildDescription += keyPair.Default;
                            }
                            else
                            {
                                switch (keyPress.VirtualKey)
                                {
                                    case KeyboardVirtualKeys.VK_DELETE:
                                    case KeyboardVirtualKeys.VK_BACK:

                                        if (buildDescription.Length > 0)
                                            buildDescription =
                                                buildDescription.Substring(0, buildDescription.Length - 1);

                                        break;
                                }
                            }

                            break;
                    }
                }
            }

            if (keyPresses.Count > 0 && string.IsNullOrWhiteSpace(buildDescription))
                buildDescription += "{ Review Control Keys }";

            return buildDescription;
        }
    }
}

//public static Dictionary<string, string> VirtutalKeys = new Dictionary<string, string>();
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_NUMPAD0.ToString(), "NumberPad0");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_NUMPAD1.ToString(), "NumberPad1");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_NUMPAD2.ToString(), "NumberPad2");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_NUMPAD3.ToString(), "NumberPad3");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_NUMPAD4.ToString(), "NumberPad4");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_NUMPAD5.ToString(), "NumberPad5");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_NUMPAD6.ToString(), "NumberPad6");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_NUMPAD7.ToString(), "NumberPad7");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_NUMPAD8.ToString(), "NumberPad8");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_NUMPAD9.ToString(), "NumberPad9");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_MULTIPLY.ToString(), "Multiply");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_ADD.ToString(), "Add");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_SEPARATOR.ToString(), "Separator");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_OEM_NEC_EQUAL.ToString(), "Equal");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_SUBTRACT.ToString(), "Subtract");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_DIVIDE.ToString(), "Divide");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_F1.ToString(), "F1");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_F2.ToString(), "F2");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_F3.ToString(), "F3");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_F4.ToString(), "F4");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_F5.ToString(), "F5");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_F6.ToString(), "F6");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_F7.ToString(), "F7");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_F8.ToString(), "F8");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_F9.ToString(), "F9");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_F10.ToString(), "F10");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_F11.ToString(), "F11");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_F12.ToString(), "F12");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_DECIMAL.ToString(), "Decimal");

//VirtutalKeys.Add(KeyboardVirtualKeys.VK_OEM_1.ToString(), "Semicolon");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_INSERT.ToString(), "Insert");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_CANCEL.ToString(), "Cancel");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_HELP.ToString(), "Help");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_BACK.ToString(), "Backspace");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_TAB.ToString(), "Tab");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_CLEAR.ToString(), "Clear");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_RETURN.ToString(), "Return");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_LSHIFT.ToString(), "Shift");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_RSHIFT.ToString(), "RightShift");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_LCONTROL.ToString(), "Control");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_RCONTROL.ToString(), "RightControl");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_LMENU.ToString(), "Alt");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_RMENU.ToString(), "RightAlt");

//VirtutalKeys.Add(KeyboardVirtualKeys.VK_DELETE.ToString(), "Delete");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_PAUSE.ToString(), "Pause");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_SPACE.ToString(), "Space");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_PRIOR.ToString(), "PageUp");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_NEXT.ToString(), "PageDown");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_END.ToString(), "End");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_HOME.ToString(), "Home");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_LEFT.ToString(), "Left");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_UP.ToString(), "Up");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_RIGHT.ToString(), "Right");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_DOWN.ToString(), "Down");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_ESCAPE.ToString(), "Escape");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_LWIN.ToString(), "Command");
//VirtutalKeys.Add(KeyboardVirtualKeys.VK_RWIN.ToString(), "Command");