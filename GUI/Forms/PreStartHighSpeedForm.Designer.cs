namespace LJX8_DllSampleAll.Forms
{
	partial class PreStartHighSpeedForm
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
            this._textBoxSendPos = new System.Windows.Forms.TextBox();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonOk = new System.Windows.Forms.Button();
            this._labelSendPos = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this._textBoxSendPos.Location = new System.Drawing.Point(129, 20);
            this._textBoxSendPos.Name = "_textBoxSendPos";
            this._textBoxSendPos.Size = new System.Drawing.Size(107, 21);
            this._textBoxSendPos.TabIndex = 1;
            this._textBoxSendPos.Text = "2";
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(177, 54);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 25);
            this._buttonCancel.TabIndex = 3;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(80, 54);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 25);
            this._buttonOk.TabIndex = 2;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            this._labelSendPos.AutoSize = true;
            this._labelSendPos.Location = new System.Drawing.Point(21, 23);
            this._labelSendPos.Name = "_labelSendPos";
            this._labelSendPos.Size = new System.Drawing.Size(97, 13);
            this._labelSendPos.TabIndex = 0;
            this._labelSendPos.Text = "Send start position";
            this.AcceptButton = this._buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(282, 90);
            this.Controls.Add(this._textBoxSendPos);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOk);
            this.Controls.Add(this._labelSendPos);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreStartHighSpeedForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "High-speed communication start request";
            this.ResumeLayout(false);
            this.PerformLayout();

		}


		private System.Windows.Forms.TextBox _textBoxSendPos;
		private System.Windows.Forms.Button _buttonCancel;
		private System.Windows.Forms.Button _buttonOk;
		private System.Windows.Forms.Label _labelSendPos;
	}
}
