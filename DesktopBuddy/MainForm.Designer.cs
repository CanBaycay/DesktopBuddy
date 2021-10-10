namespace DesktopBuddy
{

	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
			this.Timer = new System.Timers.Timer();
			this.DiscordProcessPriorityAdjusterTimer = new System.Timers.Timer();
			this.LaunchAtStartupCheckbox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.Timer)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DiscordProcessPriorityAdjusterTimer)).BeginInit();
			this.SuspendLayout();
			// 
			// Timer
			// 
			this.Timer.Enabled = true;
			this.Timer.SynchronizingObject = this;
			this.Timer.Elapsed += new System.Timers.ElapsedEventHandler(this.Timer_Elapsed);
			//
			// DiscordProcessPriorityAdjusterTimer
			// 
			this.DiscordProcessPriorityAdjusterTimer.Enabled = true;
			this.DiscordProcessPriorityAdjusterTimer.Interval = 20000D;
			this.DiscordProcessPriorityAdjusterTimer.SynchronizingObject = this;
			this.DiscordProcessPriorityAdjusterTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.DiscordProcessPriorityAdjusterTimer_Elapsed);
			// 
			// LaunchAtStartupCheckbox
			// 
			this.LaunchAtStartupCheckbox.Location = new System.Drawing.Point(12, 51);
			this.LaunchAtStartupCheckbox.Name = "LaunchAtStartupCheckbox";
			this.LaunchAtStartupCheckbox.Size = new System.Drawing.Size(202, 24);
			this.LaunchAtStartupCheckbox.TabIndex = 1;
			this.LaunchAtStartupCheckbox.Text = "Launch at startup";
			this.LaunchAtStartupCheckbox.UseVisualStyleBackColor = true;
			this.LaunchAtStartupCheckbox.CheckedChanged += new System.EventHandler(this.LaunchAtStartupCheckbox_CheckedChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(320, 196);
			this.Controls.Add(this.LaunchAtStartupCheckbox);
			this.Name = "MainForm";
			this.Text = "Desktop Buddy";
			((System.ComponentModel.ISupportInitialize)(this.Timer)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DiscordProcessPriorityAdjusterTimer)).EndInit();
			this.ResumeLayout(false);
		}

		private System.Windows.Forms.CheckBox LaunchAtStartupCheckbox;

		private System.Timers.Timer DiscordProcessPriorityAdjusterTimer;

		private System.Timers.Timer Timer;

		#endregion
	}

}
