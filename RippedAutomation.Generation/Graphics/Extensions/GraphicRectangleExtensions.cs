using System;
using System.Drawing;
using System.Windows.Forms;
using RippedAutomation.Generation.Events.Native;
using RippedAutomation.Generation.Events.Native.Models;
using RippedAutomation.Generation.UiAutomationElements;

namespace RippedAutomation.Generation.Graphics.Extensions
{
    public static class GraphicRectangleExtensions
    {
        /// <summary>
        ///     Draws a Rectangle [Pen]
        /// </summary>
        public static void AddRectangle(Rectangle rectangle)
        {
            var Pen = new Pen(Color.ForestGreen, 4);
            System.Drawing.Graphics.FromHwnd(UiAutomationCom.RootAutomationElement.CurrentNativeWindowHandle)
                .DrawRectangle(Pen, rectangle);
        }

        /// <summary>
        ///     Draws a Rectangle [Pen]
        /// </summary>
        public static void AddRectangle(Rectangle rectangle, Color color)
        {
            var Pen = new Pen(color, 4);
            System.Drawing.Graphics.FromHwnd(UiAutomationCom.RootAutomationElement.CurrentNativeWindowHandle)
                .DrawRectangle(Pen, rectangle);
        }

        /// <summary>
        ///     Draws a Reversible Rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="color"></param>
        public static void AddReversibleRectangle(Rectangle rectangle, Color color)
        {
            ControlPaint.DrawReversibleFrame(rectangle, color, FrameStyle.Thick);
        }

        /// <summary>
        ///     Removes Rectangle on element + window + desktop [Depending on Window Handle]
        /// </summary>
        /// <remarks>
        ///     The best approach is DrawReversibleFrame - The issue encountered, is it doesn't fully erase the drawn element
        ///     <code>
        ///      if (_uiGraphics.Item1 != null)
        ///     {
        ///         ControlPaint.DrawReversibleFrame(_uiGraphics.Item1.Rectangle, Color.Yellow, FrameStyle.Thick);
        ///     }
        /// </code>
        ///     The next best approach is by redrawing the current element w/ SetForegroundWindowFromHandle - The issue with this
        ///     is DateTimePickers + and other finicky controls
        ///     <code>
        ///      NativeMethods.SetForegroundWindowFromHandle((int)_uiGraphics.Item2);
        ///  </code>
        /// </remarks>
        public static void RemoveRectangleFromHandle(IntPtr windowHandle)
        {
            NativeMethods.RedrawWindow(windowHandle, IntPtr.Zero, IntPtr.Zero,
                RedrawWindowTypes.Invalidate | RedrawWindowTypes.UpdateNow);
        }
    }
}