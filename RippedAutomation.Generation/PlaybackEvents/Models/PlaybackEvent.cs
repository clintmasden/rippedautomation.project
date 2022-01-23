using System;
using System.Collections.Generic;
using System.Linq;
using RippedAutomation.Generation.UiAutomationElements.Extensions;
using RippedAutomation.Generation.UiAutomationElements.Models;
using RippedAutomation.Generation.UiEvents.Models;

namespace RippedAutomation.Generation.PlaybackEvents.Models
{
    /// <summary>
    ///     Leveraged in Playback Event Extensions and requires an UiEvent
    /// </summary>
    public class PlaybackEvent
    {
        public PlaybackEvent(UiEvent uiEvent)
        {
            UiEvent = uiEvent;
            AutomationElementWindows = new List<AutomationElement>();

            InitializeAutomationElementWindows();
            InitializeAutomationElement();
        }

        /// <summary>
        ///     Precursor object required to setup and apply information for Playback Event
        /// </summary>
        public UiEvent UiEvent { get; set; }

        /// <summary>
        ///     List of COM windows in regards to UiEvent.UiWindows
        /// </summary>
        public List<AutomationElement> AutomationElementWindows { get; set; }

        /// <summary>
        ///     The COM object in regards to UiEvent.UiElement
        /// </summary>
        public AutomationElement AutomationElement { get; set; }

        /// <summary>
        ///     Initializes and associates windows
        /// </summary>
        /// <remarks>
        ///     1. If children windows exist, Desktop -> First Window -> Last Window || [every window is not required]
        ///     2. If only a parent window exists, Desktop -> Window
        ///     3. A window is required otherwise throw an exception
        /// </remarks>
        private void InitializeAutomationElementWindows()
        {
            if (UiEvent.HasChildrenWindows)
            {
                UiEvent.UiWindows.ForEach(uiWindow =>
                {
                    AutomationElement windowAutomationElement = null;

                    if (AutomationElementWindows.Count > 0)
                        windowAutomationElement =
                            UiAutomationElementConditionExtensions.GetFirstElementByCondition(uiWindow.UiElement,
                                AutomationElementWindows.Last().IUIAutomationElement);

                    if (windowAutomationElement == null || !windowAutomationElement.HasUiElement)
                        windowAutomationElement =
                            UiAutomationElementConditionExtensions.GetFirstElementByCondition(uiWindow.UiElement);

                    if (windowAutomationElement.HasUiElement) AutomationElementWindows.Add(windowAutomationElement);
                });
            }
            else
            {
                var windowAutomationElement =
                    UiAutomationElementConditionExtensions.GetFirstElementByCondition(UiEvent.UiParentWindow.UiElement);

                if (windowAutomationElement.HasUiElement) AutomationElementWindows.Add(windowAutomationElement);
            }

            if (AutomationElementWindows.Count == 0)
            {
                //Fatal("PlaybackEvent: Windows cannot be null for playback", this);
                throw new Exception("PlaybackEvent: Windows cannot be null for playback");
            }
        }

        /// <summary>
        ///     Initializes and associates the targeted element
        /// </summary>
        /// <remarks>
        ///     0. Windows is a hack | Here We Go - Mario
        ///     1. Determine if element is the same as the parent || child window [element can be the same as the window since you
        ///     can click the window]
        ///     2. If the element is the same as the window return the window as the element
        ///     3. If the element is not the same as the window then send the element and last window to see if we can find~ the
        ///     element
        ///     4. If an element is not found on the last window then use the first window and see if we can find~ the element
        ///     5. An element is required otherwise throw an exception
        /// </remarks>
        private void InitializeAutomationElement()
        {
            AutomationElement automationElement = null;

            if (UiEvent.HasChildrenWindows)
            {
                if (UiEvent.UiElement != UiEvent.UiWindows.Last().UiElement)
                    automationElement = UiAutomationElementConditionExtensions.GetAllElementByCondition(
                        UiEvent.UiElement, UiEvent.UiWindows.Last().UiElement,
                        AutomationElementWindows.Last().IUIAutomationElement);
                else
                    automationElement = AutomationElementWindows.Last();
            }

            if (automationElement == null || !automationElement.HasUiElement)
            {
                if (UiEvent.UiElement != UiEvent.UiWindows.First().UiElement)
                    automationElement = UiAutomationElementConditionExtensions.GetAllElementByCondition(
                        UiEvent.UiElement, UiEvent.UiWindows.First().UiElement,
                        AutomationElementWindows.First().IUIAutomationElement);
                else
                    automationElement = AutomationElementWindows.First();
            }

            if (automationElement != null && automationElement.HasUiElement)
            {
                AutomationElement = automationElement;
            }
            else
            {
                //Fatal("PlaybackEvent: Element cannot be null for playback", this);
                throw new Exception("PlaybackEvent: Element cannot be null for playback");
            }
        }
    }
}