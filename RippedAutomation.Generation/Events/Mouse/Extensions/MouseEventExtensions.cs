using System.Drawing;
using System.Threading;
using RippedAutomation.Generation.Events.Mouse.Models;

namespace RippedAutomation.Generation.Events.Mouse.Extensions
{
    /// <summary>
    ///     Entry point for mouse native methods
    /// </summary>
    /// <remarks>
    ///     <code>
    ///     MouseMethods.MouseEvent((uint)MouseEventTypes.LEFTDOWN | (uint)MouseEventTypes.LEFTUP, 0, 0, 0, 0);
    ///     MouseMethods.MouseEvent((uint)MouseEventTypes.LEFTDOWN | (uint)MouseEventTypes.LEFTUP, 0, 0, 0, 0);
    /// </code>
    /// </remarks>
    public static class MouseEventExtensions
    {
        /// <summary>
        ///     Sends a Left Click
        /// </summary>
        /// <param name="position"></param>
        public static void LeftClick(Point position)
        {
            MouseMethods.SetPhysicalCursorPosition(position.X, position.Y);
            Thread.Sleep(50);

            MouseMethods.MouseEvent((uint) MouseEventTypes.LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(50);
            MouseMethods.MouseEvent((uint) MouseEventTypes.LEFTUP, 0, 0, 0, 0);
        }

        /// <summary>
        ///     Sends a Left Double Click
        /// </summary>
        /// <param name="position"></param>
        public static void LeftDoubleClick(Point position)
        {
            MouseMethods.SetPhysicalCursorPosition(position.X, position.Y);
            Thread.Sleep(50);

            MouseMethods.MouseEvent((uint) MouseEventTypes.LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(50);
            MouseMethods.MouseEvent((uint) MouseEventTypes.LEFTUP, 0, 0, 0, 0);

            MouseMethods.MouseEvent((uint) MouseEventTypes.LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(50);
            MouseMethods.MouseEvent((uint) MouseEventTypes.LEFTUP, 0, 0, 0, 0);
        }

        /// <summary>
        ///     Sends a Right Click
        /// </summary>
        /// <param name="position"></param>
        public static void RightClick(Point position)
        {
            MouseMethods.SetPhysicalCursorPosition(position.X, position.Y);
            Thread.Sleep(50);

            MouseMethods.MouseEvent((uint) MouseEventTypes.RIGHTDOWN, 0, 0, 0, 0);
            Thread.Sleep(50);
            MouseMethods.MouseEvent((uint) MouseEventTypes.RIGHTUP, 0, 0, 0, 0);
        }

        /// <summary>
        ///     Sends a Drag Click
        /// </summary>
        /// <param name="beginningPosition"></param>
        /// <param name="endingPosition"></param>
        public static void Drag(Point beginningPosition, Point endingPosition)
        {
            MouseMethods.SetPhysicalCursorPosition(beginningPosition.X, beginningPosition.Y);
            Thread.Sleep(50);

            MouseMethods.MouseEvent((uint) MouseEventTypes.LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(50);

            MouseMethods.SetPhysicalCursorPosition(endingPosition.X, endingPosition.Y);
            Thread.Sleep(50);

            MouseMethods.MouseEvent((uint) MouseEventTypes.LEFTUP, 0, 0, 0, 0);
        }

        /// <summary>
        ///     Sends a Scroll
        /// </summary>
        /// <remarks>
        ///     This needs some TLC still but it works for the most part
        /// </remarks>
        /// <param name="position"></param>
        /// <param name="wheel"></param>
        public static void Wheel(Point position, int wheel)
        {
            // dim ScrollValue as Integer
            // ScrollValue = 120 'or -120 for up or down scrolling
            // mouse_event(&H800, 0, 0, ScrollValue, 0)

            // For i As Int32 = 1 To number
            // apimouse_event(MOUSEEVENTF_WHEEL, 0, 0, increment, apiGetMessageExtraInfo)
            // Next

            MouseMethods.SetPhysicalCursorPosition(position.X, position.Y);
            Thread.Sleep(50);

            MouseMethods.MouseEvent((uint) MouseEventTypes.WHEEL, 0, 0, wheel, 0);
        }
    }
}