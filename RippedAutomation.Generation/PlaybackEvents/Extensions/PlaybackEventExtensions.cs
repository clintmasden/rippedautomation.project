using System;
using System.Drawing;
using System.Linq;
using RippedAutomation.Generation.Events.Keyboard.Extensions;
using RippedAutomation.Generation.Events.Mouse.Extensions;
using RippedAutomation.Generation.Events.Native;
using RippedAutomation.Generation.PlaybackEvents.Models;
using RippedAutomation.Generation.UiAutomationElements.Models;
using RippedAutomation.Generation.UiEvents.Models;

namespace RippedAutomation.Generation.PlaybackEvents.Extensions
{
    /// <summary>
    ///     The Meat and Potatoes of Playback
    /// </summary>
    /// <remarks>
    ///     Playback object must be setup correctly for this to remotely function as intended
    /// </remarks>
    public static class PlaybackEventExtensions
    {
        // Last Foreground Window - Where setting the foreground window [again] will break the playback action
        private static IntPtr _elementWindowHandle = IntPtr.Zero;

        /// <summary>
        ///     Executes UiEvent.UiEventType on the current 'windows' machine
        /// </summary>
        /// <remarks>
        ///     1. Sets the active window to either the parent or child element window
        ///     2. Sets the correct position of the mouse based on the elements previous + current size [resolves DPI | resolution
        ///     differences]
        ///     3. Executes UiEventType
        /// </remarks>
        public static void Execute(PlaybackEvent playbackEvent)
        {
            if (playbackEvent.UiEvent.HasChildrenWindows)
                SetForgroundWindow(playbackEvent.AutomationElementWindows.Last());
            else
                SetForgroundWindow(playbackEvent.AutomationElementWindows.First());

            switch (playbackEvent.UiEvent.UiEventType)
            {
                case UiEventTypes.Keyboard_SendKeys:
                    KeyboardEventExtensions.SendKeys(playbackEvent.UiEvent.KeyboardEvent);
                    break;

                case UiEventTypes.Mouse_LeftClick:
                    MouseEventExtensions.LeftClick(GetCursorPosition(playbackEvent));
                    break;

                case UiEventTypes.Mouse_RightClick:
                    MouseEventExtensions.RightClick(GetCursorPosition(playbackEvent));
                    break;

                case UiEventTypes.Mouse_LeftDoubleClick:
                    MouseEventExtensions.LeftDoubleClick(GetCursorPosition(playbackEvent));
                    break;

                case UiEventTypes.Mouse_DragStop:

                    // Drag is not corrected for DPI | Resolution
                    // var beginningPosition = GetCursorPosition(playbackEvent);
                    // var endingPosition = new Point(beginningPosition.X + playbackEvent.UiEvent.MouseEvent.DragPosition.X, beginningPosition.Y + playbackEvent.UiEvent.MouseEvent.DragPosition.Y);

                    var endingPosition =
                        new Point(
                            playbackEvent.AutomationElement.UiElement.Center.X +
                            playbackEvent.UiEvent.MouseEvent.DragPosition.X,
                            playbackEvent.AutomationElement.UiElement.Center.Y +
                            playbackEvent.UiEvent.MouseEvent.DragPosition.Y);
                    MouseEventExtensions.Drag(playbackEvent.AutomationElement.UiElement.Center, endingPosition);
                    break;

                case UiEventTypes.Mouse_Wheel:
                    MouseEventExtensions.Wheel(GetCursorPosition(playbackEvent),
                        playbackEvent.UiEvent.MouseEvent.Wheel);
                    break;

                default:
                    //Fatal("PlaybackEventExtensions: Playback has an event that is not handled.");
                    throw new Exception("PlaybackEventExtensions: Playback has an event that is not handled.");
            }
        }

        /// <summary>
        ///     Get's the corrected position of the cursor
        /// </summary>
        /// <remarks>
        ///     1. Checks the elements rectangle of the original [playbackEvent.UiEvent.UiElement.Rectangle] to the one found
        ///     [playbackEvent.AutomationElement.UiElement.Rectangle]
        ///     2. If the rectangles are different, get the offset position + scales | create the new position of the cursor
        ///     3. If they are the same, return the original cursor position
        /// </remarks>
        /// <returns></returns>
        private static Point GetCursorPosition(PlaybackEvent playbackEvent)
        {
            var position = new Point(0, 0);

            if (playbackEvent.AutomationElement.HasUiElement)
                if (playbackEvent.AutomationElement.UiElement.Rectangle != playbackEvent.UiEvent.UiElement.Rectangle)
                {
                    var offsetPosition =
                        new Point(
                            playbackEvent.UiEvent.MouseEvent.Position.X - playbackEvent.UiEvent.UiElement.Position.X,
                            playbackEvent.UiEvent.MouseEvent.Position.Y - playbackEvent.UiEvent.UiElement.Position.Y);

                    var rectangleWidthScale =
                        (playbackEvent.AutomationElement.UiElement.Rectangle.Width -
                         playbackEvent.UiEvent.UiElement.Rectangle.Width) /
                        (double) playbackEvent.UiEvent.UiElement.Rectangle.Width;
                    var rectangleHeightScale =
                        (playbackEvent.AutomationElement.UiElement.Rectangle.Height -
                         playbackEvent.UiEvent.UiElement.Rectangle.Height) /
                        (double) playbackEvent.UiEvent.UiElement.Rectangle.Height;

                    var offsetPositionScale =
                        new Point(offsetPosition.X + (int) Math.Round(offsetPosition.X * rectangleWidthScale, 0),
                            offsetPosition.Y + (int) Math.Round(offsetPosition.Y * rectangleHeightScale, 0));

                    position = new Point(playbackEvent.AutomationElement.UiElement.Position.X + offsetPositionScale.X,
                        playbackEvent.AutomationElement.UiElement.Position.Y + offsetPositionScale.Y);
                }

            if (position == new Point(0, 0)) position = playbackEvent.UiEvent.MouseEvent.Position;

            return position;
        }

        /// <summary>
        ///     Sets Foreground Window [Active Window]
        /// </summary>
        /// <param name="element"></param>
        private static void SetForgroundWindow(AutomationElement element)
        {
            var elementWindowHandle = element.IUIAutomationElement.CurrentNativeWindowHandle;

            if (_elementWindowHandle != elementWindowHandle)
            {
                NativeMethods.SetForegroundWindowFromHandle((int) elementWindowHandle);

                _elementWindowHandle = elementWindowHandle;
            }
        }
    }
}