using RippedAutomation.Generation.UiElements.Models;

namespace RippedAutomation.Generation.UiWindows.Models
{
    /// <summary>
    ///     A supporting class that is used for UiWindow [UiElement]
    /// </summary>
    public class UiWindowElement : UiElement
    {
        public UiWindowElement()
        {
        }

        public UiWindowElement(UiElement uiElement)
        {
            LocalizedControl = uiElement.LocalizedControl;
            ClassName = uiElement.ClassName;
            Name = uiElement.Name;
            AutomationId = uiElement.AutomationId;
            X = uiElement.X;
            Y = uiElement.Y;
            Width = uiElement.Width;
            Height = uiElement.Height;
            Value = uiElement.Value;

        }
    }
}