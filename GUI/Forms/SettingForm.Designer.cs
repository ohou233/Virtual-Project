namespace MyWindow.Forms
{
    partial class SettingForm
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
            this._textBoxDataLength = new System.Windows.Forms.TextBox();
            this._labelSettingsDescription = new System.Windows.Forms.Label();
            this._labelTypeDescription = new System.Windows.Forms.Label();
            this._labelDepthDescription = new System.Windows.Forms.Label();
            this._buttonCancel = new System.Windows.Forms.Button();
            this._buttonOk = new System.Windows.Forms.Button();
            this._textBoxParameter = new System.Windows.Forms.TextBox();
            this._labelParameter = new System.Windows.Forms.Label();
            this._labelDepth = new System.Windows.Forms.Label();
            this._textBoxDepth = new System.Windows.Forms.TextBox();
            this._labelHexDepth = new System.Windows.Forms.Label();
            this._labelByte = new System.Windows.Forms.Label();
            this._labelAmountOfData = new System.Windows.Forms.Label();
            this._labelTarget4 = new System.Windows.Forms.Label();
            this._textBoxTarget4 = new System.Windows.Forms.TextBox();
            this._labelHexTarget4 = new System.Windows.Forms.Label();
            this._labelTarget3 = new System.Windows.Forms.Label();
            this._textBoxTarget3 = new System.Windows.Forms.TextBox();
            this._labelHexTarget3 = new System.Windows.Forms.Label();
            this._labelTarget2 = new System.Windows.Forms.Label();
            this._textBoxTarget2 = new System.Windows.Forms.TextBox();
            this._labelHexTarget2 = new System.Windows.Forms.Label();
            this._labelTarget1 = new System.Windows.Forms.Label();
            this._textBoxTarget1 = new System.Windows.Forms.TextBox();
            this._labelHexTarget1 = new System.Windows.Forms.Label();
            this._labelItem = new System.Windows.Forms.Label();
            this._textBoxItem = new System.Windows.Forms.TextBox();
            this._labelHexItem = new System.Windows.Forms.Label();
            this._labelCategor = new System.Windows.Forms.Label();
            this._textBoxCategor = new System.Windows.Forms.TextBox();
            this._labelHexCategor = new System.Windows.Forms.Label();
            this._labelType = new System.Windows.Forms.Label();
            this._textBoxType = new System.Windows.Forms.TextBox();
            this._labelHexType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _textBoxDataLength
            // 
            this._textBoxDataLength.Location = new System.Drawing.Point(105, 367);
            this._textBoxDataLength.Name = "_textBoxDataLength";
            this._textBoxDataLength.Size = new System.Drawing.Size(45, 21);
            this._textBoxDataLength.TabIndex = 62;
            this._textBoxDataLength.Text = "1";
            // 
            // _labelSettingsDescription
            // 
            this._labelSettingsDescription.Location = new System.Drawing.Point(18, 14);
            this._labelSettingsDescription.Name = "_labelSettingsDescription";
            this._labelSettingsDescription.Size = new System.Drawing.Size(307, 30);
            this._labelSettingsDescription.TabIndex = 34;
            this._labelSettingsDescription.Text = "有关该类别后面的项目的详细信息，请参见通信命令规格中的表格。";
            this._labelSettingsDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _labelTypeDescription
            // 
            this._labelTypeDescription.Location = new System.Drawing.Point(14, 131);
            this._labelTypeDescription.Name = "_labelTypeDescription";
            this._labelTypeDescription.Size = new System.Drawing.Size(344, 38);
            this._labelTypeDescription.TabIndex = 42;
            this._labelTypeDescription.Text = "0x01：环境设置，0x02：通用测量设置，0x10：程序0，0x11：程序1，...，0x1F：程序15";
            // 
            // _labelDepthDescription
            // 
            this._labelDepthDescription.Location = new System.Drawing.Point(18, 79);
            this._labelDepthDescription.Name = "_labelDepthDescription";
            this._labelDepthDescription.Size = new System.Drawing.Size(322, 23);
            this._labelDepthDescription.TabIndex = 38;
            this._labelDepthDescription.Text = "0：写入设置区域； 1：运行设置区域； 2：保存区域";
            this._labelDepthDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _buttonCancel
            // 
            this._buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._buttonCancel.Location = new System.Drawing.Point(209, 488);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(75, 25);
            this._buttonCancel.TabIndex = 67;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = true;
            // 
            // _buttonOk
            // 
            this._buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._buttonOk.Location = new System.Drawing.Point(65, 488);
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Size = new System.Drawing.Size(75, 25);
            this._buttonOk.TabIndex = 66;
            this._buttonOk.Text = "OK";
            this._buttonOk.UseVisualStyleBackColor = true;
            // 
            // _textBoxParameter
            // 
            this._textBoxParameter.Location = new System.Drawing.Point(14, 409);
            this._textBoxParameter.Multiline = true;
            this._textBoxParameter.Name = "_textBoxParameter";
            this._textBoxParameter.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textBoxParameter.Size = new System.Drawing.Size(324, 61);
            this._textBoxParameter.TabIndex = 65;
            this._textBoxParameter.Text = "3";
            // 
            // _labelParameter
            // 
            this._labelParameter.AutoSize = true;
            this._labelParameter.Location = new System.Drawing.Point(12, 393);
            this._labelParameter.Name = "_labelParameter";
            this._labelParameter.Size = new System.Drawing.Size(209, 12);
            this._labelParameter.TabIndex = 64;
            this._labelParameter.Text = "写入参数（以逗号分隔的十六进制值）";
            // 
            // _labelDepth
            // 
            this._labelDepth.AutoSize = true;
            this._labelDepth.Location = new System.Drawing.Point(117, 60);
            this._labelDepth.Name = "_labelDepth";
            this._labelDepth.Size = new System.Drawing.Size(137, 12);
            this._labelDepth.TabIndex = 37;
            this._labelDepth.Text = "获取目标区域和设置深度";
            // 
            // _textBoxDepth
            // 
            this._textBoxDepth.Location = new System.Drawing.Point(35, 57);
            this._textBoxDepth.MaxLength = 2;
            this._textBoxDepth.Name = "_textBoxDepth";
            this._textBoxDepth.Size = new System.Drawing.Size(76, 21);
            this._textBoxDepth.TabIndex = 36;
            this._textBoxDepth.Text = "01";
            // 
            // _labelHexDepth
            // 
            this._labelHexDepth.AutoSize = true;
            this._labelHexDepth.Location = new System.Drawing.Point(12, 60);
            this._labelHexDepth.Name = "_labelHexDepth";
            this._labelHexDepth.Size = new System.Drawing.Size(17, 12);
            this._labelHexDepth.TabIndex = 35;
            this._labelHexDepth.Text = "0x";
            // 
            // _labelByte
            // 
            this._labelByte.AutoSize = true;
            this._labelByte.Location = new System.Drawing.Point(161, 370);
            this._labelByte.Name = "_labelByte";
            this._labelByte.Size = new System.Drawing.Size(29, 12);
            this._labelByte.TabIndex = 63;
            this._labelByte.Text = "BYTE";
            // 
            // _labelAmountOfData
            // 
            this._labelAmountOfData.AutoSize = true;
            this._labelAmountOfData.Location = new System.Drawing.Point(12, 370);
            this._labelAmountOfData.Name = "_labelAmountOfData";
            this._labelAmountOfData.Size = new System.Drawing.Size(41, 12);
            this._labelAmountOfData.TabIndex = 61;
            this._labelAmountOfData.Text = "数据量";
            // 
            // _labelTarget4
            // 
            this._labelTarget4.AutoSize = true;
            this._labelTarget4.Location = new System.Drawing.Point(117, 339);
            this._labelTarget4.Name = "_labelTarget4";
            this._labelTarget4.Size = new System.Drawing.Size(59, 12);
            this._labelTarget4.TabIndex = 60;
            this._labelTarget4.Text = "设定目标4";
            // 
            // _textBoxTarget4
            // 
            this._textBoxTarget4.Location = new System.Drawing.Point(35, 336);
            this._textBoxTarget4.MaxLength = 2;
            this._textBoxTarget4.Name = "_textBoxTarget4";
            this._textBoxTarget4.Size = new System.Drawing.Size(76, 21);
            this._textBoxTarget4.TabIndex = 59;
            this._textBoxTarget4.Text = "00";
            // 
            // _labelHexTarget4
            // 
            this._labelHexTarget4.AutoSize = true;
            this._labelHexTarget4.Location = new System.Drawing.Point(12, 339);
            this._labelHexTarget4.Name = "_labelHexTarget4";
            this._labelHexTarget4.Size = new System.Drawing.Size(17, 12);
            this._labelHexTarget4.TabIndex = 58;
            this._labelHexTarget4.Text = "0x";
            // 
            // _labelTarget3
            // 
            this._labelTarget3.AutoSize = true;
            this._labelTarget3.Location = new System.Drawing.Point(117, 307);
            this._labelTarget3.Name = "_labelTarget3";
            this._labelTarget3.Size = new System.Drawing.Size(59, 12);
            this._labelTarget3.TabIndex = 57;
            this._labelTarget3.Text = "设定目标3";
            // 
            // _textBoxTarget3
            // 
            this._textBoxTarget3.Location = new System.Drawing.Point(35, 304);
            this._textBoxTarget3.MaxLength = 2;
            this._textBoxTarget3.Name = "_textBoxTarget3";
            this._textBoxTarget3.Size = new System.Drawing.Size(76, 21);
            this._textBoxTarget3.TabIndex = 56;
            this._textBoxTarget3.Text = "00";
            // 
            // _labelHexTarget3
            // 
            this._labelHexTarget3.AutoSize = true;
            this._labelHexTarget3.Location = new System.Drawing.Point(12, 307);
            this._labelHexTarget3.Name = "_labelHexTarget3";
            this._labelHexTarget3.Size = new System.Drawing.Size(17, 12);
            this._labelHexTarget3.TabIndex = 55;
            this._labelHexTarget3.Text = "0x";
            // 
            // _labelTarget2
            // 
            this._labelTarget2.AutoSize = true;
            this._labelTarget2.Location = new System.Drawing.Point(117, 276);
            this._labelTarget2.Name = "_labelTarget2";
            this._labelTarget2.Size = new System.Drawing.Size(59, 12);
            this._labelTarget2.TabIndex = 54;
            this._labelTarget2.Text = "设定目标2";
            // 
            // _textBoxTarget2
            // 
            this._textBoxTarget2.Location = new System.Drawing.Point(35, 273);
            this._textBoxTarget2.MaxLength = 2;
            this._textBoxTarget2.Name = "_textBoxTarget2";
            this._textBoxTarget2.Size = new System.Drawing.Size(76, 21);
            this._textBoxTarget2.TabIndex = 53;
            this._textBoxTarget2.Text = "00";
            // 
            // _labelHexTarget2
            // 
            this._labelHexTarget2.AutoSize = true;
            this._labelHexTarget2.Location = new System.Drawing.Point(12, 276);
            this._labelHexTarget2.Name = "_labelHexTarget2";
            this._labelHexTarget2.Size = new System.Drawing.Size(17, 12);
            this._labelHexTarget2.TabIndex = 52;
            this._labelHexTarget2.Text = "0x";
            // 
            // _labelTarget1
            // 
            this._labelTarget1.AutoSize = true;
            this._labelTarget1.Location = new System.Drawing.Point(117, 245);
            this._labelTarget1.Name = "_labelTarget1";
            this._labelTarget1.Size = new System.Drawing.Size(59, 12);
            this._labelTarget1.TabIndex = 51;
            this._labelTarget1.Text = "设定目标1";
            // 
            // _textBoxTarget1
            // 
            this._textBoxTarget1.Location = new System.Drawing.Point(35, 241);
            this._textBoxTarget1.MaxLength = 2;
            this._textBoxTarget1.Name = "_textBoxTarget1";
            this._textBoxTarget1.Size = new System.Drawing.Size(76, 21);
            this._textBoxTarget1.TabIndex = 50;
            this._textBoxTarget1.Text = "00";
            // 
            // _labelHexTarget1
            // 
            this._labelHexTarget1.AutoSize = true;
            this._labelHexTarget1.Location = new System.Drawing.Point(12, 245);
            this._labelHexTarget1.Name = "_labelHexTarget1";
            this._labelHexTarget1.Size = new System.Drawing.Size(17, 12);
            this._labelHexTarget1.TabIndex = 49;
            this._labelHexTarget1.Text = "0x";
            // 
            // _labelItem
            // 
            this._labelItem.AutoSize = true;
            this._labelItem.Location = new System.Drawing.Point(117, 213);
            this._labelItem.Name = "_labelItem";
            this._labelItem.Size = new System.Drawing.Size(53, 12);
            this._labelItem.TabIndex = 48;
            this._labelItem.Text = "设定项目";
            // 
            // _textBoxItem
            // 
            this._textBoxItem.Location = new System.Drawing.Point(35, 210);
            this._textBoxItem.MaxLength = 2;
            this._textBoxItem.Name = "_textBoxItem";
            this._textBoxItem.Size = new System.Drawing.Size(76, 21);
            this._textBoxItem.TabIndex = 47;
            this._textBoxItem.Text = "02";
            // 
            // _labelHexItem
            // 
            this._labelHexItem.AutoSize = true;
            this._labelHexItem.Location = new System.Drawing.Point(12, 213);
            this._labelHexItem.Name = "_labelHexItem";
            this._labelHexItem.Size = new System.Drawing.Size(17, 12);
            this._labelHexItem.TabIndex = 46;
            this._labelHexItem.Text = "0x";
            // 
            // _labelCategor
            // 
            this._labelCategor.AutoSize = true;
            this._labelCategor.Location = new System.Drawing.Point(117, 182);
            this._labelCategor.Name = "_labelCategor";
            this._labelCategor.Size = new System.Drawing.Size(29, 12);
            this._labelCategor.TabIndex = 45;
            this._labelCategor.Text = "类别";
            // 
            // _textBoxCategor
            // 
            this._textBoxCategor.Location = new System.Drawing.Point(35, 178);
            this._textBoxCategor.MaxLength = 2;
            this._textBoxCategor.Name = "_textBoxCategor";
            this._textBoxCategor.Size = new System.Drawing.Size(76, 21);
            this._textBoxCategor.TabIndex = 44;
            this._textBoxCategor.Text = "00";
            // 
            // _labelHexCategor
            // 
            this._labelHexCategor.AutoSize = true;
            this._labelHexCategor.Location = new System.Drawing.Point(12, 182);
            this._labelHexCategor.Name = "_labelHexCategor";
            this._labelHexCategor.Size = new System.Drawing.Size(17, 12);
            this._labelHexCategor.TabIndex = 43;
            this._labelHexCategor.Text = "0x";
            // 
            // _labelType
            // 
            this._labelType.AutoSize = true;
            this._labelType.Location = new System.Drawing.Point(117, 110);
            this._labelType.Name = "_labelType";
            this._labelType.Size = new System.Drawing.Size(53, 12);
            this._labelType.TabIndex = 41;
            this._labelType.Text = "设定类型";
            // 
            // _textBoxType
            // 
            this._textBoxType.Location = new System.Drawing.Point(35, 107);
            this._textBoxType.MaxLength = 2;
            this._textBoxType.Name = "_textBoxType";
            this._textBoxType.Size = new System.Drawing.Size(76, 21);
            this._textBoxType.TabIndex = 40;
            this._textBoxType.Text = "10";
            // 
            // _labelHexType
            // 
            this._labelHexType.AutoSize = true;
            this._labelHexType.Location = new System.Drawing.Point(12, 110);
            this._labelHexType.Name = "_labelHexType";
            this._labelHexType.Size = new System.Drawing.Size(17, 12);
            this._labelHexType.TabIndex = 39;
            this._labelHexType.Text = "0x";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 527);
            this.Controls.Add(this._textBoxDataLength);
            this.Controls.Add(this._labelSettingsDescription);
            this.Controls.Add(this._labelTypeDescription);
            this.Controls.Add(this._labelDepthDescription);
            this.Controls.Add(this._buttonCancel);
            this.Controls.Add(this._buttonOk);
            this.Controls.Add(this._textBoxParameter);
            this.Controls.Add(this._labelParameter);
            this.Controls.Add(this._labelDepth);
            this.Controls.Add(this._textBoxDepth);
            this.Controls.Add(this._labelHexDepth);
            this.Controls.Add(this._labelByte);
            this.Controls.Add(this._labelAmountOfData);
            this.Controls.Add(this._labelTarget4);
            this.Controls.Add(this._textBoxTarget4);
            this.Controls.Add(this._labelHexTarget4);
            this.Controls.Add(this._labelTarget3);
            this.Controls.Add(this._textBoxTarget3);
            this.Controls.Add(this._labelHexTarget3);
            this.Controls.Add(this._labelTarget2);
            this.Controls.Add(this._textBoxTarget2);
            this.Controls.Add(this._labelHexTarget2);
            this.Controls.Add(this._labelTarget1);
            this.Controls.Add(this._textBoxTarget1);
            this.Controls.Add(this._labelHexTarget1);
            this.Controls.Add(this._labelItem);
            this.Controls.Add(this._textBoxItem);
            this.Controls.Add(this._labelHexItem);
            this.Controls.Add(this._labelCategor);
            this.Controls.Add(this._textBoxCategor);
            this.Controls.Add(this._labelHexCategor);
            this.Controls.Add(this._labelType);
            this.Controls.Add(this._textBoxType);
            this.Controls.Add(this._labelHexType);
            this.Name = "SettingForm";
            this.Text = "SettingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _textBoxDataLength;
        private System.Windows.Forms.Label _labelSettingsDescription;
        private System.Windows.Forms.Label _labelTypeDescription;
        private System.Windows.Forms.Label _labelDepthDescription;
        private System.Windows.Forms.Button _buttonCancel;
        private System.Windows.Forms.Button _buttonOk;
        private System.Windows.Forms.TextBox _textBoxParameter;
        private System.Windows.Forms.Label _labelParameter;
        private System.Windows.Forms.Label _labelDepth;
        private System.Windows.Forms.TextBox _textBoxDepth;
        private System.Windows.Forms.Label _labelHexDepth;
        private System.Windows.Forms.Label _labelByte;
        private System.Windows.Forms.Label _labelAmountOfData;
        private System.Windows.Forms.Label _labelTarget4;
        private System.Windows.Forms.TextBox _textBoxTarget4;
        private System.Windows.Forms.Label _labelHexTarget4;
        private System.Windows.Forms.Label _labelTarget3;
        private System.Windows.Forms.TextBox _textBoxTarget3;
        private System.Windows.Forms.Label _labelHexTarget3;
        private System.Windows.Forms.Label _labelTarget2;
        private System.Windows.Forms.TextBox _textBoxTarget2;
        private System.Windows.Forms.Label _labelHexTarget2;
        private System.Windows.Forms.Label _labelTarget1;
        private System.Windows.Forms.TextBox _textBoxTarget1;
        private System.Windows.Forms.Label _labelHexTarget1;
        private System.Windows.Forms.Label _labelItem;
        private System.Windows.Forms.TextBox _textBoxItem;
        private System.Windows.Forms.Label _labelHexItem;
        private System.Windows.Forms.Label _labelCategor;
        private System.Windows.Forms.TextBox _textBoxCategor;
        private System.Windows.Forms.Label _labelHexCategor;
        private System.Windows.Forms.Label _labelType;
        private System.Windows.Forms.TextBox _textBoxType;
        private System.Windows.Forms.Label _labelHexType;
    }
}