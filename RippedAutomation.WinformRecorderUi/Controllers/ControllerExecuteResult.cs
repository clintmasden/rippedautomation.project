using System;

namespace RippedAutomation.WinformRecorderUi.Controllers
{
    public class ControllerExecuteResult
    {
        public Exception Exception { get; set; }

        public bool IsPass => Exception is null;
    }
}