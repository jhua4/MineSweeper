namespace MS
{
	partial class Form1
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
			this.components = new System.ComponentModel.Container();
			this.newGameButton = new System.Windows.Forms.Button();
			this.bombsLeftLabel = new System.Windows.Forms.Label();
			this.timerLabel = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.MessageLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// newGameButton
			// 
			this.newGameButton.Location = new System.Drawing.Point(1352, 22);
			this.newGameButton.Name = "newGameButton";
			this.newGameButton.Size = new System.Drawing.Size(106, 31);
			this.newGameButton.TabIndex = 0;
			this.newGameButton.Text = "New Game";
			this.newGameButton.UseVisualStyleBackColor = true;
			this.newGameButton.Click += new System.EventHandler(this.newGameButton_Click);
			// 
			// bombsLeftLabel
			// 
			this.bombsLeftLabel.AutoSize = true;
			this.bombsLeftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bombsLeftLabel.Location = new System.Drawing.Point(1352, 117);
			this.bombsLeftLabel.Name = "bombsLeftLabel";
			this.bombsLeftLabel.Size = new System.Drawing.Size(130, 20);
			this.bombsLeftLabel.TabIndex = 1;
			this.bombsLeftLabel.Text = "Bombs Left: X";
			// 
			// timerLabel
			// 
			this.timerLabel.AutoSize = true;
			this.timerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerLabel.Location = new System.Drawing.Point(1345, 213);
			this.timerLabel.Name = "timerLabel";
			this.timerLabel.Size = new System.Drawing.Size(98, 38);
			this.timerLabel.TabIndex = 2;
			this.timerLabel.Text = "00:00";
			// 
			// timer1
			// 
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// MessageLabel
			// 
			this.MessageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MessageLabel.Location = new System.Drawing.Point(347, 793);
			this.MessageLabel.Name = "MessageLabel";
			this.MessageLabel.Size = new System.Drawing.Size(531, 46);
			this.MessageLabel.TabIndex = 3;
			this.MessageLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.ClientSize = new System.Drawing.Size(1509, 848);
			this.Controls.Add(this.MessageLabel);
			this.Controls.Add(this.timerLabel);
			this.Controls.Add(this.bombsLeftLabel);
			this.Controls.Add(this.newGameButton);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button newGameButton;
		private System.Windows.Forms.Label bombsLeftLabel;
		private System.Windows.Forms.Label timerLabel;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label MessageLabel;
	}
}

