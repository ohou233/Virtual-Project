namespace LJX8_DllSampleAll.Forms
{
	partial class GetBatchProfileForm
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
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonOk = new System.Windows.Forms.Button();
            this._labelEraseDescription = new System.Windows.Forms.Label();
            this._labelErase = new System.Windows.Forms.Label();
            this._textBoxErase = new System.Windows.Forms.TextBox();
            this._textBoxPosModeDescription = new System.Windows.Forms.Label();
            this._labelTargetBankDescription = new System.Windows.Forms.Label();
            this._textBoxGetProfileCount = new System.Windows.Forms.TextBox();
            this._labelGetProfileCount = new System.Windows.Forms.Label();
            this._textBoxGetProfileNo = new System.Windows.Forms.TextBox();
            this._labelGetProfileNo = new System.Windows.Forms.Label();
            this._textBoxPositionMode = new System.Windows.Forms.TextBox();
            this._labelPosionMode = new System.Windows.Forms.Label();
            this._textBoxTargetBank = new System.Windows.Forms.TextBox();
            this._labelTargetBank = new System.Windows.Forms.Label();
            this._textBoxGetBatchNo = new System.Windows.Forms.TextBox();
            this._labelGetBatchNo = new System.Windows.Forms.Label();
            this._labelProfileDescription = new System.Windows.Forms.Label();
            this._labelGetBatchNoDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(361, 409);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 25);
            this._buttonCancel.TabIndex = 18;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            // 
            // _buttonOk
            // 
            this._buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(271, 409);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 25);
            this._buttonOk.TabIndex = 17;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            // 
            // _labelEraseDescription
            // 
            this._labelEraseDescription.AutoSize = true;
            this._labelEraseDescription.Location = new System.Drawing.Point(23, 379);
            this._labelEraseDescription.Name = "_labelEraseDescription";
            this._labelEraseDescription.Size = new System.Drawing.Size(221, 26);
            this._labelEraseDescription.TabIndex = 16;
            this._labelEraseDescription.Text = "0: Do not erase the batches that were read.\r\n1: Erase the batches that were read." +
    "";
            // 
            // _labelErase
            // 
            this._labelErase.AutoSize = true;
            this._labelErase.Location = new System.Drawing.Point(96, 359);
            this._labelErase.Name = "_labelErase";
            this._labelErase.Size = new System.Drawing.Size(138, 13);
            this._labelErase.TabIndex = 15;
            this._labelErase.Text = "Already read indication flag";
            // 
            // _textBoxErase
            // 
            this._textBoxErase.Location = new System.Drawing.Point(21, 356);
            this._textBoxErase.Name = "_textBoxErase";
            this._textBoxErase.Size = new System.Drawing.Size(69, 21);
            this._textBoxErase.TabIndex = 14;
            this._textBoxErase.Text = "0";
            // 
            // _textBoxPosModeDescription
            // 
            this._textBoxPosModeDescription.AutoSize = true;
            this._textBoxPosModeDescription.Location = new System.Drawing.Point(23, 151);
            this._textBoxPosModeDescription.Name = "_textBoxPosModeDescription";
            this._textBoxPosModeDescription.Size = new System.Drawing.Size(217, 52);
            this._textBoxPosModeDescription.TabIndex = 6;
            this._textBoxPosModeDescription.Text = "0x00: From current\r\n0x02: Specify position\r\n0x03: From current after batch commit" +
    "ment\r\n0x04: Current only";
            // 
            // _labelTargetBankDescription
            // 
            this._labelTargetBankDescription.AutoSize = true;
            this._labelTargetBankDescription.Location = new System.Drawing.Point(23, 87);
            this._labelTargetBankDescription.Name = "_labelTargetBankDescription";
            this._labelTargetBankDescription.Size = new System.Drawing.Size(116, 26);
            this._labelTargetBankDescription.TabIndex = 3;
            this._labelTargetBankDescription.Text = "0x00: Active surface\r\n0x01: Inactive surface";
            // 
            // _textBoxGetProfileCount
            // 
            this._textBoxGetProfileCount.Location = new System.Drawing.Point(21, 318);
            this._textBoxGetProfileCount.Name = "_textBoxGetProfileCount";
            this._textBoxGetProfileCount.Size = new System.Drawing.Size(69, 21);
            this._textBoxGetProfileCount.TabIndex = 12;
            this._textBoxGetProfileCount.Text = "1";
            // 
            // _labelGetProfileCount
            // 
            this._labelGetProfileCount.AutoSize = true;
            this._labelGetProfileCount.Location = new System.Drawing.Point(96, 321);
            this._labelGetProfileCount.Name = "_labelGetProfileCount";
            this._labelGetProfileCount.Size = new System.Drawing.Size(220, 13);
            this._labelGetProfileCount.TabIndex = 13;
            this._labelGetProfileCount.Text = "Number of profiles to request the acquisition";
            // 
            // _textBoxGetProfileNo
            // 
            this._textBoxGetProfileNo.Location = new System.Drawing.Point(21, 282);
            this._textBoxGetProfileNo.Name = "_textBoxGetProfileNo";
            this._textBoxGetProfileNo.Size = new System.Drawing.Size(69, 21);
            this._textBoxGetProfileNo.TabIndex = 9;
            this._textBoxGetProfileNo.Text = "0";
            // 
            // _labelGetProfileNo
            // 
            this._labelGetProfileNo.AutoSize = true;
            this._labelGetProfileNo.Location = new System.Drawing.Point(96, 285);
            this._labelGetProfileNo.Name = "_labelGetProfileNo";
            this._labelGetProfileNo.Size = new System.Drawing.Size(321, 13);
            this._labelGetProfileNo.TabIndex = 10;
            this._labelGetProfileNo.Text = "From what number profile do you want to start acquiring profiles?";
            // 
            // _textBoxPositionMode
            // 
            this._textBoxPositionMode.Location = new System.Drawing.Point(21, 127);
            this._textBoxPositionMode.Name = "_textBoxPositionMode";
            this._textBoxPositionMode.Size = new System.Drawing.Size(69, 21);
            this._textBoxPositionMode.TabIndex = 4;
            this._textBoxPositionMode.Text = "00";
            // 
            // _labelPosionMode
            // 
            this._labelPosionMode.AutoSize = true;
            this._labelPosionMode.Location = new System.Drawing.Point(96, 130);
            this._labelPosionMode.Name = "_labelPosionMode";
            this._labelPosionMode.Size = new System.Drawing.Size(191, 13);
            this._labelPosionMode.TabIndex = 5;
            this._labelPosionMode.Text = "Get bank position specification method";
            // 
            // _textBoxTargetBank
            // 
            this._textBoxTargetBank.Location = new System.Drawing.Point(21, 63);
            this._textBoxTargetBank.Name = "_textBoxTargetBank";
            this._textBoxTargetBank.Size = new System.Drawing.Size(69, 21);
            this._textBoxTargetBank.TabIndex = 1;
            this._textBoxTargetBank.Text = "00";
            // 
            // _labelTargetBank
            // 
            this._labelTargetBank.AutoSize = true;
            this._labelTargetBank.Location = new System.Drawing.Point(96, 69);
            this._labelTargetBank.Name = "_labelTargetBank";
            this._labelTargetBank.Size = new System.Drawing.Size(145, 13);
            this._labelTargetBank.TabIndex = 2;
            this._labelTargetBank.Text = "Get profile bank specification";
            // 
            // _textBoxGetBatchNo
            // 
            this._textBoxGetBatchNo.Location = new System.Drawing.Point(21, 216);
            this._textBoxGetBatchNo.Name = "_textBoxGetBatchNo";
            this._textBoxGetBatchNo.Size = new System.Drawing.Size(69, 21);
            this._textBoxGetBatchNo.TabIndex = 7;
            this._textBoxGetBatchNo.Text = "0";
            // 
            // _labelGetBatchNo
            // 
            this._labelGetBatchNo.AutoSize = true;
            this._labelGetBatchNo.Location = new System.Drawing.Point(96, 219);
            this._labelGetBatchNo.Name = "_labelGetBatchNo";
            this._labelGetBatchNo.Size = new System.Drawing.Size(318, 13);
            this._labelGetBatchNo.TabIndex = 8;
            this._labelGetBatchNo.Text = "What is the number of the batch that contains the profile to get?";
            // 
            // _labelProfileDescription
            // 
            this._labelProfileDescription.AutoSize = true;
            this._labelProfileDescription.Location = new System.Drawing.Point(23, 10);
            this._labelProfileDescription.Name = "_labelProfileDescription";
            this._labelProfileDescription.Size = new System.Drawing.Size(395, 26);
            this._labelProfileDescription.TabIndex = 0;
            this._labelProfileDescription.Text = "From what number profile do you want to start acquiring profiles : ushort format " +
    "\r\nother than that above : byte format";
            // 
            // _labelGetBatchNoDescription
            // 
            this._labelGetBatchNoDescription.AutoSize = true;
            this._labelGetBatchNoDescription.Location = new System.Drawing.Point(23, 240);
            this._labelGetBatchNoDescription.Name = "_labelGetBatchNoDescription";
            this._labelGetBatchNoDescription.Size = new System.Drawing.Size(374, 26);
            this._labelGetBatchNoDescription.TabIndex = 11;
            this._labelGetBatchNoDescription.Text = "When the get bank position specification method is [0x02: Specify position], \r\nsp" +
    "ecify the number of the batch to get.";
            // 
            // GetBatchProfileForm
            // 
            this.AcceptButton = this._buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(465, 446);
            this.Controls.Add(this._labelProfileDescription);
            this.Controls.Add(this._textBoxGetBatchNo);
            this.Controls.Add(this._labelGetBatchNo);
            this.Controls.Add(this._labelEraseDescription);
            this.Controls.Add(this._labelErase);
            this.Controls.Add(this._textBoxErase);
            this.Controls.Add(this._labelGetBatchNoDescription);
            this.Controls.Add(this._textBoxPosModeDescription);
            this.Controls.Add(this._labelTargetBankDescription);
            this.Controls.Add(this._textBoxGetProfileCount);
            this.Controls.Add(this._labelGetProfileCount);
            this.Controls.Add(this._textBoxGetProfileNo);
            this.Controls.Add(this._labelGetProfileNo);
            this.Controls.Add(this._textBoxPositionMode);
            this.Controls.Add(this._labelPosionMode);
            this.Controls.Add(this._textBoxTargetBank);
            this.Controls.Add(this._labelTargetBank);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOk);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GetBatchProfileForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GetBatchProfileForm";
            this.ResumeLayout(false);
            this.PerformLayout();

		}


		private System.Windows.Forms.Button _buttonCancel;
		private System.Windows.Forms.Button _buttonOk;
		private System.Windows.Forms.Label _labelEraseDescription;
		private System.Windows.Forms.Label _labelErase;
		private System.Windows.Forms.TextBox _textBoxErase;
		private System.Windows.Forms.Label _textBoxPosModeDescription;
		private System.Windows.Forms.Label _labelTargetBankDescription;
		private System.Windows.Forms.TextBox _textBoxGetProfileCount;
		private System.Windows.Forms.Label _labelGetProfileCount;
		private System.Windows.Forms.TextBox _textBoxGetProfileNo;
		private System.Windows.Forms.Label _labelGetProfileNo;
		private System.Windows.Forms.TextBox _textBoxPositionMode;
		private System.Windows.Forms.Label _labelPosionMode;
		private System.Windows.Forms.TextBox _textBoxTargetBank;
		private System.Windows.Forms.Label _labelTargetBank;
		private System.Windows.Forms.TextBox _textBoxGetBatchNo;
		private System.Windows.Forms.Label _labelGetBatchNo;
		private System.Windows.Forms.Label _labelProfileDescription;
        private System.Windows.Forms.Label _labelGetBatchNoDescription;
    }
}
