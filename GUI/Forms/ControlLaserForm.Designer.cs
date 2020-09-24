namespace LJX8_DllSampleAll.Forms
{
    partial class ControlLaserForm
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
            this._labelHexErrCode = new System.Windows.Forms.Label();
            this._textBoxLaserStatus = new System.Windows.Forms.TextBox();
            this._labelLaserState = new System.Windows.Forms.Label();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonOk = new System.Windows.Forms.Button();
            this._labelLaserStateDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this._labelHexErrCode.AutoSize = true;
            this._labelHexErrCode.Location = new System.Drawing.Point(16, 34);
            this._labelHexErrCode.Name = "_labelHexErrCode";
            this._labelHexErrCode.Size = new System.Drawing.Size(19, 13);
            this._labelHexErrCode.TabIndex = 0;
            this._labelHexErrCode.Text = "0x";
            this._textBoxLaserStatus.Location = new System.Drawing.Point(37, 29);
            this._textBoxLaserStatus.MaxLength = 4;
            this._textBoxLaserStatus.Name = "_textBoxLaserStatus";
            this._textBoxLaserStatus.Size = new System.Drawing.Size(71, 21);
            this._textBoxLaserStatus.TabIndex = 1;
            this._textBoxLaserStatus.Text = "00";
            this._labelLaserState.AutoSize = true;
            this._labelLaserState.Location = new System.Drawing.Point(114, 34);
            this._labelLaserState.Name = "_labelLaserState";
            this._labelLaserState.Size = new System.Drawing.Size(64, 13);
            this._labelLaserState.TabIndex = 2;
            this._labelLaserState.Text = "Laser state ";
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(205, 100);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 25);
            this._buttonCancel.TabIndex = 5;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(117, 100);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 25);
            this._buttonOk.TabIndex = 4;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            this._labelLaserStateDescription.Location = new System.Drawing.Point(16, 59);
            this._labelLaserStateDescription.Name = "_labelLaserStateDescription";
            this._labelLaserStateDescription.Size = new System.Drawing.Size(220, 38);
            this._labelLaserStateDescription.TabIndex = 3;
            this._labelLaserStateDescription.Text = "0x00: Laser OFF\r\nother: Laser ON";
            this.AcceptButton = this._buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._buttonCancel;
            this.ClientSize = new System.Drawing.Size(292, 137);
            this.Controls.Add(this._labelLaserStateDescription);
            this.Controls.Add(this._labelHexErrCode);
            this.Controls.Add(this._textBoxLaserStatus);
            this.Controls.Add(this._labelLaserState);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOk);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ControlLaserForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Control laser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private System.Windows.Forms.Label _labelHexErrCode;
        private System.Windows.Forms.TextBox _textBoxLaserStatus;
        private System.Windows.Forms.Label _labelLaserState;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.Button _buttonOk;
        private System.Windows.Forms.Label _labelLaserStateDescription;
    }
}
