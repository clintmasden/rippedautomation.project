using RippedAutomation.Generation.UiElements.Extensions;
using RippedAutomation.Generation.UiElements.Models;
using UIAutomationClient;

namespace RippedAutomation.Generation.UiAutomationElements.Models
{
    /// <summary>
    ///     Supporting class for IUIAutomationElement and UiElement
    /// </summary>
    /// <remarks>
    ///     This isn't necessary but allows you to see which Automation Element Pointer you're dealing with
    /// </remarks>
    public class AutomationElement
    {
        public AutomationElement(IUIAutomationElement element)
        {
            IUIAutomationElement = element;

            if (IUIAutomationElement != null)
                UiElement = UiElementExtensions.GetUiElementByIUIAutomationElement(element);
        }

        public IUIAutomationElement IUIAutomationElement { get; set; }

        public UiElement UiElement { get; set; }

        public bool HasUiElement => UiElement != null;
    }
}