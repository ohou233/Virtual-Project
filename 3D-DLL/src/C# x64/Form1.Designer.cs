namespace LjxaDllSampleProgramCS
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ljxaWindows3D1 = new LjxaDisp.LjxaWindows3D();
            this._button_close_device = new System.Windows.Forms.Button();
            this._button_acquire_stop = new System.Windows.Forms.Button();
            this._button_acquire_start = new System.Windows.Forms.Button();
            this._button_openDevice = new System.Windows.Forms.Button();
            this.communicationSetting1 = new LjxaDisp.CommunicationSetting();
            this.timerwaitimage = new System.Windows.Forms.Timer(this.components);
            this._logBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ljxaWindows3D1
            // 
            this.ljxaWindows3D1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ljxaWindows3D1.BinningSize = 4;
            this.ljxaWindows3D1.ColorHighValue = 65535;
            this.ljxaWindows3D1.ColorLowValue = 0;
            this.ljxaWindows3D1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ljxaWindows3D1.GrayMixPercent = 0.5D;
            this.ljxaWindows3D1.Location = new System.Drawing.Point(6, 6);
            this.ljxaWindows3D1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ljxaWindows3D1.Name = "ljxaWindows3D1";
            this.ljxaWindows3D1.Resolution_X = 0.0125D;
            this.ljxaWindows3D1.Resolution_Z = 0.0016D;
            this.ljxaWindows3D1.Size = new System.Drawing.Size(759, 313);
            this.ljxaWindows3D1.TabIndex = 0;
            this.ljxaWindows3D1.ZoomScaleX = 1D;
            this.ljxaWindows3D1.ZoomScaleY = 1D;
            this.ljxaWindows3D1.ZoomScaleZ = 1D;
            // 
            // _button_close_device
            // 
            this._button_close_device.Location = new System.Drawing.Point(12, 62);
            this._button_close_device.Name = "_button_close_device";
            this._button_close_device.Size = new System.Drawing.Size(94, 39);
            this._button_close_device.TabIndex = 7;
            this._button_close_device.Text = "关闭设备 Close_Device";
            this._button_close_device.UseVisualStyleBackColor = true;
            this._button_close_device.Click += new System.EventHandler(this._button_close_device_Click);
            // 
            // _button_acquire_stop
            // 
            this._button_acquire_stop.Location = new System.Drawing.Point(129, 60);
            this._button_acquire_stop.Name = "_button_acquire_stop";
            this._button_acquire_stop.Size = new System.Drawing.Size(94, 42);
            this._button_acquire_stop.TabIndex = 6;
            this._button_acquire_stop.Text = "停止获取 Acquire_Stop";
            this._button_acquire_stop.UseVisualStyleBackColor = true;
            this._button_acquire_stop.Click += new System.EventHandler(this._button_acquire_stop_Click);
            // 
            // _button_acquire_start
            // 
            this._button_acquire_start.Location = new System.Drawing.Point(129, 12);
            this._button_acquire_start.Name = "_button_acquire_start";
            this._button_acquire_start.Size = new System.Drawing.Size(94, 42);
            this._button_acquire_start.TabIndex = 5;
            this._button_acquire_start.Text = "开始获取 Acquire_Start";
            this._button_acquire_start.UseVisualStyleBackColor = true;
            this._button_acquire_start.Click += new System.EventHandler(this._button_acquire_start_Click);
            // 
            // _button_openDevice
            // 
            this._button_openDevice.Location = new System.Drawing.Point(12, 11);
            this._button_openDevice.Name = "_button_openDevice";
            this._button_openDevice.Size = new System.Drawing.Size(94, 42);
            this._button_openDevice.TabIndex = 4;
            this._button_openDevice.Text = "打开设备 Open_Device";
            this._button_openDevice.UseVisualStyleBackColor = true;
            this._button_openDevice.Click += new System.EventHandler(this._button_openDevice_Click);
            // 
            // communicationSetting1
            // 
            this.communicationSetting1.Location = new System.Drawing.Point(242, 16);
            this.communicationSetting1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.communicationSetting1.Name = "communicationSetting1";
            this.communicationSetting1.Size = new System.Drawing.Size(576, 75);
            this.communicationSetting1.TabIndex = 8;
            // 
            // timerwaitimage
            // 
            this.timerwaitimage.Tick += new System.EventHandler(this.timerwaitimage_Tick);
            // 
            // _logBox
            // 
            this._logBox.Location = new System.Drawing.Point(12, 564);
            this._logBox.Multiline = true;
            this._logBox.Name = "_logBox";
            this._logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._logBox.Size = new System.Drawing.Size(555, 86);
            this._logBox.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.ljxaWindows3D1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(47, 118);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(771, 325);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 739);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this._logBox);
            this.Controls.Add(this.communicationSetting1);
            this.Controls.Add(this._button_close_device);
            this.Controls.Add(this._button_acquire_stop);
            this.Controls.Add(this._button_acquire_start);
            this.Controls.Add(this._button_openDevice);
            this.Name = "Form1";
            this.Text = "LJXA Simple Dll Sample Program";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LjxaDisp.LjxaWindows3D ljxaWindows3D1;
        private System.Windows.Forms.Button _button_close_device;
        private System.Windows.Forms.Button _button_acquire_stop;
        private System.Windows.Forms.Button _button_acquire_start;
        private System.Windows.Forms.Button _button_openDevice;
        private LjxaDisp.CommunicationSetting communicationSetting1;
        private System.Windows.Forms.Timer timerwaitimage;
        private System.Windows.Forms.TextBox _logBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

