namespace DesktopLayouts
{

	partial class Form1
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
			this.TestTimer = new System.Timers.Timer();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.TestTimer)).BeginInit();
			this.SuspendLayout();
			// 
			// TestTimer
			// 
			this.TestTimer.Enabled = true;
			this.TestTimer.Interval = 10D;
			this.TestTimer.SynchronizingObject = this;
			this.TestTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.TestTimer_Elapsed);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(22, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(712, 354);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.TestTimer)).EndInit();
			this.ResumeLayout(false);
		}

		private System.Windows.Forms.Label label1;

		private System.Timers.Timer TestTimer;

		#endregion
	}

}
