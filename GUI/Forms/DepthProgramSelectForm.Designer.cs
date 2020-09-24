namespace LJX8_DllSampleAll.Forms
{
	partial class DepthProgramSelectForm
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}


		private void InitializeComponent()
		{
            this._textBoxDepth = new System.Windows.Forms.TextBox();
            this._labelDepth = new System.Windows.Forms.Label();
            this._labelTargetProgram = new System.Windows.Forms.Label();
            this._textBoxTargetProgram = new System.Windows.Forms.TextBox();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonOk = new System.Windows.Forms.Button();
            this._labelDepthDescription = new System.Windows.Forms.Label();
            this._labelHexDepth = new System.Windows.Forms.Label();
            this._labelTargetDescription = new System.Windows.Forms.Label();
            this._labelHexTarget = new System.Windows.Forms.Label();
            this._panelLevel = new System.Windows.Forms.Panel();
            this._panelTarget = new System.Windows.Forms.Panel();
            this._panelBottom = new System.Windows.Forms.Panel();
            this._panelLevel.SuspendLayout();
            this._panelTarget.SuspendLayout();
            this._panelBottom.SuspendLayout();
            this.SuspendLayout();
            this._textBoxDepth.Location = new System.Drawing.Point(29, 27);
            this._textBoxDepth.Name = "_textBoxDepth";
            this._textBoxDepth.Size = new System.Drawing.Size(81, 21);
            this._textBoxDepth.TabIndex = 1;
            this._textBoxDepth.Text = "00";
            this._labelDepth.AutoSize = true;
            this._labelDepth.Location = new System.Drawing.Point(116, 31);
            this._labelDepth.Name = "_labelDepth";
            this._labelDepth.Size = new System.Drawing.Size(32, 13);
            this._labelDepth.TabIndex = 2;
            this._labelDepth.Text = "Level";
            this._labelTargetProgram.AutoSize = true;
            this._labelTargetProgram.Location = new System.Drawing.Point(113, 28);
            this._labelTargetProgram.Name = "_labelTargetProgram";
            this._labelTargetProgram.Size = new System.Drawing.Size(93, 13);
            this._labelTargetProgram.TabIndex = 2;
            this._labelTargetProgram.Text = "Specified program";
            this._textBoxTargetProgram.Location = new System.Drawing.Point(29, 27);
            this._textBoxTargetProgram.Name = "_textBoxTargetProgram";
            this._textBoxTargetProgram.Size = new System.Drawing.Size(78, 21);
            this._textBoxTargetProgram.TabIndex = 1;
            this._textBoxTargetProgram.Text = "00";
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(291, 7);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 25);
            this._buttonCancel.TabIndex = 1;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(210, 7);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 25);
            this._buttonOk.TabIndex = 0;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            this._labelDepthDescription.AutoSize = true;
            this._labelDepthDescription.Location = new System.Drawing.Point(29, 51);
            this._labelDepthDescription.Name = "_labelDepthDescription";
            this._labelDepthDescription.Size = new System.Drawing.Size(232, 52);
            this._labelDepthDescription.TabIndex = 3;
            this._labelDepthDescription.Text = "Level (specified with LJX8IF_SETTING_DEPTH) \r\n 0x00  Write settings memory  \r\n 0x" +
    "01  Running memory   \r\n 0x02  Save memory \r\n";
            this._labelHexDepth.AutoSize = true;
            this._labelHexDepth.Location = new System.Drawing.Point(6, 31);
            this._labelHexDepth.Name = "_labelHexDepth";
            this._labelHexDepth.Size = new System.Drawing.Size(19, 13);
            this._labelHexDepth.TabIndex = 0;
            this._labelHexDepth.Text = "0x";
            this._labelTargetDescription.AutoSize = true;
            this._labelTargetDescription.Location = new System.Drawing.Point(29, 52);
            this._labelTargetDescription.Name = "_labelTargetDescription";
            this._labelTargetDescription.Size = new System.Drawing.Size(324, 65);
            this._labelTargetDescription.TabIndex = 3;
            this._labelTargetDescription.Text = "Specified program (specified with LJX8IF_INIT_SETTING_TARGET)\r\n 0x00 program 0\r\n " +
    "0x01 program 1\r\n ...\r\n 0x0F program 15";
            this._labelHexTarget.AutoSize = true;
            this._labelHexTarget.Location = new System.Drawing.Point(3, 31);
            this._labelHexTarget.Name = "_labelHexTarget";
            this._labelHexTarget.Size = new System.Drawing.Size(19, 13);
            this._labelHexTarget.TabIndex = 0;
            this._labelHexTarget.Text = "0x";
            this._panelLevel.Controls.Add(this._textBoxDepth);
            this._panelLevel.Controls.Add(this._labelDepth);
            this._panelLevel.Controls.Add(this._labelDepthDescription);
            this._panelLevel.Controls.Add(this._labelHexDepth);
            this._panelLevel.Location = new System.Drawing.Point(0, 0);
            this._panelLevel.Name = "_panelLevel";
            this._panelLevel.Size = new System.Drawing.Size(378, 120);
            this._panelLevel.TabIndex = 0;
            this._panelTarget.Controls.Add(this._textBoxTargetProgram);
            this._panelTarget.Controls.Add(this._labelTargetProgram);
            this._panelTarget.Controls.Add(this._labelHexTarget);
            this._panelTarget.Controls.Add(this._labelTargetDescription);
            this._panelTarget.Location = new System.Drawing.Point(0, 120);
            this._panelTarget.Name = "_panelTarget";
            this._panelTarget.Size = new System.Drawing.Size(378, 130);
            this._panelTarget.TabIndex = 1;
            this._panelBottom.Controls.Add(this._buttonOk);
            this._panelBottom.Controls.Add(this._buttonCancel);
            this._panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._panelBottom.Location = new System.Drawing.Point(0, 264);
            this._panelBottom.Name = "_panelBottom";
            this._panelBottom.Size = new System.Drawing.Size(378, 44);
            this._panelBottom.TabIndex = 2;
            this.AcceptButton = this._buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(378, 308);
            this.Controls.Add(this._panelBottom);
            this.Controls.Add(this._panelTarget);
            this.Controls.Add(this._panelLevel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DepthProgramSelectForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Program settings";
            this._panelLevel.ResumeLayout(false);
            this._panelLevel.PerformLayout();
            this._panelTarget.ResumeLayout(false);
            this._panelTarget.PerformLayout();
            this._panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

		}


		private System.Windows.Forms.TextBox _textBoxDepth;
		private System.Windows.Forms.Label _labelDepth;
		private System.Windows.Forms.Label _labelTargetProgram;
		private System.Windows.Forms.TextBox _textBoxTargetProgram;
		private System.Windows.Forms.Button _buttonCancel;
		private System.Windows.Forms.Button _buttonOk;
		private System.Windows.Forms.Label _labelDepthDescription;
		private System.Windows.Forms.Label _labelHexDepth;
		private System.Windows.Forms.Label _labelTargetDescription;
		private System.Windows.Forms.Label _labelHexTarget;
        private System.Windows.Forms.Panel _panelLevel;
        private System.Windows.Forms.Panel _panelTarget;
        private System.Windows.Forms.Panel _panelBottom;
    }
}
