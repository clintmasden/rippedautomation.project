using System.Collections.Generic;
using System.ComponentModel;
using RippedAutomation.Generation.UiElements.Extensions;
using RippedAutomation.Generation.UiElements.Models;

namespace RippedAutomation.Generation.UiWindows.Models
{
    /// <summary>
    ///     Summary of related information regarding the UiElement [Which is the UiWindow]
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class UiWindow
    {
        public UiWindow()
        {
            UiWindowElements = new List<UiWindowElement>();
        }

        public UiWindow(UiElement uiElement)
        {
            UiElement = uiElement;

            UiWindowElements = new List<UiWindowElement>();
        }

        public UiElement UiElement { get; set; }

        public int Sequence { get; set; }

        /// <summary>
        ///     List of elements associated with the UiWindow
        /// </summary>
        public List<UiWindowElement> UiWindowElements { get; set; }

        public override string ToString()
        {
            return $"{UiElementExtensions.GetUiElementToString(UiElement)}";
        }
    }
}