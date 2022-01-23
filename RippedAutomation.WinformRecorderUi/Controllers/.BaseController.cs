using System;
using System.Threading.Tasks;

namespace RippedAutomation.WinformRecorderUi.Controllers
{
    public class Controller : IController
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan Delay { get; set; }

        public virtual bool HasRetry => false;

        public virtual async Task<ControllerExecuteResult> Execute()
        {
            await Task.Delay(Delay);

            return new ControllerExecuteResult();
        }
    }
}