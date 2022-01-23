using System.Collections.Generic;
using RippedAutomation.Generation.Events.Keyboard.Models;
using RippedAutomation.Generation.Events.Mouse.Models;
using RippedAutomation.Generation.UiElements.Models;
using RippedAutomation.Generation.UiEvents.Models;

namespace RippedAutomation.Generation.Events.Hook.Models
{
    /// <summary>
    ///     Leveraged in Hook Event Handler and is the precursor to UiEvent
    /// </summary>
    public class HookEvent
    {
        public HookEvent()
        {
            UiEventType = UiEventTypes.Inspect;
            UiWindows = new List<UiElement>();
        }

        public HookEvent(UiElement uiElement, List<UiElement> uiWindows)
        {
            UiEventType = UiEventTypes.Inspect;
            UiWindows = new List<UiElement>();

            UiElement = uiElement;
            UiWindows = uiWindows;
        }

        public UiElement UiElement { get; set; }

        public List<UiElement> UiWindows { get; set; }

        public UiEventTypes UiEventType { get; set; }

        public MouseState MouseState { get; set; }

        public KeyboardState KeyboardState { get; set; }
    }
}