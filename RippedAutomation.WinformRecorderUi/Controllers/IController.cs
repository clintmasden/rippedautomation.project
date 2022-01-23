using System;
using System.Threading.Tasks;

namespace RippedAutomation.WinformRecorderUi.Controllers
{
    public interface IController
    {
        string Name { get; set; }

        string Description { get; set; }

        TimeSpan Delay { get; set; }

        bool HasRetry { get; }

        Task<ControllerExecuteResult> Execute();
    }
}