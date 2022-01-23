using RippedAutomation.Generation.UiElements.Models;

namespace RippedAutomation.Generation.UiElements.Extensions
{
    /// <summary>
    ///     Builds Localized Control Type on UiElement
    /// </summary>
    public static class LocalizedControlTypeExtensions
    {
        public static LocalizedControlTypes GetLocalizedControlType(string elementType)
        {
            switch (elementType)
            {
                case "window":
                    return LocalizedControlTypes.Window;

                case "document":
                    return LocalizedControlTypes.Document;

                case "pane":
                    return LocalizedControlTypes.Pane;

                case "scroll bar":
                    return LocalizedControlTypes.ScrollBar;

                case "button":
                    return LocalizedControlTypes.Button;

                case "edit":
                    return LocalizedControlTypes.Edit;

                case "title bar":
                    return LocalizedControlTypes.TitleBar;

                case "menu bar":
                    return LocalizedControlTypes.MenuBar;

                case "menu item":
                    return LocalizedControlTypes.MenuItem;

                case "check box":
                    return LocalizedControlTypes.CheckBox;

                case "combo box":
                    return LocalizedControlTypes.ComboBox;

                case "text":
                    return LocalizedControlTypes.Text;

                case "group":
                    return LocalizedControlTypes.Group;

                case "table":
                    return LocalizedControlTypes.Table;

                case "thumb":
                    return LocalizedControlTypes.Thumb;

                case "header":
                    return LocalizedControlTypes.Header;

                case "item":
                    return LocalizedControlTypes.DataItem;

                case "list":
                    return LocalizedControlTypes.List;

                default:
                    return LocalizedControlTypes.Default;
            }
        }
    }
}