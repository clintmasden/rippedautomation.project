
namespace RippedAutomation.WinformRecorderUi.Forms
{
    partial class DemoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RecordButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.ControllerManagerDataGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ControllerManagerDataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RecordButton
            // 
            this.RecordButton.Location = new System.Drawing.Point(12, 12);
            this.RecordButton.Name = "RecordButton";
            this.RecordButton.Size = new System.Drawing.Size(95, 40);
            this.RecordButton.TabIndex = 0;
            this.RecordButton.Text = "Record";
            this.RecordButton.UseVisualStyleBackColor = true;
            this.RecordButton.Click += new System.EventHandler(this.RecordButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(113, 12);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(95, 40);
            this.PlayButton.TabIndex = 1;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(214, 12);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(95, 40);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // ControllerManagerDataGridView
            // 
            this.ControllerManagerDataGridView.AllowUserToAddRows = false;
            this.ControllerManagerDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.ControllerManagerDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ControllerManagerDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ControllerManagerDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControllerManagerDataGridView.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.ControllerManagerDataGridView.Location = new System.Drawing.Point(0, 0);
            this.ControllerManagerDataGridView.Name = "ControllerManagerDataGridView";
            this.ControllerManagerDataGridView.ReadOnly = true;
            this.ControllerManagerDataGridView.RowHeadersWidth = 51;
            this.ControllerManagerDataGridView.RowTemplate.Height = 24;
            this.ControllerManagerDataGridView.Size = new System.Drawing.Size(1116, 689);
            this.ControllerManagerDataGridView.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.ControllerManagerDataGridView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1116, 689);
            this.panel1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1116, 747);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.RecordButton);
            this.Name = "Form1";
            this.Text = "Ripped Automation Demo";
            ((System.ComponentModel.ISupportInitialize)(this.ControllerManagerDataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RecordButton;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.DataGridView ControllerManagerDataGridView;
        private System.Windows.Forms.Panel panel1;
    }
}

