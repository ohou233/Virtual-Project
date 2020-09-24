namespace LJX8_DllSampleAll.Forms
{
	partial class ProfileForm
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
            this._textBoxTargetBank = new System.Windows.Forms.TextBox();
            this._labelTargetBank = new System.Windows.Forms.Label();
            this._textBoxPositionMode = new System.Windows.Forms.TextBox();
            this._labelPosMode = new System.Windows.Forms.Label();
            this._textBoxGetProfileNo = new System.Windows.Forms.TextBox();
            this._labelGetProfileNo = new System.Windows.Forms.Label();
            this._textBoxGetProfileCount = new System.Windows.Forms.TextBox();
            this._labelGetProfileCount = new System.Windows.Forms.Label();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonOk = new System.Windows.Forms.Button();
            this._labelTargetBankDescription = new System.Windows.Forms.Label();
            this._textBoxPosModeDescription = new System.Windows.Forms.Label();
            this._labelGetProfileNoDescription = new System.Windows.Forms.Label();
            this._textBoxErase = new System.Windows.Forms.TextBox();
            this._labelErase = new System.Windows.Forms.Label();
            this._labelEraseDescription = new System.Windows.Forms.Label();
            this._labelProfileDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this._textBoxTargetBank.Location = new System.Drawing.Point(18, 51);
            this._textBoxTargetBank.Name = "_textBoxTargetBank";
            this._textBoxTargetBank.Size = new System.Drawing.Size(69, 21);
            this._textBoxTargetBank.TabIndex = 1;
            this._textBoxTargetBank.Text = "00";
            this._labelTargetBank.AutoSize = true;
            this._labelTargetBank.Location = new System.Drawing.Point(93, 54);
            this._labelTargetBank.Name = "_labelTargetBank";
            this._labelTargetBank.Size = new System.Drawing.Size(145, 13);
            this._labelTargetBank.TabIndex = 2;
            this._labelTargetBank.Text = "Get profile bank specification";
            this._textBoxPositionMode.Location = new System.Drawing.Point(18, 115);
            this._textBoxPositionMode.Name = "_textBoxPositionMode";
            this._textBoxPositionMode.Size = new System.Drawing.Size(69, 21);
            this._textBoxPositionMode.TabIndex = 4;
            this._textBoxPositionMode.Text = "00";
            this._labelPosMode.AutoSize = true;
            this._labelPosMode.Location = new System.Drawing.Point(93, 118);
            this._labelPosMode.Name = "_labelPosMode";
            this._labelPosMode.Size = new System.Drawing.Size(198, 13);
            this._labelPosMode.TabIndex = 5;
            this._labelPosMode.Text = "Get profile position specification method";
            this._textBoxGetProfileNo.Location = new System.Drawing.Point(18, 189);
            this._textBoxGetProfileNo.Name = "_textBoxGetProfileNo";
            this._textBoxGetProfileNo.Size = new System.Drawing.Size(69, 21);
            this._textBoxGetProfileNo.TabIndex = 7;
            this._textBoxGetProfileNo.Text = "0";
            this._labelGetProfileNo.AutoSize = true;
            this._labelGetProfileNo.Location = new System.Drawing.Point(93, 192);
            this._labelGetProfileNo.Name = "_labelGetProfileNo";
            this._labelGetProfileNo.Size = new System.Drawing.Size(321, 13);
            this._labelGetProfileNo.TabIndex = 8;
            this._labelGetProfileNo.Text = "From what number profile do you want to start acquiring profiles?";
            this._textBoxGetProfileCount.Location = new System.Drawing.Point(18, 251);
            this._textBoxGetProfileCount.Name = "_textBoxGetProfileCount";
            this._textBoxGetProfileCount.Size = new System.Drawing.Size(69, 21);
            this._textBoxGetProfileCount.TabIndex = 10;
            this._textBoxGetProfileCount.Text = "1";
            this._labelGetProfileCount.AutoSize = true;
            this._labelGetProfileCount.Location = new System.Drawing.Point(93, 255);
            this._labelGetProfileCount.Name = "_labelGetProfileCount";
            this._labelGetProfileCount.Size = new System.Drawing.Size(233, 13);
            this._labelGetProfileCount.TabIndex = 11;
            this._labelGetProfileCount.Text = "Number of profiles to request the acquisition of";
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(363, 349);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 25);
            this._buttonCancel.TabIndex = 16;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(267, 349);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 25);
            this._buttonOk.TabIndex = 15;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            this._labelTargetBankDescription.AutoSize = true;
            this._labelTargetBankDescription.Location = new System.Drawing.Point(20, 75);
            this._labelTargetBankDescription.Name = "_labelTargetBankDescription";
            this._labelTargetBankDescription.Size = new System.Drawing.Size(116, 26);
            this._labelTargetBankDescription.TabIndex = 3;
            this._labelTargetBankDescription.Text = "0x00: Active surface\r\n0x01: Inactive surface";
            this._textBoxPosModeDescription.AutoSize = true;
            this._textBoxPosModeDescription.Location = new System.Drawing.Point(20, 139);
            this._textBoxPosModeDescription.Name = "_textBoxPosModeDescription";
            this._textBoxPosModeDescription.Size = new System.Drawing.Size(113, 39);
            this._textBoxPosModeDescription.TabIndex = 6;
            this._textBoxPosModeDescription.Text = "0x00: From current\r\n0x01: From oldest\r\n0x02: Specify position";
            this._labelGetProfileNoDescription.AutoSize = true;
            this._labelGetProfileNoDescription.Location = new System.Drawing.Point(20, 212);
            this._labelGetProfileNoDescription.Name = "_labelGetProfileNoDescription";
            this._labelGetProfileNoDescription.Size = new System.Drawing.Size(323, 26);
            this._labelGetProfileNoDescription.TabIndex = 9;
            this._labelGetProfileNoDescription.Text = "When the profile position specification is [0x02: Specify position], \r\nspecify th" +
    "e number of the profile to get.";
            this._textBoxErase.Location = new System.Drawing.Point(18, 289);
            this._textBoxErase.Name = "_textBoxErase";
            this._textBoxErase.Size = new System.Drawing.Size(69, 21);
            this._textBoxErase.TabIndex = 12;
            this._textBoxErase.Text = "0";
            this._labelErase.AutoSize = true;
            this._labelErase.Location = new System.Drawing.Point(93, 293);
            this._labelErase.Name = "_labelErase";
            this._labelErase.Size = new System.Drawing.Size(138, 13);
            this._labelErase.TabIndex = 13;
            this._labelErase.Text = "Already read indication flag";
            this._labelEraseDescription.AutoSize = true;
            this._labelEraseDescription.Location = new System.Drawing.Point(20, 313);
            this._labelEraseDescription.Name = "_labelEraseDescription";
            this._labelEraseDescription.Size = new System.Drawing.Size(397, 13);
            this._labelEraseDescription.TabIndex = 14;
            this._labelEraseDescription.Text = "0: Do not erase the profiles that were read. 1: Erase the profiles that were read" +
    ".";
            this._labelProfileDescription.AutoSize = true;
            this._labelProfileDescription.Location = new System.Drawing.Point(16, 10);
            this._labelProfileDescription.Name = "_labelProfileDescription";
            this._labelProfileDescription.Size = new System.Drawing.Size(395, 26);
            this._labelProfileDescription.TabIndex = 0;
            this._labelProfileDescription.Text = "From what number profile do you want to start acquiring profiles : ushort format " +
    "\r\nother than that above : byte format";
            this.AcceptButton = this._buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(471, 387);
            this.Controls.Add(this._labelProfileDescription);
            this.Controls.Add(this._labelEraseDescription);
            this.Controls.Add(this._labelErase);
            this.Controls.Add(this._textBoxErase);
            this.Controls.Add(this._labelGetProfileNoDescription);
            this.Controls.Add(this._textBoxPosModeDescription);
            this.Controls.Add(this._labelTargetBankDescription);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOk);
            this.Controls.Add(this._textBoxGetProfileCount);
            this.Controls.Add(this._labelGetProfileCount);
            this.Controls.Add(this._textBoxGetProfileNo);
            this.Controls.Add(this._labelGetProfileNo);
            this.Controls.Add(this._textBoxPositionMode);
            this.Controls.Add(this._labelPosMode);
            this.Controls.Add(this._textBoxTargetBank);
            this.Controls.Add(this._labelTargetBank);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Read the profile.";
            this.ResumeLayout(false);
            this.PerformLayout();

		}


		private System.Windows.Forms.TextBox _textBoxTargetBank;
		private System.Windows.Forms.Label _labelTargetBank;
		private System.Windows.Forms.TextBox _textBoxPositionMode;
		private System.Windows.Forms.Label _labelPosMode;
		private System.Windows.Forms.TextBox _textBoxGetProfileNo;
		private System.Windows.Forms.Label _labelGetProfileNo;
		private System.Windows.Forms.TextBox _textBoxGetProfileCount;
		private System.Windows.Forms.Label _labelGetProfileCount;
		private System.Windows.Forms.Button _buttonCancel;
		private System.Windows.Forms.Button _buttonOk;
		private System.Windows.Forms.Label _labelTargetBankDescription;
		private System.Windows.Forms.Label _textBoxPosModeDescription;
		private System.Windows.Forms.Label _labelGetProfileNoDescription;
		private System.Windows.Forms.TextBox _textBoxErase;
		private System.Windows.Forms.Label _labelErase;
		private System.Windows.Forms.Label _labelEraseDescription;
		private System.Windows.Forms.Label _labelProfileDescription;
	}
}
