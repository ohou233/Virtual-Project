namespace LJX8_DllSampleAll.Forms
{
	partial class ClearErrorForm
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
            this._labelClearErrCode = new System.Windows.Forms.Label();
            this._textBoxErrCode = new System.Windows.Forms.TextBox();
            this._labelHexErrCode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(198, 62);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 25);
            this._buttonCancel.TabIndex = 4;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(102, 62);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 25);
            this._buttonOk.TabIndex = 3;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            this._labelClearErrCode.AutoSize = true;
            this._labelClearErrCode.Location = new System.Drawing.Point(110, 21);
            this._labelClearErrCode.Name = "_labelClearErrCode";
            this._labelClearErrCode.Size = new System.Drawing.Size(155, 13);
            this._labelClearErrCode.TabIndex = 2;
            this._labelClearErrCode.Text = "Error code of the error to clear";
            this._textBoxErrCode.Location = new System.Drawing.Point(33, 16);
            this._textBoxErrCode.MaxLength = 4;
            this._textBoxErrCode.Name = "_textBoxErrCode";
            this._textBoxErrCode.Size = new System.Drawing.Size(71, 21);
            this._textBoxErrCode.TabIndex = 1;
            this._textBoxErrCode.Text = "0085";
            this._labelHexErrCode.AutoSize = true;
            this._labelHexErrCode.Location = new System.Drawing.Point(12, 21);
            this._labelHexErrCode.Name = "_labelHexErrCode";
            this._labelHexErrCode.Size = new System.Drawing.Size(19, 13);
            this._labelHexErrCode.TabIndex = 0;
            this._labelHexErrCode.Text = "0x";
            this.AcceptButton = this._buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(292, 106);
            this.Controls.Add(this._labelHexErrCode);
            this.Controls.Add(this._textBoxErrCode);
            this.Controls.Add(this._labelClearErrCode);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOk);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClearErrorForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clear the error";
            this.ResumeLayout(false);
            this.PerformLayout();

		}


		private System.Windows.Forms.Button _buttonCancel;
		private System.Windows.Forms.Button _buttonOk;
		private System.Windows.Forms.Label _labelClearErrCode;
		private System.Windows.Forms.TextBox _textBoxErrCode;
		private System.Windows.Forms.Label _labelHexErrCode;
	}
}
