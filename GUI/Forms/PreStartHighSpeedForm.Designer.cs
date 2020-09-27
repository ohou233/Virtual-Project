namespace MyWindow.Forms
{
    partial class PreStartHighSpeedForm
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
            this._textBoxSendPos = new System.Windows.Forms.TextBox();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonOk = new System.Windows.Forms.Button();
            this._labelSendPos = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _textBoxSendPos
            // 
            this._textBoxSendPos.Location = new System.Drawing.Point(161, 19);
            this._textBoxSendPos.Name = "_textBoxSendPos";
            this._textBoxSendPos.Size = new System.Drawing.Size(107, 21);
            this._textBoxSendPos.TabIndex = 5;
            this._textBoxSendPos.Text = "2";
            // 
            // _buttonCancel
            // 
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(173, 69);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 25);
            this._buttonCancel.TabIndex = 7;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            // 
            // _buttonOk
            // 
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(62, 69);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 25);
            this._buttonOk.TabIndex = 6;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            // 
            // _labelSendPos
            // 
            this._labelSendPos.AutoSize = true;
            this._labelSendPos.Location = new System.Drawing.Point(60, 22);
            this._labelSendPos.Name = "_labelSendPos";
            this._labelSendPos.Size = new System.Drawing.Size(77, 12);
            this._labelSendPos.TabIndex = 4;
            this._labelSendPos.Text = "发送开始位置";
            // 
            // PreStartHighSpeedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 115);
            this.Controls.Add(this._textBoxSendPos);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOk);
            this.Controls.Add(this._labelSendPos);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreStartHighSpeedForm";
            this.Text = "PreStartHighSpeedForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _textBoxSendPos;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.Button _buttonOk;
        private System.Windows.Forms.Label _labelSendPos;
    }
}