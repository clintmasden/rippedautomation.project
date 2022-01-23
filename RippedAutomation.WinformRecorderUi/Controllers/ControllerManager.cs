using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RippedAutomation.WinformRecorderUi.Controllers
{
    public class ControllerManager
    {
        public ControllerManager()
        {
            Controllers = new List<IController>();
        }

        public List<IController> Controllers { get; set; }


        private CancellationTokenSource CancellationTokenSource { get; set; }


        public async Task InitializeExecution(Action<int> controllerIndexAction)
        {
            CancellationTokenSource = new CancellationTokenSource();
            TerminateExecutionIsCancellationRequested();

            await Execute(controllerIndexAction);
        }

        private async Task Execute(Action<int> controllerSequenceAction)
        {
            for (var controllerIndex = 0; controllerIndex < Controllers.Count; controllerIndex++)
            {
                controllerSequenceAction?.Invoke(controllerIndex);
                var controller = Controllers[controllerIndex];

                if (CancellationTokenSource.IsCancellationRequested)
                {
                    return;
                }


                var executeControllerStartDateTime = DateTime.Now;

                Console.WriteLine(@"Controller", controller);
                var controllerExecuteResult = await controller.Execute();

                while (!controllerExecuteResult.IsPass && controller.HasRetry &&
                       (DateTime.Now - executeControllerStartDateTime).TotalSeconds <= new TimeSpan(0, 0, 5).TotalSeconds)
                {
                    controllerExecuteResult = await controller.Execute();
                }

                if (controllerExecuteResult.IsPass)
                {
                    continue;
                }

                Console.WriteLine($@"Controller {controller.Name} Exception", controllerExecuteResult.Exception);
                Console.WriteLine($@"Controller {controller.Name} Throw: {controllerExecuteResult.Exception.Message}");

                MessageBox.Show($@"{controller.Name}: {controller.Description}", @"Execute - Error", MessageBoxButtons.OK);

                Terminate();

                return;
            }

            MessageBox.Show(@"Execute completed successfully", @"Execute - Complete", MessageBoxButtons.OK);
            Terminate();
        }

        internal void Terminate()
        {
            CancellationTokenSource.Cancel();
        }

        /// <summary>
        ///     Hold CTRL key to terminate execution
        /// </summary>
        private async void TerminateExecutionIsCancellationRequested()
        {
            while (!CancellationTokenSource.IsCancellationRequested)
            {
                if (Control.ModifierKeys != 0)
                {
                    Terminate();
                }

                await Task.Delay(500);
            }
        }
    }
}