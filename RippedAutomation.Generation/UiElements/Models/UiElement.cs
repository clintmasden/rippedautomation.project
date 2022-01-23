using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using RippedAutomation.Generation.UiElements.Extensions;

namespace RippedAutomation.Generation.UiElements.Models
{
    /// <summary>
    ///     Summary of related information regarding the COM element
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class UiElement
    {
        public string LocalizedControl { get; set; }

        public string ClassName { get; set; }

        public string Name { get; set; }

        public string AutomationId { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Value { get; set; }

        /// <summary>
        ///     Enumeration of Control Type
        /// </summary>
        public LocalizedControlTypes LocalizedControlType =>
            LocalizedControlTypeExtensions.GetLocalizedControlType(LocalizedControl);

        /// <summary>
        ///     Enumeration of Condition Type
        /// </summary>
        internal ConditionTypes ConditionType => this.GetCriteriaType();

        internal Rectangle Rectangle => new Rectangle(X, Y, Width, Height);

        internal Point Position => new Point(X, Y);

        internal Point Center => new Point(X + Width / 2, Y + Height / 2);

        public override string ToString()
        {
            return $"{UiElementExtensions.GetUiElementToString(this)}";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is UiElement))
                return false;

            var uiElement = obj as UiElement;

            if (LocalizedControlType != uiElement.LocalizedControlType
                || ClassName != uiElement.ClassName
                || Name != uiElement.Name
                || AutomationId != uiElement.AutomationId)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = 1417081814;
            hashCode = hashCode * -1521134295 + LocalizedControlType.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ClassName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(AutomationId);
            return hashCode;
        }

        public static bool operator ==(UiElement x, UiElement y)
        {
            if (ReferenceEquals(x, null)) return ReferenceEquals(y, null);

            return x.Equals(y);
        }

        public static bool operator !=(UiElement x, UiElement y)
        {
            return !(x == y);
        }
    }
}