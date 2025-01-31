﻿namespace MyWindow.Forms
{
    partial class HighSpeedInitializeForm
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
            this._textBoxProfileCnt = new System.Windows.Forms.TextBox();
            this._labelProfileCnt = new System.Windows.Forms.Label();
            this._textBoxHighSpeedPortNo = new System.Windows.Forms.TextBox();
            this._labelHighSpeedPortNo = new System.Windows.Forms.Label();
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
            // _textBoxProfileCnt
            // 
            this._textBoxProfileCnt.Location = new System.Drawing.Point(157, 153);
            this._textBoxProfileCnt.Name = "_textBoxProfileCnt";
            this._textBoxProfileCnt.Size = new System.Drawing.Size(64, 21);
            this._textBoxProfileCnt.TabIndex = 25;
            this._textBoxProfileCnt.Text = "2000";
            // 
            // _labelProfileCnt
            // 
            this._labelProfileCnt.AutoSize = true;
            this._labelProfileCnt.Location = new System.Drawing.Point(35, 157);
            this._labelProfileCnt.Name = "_labelProfileCnt";
            this._labelProfileCnt.Size = new System.Drawing.Size(113, 12);
            this._labelProfileCnt.TabIndex = 24;
            this._labelProfileCnt.Text = "Number of profiles";
            // 
            // _textBoxHighSpeedPortNo
            // 
            this._textBoxHighSpeedPortNo.Location = new System.Drawing.Point(141, 121);
            this._textBoxHighSpeedPortNo.Name = "_textBoxHighSpeedPortNo";
            this._textBoxHighSpeedPortNo.Size = new System.Drawing.Size(157, 21);
            this._textBoxHighSpeedPortNo.TabIndex = 23;
            this._textBoxHighSpeedPortNo.Text = "24692";
            // 
            // _labelHighSpeedPortNo
            // 
            this._labelHighSpeedPortNo.AutoSize = true;
            this._labelHighSpeedPortNo.Location = new System.Drawing.Point(35, 125);
            this._labelHighSpeedPortNo.Name = "_labelHighSpeedPortNo";
            this._labelHighSpeedPortNo.Size = new System.Drawing.Size(107, 12);
            this._labelHighSpeedPortNo.TabIndex = 22;
            this._labelHighSpeedPortNo.Text = "Port (high speed)";
            // 
            // _labelDescription
            // 
            this._labelDescription.AutoSize = true;
            this._labelDescription.Location = new System.Drawing.Point(35, 31);
            this._labelDescription.Name = "_labelDescription";
            this._labelDescription.Size = new System.Drawing.Size(263, 12);
            this._labelDescription.TabIndex = 14;
            this._labelDescription.Text = "[有效范围] IP地址是字节值，端口是ushort值。";
            // 
            // _textBoxPort
            // 
            this._textBoxPort.Location = new System.Drawing.Point(104, 89);
            this._textBoxPort.Name = "_textBoxPort";
            this._textBoxPort.Size = new System.Drawing.Size(194, 21);
            this._textBoxPort.TabIndex = 21;
            this._textBoxPort.Text = "24691";
            // 
            // _buttonCancel
            // 
            this._buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(421, 150);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 25);
            this._buttonCancel.TabIndex = 27;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            // 
            // _buttonOk
            // 
            this._buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(313, 150);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 25);
            this._buttonOk.TabIndex = 26;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            // 
            // _labelPort
            // 
            this._labelPort.AutoSize = true;
            this._labelPort.Location = new System.Drawing.Point(35, 93);
            this._labelPort.Name = "_labelPort";
            this._labelPort.Size = new System.Drawing.Size(29, 12);
            this._labelPort.TabIndex = 20;
            this._labelPort.Text = "Port";
            // 
            // _textBoxIpFourthSegment
            // 
            this._textBoxIpFourthSegment.Location = new System.Drawing.Point(254, 57);
            this._textBoxIpFourthSegment.Name = "_textBoxIpFourthSegment";
            this._textBoxIpFourthSegment.Size = new System.Drawing.Size(44, 21);
            this._textBoxIpFourthSegment.TabIndex = 19;
            this._textBoxIpFourthSegment.Text = "1";
            // 
            // _textBoxIpThirdSegment
            // 
            this._textBoxIpThirdSegment.Location = new System.Drawing.Point(204, 57);
            this._textBoxIpThirdSegment.Name = "_textBoxIpThirdSegment";
            this._textBoxIpThirdSegment.Size = new System.Drawing.Size(44, 21);
            this._textBoxIpThirdSegment.TabIndex = 18;
            this._textBoxIpThirdSegment.Text = "0";
            // 
            // _textBoxIpSecondSegment
            // 
            this._textBoxIpSecondSegment.Location = new System.Drawing.Point(154, 57);
            this._textBoxIpSecondSegment.Name = "_textBoxIpSecondSegment";
            this._textBoxIpSecondSegment.Size = new System.Drawing.Size(44, 21);
            this._textBoxIpSecondSegment.TabIndex = 17;
            this._textBoxIpSecondSegment.Text = "168";
            // 
            // _textBoxIpFirstSegment
            // 
            this._textBoxIpFirstSegment.Location = new System.Drawing.Point(104, 57);
            this._textBoxIpFirstSegment.Name = "_textBoxIpFirstSegment";
            this._textBoxIpFirstSegment.Size = new System.Drawing.Size(44, 21);
            this._textBoxIpFirstSegment.TabIndex = 16;
            this._textBoxIpFirstSegment.Text = "192";
            // 
            // _labelIpAddress
            // 
            this._labelIpAddress.AutoSize = true;
            this._labelIpAddress.Location = new System.Drawing.Point(35, 61);
            this._labelIpAddress.Name = "_labelIpAddress";
            this._labelIpAddress.Size = new System.Drawing.Size(65, 12);
            this._labelIpAddress.TabIndex = 15;
            this._labelIpAddress.Text = "IP address";
            // 
            // HighSpeedInitializeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 238);
            this.Controls.Add(this._textBoxProfileCnt);
            this.Controls.Add(this._labelProfileCnt);
            this.Controls.Add(this._textBoxHighSpeedPortNo);
            this.Controls.Add(this._labelHighSpeedPortNo);
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
            this.Name = "HighSpeedInitializeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HighSpeedInitializeForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _textBoxProfileCnt;
        private System.Windows.Forms.Label _labelProfileCnt;
        private System.Windows.Forms.TextBox _textBoxHighSpeedPortNo;
        private System.Windows.Forms.Label _labelHighSpeedPortNo;
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