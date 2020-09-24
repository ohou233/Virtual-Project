namespace LJX8_DllSampleAll.Forms
{
	partial class GetErrorForm
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
            this._textBoxErrCnt = new System.Windows.Forms.TextBox();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonOk = new System.Windows.Forms.Button();
            this._labelErrCnt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this._textBoxErrCnt.Location = new System.Drawing.Point(180, 13);
            this._textBoxErrCnt.Name = "_textBoxErrCnt";
            this._textBoxErrCnt.Size = new System.Drawing.Size(75, 21);
            this._textBoxErrCnt.TabIndex = 1;
            this._textBoxErrCnt.Text = "10";
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(180, 62);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 25);
            this._buttonCancel.TabIndex = 3;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(75, 62);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 25);
            this._buttonOk.TabIndex = 2;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            this._labelErrCnt.Location = new System.Drawing.Point(17, 10);
            this._labelErrCnt.Name = "_labelErrCnt";
            this._labelErrCnt.Size = new System.Drawing.Size(157, 38);
            this._labelErrCnt.TabIndex = 0;
            this._labelErrCnt.Text = "Amount of system \r\nerror information to receive";
            this.AcceptButton = this._buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(266, 102);
            this.Controls.Add(this._textBoxErrCnt);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOk);
            this.Controls.Add(this._labelErrCnt);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GetErrorForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Get the detailed error information.";
            this.ResumeLayout(false);
            this.PerformLayout();

		}


		private System.Windows.Forms.TextBox _textBoxErrCnt;
		private System.Windows.Forms.Button _buttonCancel;
		private System.Windows.Forms.Button _buttonOk;
		private System.Windows.Forms.Label _labelErrCnt;
	}
}
