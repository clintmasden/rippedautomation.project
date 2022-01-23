using RippedAutomation.Generation.UiElements.Models;
using UIAutomationClient;

namespace RippedAutomation.Generation.UiElements.Extensions
{
    /// <summary>
    ///     Supporting methods for UiElement
    /// </summary>
    /// <remarks>
    ///     Could be part of UiElement class butt i decided to keep it simple w/ AutoMapper
    /// </remarks>
    public class UiElementExtensions
    {
        /// <summary>
        ///     Automation COM to UiElement
        /// </summary>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        public static UiElement GetUiElementByIUIAutomationElement(IUIAutomationElement automationElement)
        {
            var element = new UiElement();

            element.LocalizedControl = automationElement.CurrentLocalizedControlType;
            element.ClassName = automationElement.CurrentClassName;
            element.Name = automationElement.CurrentName;
            element.AutomationId = automationElement.CurrentAutomationId;
            element.Value = (automationElement.GetCurrentPropertyValue(30045) as string).Trim();

            var elementTagRectangle = automationElement.CurrentBoundingRectangle;

            element.X = elementTagRectangle.left;
            element.Y = elementTagRectangle.top;
            element.Width = elementTagRectangle.right - elementTagRectangle.left;
            element.Height = elementTagRectangle.bottom - elementTagRectangle.top;

            return element;
        }

        /// <summary>
        ///     Builds ToString for UiElement
        /// </summary>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        public static string GetUiElementToString(UiElement uiElement)
        {
            var buildName = string.Empty;

            if (!string.IsNullOrWhiteSpace(uiElement.Name)) buildName += $"{uiElement.Name}";

            if (!string.IsNullOrWhiteSpace(uiElement.Name) && !string.IsNullOrWhiteSpace(uiElement.AutomationId))
                buildName += ".";

            if (!string.IsNullOrWhiteSpace(uiElement.AutomationId)) buildName += $"{uiElement.AutomationId}";

            if (string.IsNullOrWhiteSpace(uiElement.Name) && string.IsNullOrWhiteSpace(uiElement.AutomationId))
                buildName += $"{uiElement.ClassName}";

            return buildName;
        }
    }
}