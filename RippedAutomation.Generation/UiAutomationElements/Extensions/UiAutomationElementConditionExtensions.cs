using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RippedAutomation.Generation.UiAutomationElements.Models;
using RippedAutomation.Generation.UiElements.Models;
using UIAutomationClient;

namespace RippedAutomation.Generation.UiAutomationElements.Extensions
{
    /// <summary>
    ///     Supporting methods in regards to UiAutomationCom && AutomationElements
    /// </summary>
    public class UiAutomationElementConditionExtensions
    {
        /// <summary>
        ///     Returns related windows
        /// </summary>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        public static List<IUIAutomationElement> GetElementWindows(IUIAutomationElement automationElement)
        {
            var elementWindows = new List<IUIAutomationElement>();

            var targetAutomationElement = automationElement;
            IUIAutomationElement parentAutomationElement = null;

            while (true)
            {
                parentAutomationElement =
                    UiAutomationCom.IUIAutomationTreeWalker.GetParentElement(targetAutomationElement);

                if (parentAutomationElement == null) break;
                elementWindows.Insert(0, parentAutomationElement);

                var result = UiAutomationCom.CUIAutomation.CompareElements(parentAutomationElement,
                    UiAutomationCom.RootAutomationElement);

                if (result == 1) break;

                targetAutomationElement = parentAutomationElement;
            }

            return elementWindows;
        }

        /// <summary>
        ///     Returns first element based off conditions
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        public static AutomationElement GetFirstElementByCondition(UiElement uiElement,
            IUIAutomationElement automationElement = null)
        {
            automationElement = automationElement != null ? automationElement : UiAutomationCom.RootAutomationElement;

            var conditionElement = FindFirstElementByCondition(uiElement, automationElement);

            return new AutomationElement(conditionElement);
        }

        private static IUIAutomationElement FindFirstElementByCondition(UiElement uiElement,
            IUIAutomationElement automationElement)
        {
            IUIAutomationElement conditionElement = null;

            if (uiElement.ConditionType.HasFlag(ConditionTypes.AutomationId))
                conditionElement = GetElementByAutomationId(uiElement, automationElement);

            if (uiElement.ConditionType.HasFlag(ConditionTypes.Name) && conditionElement == null)
                conditionElement = GetElementByName(uiElement, automationElement);

            if (uiElement.ConditionType.HasFlag(ConditionTypes.ClassName) && conditionElement == null)
                conditionElement = GetElementByClassName(uiElement, automationElement);

            if (uiElement.ConditionType.HasFlag(ConditionTypes.ElementType) && conditionElement == null)
                conditionElement = GetElementByLocalizedControlType(uiElement, automationElement);

            return conditionElement;
        }

        /// <summary>
        ///     Returns 'best attempt' element based off conditions
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="uiWindow"></param>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        public static AutomationElement GetAllElementByCondition(UiElement uiElement, UiElement uiWindow,
            IUIAutomationElement automationElement = null)
        {
            automationElement = automationElement != null ? automationElement : UiAutomationCom.RootAutomationElement;

            var conditionElement = FindAllElementByCondition(uiElement, uiWindow, automationElement);

            return conditionElement;
        }

        private static AutomationElement FindAllElementByCondition(UiElement uiElement, UiElement uiWindow,
            IUIAutomationElement automationElement)
        {
            AutomationElement conditionElement = null;
            var conditionElements = GetElementsByCondition(uiElement, automationElement);

            // No elements found
            if (conditionElements.Count > 0)
            {
                // Single element returned (best case scenario)
                if (conditionElements.Count == 1)
                {
                    conditionElement = conditionElements.FirstOrDefault();
                }
                else if (conditionElements.Count > 1)
                {
                    // Fall back to elements properties
                    conditionElement = GetElementByProperties(conditionElements, uiElement);

                    if (conditionElement == null)
                        // Fall back to position
                        conditionElement =
                            GetElementByPosition(conditionElements, uiElement, uiWindow, automationElement);

                    if (conditionElement == null) throw new Exception("FindAllElementByCondition Failed");
                }
            }

            return conditionElement;
        }

        private static List<AutomationElement> GetElementByConditionCount(UiElement uiElement,
            IUIAutomationElement automationElement)
        {
            var conditionElements = new List<AutomationElement>();

            if (uiElement.ConditionType.HasFlag(ConditionTypes.AutomationId))
                conditionElements = GetElementsByAutomationId(uiElement, automationElement);

            if (uiElement.ConditionType.HasFlag(ConditionTypes.Name) &&
                (conditionElements.Count == 0 || conditionElements.Count > 1))
                conditionElements = GetElementsByName(uiElement, automationElement);

            if (uiElement.ConditionType.HasFlag(ConditionTypes.ClassName) &&
                (conditionElements.Count == 0 || conditionElements.Count > 1))
                conditionElements = GetElementsByClassName(uiElement, automationElement);

            if (uiElement.ConditionType.HasFlag(ConditionTypes.ElementType) &&
                (conditionElements.Count == 0 || conditionElements.Count > 1))
                conditionElements = GetElementsByLocalizedControlType(uiElement, automationElement);

            return conditionElements;
        }

        /// <summary>
        ///     Returns all elements that fit conditions
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        private static List<AutomationElement> GetElementsByCondition(UiElement uiElement,
            IUIAutomationElement automationElement)
        {
            var conditionElements = new List<AutomationElement>();

            if (uiElement.ConditionType.HasFlag(ConditionTypes.AutomationId))
                conditionElements = GetElementsByAutomationId(uiElement, automationElement);

            if (uiElement.ConditionType.HasFlag(ConditionTypes.Name) && conditionElements.Count == 0)
                conditionElements = GetElementsByName(uiElement, automationElement);

            if (uiElement.ConditionType.HasFlag(ConditionTypes.ClassName) && conditionElements.Count == 0)
                conditionElements = GetElementsByClassName(uiElement, automationElement);

            if (uiElement.ConditionType.HasFlag(ConditionTypes.ElementType) && conditionElements.Count == 0)
                conditionElements = GetElementsByLocalizedControlType(uiElement, automationElement);

            return conditionElements;
        }

        /// <summary>
        ///     Returns first element based on Automation Id
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        private static IUIAutomationElement GetElementByAutomationId(UiElement uiElement,
            IUIAutomationElement automationElement)
        {
            IUIAutomationElement conditionElement = null;

            try
            {
                var automationIdPropertyCondition =
                    UiAutomationCom.CUIAutomation.CreatePropertyCondition((int) PropertyConditionTypes.AutomationId,
                        uiElement.AutomationId);

                conditionElement =
                    automationElement.FindFirst(TreeScope.TreeScope_Children, automationIdPropertyCondition);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return conditionElement;
        }

        /// <summary>
        ///     Returns all elements based on Automation Id
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        private static List<AutomationElement> GetElementsByAutomationId(UiElement uiElement,
            IUIAutomationElement automationElement)
        {
            IUIAutomationElementArray conditionElements = null;

            try
            {
                var automationIdPropertyCondition =
                    UiAutomationCom.CUIAutomation.CreatePropertyCondition((int) PropertyConditionTypes.AutomationId,
                        uiElement.AutomationId);

                conditionElements =
                    automationElement.FindAll(TreeScope.TreeScope_Descendants, automationIdPropertyCondition);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return GetAutomationElementArrayToAutomationElementList(conditionElements);
        }

        /// <summary>
        ///     Returns first element based on Name
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        private static IUIAutomationElement GetElementByName(UiElement uiElement,
            IUIAutomationElement automationElement)
        {
            IUIAutomationElement conditionElement = null;

            try
            {
                var namePropertyCondition =
                    UiAutomationCom.CUIAutomation.CreatePropertyCondition((int) PropertyConditionTypes.Name,
                        uiElement.Name);

                conditionElement = automationElement.FindFirst(TreeScope.TreeScope_Children, namePropertyCondition);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return conditionElement;
        }

        /// <summary>
        ///     Returns all elements based on Name
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        private static List<AutomationElement> GetElementsByName(UiElement uiElement,
            IUIAutomationElement automationElement)
        {
            IUIAutomationElementArray conditionElements = null;

            try
            {
                var namePropertyCondition =
                    UiAutomationCom.CUIAutomation.CreatePropertyCondition((int) PropertyConditionTypes.Name,
                        uiElement.Name);

                conditionElements = automationElement.FindAll(TreeScope.TreeScope_Descendants, namePropertyCondition);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return GetAutomationElementArrayToAutomationElementList(conditionElements);
        }

        /// <summary>
        ///     Returns first element based on Class Name
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        private static IUIAutomationElement GetElementByClassName(UiElement uiElement,
            IUIAutomationElement automationElement)
        {
            IUIAutomationElement conditionElement = null;

            try
            {
                var classNamePropertyCondition =
                    UiAutomationCom.CUIAutomation.CreatePropertyCondition((int) PropertyConditionTypes.ClassName,
                        uiElement.ClassName);

                conditionElement =
                    automationElement.FindFirst(TreeScope.TreeScope_Children, classNamePropertyCondition);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return conditionElement;
        }

        /// <summary>
        ///     Returns all elements based on Class Name
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        private static List<AutomationElement> GetElementsByClassName(UiElement uiElement,
            IUIAutomationElement automationElement)
        {
            IUIAutomationElementArray conditionElements = null;

            try
            {
                var classNamePropertyCondition =
                    UiAutomationCom.CUIAutomation.CreatePropertyCondition((int) PropertyConditionTypes.ClassName,
                        uiElement.ClassName);

                conditionElements =
                    automationElement.FindAll(TreeScope.TreeScope_Descendants, classNamePropertyCondition);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return GetAutomationElementArrayToAutomationElementList(conditionElements);
        }

        /// <summary>
        ///     Returns first element based on Localized Control Type
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        private static IUIAutomationElement GetElementByLocalizedControlType(UiElement uiElement,
            IUIAutomationElement automationElement)
        {
            IUIAutomationElement conditionElement = null;

            try
            {
                var localizedControlTypePropertyCondition =
                    UiAutomationCom.CUIAutomation.CreatePropertyCondition(
                        (int) PropertyConditionTypes.LocalizedControlType, uiElement.LocalizedControl);

                conditionElement = automationElement.FindFirst(TreeScope.TreeScope_Children,
                    localizedControlTypePropertyCondition);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return conditionElement;
        }

        /// <summary>
        ///     Returns all elements based on Localized Control Type
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        private static List<AutomationElement> GetElementsByLocalizedControlType(UiElement uiElement,
            IUIAutomationElement automationElement)
        {
            IUIAutomationElementArray conditionElements = null;

            try
            {
                var localizedControlTypePropertyCondition =
                    UiAutomationCom.CUIAutomation.CreatePropertyCondition(
                        (int) PropertyConditionTypes.LocalizedControlType, uiElement.LocalizedControl);

                conditionElements = automationElement.FindAll(TreeScope.TreeScope_Descendants,
                    localizedControlTypePropertyCondition);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return GetAutomationElementArrayToAutomationElementList(conditionElements);
        }

        /// <summary>
        ///     Returns first element that matches the most properties of the original UiElement
        /// </summary>
        /// <param name="conditionElements"></param>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        private static AutomationElement GetElementByProperties(List<AutomationElement> conditionElements,
            UiElement uiElement)
        {
            AutomationElement conditionElement = null;

            var conditionWhereElements = conditionElements.Where(c =>
                c.UiElement.ClassName == uiElement.ClassName &&
                c.UiElement.LocalizedControlType == uiElement.LocalizedControlType).ToList();

            if (conditionWhereElements.Count == 1)
            {
                conditionElement = conditionWhereElements.SingleOrDefault();
            }
            else
            {
                conditionWhereElements = conditionWhereElements.Where(c =>
                    c.UiElement.Width == uiElement.Width && c.UiElement.Height == uiElement.Height).ToList();

                if (conditionWhereElements.Count == 1) conditionElement = conditionWhereElements.SingleOrDefault();
            }

            if (conditionElement == null)
            {
                // Text = Value || Name (depending on the element type)
                conditionWhereElements = conditionElements.Where(c =>
                    c.UiElement.Value.Trim() == uiElement.Value || c.UiElement.Value.Trim() == uiElement.Name).ToList();

                if (conditionWhereElements.Count == 1) conditionElement = conditionWhereElements.SingleOrDefault();
            }

            return conditionElement;
        }

        /// <summary>
        ///     Returns first element that matches by the position of the original UiElement
        /// </summary>
        /// <param name="conditionElements"></param>
        /// <param name="uiElement"></param>
        /// <param name="parentElement"></param>
        /// <param name="uiWindow"></param>
        /// <param name="automationElement"></param>
        /// <returns></returns>
        private static AutomationElement GetElementByPosition(List<AutomationElement> conditionElements,
            UiElement uiElement, UiElement uiWindow, IUIAutomationElement automationElement)
        {
            AutomationElement conditionElement = null;

            conditionElement = conditionElements.FirstOrDefault(c => c.UiElement.Position == uiElement.Position);

            if (conditionElement == null)
            {
                var currentUiWindow = new AutomationElement(automationElement);

                var offsetPosition = new Point(currentUiWindow.UiElement.Position.X - uiWindow.Position.X,
                    currentUiWindow.UiElement.Position.Y - uiWindow.Position.Y);

                var rectangleWidthScale = (currentUiWindow.UiElement.Rectangle.Width - uiWindow.Rectangle.Width) /
                                          (double) uiWindow.Rectangle.Width;
                var rectangleHeightScale = (currentUiWindow.UiElement.Rectangle.Height - uiWindow.Rectangle.Height) /
                                           (double) uiWindow.Rectangle.Height;

                var offsetPositionScale =
                    new Point(offsetPosition.X + (int) Math.Round(offsetPosition.X * rectangleWidthScale, 0),
                        offsetPosition.Y + (int) Math.Round(offsetPosition.Y * rectangleHeightScale, 0));

                var originalUiElementPosition = new Point(uiWindow.X - uiElement.X, uiWindow.Y - uiElement.Y);
                var originalUiElementPositionWithOffsetPositionScale = new Point(
                    originalUiElementPosition.X + offsetPositionScale.X,
                    originalUiElementPosition.Y + offsetPositionScale.Y);

                foreach (var element in conditionElements)
                {
                    var elementPosition = new Point(currentUiWindow.UiElement.X - element.UiElement.X,
                        currentUiWindow.UiElement.Y - element.UiElement.Y);
                    var elementPostionWithOffsetPositionScale = new Point(elementPosition.X + offsetPositionScale.X,
                        elementPosition.Y + offsetPositionScale.Y);

                    var elementPostionXWithOffsetPositionPercentage =
                        Math.Abs((elementPostionWithOffsetPositionScale.X -
                                  originalUiElementPositionWithOffsetPositionScale.X) /
                                 (double) originalUiElementPositionWithOffsetPositionScale.X) * 100;
                    var elementPostionYWithOffsetPositionPercentage =
                        Math.Abs((elementPostionWithOffsetPositionScale.Y -
                                  originalUiElementPositionWithOffsetPositionScale.Y) /
                                 (double) originalUiElementPositionWithOffsetPositionScale.Y) * 100;

                    if (elementPostionXWithOffsetPositionPercentage < 1 &&
                        elementPostionYWithOffsetPositionPercentage < 1)
                    {
                        conditionElement = element;

                        break;
                    }
                }
            }

            return conditionElement;
        }

        public static List<AutomationElement> GetDescendantElementsByAutomationElement(
            AutomationElement automationElement)
        {
            var conditionElements = automationElement.IUIAutomationElement.FindAll(TreeScope.TreeScope_Descendants,
                UiAutomationCom.CUIAutomation.CreateTrueCondition());

            return GetAutomationElementArrayToAutomationElementList(conditionElements);
        }

        private static List<AutomationElement> GetAutomationElementArrayToAutomationElementList(
            IUIAutomationElementArray automationElementArray)
        {
            var automationElementList = new List<AutomationElement>();

            for (var automationElementIndex = 0;
                automationElementIndex < automationElementArray.Length;
                automationElementIndex++)
            {
                var automationElement = automationElementArray.GetElement(automationElementIndex);

                automationElementList.Add(new AutomationElement(automationElement));
            }

            return automationElementList;
        }
    }
}