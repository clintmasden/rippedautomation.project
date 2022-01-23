using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using RippedAutomation.Generation.Client;
using RippedAutomation.Generation.Client.Models;
using RippedAutomation.Generation.Events.Hook.Models;
using RippedAutomation.Generation.UiEvents.Models;
using RippedAutomation.WinformRecorderUi.Controllers;

namespace RippedAutomation.WinformRecorderUi.Forms
{
    public partial class DemoForm : Form
    {
        public DemoForm()
        {
            InitializeComponent();
            SetupDataGridView();


            ControllerManager = new ControllerManager();
            UiEvents = new List<UiEvent>();


            GenerationClient = new GenerationClient(new GenerationClientSettings
            {
                AutomationTransactionTimeout = new TimeSpan(0, 0, 5),
                IgnoreProcessId = Process.GetCurrentProcess().Id,
                HasGraphicThreadLoop = true
            });


            InitializeEventHandlers();
        }

        /// <summary>
        ///     Manages execution per controller type.
        /// </summary>
        private ControllerManager ControllerManager { get; }

        //simplified version
        private List<UiEvent> UiEvents { get; }

        /// <summary>
        ///     The meat
        /// </summary>
        private GenerationClient GenerationClient { get; }

        /// <summary>
        ///     Returns the elements event / trigger | UiEvent EventHandler - UiEvent [On Click]
        /// </summary>
        private EventHandler<UiEventEventArgs> GenerationClientUiEventEventHandler { get; set; }

        /// <summary>
        ///     Returns the element that is being hovered | Hook EventHandler - UiElement [On Hover]
        /// </summary>
        private EventHandler<HookEventEventArgs> GenerationClientHookEventEventHanlder { get; set; }


        /// <summary>
        ///     UI / Prettiness is irrelevant, this is here for demonstration purposes only
        /// </summary>
        private void SetupDataGridView()
        {
            ControllerManagerDataGridView.AutoGenerateColumns = false;
            ControllerManagerDataGridView.AutoSize = true;

            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            column.Width = 250;
            ControllerManagerDataGridView.Columns.Add(column);

            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            column2.DataPropertyName = "Description";
            column2.Name = "Description";
            column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ControllerManagerDataGridView.Columns.Add(column2);
        }


        private void InitializeEventHandlers()
        {
            GenerationClientUiEventEventHandler = GenerationClientUiEvent;
            GenerationClient.GenerationUiEventEventHandler += GenerationClientUiEventEventHandler;
        }

        private void GenerationClientUiEvent(object sender, UiEventEventArgs e)
        {
            ControllerManager.Controllers.Add(new UiEventController(e.UiEvent, e.UiEventDelay));
            UiEvents.Add(e.UiEvent);

            ControllerManagerDataGridView.DataSource = null;
            ControllerManagerDataGridView.DataSource = ControllerManager.Controllers;

            //simplified version
            //EventDataGridView.DataSource = UiEvents;
        }

        private void RecordButton_Click(object sender, EventArgs e)
        {
            SetUiForRecordingButton();

            ControllerManager.Controllers.Clear();
            UiEvents.Clear();

            ControllerManagerDataGridView.DataSource = null;

            GenerationClient.InitializeHooks();
        }

        private async void PlayButton_Click(object sender, EventArgs e)
        {
            SetUiForPlayButton();
            await ControllerManager.InitializeExecution(ControllerIndexAction);
            SetUiForStopButton();

            //simplified version
            //foreach (var uiEvent in UiEvents)
            //{
            //    PlaybackEventExtensions.Execute(new PlaybackEvent(uiEvent));
            //}
        }

        private void ControllerIndexAction(int controllerIndex)
        {
            ControllerManagerDataGridView.ClearSelection();
            ControllerManagerDataGridView.Rows[controllerIndex].Selected = true;
            ControllerManagerDataGridView.Rows[controllerIndex].Cells[0].Selected = true;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            SetUiForStopButton();
            GenerationClient.Terminate();
        }

        private void SetUiForRecordingButton()
        {
            RecordButton.Enabled = false;
            PlayButton.Enabled = false;
            StopButton.Enabled = true;
        }

        private void SetUiForStopButton()
        {
            RecordButton.Enabled = true;
            PlayButton.Enabled = true;
        }

        private void SetUiForPlayButton()
        {
            PlayButton.Enabled = false;
            RecordButton.Enabled = false;
            StopButton.Enabled = true;
        }
    }
}