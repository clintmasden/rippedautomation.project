using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using RippedAutomation.Generation.Events.Hook.Models;
using RippedAutomation.Generation.Events.Keyboard.Models;
using RippedAutomation.Generation.Events.Mouse.Models;
using RippedAutomation.Generation.UiElements.Models;
using RippedAutomation.Generation.UiWindows.Models;

namespace RippedAutomation.Generation.UiEvents.Models
{
    /// <summary>
    ///     The top level object generated from Hook Event Handler and the precursor to PlaybackEvent
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class UiEvent
    {
        private List<UiWindow> _uiWindows = new List<UiWindow>();

        public UiEvent()
        {
        }

        public UiEvent(HookEvent hookEvent)
        {
            UiEventType = hookEvent.UiEventType;

            UiElement = hookEvent.UiElement;

            MouseEvent = new MouseEvent(hookEvent.MouseState);

            KeyboardEvent = new KeyboardEvent(hookEvent.KeyboardState);

            var hookEventUiWindowList = new List<UiWindow>();

            hookEvent.UiWindows.ForEach(uiWindow => { hookEventUiWindowList.Add(new UiWindow(uiWindow)); });

            UiWindows = hookEventUiWindowList;
        }

        public UiEventTypes UiEventType { get; set; }

        public MouseEvent MouseEvent { get; set; }

        public KeyboardEvent KeyboardEvent { get; set; }

        public UiElement UiElement { get; set; }

        public List<UiWindow> UiWindows
        {
            get
            {
                _uiWindows = _uiWindows.OrderBy(c => c.Sequence).ToList();

                return _uiWindows;
            }
            set
            {
                var uiWindowList = value;

                // Reorder if any new sequences exist
                if (uiWindowList.Any(c => c.Sequence == 0))
                    for (var uiWindowIndex = 0; uiWindowIndex < uiWindowList.Count; uiWindowIndex++)
                    {
                        var uiWindow = uiWindowList[uiWindowIndex];
                        uiWindow.Sequence = uiWindowIndex + 1;
                    }

                _uiWindows = uiWindowList;
            }
        }

        public UiWindow UiParentWindow => UiWindows.FirstOrDefault();

        public bool HasChildrenWindows
        {
            get
            {
                if (UiWindows.Count > 1) return true;

                return false;
            }
        }

        public override string ToString()
        {
            if (HasChildrenWindows) return $"{UiParentWindow.UiElement} | {UiWindows.Last().UiElement}";

            return $"{UiParentWindow.UiElement}";
        }
    }
}