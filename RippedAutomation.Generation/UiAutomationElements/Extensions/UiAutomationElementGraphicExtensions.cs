using System.Drawing;
using RippedAutomation.Generation.Graphics.Extensions;
using RippedAutomation.Generation.UiAutomationElements.Models;
using RippedAutomation.Generation.UiElements.Models;
using RippedAutomation.Generation.UiWindows.Models;

namespace RippedAutomation.Generation.UiAutomationElements.Extensions
{
    /// <summary>
    ///     Supporting methods for element graphics
    /// </summary>
    public class UiAutomationElementGraphicExtensions
    {
        private static AutomationElement _automationElementWindow { get; set; }

        public static void AddCacheReversibleRectangle(UiWindow uiWindow, UiElement uiElement)
        {
            if (_automationElementWindow == null || !_automationElementWindow.HasUiElement)
                _automationElementWindow =
                    UiAutomationElementConditionExtensions.GetFirstElementByCondition(uiWindow.UiElement);
            else if (_automationElementWindow.UiElement != uiWindow.UiElement)
                _automationElementWindow =
                    UiAutomationElementConditionExtensions.GetFirstElementByCondition(uiWindow.UiElement);

            var automationElement = UiAutomationElementConditionExtensions.GetAllElementByCondition(uiElement,
                uiWindow.UiElement, _automationElementWindow.IUIAutomationElement);

            GraphicRectangleExtensions.AddReversibleRectangle(automationElement.UiElement.Rectangle, Color.Purple);
        }

        public static void AddReversibleRectangle(UiWindow uiWindow, UiElement uiElement)
        {
            var windowAutomationElement =
                UiAutomationElementConditionExtensions.GetFirstElementByCondition(uiWindow.UiElement);
            var elementAutomationElement = UiAutomationElementConditionExtensions.GetAllElementByCondition(uiElement,
                uiWindow.UiElement, windowAutomationElement.IUIAutomationElement);

            GraphicRectangleExtensions.AddReversibleRectangle(elementAutomationElement.UiElement.Rectangle,
                Color.Purple);
        }
    }
}