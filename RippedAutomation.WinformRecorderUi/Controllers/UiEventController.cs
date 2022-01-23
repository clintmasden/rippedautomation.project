using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RippedAutomation.Generation.PlaybackEvents.Extensions;
using RippedAutomation.Generation.PlaybackEvents.Models;
using RippedAutomation.Generation.UiElements.Extensions;
using RippedAutomation.Generation.UiEvents.Models;

namespace RippedAutomation.WinformRecorderUi.Controllers
{
    public class UiEventController : Controller
    {
        public UiEventController(UiEvent uiEvent, TimeSpan uiEventDelay)
        {
            UiEvent = uiEvent;
            Delay = new TimeSpan(0, 0, (int) uiEventDelay.TotalSeconds);

            GenerateNameAndDescription();
        }

        public UiEvent UiEvent { get; set; }

        public override bool HasRetry => true;

        public void GenerateNameAndDescription()
        {
            var buildElementName = string.Empty;

            buildElementName += $"({UiEvent.UiElement.LocalizedControlType} - ";

            var uiElementValue = UiEvent.UiElement.Value;

            if (!string.IsNullOrWhiteSpace(uiElementValue))
            {
                if (uiElementValue.Length >= 45)
                {
                    uiElementValue = uiElementValue.Substring(0, 45) + " ...";
                }

                uiElementValue = Regex.Replace(uiElementValue, @"\r\n?|\n", " -> ");

                buildElementName += uiElementValue;
            }
            else
            {
                buildElementName += UiElementExtensions.GetUiElementToString(UiEvent.UiElement);
            }

            buildElementName += ")";

            var buildDescription = string.Empty;
            buildDescription += buildDescription = $"{UiEvent} || ";

            switch (UiEvent.UiEventType)
            {
                case UiEventTypes.Keyboard_SendKeys:
                    buildDescription += $"Typed: {UiEvent.KeyboardEvent.Description} | {buildElementName}";
                    break;

                case UiEventTypes.Mouse_LeftClick:
                    buildDescription += $"Left Clicked: {buildElementName}";
                    break;

                case UiEventTypes.Mouse_RightClick:
                    buildDescription += $"Right Clicked: {buildElementName}";
                    break;

                case UiEventTypes.Mouse_LeftDoubleClick:
                    buildDescription += $"Double Left Clicked: {buildElementName}";
                    break;

                case UiEventTypes.Mouse_Drag:
                case UiEventTypes.Mouse_DragStop:
                    buildDescription += "Dragged: Window";
                    break;

                case UiEventTypes.Mouse_Wheel:
                    buildDescription += $"Scrolled: {buildElementName}";
                    break;
            }

            buildDescription += $" | Delayed {Delay.TotalSeconds}s";

            Name = $"{GetType().Name}: {UiEvent.UiEventType} - {UiEvent.UiElement}";
            Description = buildDescription;
        }

        public override async Task<ControllerExecuteResult> Execute()
        {
            await Task.Delay(Delay + new TimeSpan(0, 0, 0, 0, 200));

            var controllerExecuteResult = new ControllerExecuteResult();
            try
            {
                PlaybackEventExtensions.Execute(new PlaybackEvent(UiEvent));
            }
            catch (Exception exception)
            {
                controllerExecuteResult.Exception = exception;
            }

            return controllerExecuteResult;
        }
    }
}