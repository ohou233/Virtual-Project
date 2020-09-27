namespace MyWindow.Forms
{
    partial class OpenEthernetForm
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
            this._labelDescription = new System.Windows.Forms.Label();
            this._textBoxPort = new System.Windows.Forms.TextBox();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonOk = new System.Windows.Forms.Button();
            this._labelPort = new System.Windows.Forms.Label();
            this._textBoxIpFourthSegment = new System.Windows.Forms.TextBox();
            this._textBoxIpThirdSegment = new System.Windows.Forms.TextBox();
            this._textBoxIpSecondSegment = new System.Windows.Forms.TextBox();
            this._textBoxIpFirstSegment = new System.Windows.Forms.TextBox();
            this._labelIpAddress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _labelDescription
            // 
            this._labelDescription.AutoSize = true;
            this._labelDescription.Location = new System.Drawing.Point(87, 63);
            this._labelDescription.Name = "_labelDescription";
            this._labelDescription.Size = new System.Drawing.Size(263, 12);
            this._labelDescription.TabIndex = 10;
            this._labelDescription.Text = "[有效范围] IP地址是字节值，端口是ushort值。";
            // 
            // _textBoxPort
            // 
            this._textBoxPort.Location = new System.Drawing.Point(156, 123);
            this._textBoxPort.Name = "_textBoxPort";
            this._textBoxPort.Size = new System.Drawing.Size(194, 21);
            this._textBoxPort.TabIndex = 17;
            this._textBoxPort.Text = "24691";
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(236, 172);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 25);
            this._buttonCancel.TabIndex = 19;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            // 
            // _buttonOk
            // 
            this._buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(107, 172);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 25);
            this._buttonOk.TabIndex = 18;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            // 
            // _labelPort
            // 
            this._labelPort.AutoSize = true;
            this._labelPort.Location = new System.Drawing.Point(87, 127);
            this._labelPort.Name = "_labelPort";
            this._labelPort.Size = new System.Drawing.Size(29, 12);
            this._labelPort.TabIndex = 16;
            this._labelPort.Text = "Port";
            // 
            // _textBoxIpFourthSegment
            // 
            this._textBoxIpFourthSegment.Location = new System.Drawing.Point(306, 90);
            this._textBoxIpFourthSegment.Name = "_textBoxIpFourthSegment";
            this._textBoxIpFourthSegment.Size = new System.Drawing.Size(44, 21);
            this._textBoxIpFourthSegment.TabIndex = 15;
            this._textBoxIpFourthSegment.Text = "1";
            // 
            // _textBoxIpThirdSegment
            // 
            this._textBoxIpThirdSegment.Location = new System.Drawing.Point(256, 90);
            this._textBoxIpThirdSegment.Name = "_textBoxIpThirdSegment";
            this._textBoxIpThirdSegment.Size = new System.Drawing.Size(44, 21);
            this._textBoxIpThirdSegment.TabIndex = 14;
            this._textBoxIpThirdSegment.Text = "0";
            // 
            // _textBoxIpSecondSegment
            // 
            this._textBoxIpSecondSegment.Location = new System.Drawing.Point(206, 90);
            this._textBoxIpSecondSegment.Name = "_textBoxIpSecondSegment";
            this._textBoxIpSecondSegment.Size = new System.Drawing.Size(44, 21);
            this._textBoxIpSecondSegment.TabIndex = 13;
            this._textBoxIpSecondSegment.Text = "168";
            // 
            // _textBoxIpFirstSegment
            // 
            this._textBoxIpFirstSegment.Location = new System.Drawing.Point(156, 90);
            this._textBoxIpFirstSegment.Name = "_textBoxIpFirstSegment";
            this._textBoxIpFirstSegment.Size = new System.Drawing.Size(44, 21);
            this._textBoxIpFirstSegment.TabIndex = 12;
            this._textBoxIpFirstSegment.Text = "192";
            // 
            // _labelIpAddress
            // 
            this._labelIpAddress.AutoSize = true;
            this._labelIpAddress.Location = new System.Drawing.Point(87, 93);
            this._labelIpAddress.Name = "_labelIpAddress";
            this._labelIpAddress.Size = new System.Drawing.Size(65, 12);
            this._labelIpAddress.TabIndex = 11;
            this._labelIpAddress.Text = "IP address";
            // 
            // Open_Ethernet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 265);
            this.Controls.Add(this._labelDescription);
            this.Controls.Add(this._textBoxPort);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOk);
            this.Controls.Add(this._labelPort);
            this.Controls.Add(this._textBoxIpFourthSegment);
            this.Controls.Add(this._textBoxIpThirdSegment);
            this.Controls.Add(this._textBoxIpSecondSegment);
            this.Controls.Add(this._textBoxIpFirstSegment);
            this.Controls.Add(this._labelIpAddress);
            this.Name = "Open_Ethernet";
            this.Text = "Open_Ethernet";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _labelDescription;
        private System.Windows.Forms.TextBox _textBoxPort;
        public System.Windows.Forms.Button _buttonCancel;
        public System.Windows.Forms.Button _buttonOk;
        private System.Windows.Forms.Label _labelPort;
        private System.Windows.Forms.TextBox _textBoxIpFourthSegment;
        private System.Windows.Forms.TextBox _textBoxIpThirdSegment;
        private System.Windows.Forms.TextBox _textBoxIpSecondSegment;
        private System.Windows.Forms.TextBox _textBoxIpFirstSegment;
        private System.Windows.Forms.Label _labelIpAddress;
    }
}