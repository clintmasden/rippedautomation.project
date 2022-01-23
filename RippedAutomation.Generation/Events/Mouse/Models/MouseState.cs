using System;
using System.Drawing;
using System.Windows.Forms;

namespace RippedAutomation.Generation.Events.Mouse.Models
{
    /// <summary>
    ///     Leveraged in Hook Event Handler and is the precursor to MouseEvent
    /// </summary>
    /// <remarks>
    ///     Separation of concern here between State and Event for better maintainability
    /// </remarks>
    public class MouseState
    {
        public MouseState()
        {
            Tick = 0;

            HoverCount = 0;

            State = MouseStates.LeftMouseUp;

            CursorPosition = new Point(0, 0);

            CursorMove = new Point(0, 0);

            PositionDown = new Point(0, 0);

            PositionUp = new Point(0, 0);

            Wheel = 0;
        }

        public int Tick { get; set; }

        public int HoverCount { get; set; }

        public MouseStates State { get; set; }

        /// <summary>
        ///     Cursor on a down hook
        /// </summary>
        /// <remarks>
        ///     Mouse Hook + Hook Event Handler
        /// </remarks>
        public Point PositionDown { get; set; }

        /// <summary>
        ///     Cursor on an up hook
        /// </summary>
        /// <remarks>
        ///     Mouse Hook + Hook Event Handler
        /// </remarks>
        public Point PositionUp { get; set; }

        /// <summary>
        ///     Cursor on an element
        /// </summary>
        /// <remarks>
        ///     Hook Event Handler - Recorder Thread
        /// </remarks>
        public Point CursorPosition { get; set; }

        /// <summary>
        ///     Uses MinimumDistance to know if the mouse moved to create another event
        /// </summary>
        public Point CursorMove { get; set; }

        /// <summary>
        ///     Mouse Scroll Wheel
        /// </summary>
        public int Wheel { get; set; }

        /// <summary>
        ///     Creates the ending point for a drag hook
        /// </summary>
        public Point DragPosition
        {
            get
            {
                var dragDeltaX = PositionUp.X - PositionDown.X;
                var dragDeltaY = PositionUp.Y - PositionDown.Y;

                return new Point(dragDeltaX, dragDeltaY);
            }
        }

        /// <summary>
        ///     Determines if the mouse moved far enough to be considered a drag.
        /// </summary>
        /// <remarks>
        ///     Distance is based on machine's configuration.
        ///     Uses square of this distance to save on a square root calculation when finding the movement's hypotenuse.
        /// </remarks>
        public double MinimiumDistanceSquared => Math.Pow(SystemInformation.DragSize.Width, 2) + Math.Pow(SystemInformation.DragSize.Height, 2);

        /// <summary>
        ///     Determines if two clicks were pressed in a certain time frame to create a double click event.
        /// </summary>
        /// <remarks>
        ///     Uses the current machine's configuration.
        /// </remarks>
        public int DeltaDoubleClick => SystemInformation.DoubleClickTime;
    }
}