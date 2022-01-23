using System;
using RippedAutomation.Generation.Client.Models;
using RippedAutomation.Generation.Events.Hook;
using RippedAutomation.Generation.Events.Hook.Models;
using RippedAutomation.Generation.Events.Keyboard;
using RippedAutomation.Generation.Events.Native;
using RippedAutomation.Generation.Events.Native.Models;
using RippedAutomation.Generation.UiAutomationElements;
using RippedAutomation.Generation.UiEvents.Models;

namespace RippedAutomation.Generation.Client
{
    /// <summary>
    ///     Creates client for Automation
    /// </summary>
    public class GenerationClient
    {
        private readonly GenerationClientSettings _settings;

        public GenerationClient(GenerationClientSettings settings)
        {
            _settings = settings;

            UiAutomationCom.SetTransactionTimeout(settings.AutomationTransactionTimeout);
            KeyboardHook.DefaultControls();

            InitializeDPIAwareness();
        }

        /// <summary>
        ///     Returns State of Client (IsHookProcedurePaused)
        /// </summary>
        public bool IsPaused => HookConstants.IsHookProcedurePaused;

        public event EventHandler<UiEventEventArgs> GenerationUiEventEventHandler;

        public event EventHandler<HookEventEventArgs> GenerationHookEventEventHandler;

        public event EventHandler<EventArgs> GenerationHookProcedureEventHandler;

        /// <summary>
        ///     UiEvent EventHandler - UiEvent [On Click]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PostGenerationUiEventEventHandler(object sender, UiEventEventArgs e)
        {
            GenerationUiEventEventHandler?.Invoke(this, e);
        }

        /// <summary>
        ///     Hook EventHandler - UiElement [On Hover]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PostGenerationHookEventEventHandler(object sender, HookEventEventArgs e)
        {
            GenerationHookEventEventHandler?.Invoke(this, e);
        }

        /// <summary>
        ///     Keyboard Hook EventHandler - IsHookProcedurePaused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PostGenerationHookProcedureEventHandler(object sender, EventArgs e)
        {
            GenerationHookProcedureEventHandler?.Invoke(this, e);
        }

        /// <summary>
        ///     Corrects DPI Awareness for Windows Environment
        /// </summary>
        /// <remarks>
        ///     https://stackoverflow.com/questions/50239138/dpi-awareness-unaware-in-one-release-system-aware-in-the-other
        ///     https://stackoverflow.com/questions/43537990/wpf-clickonce-dpi-awareness-per-monitor-v2/43537991#43537991
        /// </remarks>
        private void InitializeDPIAwareness()
        {
            if (Environment.OSVersion.Version >= new Version(6, 3, 0))
            {
                if (Environment.OSVersion.Version >= new Version(10, 0, 15063))
                {
                    NativeMethods.SetProcessDpiAwarenessContext((int)DpiAwarenessContextTypes
                        .Context_Per_Monitor_Aware_V2);
                   //Information("Set DPI Awareness for Windows 10");
                }
                else
                {
                    NativeMethods.SetProcessDpiAwareness(DpiAwarenessProcessTypes.Process_Per_Monitor_Dpi_Aware);
                    //Information("Set DPI Awareness for Windows 8");
                }
            }
            else
            {
                NativeMethods.SetProcessDPIAware();
                //Information("Set DPI Awareness for Windows 7");
            }
        }

        private void TerminateAutomation()
        {
            UiAutomationCom.Terminate();
        }

        public void InitializeHooks()
        {
            HookEventHandler.Initialize(new HookEventHandlerSettings()
            {
                HasGraphicThreadLoop = _settings.HasGraphicThreadLoop,
                IgnoreProcessId = _settings.IgnoreProcessId
            });

            HookEventHandler.UiEventEventHandler += PostGenerationUiEventEventHandler;
            HookEventHandler.HookElementEventHandler += PostGenerationHookEventEventHandler;
            KeyboardHook.HookProcedureEventHandler += PostGenerationHookProcedureEventHandler;
        }

        private void TerminateHooks()
        {
            HookEventHandler.UiEventEventHandler -= PostGenerationUiEventEventHandler;
            HookEventHandler.HookElementEventHandler -= PostGenerationHookEventEventHandler;
            KeyboardHook.HookProcedureEventHandler -= PostGenerationHookProcedureEventHandler;

            HookEventHandler.Terminate();
        }

        public void Terminate()
        {
            TerminateHooks();
            TerminateAutomation();
        }

        /// <summary>
        ///     Changes State of Client (IsHookProcedurePaused)
        /// </summary>
        public void Pause()
        {
            HookConstants.IsHookProcedurePaused = !HookConstants.IsHookProcedurePaused;
        }
    }
}