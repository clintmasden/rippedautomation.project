namespace RippedAutomation.Generation.UiEvents.Models
{
    /// <summary>
    ///     UiEventType Enumeration [Domain + Generation]
    /// </summary>
    public enum UiEventTypes : uint
    {
        Unknown = 3000,

        Keyboard_SendKeys,

        Mouse_LeftClick,
        Mouse_RightClick,
        Mouse_LeftDoubleClick,
        Mouse_Drag,
        Mouse_DragStop,
        Mouse_Wheel,
        Mouse_Hover,

        Inspect
    }
}