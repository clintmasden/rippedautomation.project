using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using RippedAutomation.Generation.Client.Models;
using RippedAutomation.Generation.Events.Hook.Models;
using RippedAutomation.Generation.Events.Keyboard;
using RippedAutomation.Generation.Events.Keyboard.Models;
using RippedAutomation.Generation.Events.Mouse;
using RippedAutomation.Generation.Events.Mouse.Models;
using RippedAutomation.Generation.Events.Native;
using RippedAutomation.Generation.Events.Native.Models;
using RippedAutomation.Generation.Graphics.Extensions;
using RippedAutomation.Generation.UiAutomationElements;
using RippedAutomation.Generation.UiAutomationElements.Extensions;
using RippedAutomation.Generation.UiElements.Extensions;
using RippedAutomation.Generation.UiElements.Models;
using RippedAutomation.Generation.UiEvents.Models;

namespace RippedAutomation.Generation.Events.Hook
{
    /// <summary>
    ///     The Meat and Potatoes of Operator.Generation
    /// </summary>
    /// <remarks>
    ///     Leverages WinAppDriver Logic [Entire project was static | Leftovers exist]
    /// </remarks>
    internal static class HookEventHandler
    {
        private static HookEventHandlerSettings _hookEventHandlerSettings;

        private static MouseState _mouseState;
        private static KeyboardState _keyboardState;

        private static Thread _recordThread;
        private static Thread _graphicThread;

        private static Point _uiPosition = new Point(0, 0);
        private static Tuple<UiElement, IntPtr> _uiGraphic = new Tuple<UiElement, IntPtr>(null, IntPtr.Zero);

        private static HookEvent _hookEvent;
        private static DateTime _hookEventDateTime;

        internal static event EventHandler<UiEventEventArgs> UiEventEventHandler;

        internal static event EventHandler<HookEventEventArgs> HookElementEventHandler;

        /// <summary>
        ///     Initializes static classes and hooks
        /// </summary>
        internal static void Initialize(HookEventHandlerSettings hookEventHandlerSettings)
        {
            _hookEventHandlerSettings = hookEventHandlerSettings;

            KeyboardConstants.Initialize();

            MouseHook.Initialize(_hookEventHandlerSettings.IgnoreProcessId);
            KeyboardHook.Initialize();

            _mouseState = new MouseState();
            _keyboardState = new KeyboardState();

            _hookEventDateTime = DateTime.Now;

            UiEventEventHandler = null;
            HookElementEventHandler = null;

            _recordThread = new Thread(RecorderThread);
            _recordThread.Start();

            _graphicThread = new Thread(GraphicThread);
            _graphicThread.Start();

            HookConstants.IsHookProcedurePaused = false;
        }

        /// <summary>
        ///     Disposes static classes and hooks
        /// </summary>
        internal static void Terminate()
        {
            MouseHook.Terminate();
            KeyboardHook.Terminate();

            if (_recordThread != null)
            {
                _recordThread.Abort();
                _recordThread = null;
            }

            if (_graphicThread != null)
            {
                _graphicThread.Abort();
                _graphicThread = null;
            }
        }

        /// <summary>
        ///     Returns Window Handle of a specific point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private static IntPtr GetWindowHandleFromPoint(Point point)
        {
            var windowHandle = NativeMethods.GetWindowFromPoint(point.X, point.Y);

            return windowHandle;
        }

        /// <summary>
        ///     Returns if the Window Handle is part of the Thread / Process
        /// </summary>
        /// <param name="currentWindowHandle"></param>
        /// <returns></returns>
        private static bool IsIgnoreWindowProcess(IntPtr currentWindowHandle)
        {
            var currentWindowProcessId =
                NativeMethods.GetWindowThreadProcessId(currentWindowHandle, out var currentMainWindowProcessId);

            if (_hookEventHandlerSettings.IgnoreProcessId == currentMainWindowProcessId) return true;

            return false;
        }

        /// <summary>
        ///     Graphic thread of drawing the rectangle on the element
        /// </summary>
        /// <remarks>
        ///     This is required since moving your mouse over the element erases the drawn element.
        /// </remarks>
        private static void GraphicThread()
        {
            while (true)
            {
                if (_hookEventHandlerSettings.HasGraphicThreadLoop) AddGraphicsRectangleOnElement();

                Thread.Sleep(500);
            }
        }

        /// <summary>
        ///     Main thread of setting all States + Events + Graphics
        /// </summary>
        /// <remarks>
        ///     1. Gets the current element based on physical cursor (there is a difference between physical and logical)
        ///     2. Verifies that the mouse movement is "enough" since the last the element
        ///     3. Gets the root AutomationCOM Element and creates an UiElement [otherwise you're dealing with a pointer]
        ///     4. Removes Graphic Element if one exists before
        ///     5. Gets Window -> Children Windows -> Element [If no windows are found, it checks the selected element's type and
        ///     if it's a window, adds it as the parent window]
        ///     6. Sets all States + Events + Graphics
        ///     7. Loop
        /// </remarks>
        private static void RecorderThread()
        {
            var currentPosition = new Point(0, 0);

            while (true)
            {
                MouseMethods.GetPhysicalCursorPosition(out currentPosition);
                var currentWindowHandle = GetWindowHandleFromPoint(currentPosition);

                if (!IsIgnoreWindowProcess(currentWindowHandle) && !HookConstants.IsHookProcedurePaused)
                {
                    var cursorDistance = Math.Sqrt(Math.Pow(currentPosition.X - _uiPosition.X, 2) + Math.Pow(currentPosition.Y - _uiPosition.Y, 2));

                    if (cursorDistance > _mouseState.MinimiumDistanceSquared)
                    {
                        _uiPosition = currentPosition;

                        var uiAutomationElement = UiAutomationCom.GetUiElementFromPoint(currentPosition);
                        var uiElement = UiElementExtensions.GetUiElementByIUIAutomationElement(uiAutomationElement);

                        if (_uiGraphic.Item1 != uiElement)
                        {
                            RemoveGraphicsRectangleOnElement();

                            // Element -> Root Window
                            var uiElementWindows = new List<UiElement>();
                            var uiAutomationElementWindows =
                                UiAutomationElementConditionExtensions.GetElementWindows(uiAutomationElement);

                            uiAutomationElementWindows.ForEach(uiWindow =>
                            {
                                uiElementWindows.Add(
                                    UiElementExtensions.GetUiElementByIUIAutomationElement(uiWindow));
                            });

                            // Element -> Window
                            if (uiElement.LocalizedControlType == LocalizedControlTypes.Window)
                                uiElementWindows.Add(uiElement);

                            // Element -> Desktop
                            if (uiElementWindows.Count == 0)
                                uiElementWindows.Add(
                                    UiElementExtensions.GetUiElementByIUIAutomationElement(UiAutomationCom
                                        .RootAutomationElement));

                            _mouseState = new MouseState();
                            _hookEvent = new HookEvent(uiElement, uiElementWindows);
                            _uiGraphic = new Tuple<UiElement, IntPtr>(uiElement, currentWindowHandle);

                            AddGraphicsRectangleOnElement();
                        }

                        _mouseState.HoverCount = 0;
                        _mouseState.CursorPosition = currentPosition;

                        if (_mouseState.State == MouseStates.LeftMouseUp) PostHookEvent(UiEventTypes.Inspect);
                    }
                }

                Thread.Sleep(125);
            }
        }

        /// <summary>
        ///     Draws Rectangle [Pen] on element
        /// </summary>
        private static void AddGraphicsRectangleOnElement()
        {
            if (_uiGraphic.Item1 != null)
                GraphicRectangleExtensions.AddRectangle(_uiGraphic.Item1.Rectangle);

            //Pen Pen = new Pen(Color.ForestGreen, 4);
            //Graphics.FromHwnd(UiAutomationCom.RootAutomationElement.CurrentNativeWindowHandle).DrawRectangle(Pen, _uiGraphic.Item1.Rectangle);
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
        private static void RemoveGraphicsRectangleOnElement()
        {
            if (_uiGraphic.Item2 != IntPtr.Zero)
            {
                NativeMethods.RedrawWindow(_uiGraphic.Item2, IntPtr.Zero, IntPtr.Zero,
                    RedrawWindowTypes.Invalidate | RedrawWindowTypes.UpdateNow);

                GraphicRectangleExtensions.RemoveRectangleFromHandle(_uiGraphic.Item2);
            }
        }

        /// <summary>
        ///     Posts Hook Event on Mouse - Mouse Move Event
        /// </summary>
        internal static void MouseMove(int x, int y)
        {
            var cursorDistance = Math.Abs(x - _uiPosition.X) + Math.Abs(y - _uiPosition.Y);

            var moveDelta = Math.Abs(x - _mouseState.CursorMove.X) + Math.Abs(y - _mouseState.CursorMove.Y);
            _mouseState.CursorMove = new Point(x, y);

            if (_mouseState.State == MouseStates.LeftMouseUp)
            {
                if (cursorDistance > _mouseState.MinimiumDistanceSquared)
                    if (Environment.TickCount - _keyboardState.Tick > 2000)
                    {
                        PostKeyboardSendKeys();
                        PostMouseWheel();
                    }

                if (moveDelta < 2 && _hookEvent != null) _mouseState.HoverCount++;

                if (_mouseState.HoverCount > _mouseState.MinimiumDistanceSquared)
                    _mouseState.HoverCount = 0;
                // PostHookEvent(UiEventType.MouseHover);
            }

            // Drag start - down and up are at different points
            else if (_mouseState.State == MouseStates.LeftMouseDown)
            {
                if (cursorDistance > _mouseState.MinimiumDistanceSquared)
                {
                    _mouseState.State = MouseStates.LeftMouseDrag;

                    PostKeyboardSendKeys();
                    PostMouseWheel();

                    // PostHookEvent(UiEventType.Drag);

                    _mouseState.PositionDown = new Point(x, y);
                }
            }
        }

        internal static void MouseWheel(int x, int y, short delta)
        {
            _mouseState.Wheel += delta;
        }

        /// <summary>
        ///     Posts Hook Event on Mouse - Left Down Event
        /// </summary>
        internal static void MouseLeftDown(int x, int y)
        {
            _mouseState.PositionDown = new Point(x, y);

            if (_mouseState.State == MouseStates.LeftMouseUp)
                _mouseState.State = MouseStates.LeftMouseDown;
            else if (_mouseState.State == MouseStates.LeftMouseDown)
                throw new Exception("Mouse Left Down - LeftMouseDown Exception");
            else if (_mouseState.State == MouseStates.LeftMouseDrag)
                throw new Exception("Mouse Left Down - LeftMouseDrag Exception");
            else
                throw new Exception("Mouse Left Down Exception");
        }

        /// <summary>
        ///     Posts Hook Event on Mouse - Left Up Event
        /// </summary>
        internal static void MouseLeftUp(int x, int y)
        {
            _mouseState.PositionUp = new Point(x, y);

            //saving on a square root by using squares of x & y components
            var cursorDistanceSquared = Math.Pow(x - _mouseState.PositionDown.X, 2) + Math.Pow(y - _mouseState.PositionDown.Y, 2);

            if (cursorDistanceSquared <= _mouseState.MinimiumDistanceSquared)
            {
                // Event: Mouse Double Click
                if (Environment.TickCount - _mouseState.Tick < _mouseState.DeltaDoubleClick)
                {
                    PostHookEvent(UiEventTypes.Mouse_LeftDoubleClick);
                }
                else
                {
                    PostKeyboardSendKeys();
                    PostMouseWheel();

                    PostHookEvent(UiEventTypes.Mouse_LeftClick);
                }
            }

            // Drag stop - down and up are at different points
            else if (_mouseState.State == MouseStates.LeftMouseDrag)
            {
                PostHookEvent(UiEventTypes.Mouse_DragStop);
            }

            _mouseState.Tick = Environment.TickCount;
            _mouseState.State = MouseStates.LeftMouseUp;
        }

        /// <summary>
        ///     Posts Hook Event on Mouse - Right Down Event
        /// </summary>
        /// <remarks>
        ///     Leftover logic from WinAppDriver
        /// </remarks>
        internal static void MouseRightDown(int left, int top)
        {
        }

        /// <summary>
        ///     Posts Hook Event on Mouse - Right Up Event
        /// </summary>
        internal static void MouseRightUp(int left, int top)
        {
            PostKeyboardSendKeys();
            PostMouseWheel();

            PostHookEvent(UiEventTypes.Mouse_RightClick);
        }

        /// <summary>
        ///     Adds Keyboard Entries to Keyboard State + Posts Hook Event on Keyboard
        /// </summary>
        internal static void KeyboardRecordInput(KeyboardHookTypes hookType, KeyboardVirtualKeys virtualKey)
        {
            var isKeyDown = hookType == KeyboardHookTypes.SystemKeyDown || hookType == KeyboardHookTypes.KeyDown;

            _keyboardState.IsVirtualKeyDown = isKeyDown;
            _keyboardState.LastVirtualKey = virtualKey;

            if (_keyboardState.KeyPresses.Count == 0) _keyboardState.Tick = Environment.TickCount;

            if (KeyboardConstants.IsControlSendKey(virtualKey) || isKeyDown)
                _keyboardState.KeyPresses.Add(
                    new KeyboardKeyPress
                    {
                        VirtualKey = virtualKey,
                        IsDown = isKeyDown
                    });

            if (isKeyDown == false)
                if (virtualKey == KeyboardVirtualKeys.VK_RETURN || virtualKey == KeyboardVirtualKeys.VK_ESCAPE ||
                    KeyboardVirtualKeys.VK_F1 <= virtualKey && virtualKey <= KeyboardVirtualKeys.VK_F24)
                {
                    PostKeyboardSendKeys();
                }
        }

        /// <summary>
        ///     Posts Hook Event on Keyboard if KeyPresses > 0
        /// </summary>
        private static void PostKeyboardSendKeys()
        {
            if (_keyboardState.KeyPresses.Count == 0) return;

            PostHookEvent(UiEventTypes.Keyboard_SendKeys);
        }

        /// <summary>
        ///     Posts Hook Event on Mouse if Mouse Wheel Event is found
        /// </summary>
        private static void PostMouseWheel()
        {
            if (_mouseState.Wheel == 0) return;

            PostHookEvent(UiEventTypes.Mouse_Wheel);
        }

        /// <summary>
        ///     Gets the delay between each UiEvent Hook Event
        /// </summary>
        /// <returns></returns>
        private static TimeSpan GetUiEventDelay()
        {
            var uiEventDelay = DateTime.Now - _hookEventDateTime;

            _hookEventDateTime = DateTime.Now;

            if (uiEventDelay.TotalSeconds <= .25) uiEventDelay = new TimeSpan(0);

            return uiEventDelay;
        }

        /// <summary>
        ///     Creates Ui Event and Sends Event Handlers to Generation Client
        /// </summary>
        /// <remarks>
        ///     1. Combines Hook Event + States
        ///     2. HookElementEventHandler - Sends [On Focus]
        ///     3. Checks UiEventType != Inspect
        ///     4. Creates Ui Event
        ///     5. UiEventEventHandle - Sends [On Click]
        ///     6. Resets States
        /// </remarks>
        private static void PostHookEvent(UiEventTypes uiEventType)
        {
            if (_hookEvent == null) return;

            try
            {
                var hookEvent = _hookEvent;
                hookEvent.UiEventType = uiEventType;
                hookEvent.MouseState = _mouseState;
                hookEvent.KeyboardState = _keyboardState;

                HookElementEventHandler?.Invoke(typeof(HookEventHandler),
                    new HookEventEventArgs { HookEvent = hookEvent });

                switch (uiEventType)
                {
                    case UiEventTypes.Inspect:
                        break;

                    default:
                        var uiEvent = new UiEvent(hookEvent);

                        UiEventEventHandler?.Invoke(typeof(HookEventHandler),
                            new UiEventEventArgs { UiEvent = uiEvent, UiEventDelay = GetUiEventDelay() });

                        _keyboardState = new KeyboardState();

                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}