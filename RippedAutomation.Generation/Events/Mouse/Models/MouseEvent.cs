using System.ComponentModel;
using System.Drawing;

namespace RippedAutomation.Generation.Events.Mouse.Models
{
    /// <summary>
    ///     Leveraged in UiEvent and is the main supporting class for mouse positions
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MouseEvent
    {
        public MouseEvent()
        {
        }

        public MouseEvent(MouseState mouseState)
        {
            Position = mouseState.PositionDown;

            DragPosition = mouseState.DragPosition;

            Wheel = mouseState.Wheel;
        }

        /// <summary>
        ///     Mouse position
        /// </summary>
        /// <remarks>
        ///     This does not account for the relative position when the resolution | DPI scaling is different
        /// </remarks>
        public Point Position { get; set; }

        /// <summary>
        ///     Ending position for a drag event
        /// </summary>
        public Point DragPosition { get; set; }

        /// <summary>
        ///     Mouse wheel (- | +)
        /// </summary>
        public int Wheel { get; set; }
    }
}