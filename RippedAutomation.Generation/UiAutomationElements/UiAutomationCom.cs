using System;
using System.Drawing;
using System.Runtime.InteropServices;
using RippedAutomation.Generation.UiAutomationElements.Models;
using UIAutomationClient;

namespace RippedAutomation.Generation.UiAutomationElements
{
    /// <summary>
    ///     Root COM for accessing IUIAutomationElements
    /// </summary>
    /// <remarks>
    ///     This is responsible for replacing the root of WinAppDriver
    /// </remarks>
    /// <see cref="https://github.com/TestStack/UIAComWrapper/blob/master/UiaComWrapper/Automation.cs" />
    internal static class UiAutomationCom
    {
        private static CUIAutomation8 _cUIAutomation;
        private static IUIAutomationElement _rootAutomationElement;
        private static IUIAutomationTreeWalker _iUIAutomationTreeWalker;

        /// <summary>
        ///     Main COM Interface
        /// </summary>
        internal static CUIAutomation8 CUIAutomation
        {
            get
            {
                // Try CUIAutomation8
                if (_cUIAutomation == null)
                    try
                    {
                        _cUIAutomation = new CUIAutomation8
                        {
                            TransactionTimeout = 5000
                        };
                    }
                    catch (COMException)
                    {
                        throw new Exception("CUIAutomation8 Failure");
                    }

                return _cUIAutomation;
            }
        }

        /// <summary>
        ///     Used for searching up and down a tree of related elements
        /// </summary>
        /// <remarks>
        ///     Used exclusively for finding all windows regarding the element given [will need to reworked if needed for any other
        ///     purpose]
        /// </remarks>
        internal static IUIAutomationTreeWalker IUIAutomationTreeWalker
        {
            get
            {
                if (_iUIAutomationTreeWalker == null)
                {
                    // var truePropertyCondition = CUIAutomation.CreateTrueCondition();
                    var localizedPropertyCondition =
                        CUIAutomation.CreatePropertyCondition((int) PropertyConditionTypes.LocalizedControlType,
                            "window");

                    _iUIAutomationTreeWalker = CUIAutomation.CreateTreeWalker(localizedPropertyCondition);
                }

                return _iUIAutomationTreeWalker;
            }
        }

        /// <summary>
        ///     Desktop IUIAutomationElement
        /// </summary>
        internal static IUIAutomationElement RootAutomationElement
        {
            get
            {
                if (_rootAutomationElement == null) _rootAutomationElement = CUIAutomation.GetRootElement();

                return _rootAutomationElement;
            }
        }

        /// <summary>
        ///     Sets the Transaction Timeout [How long should it query to grab the information requested]
        /// </summary>
        /// <param name="automationTransactionTimeout"></param>
        internal static void SetTransactionTimeout(TimeSpan automationTransactionTimeout)
        {
            CUIAutomation.TransactionTimeout = (uint) automationTransactionTimeout.TotalSeconds * 1000;
        }

        /// <summary>
        ///     Disposes static classes and properties
        /// </summary>
        internal static void Terminate()
        {
            _cUIAutomation = null;
            _rootAutomationElement = null;
            _iUIAutomationTreeWalker = null;
        }

        /// <summary>
        ///     Cursor Point IUIAutomationElement [Infinite Transaction Time]
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        internal static IUIAutomationElement GetUiElementFromPoint(Point point)
        {
            var tagPoint = new tagPOINT
            {
                x = point.X,
                y = point.Y
            };

            IUIAutomationElement automationElement = null;

            while (automationElement == null)
                try
                {
                    automationElement = CUIAutomation.ElementFromPoint(tagPoint);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                    CUIAutomation.TransactionTimeout += 1000;
                }

            CUIAutomation.TransactionTimeout = 5000;

            return automationElement;
        }
    }
}