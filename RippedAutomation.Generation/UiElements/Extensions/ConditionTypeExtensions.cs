using RippedAutomation.Generation.UiElements.Models;

namespace RippedAutomation.Generation.UiElements.Extensions
{
    /// <summary>
    ///     Builds Condition Type on UiElement
    /// </summary>
    public static class ConditionTypeExtensions
    {
        public static ConditionTypes GetCriteriaType(this UiElement uiElement)
        {
            var criteriaType = ConditionTypes.Default;

            if (!string.IsNullOrWhiteSpace(uiElement.AutomationId)) criteriaType |= ConditionTypes.AutomationId;

            if (!string.IsNullOrWhiteSpace(uiElement.Name)) criteriaType |= ConditionTypes.Name;

            if (!string.IsNullOrWhiteSpace(uiElement.ClassName)) criteriaType |= ConditionTypes.ClassName;

            if (!string.IsNullOrWhiteSpace(uiElement.LocalizedControlType.ToString()))
                criteriaType |= ConditionTypes.ElementType;

            return criteriaType;
        }
    }
}