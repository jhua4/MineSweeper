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
			this.minesLeftLabel = new System.Windows.Forms.Label();
			this.timerLabel = new System.Windows.Forms.Label();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.MessageLabel = new System.Windows.Forms.Label();
			this.spacesLeftLabel = new System.Windows.Forms.Label();
			this.beginnerRB = new System.Windows.Forms.RadioButton();
			this.intermediateRB = new System.Windows.Forms.RadioButton();
			this.expertRB = new System.Windows.Forms.RadioButton();
			this.customRB = new System.Windows.Forms.RadioButton();
			this.rowsBox = new System.Windows.Forms.TextBox();
			this.columnsBox = new System.Windows.Forms.TextBox();
			this.minesBox = new System.Windows.Forms.TextBox();
			this.customOptionsPanel = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.customOptionsPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// newGameButton
			// 
			this.newGameButton.FlatAppearance.BorderSize = 2;
			this.newGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.newGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.newGameButton.ForeColor = System.Drawing.Color.Yellow;
			this.newGameButton.Location = new System.Drawing.Point(1352, 47);
			this.newGameButton.Name = "newGameButton";
			this.newGameButton.Size = new System.Drawing.Size(153, 41);
			this.newGameButton.TabIndex = 0;
			this.newGameButton.Text = "New Game";
			this.newGameButton.UseVisualStyleBackColor = true;
			this.newGameButton.Click += new System.EventHandler(this.newGameButton_Click);
			// 
			// minesLeftLabel
			// 
			this.minesLeftLabel.AutoSize = true;
			this.minesLeftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.minesLeftLabel.ForeColor = System.Drawing.Color.Aqua;
			this.minesLeftLabel.Location = new System.Drawing.Point(1348, 120);
			this.minesLeftLabel.Name = "minesLeftLabel";
			this.minesLeftLabel.Size = new System.Drawing.Size(130, 20);
			this.minesLeftLabel.TabIndex = 1;
			this.minesLeftLabel.Text = "Bombs Left: X";
			// 
			// timerLabel
			// 
			this.timerLabel.AutoSize = true;
			this.timerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timerLabel.ForeColor = System.Drawing.Color.Aqua;
			this.timerLabel.Location = new System.Drawing.Point(1345, 183);
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
			// spacesLeftLabel
			// 
			this.spacesLeftLabel.AutoSize = true;
			this.spacesLeftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.spacesLeftLabel.ForeColor = System.Drawing.Color.Aqua;
			this.spacesLeftLabel.Location = new System.Drawing.Point(1348, 151);
			this.spacesLeftLabel.Name = "spacesLeftLabel";
			this.spacesLeftLabel.Size = new System.Drawing.Size(134, 20);
			this.spacesLeftLabel.TabIndex = 4;
			this.spacesLeftLabel.Text = "Spaces Left: X";
			// 
			// beginnerRB
			// 
			this.beginnerRB.AutoSize = true;
			this.beginnerRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.beginnerRB.ForeColor = System.Drawing.Color.Aqua;
			this.beginnerRB.Location = new System.Drawing.Point(1352, 247);
			this.beginnerRB.Name = "beginnerRB";
			this.beginnerRB.Size = new System.Drawing.Size(97, 24);
			this.beginnerRB.TabIndex = 5;
			this.beginnerRB.TabStop = true;
			this.beginnerRB.Text = "Beginner";
			this.beginnerRB.UseVisualStyleBackColor = true;
			// 
			// intermediateRB
			// 
			this.intermediateRB.AutoSize = true;
			this.intermediateRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.intermediateRB.ForeColor = System.Drawing.Color.Aqua;
			this.intermediateRB.Location = new System.Drawing.Point(1352, 274);
			this.intermediateRB.Name = "intermediateRB";
			this.intermediateRB.Size = new System.Drawing.Size(122, 24);
			this.intermediateRB.TabIndex = 6;
			this.intermediateRB.TabStop = true;
			this.intermediateRB.Text = "Intermediate";
			this.intermediateRB.UseVisualStyleBackColor = true;
			// 
			// expertRB
			// 
			this.expertRB.AutoSize = true;
			this.expertRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.expertRB.ForeColor = System.Drawing.Color.Aqua;
			this.expertRB.Location = new System.Drawing.Point(1352, 301);
			this.expertRB.Name = "expertRB";
			this.expertRB.Size = new System.Drawing.Size(78, 24);
			this.expertRB.TabIndex = 7;
			this.expertRB.TabStop = true;
			this.expertRB.Text = "Expert";
			this.expertRB.UseVisualStyleBackColor = true;
			// 
			// customRB
			// 
			this.customRB.AutoSize = true;
			this.customRB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.customRB.ForeColor = System.Drawing.Color.Aqua;
			this.customRB.Location = new System.Drawing.Point(1352, 328);
			this.customRB.Name = "customRB";
			this.customRB.Size = new System.Drawing.Size(88, 24);
			this.customRB.TabIndex = 8;
			this.customRB.TabStop = true;
			this.customRB.Text = "Custom";
			this.customRB.UseVisualStyleBackColor = true;
			this.customRB.CheckedChanged += new System.EventHandler(this.customRB_CheckedChanged);
			// 
			// rowsBox
			// 
			this.rowsBox.Location = new System.Drawing.Point(77, 11);
			this.rowsBox.Name = "rowsBox";
			this.rowsBox.Size = new System.Drawing.Size(100, 22);
			this.rowsBox.TabIndex = 9;
			// 
			// columnsBox
			// 
			this.columnsBox.Location = new System.Drawing.Point(77, 39);
			this.columnsBox.Name = "columnsBox";
			this.columnsBox.Size = new System.Drawing.Size(100, 22);
			this.columnsBox.TabIndex = 10;
			// 
			// minesBox
			// 
			this.minesBox.Location = new System.Drawing.Point(77, 67);
			this.minesBox.Name = "minesBox";
			this.minesBox.Size = new System.Drawing.Size(100, 22);
			this.minesBox.TabIndex = 11;
			// 
			// customOptionsPanel
			// 
			this.customOptionsPanel.Controls.Add(this.label3);
			this.customOptionsPanel.Controls.Add(this.label2);
			this.customOptionsPanel.Controls.Add(this.label1);
			this.customOptionsPanel.Controls.Add(this.minesBox);
			this.customOptionsPanel.Controls.Add(this.rowsBox);
			this.customOptionsPanel.Controls.Add(this.columnsBox);
			this.customOptionsPanel.Location = new System.Drawing.Point(1323, 358);
			this.customOptionsPanel.Name = "customOptionsPanel";
			this.customOptionsPanel.Size = new System.Drawing.Size(207, 100);
			this.customOptionsPanel.TabIndex = 12;
			this.customOptionsPanel.Visible = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.Aqua;
			this.label3.Location = new System.Drawing.Point(9, 70);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(45, 17);
			this.label3.TabIndex = 13;
			this.label3.Text = "Mines";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.Aqua;
			this.label2.Location = new System.Drawing.Point(9, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 17);
			this.label2.TabIndex = 13;
			this.label2.Text = "Columns";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.Aqua;
			this.label1.Location = new System.Drawing.Point(9, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 17);
			this.label1.TabIndex = 12;
			this.label1.Text = "Rows";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Purple;
			this.ClientSize = new System.Drawing.Size(1542, 848);
			this.Controls.Add(this.customOptionsPanel);
			this.Controls.Add(this.customRB);
			this.Controls.Add(this.expertRB);
			this.Controls.Add(this.intermediateRB);
			this.Controls.Add(this.beginnerRB);
			this.Controls.Add(this.spacesLeftLabel);
			this.Controls.Add(this.MessageLabel);
			this.Controls.Add(this.timerLabel);
			this.Controls.Add(this.minesLeftLabel);
			this.Controls.Add(this.newGameButton);
			this.Name = "Form1";
			this.Text = "MineSweeper";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.customOptionsPanel.ResumeLayout(false);
			this.customOptionsPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button newGameButton;
		private System.Windows.Forms.Label minesLeftLabel;
		private System.Windows.Forms.Label timerLabel;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label MessageLabel;
		private System.Windows.Forms.Label spacesLeftLabel;
		private System.Windows.Forms.RadioButton beginnerRB;
		private System.Windows.Forms.RadioButton intermediateRB;
		private System.Windows.Forms.RadioButton expertRB;
		private System.Windows.Forms.RadioButton customRB;
		private System.Windows.Forms.TextBox rowsBox;
		private System.Windows.Forms.TextBox columnsBox;
		private System.Windows.Forms.TextBox minesBox;
		private System.Windows.Forms.Panel customOptionsPanel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}

