﻿namespace MyWindow
{
    partial class MyWindow
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
            this.pb_in = new System.Windows.Forms.PictureBox();
            this.pb_out = new System.Windows.Forms.PictureBox();
            this.tp_SetOption = new System.Windows.Forms.TabControl();
            this.tp_2Dcamera_option = new System.Windows.Forms.TabPage();
            this.gb_camera_parameter = new System.Windows.Forms.GroupBox();
            this.bt_SetParam = new System.Windows.Forms.Button();
            this.bt_GetParam = new System.Windows.Forms.Button();
            this.lb_FrameRate = new System.Windows.Forms.Label();
            this.lb_Gain = new System.Windows.Forms.Label();
            this.lb_Exposure = new System.Windows.Forms.Label();
            this.tb_FrameRate = new System.Windows.Forms.TextBox();
            this.tb_Gain = new System.Windows.Forms.TextBox();
            this.tb_Exposure = new System.Windows.Forms.TextBox();
            this.gb_camera_init = new System.Windows.Forms.GroupBox();
            this.gb_image_grab = new System.Windows.Forms.GroupBox();
            this.bt_StopGrab = new System.Windows.Forms.Button();
            this.bt_StartGrab = new System.Windows.Forms.Button();
            this.cb_DeviceList = new System.Windows.Forms.ComboBox();
            this.bt_DiscoverCamera = new System.Windows.Forms.Button();
            this.bt_CloseCamera = new System.Windows.Forms.Button();
            this.bt_OpenCamera = new System.Windows.Forms.Button();
            this.tp_process_option = new System.Windows.Forms.TabPage();
            this.gb_Test = new System.Windows.Forms.GroupBox();
            this.bt_StartTest = new System.Windows.Forms.Button();
            this.bt_StopTest = new System.Windows.Forms.Button();
            this.gb_measurement = new System.Windows.Forms.GroupBox();
            this.rbt_Measure9 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bt_ClearData = new System.Windows.Forms.Button();
            this.bt_SaveCSV = new System.Windows.Forms.Button();
            this.bt_SaveBmp = new System.Windows.Forms.Button();
            this.lv_AllFrameData = new System.Windows.Forms.ListView();
            this.tp_3Dcamera_option = new System.Windows.Forms.TabPage();
            this.rbt_Measure18 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_StopGrab_3D = new System.Windows.Forms.Button();
            this.bt_StartGrab_3D = new System.Windows.Forms.Button();
            this.bt_InitCamera3D = new System.Windows.Forms.Button();
            this.bt_CloseCamera_3D = new System.Windows.Forms.Button();
            this.bt_OpenCamera_3D = new System.Windows.Forms.Button();
            this.bt_ClearBufferData = new System.Windows.Forms.Button();
            this.rbt_outLineMode = new System.Windows.Forms.RadioButton();
            this.rbt_inLineMode = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pb_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_out)).BeginInit();
            this.tp_SetOption.SuspendLayout();
            this.tp_2Dcamera_option.SuspendLayout();
            this.gb_camera_parameter.SuspendLayout();
            this.gb_camera_init.SuspendLayout();
            this.gb_image_grab.SuspendLayout();
            this.tp_process_option.SuspendLayout();
            this.gb_Test.SuspendLayout();
            this.gb_measurement.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tp_3Dcamera_option.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pb_in
            // 
            this.pb_in.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pb_in.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pb_in.Location = new System.Drawing.Point(11, 4);
            this.pb_in.Margin = new System.Windows.Forms.Padding(2);
            this.pb_in.Name = "pb_in";
            this.pb_in.Size = new System.Drawing.Size(582, 437);
            this.pb_in.TabIndex = 0;
            this.pb_in.TabStop = false;
            // 
            // pb_out
            // 
            this.pb_out.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pb_out.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pb_out.Location = new System.Drawing.Point(596, 4);
            this.pb_out.Margin = new System.Windows.Forms.Padding(2);
            this.pb_out.Name = "pb_out";
            this.pb_out.Size = new System.Drawing.Size(582, 437);
            this.pb_out.TabIndex = 1;
            this.pb_out.TabStop = false;
            // 
            // tp_SetOption
            // 
            this.tp_SetOption.Controls.Add(this.tp_process_option);
            this.tp_SetOption.Controls.Add(this.tp_2Dcamera_option);
            this.tp_SetOption.Controls.Add(this.tp_3Dcamera_option);
            this.tp_SetOption.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tp_SetOption.Location = new System.Drawing.Point(1, 442);
            this.tp_SetOption.Margin = new System.Windows.Forms.Padding(2);
            this.tp_SetOption.Name = "tp_SetOption";
            this.tp_SetOption.SelectedIndex = 0;
            this.tp_SetOption.Size = new System.Drawing.Size(509, 325);
            this.tp_SetOption.TabIndex = 2;
            // 
            // tp_2Dcamera_option
            // 
            this.tp_2Dcamera_option.Controls.Add(this.gb_camera_parameter);
            this.tp_2Dcamera_option.Controls.Add(this.gb_camera_init);
            this.tp_2Dcamera_option.Location = new System.Drawing.Point(4, 22);
            this.tp_2Dcamera_option.Margin = new System.Windows.Forms.Padding(2);
            this.tp_2Dcamera_option.Name = "tp_2Dcamera_option";
            this.tp_2Dcamera_option.Padding = new System.Windows.Forms.Padding(2);
            this.tp_2Dcamera_option.Size = new System.Drawing.Size(501, 299);
            this.tp_2Dcamera_option.TabIndex = 0;
            this.tp_2Dcamera_option.Text = " 2D相机设置";
            this.tp_2Dcamera_option.UseVisualStyleBackColor = true;
            // 
            // gb_camera_parameter
            // 
            this.gb_camera_parameter.Controls.Add(this.bt_SetParam);
            this.gb_camera_parameter.Controls.Add(this.bt_GetParam);
            this.gb_camera_parameter.Controls.Add(this.lb_FrameRate);
            this.gb_camera_parameter.Controls.Add(this.lb_Gain);
            this.gb_camera_parameter.Controls.Add(this.lb_Exposure);
            this.gb_camera_parameter.Controls.Add(this.tb_FrameRate);
            this.gb_camera_parameter.Controls.Add(this.tb_Gain);
            this.gb_camera_parameter.Controls.Add(this.tb_Exposure);
            this.gb_camera_parameter.Font = new System.Drawing.Font("宋体", 12F);
            this.gb_camera_parameter.Location = new System.Drawing.Point(245, 14);
            this.gb_camera_parameter.Name = "gb_camera_parameter";
            this.gb_camera_parameter.Size = new System.Drawing.Size(255, 277);
            this.gb_camera_parameter.TabIndex = 6;
            this.gb_camera_parameter.TabStop = false;
            this.gb_camera_parameter.Text = "设备参数";
            // 
            // bt_SetParam
            // 
            this.bt_SetParam.Enabled = false;
            this.bt_SetParam.Font = new System.Drawing.Font("宋体", 14F);
            this.bt_SetParam.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_SetParam.Location = new System.Drawing.Point(131, 181);
            this.bt_SetParam.Name = "bt_SetParam";
            this.bt_SetParam.Size = new System.Drawing.Size(112, 32);
            this.bt_SetParam.TabIndex = 7;
            this.bt_SetParam.Text = "设置参数";
            this.bt_SetParam.UseVisualStyleBackColor = true;
            this.bt_SetParam.Click += new System.EventHandler(this.bt_SetParam_Click);
            // 
            // bt_GetParam
            // 
            this.bt_GetParam.Enabled = false;
            this.bt_GetParam.Font = new System.Drawing.Font("宋体", 14F);
            this.bt_GetParam.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_GetParam.Location = new System.Drawing.Point(13, 181);
            this.bt_GetParam.Name = "bt_GetParam";
            this.bt_GetParam.Size = new System.Drawing.Size(112, 32);
            this.bt_GetParam.TabIndex = 6;
            this.bt_GetParam.Text = "获取参数";
            this.bt_GetParam.UseVisualStyleBackColor = true;
            this.bt_GetParam.Click += new System.EventHandler(this.bt_GetParam_Click);
            // 
            // lb_FrameRate
            // 
            this.lb_FrameRate.AutoSize = true;
            this.lb_FrameRate.Font = new System.Drawing.Font("宋体", 14F);
            this.lb_FrameRate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb_FrameRate.Location = new System.Drawing.Point(26, 140);
            this.lb_FrameRate.Name = "lb_FrameRate";
            this.lb_FrameRate.Size = new System.Drawing.Size(47, 19);
            this.lb_FrameRate.TabIndex = 5;
            this.lb_FrameRate.Text = "帧率";
            // 
            // lb_Gain
            // 
            this.lb_Gain.AutoSize = true;
            this.lb_Gain.Font = new System.Drawing.Font("宋体", 14F);
            this.lb_Gain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb_Gain.Location = new System.Drawing.Point(26, 101);
            this.lb_Gain.Name = "lb_Gain";
            this.lb_Gain.Size = new System.Drawing.Size(47, 19);
            this.lb_Gain.TabIndex = 4;
            this.lb_Gain.Text = "增益";
            // 
            // lb_Exposure
            // 
            this.lb_Exposure.AutoSize = true;
            this.lb_Exposure.Font = new System.Drawing.Font("宋体", 14F);
            this.lb_Exposure.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb_Exposure.Location = new System.Drawing.Point(26, 66);
            this.lb_Exposure.Name = "lb_Exposure";
            this.lb_Exposure.Size = new System.Drawing.Size(47, 19);
            this.lb_Exposure.TabIndex = 3;
            this.lb_Exposure.Text = "曝光";
            // 
            // tb_FrameRate
            // 
            this.tb_FrameRate.Enabled = false;
            this.tb_FrameRate.Location = new System.Drawing.Point(118, 134);
            this.tb_FrameRate.Name = "tb_FrameRate";
            this.tb_FrameRate.Size = new System.Drawing.Size(95, 26);
            this.tb_FrameRate.TabIndex = 2;
            // 
            // tb_Gain
            // 
            this.tb_Gain.Enabled = false;
            this.tb_Gain.Location = new System.Drawing.Point(118, 101);
            this.tb_Gain.Name = "tb_Gain";
            this.tb_Gain.Size = new System.Drawing.Size(95, 26);
            this.tb_Gain.TabIndex = 1;
            // 
            // tb_Exposure
            // 
            this.tb_Exposure.Enabled = false;
            this.tb_Exposure.Location = new System.Drawing.Point(118, 66);
            this.tb_Exposure.Name = "tb_Exposure";
            this.tb_Exposure.Size = new System.Drawing.Size(95, 26);
            this.tb_Exposure.TabIndex = 0;
            // 
            // gb_camera_init
            // 
            this.gb_camera_init.Controls.Add(this.gb_image_grab);
            this.gb_camera_init.Controls.Add(this.cb_DeviceList);
            this.gb_camera_init.Controls.Add(this.bt_DiscoverCamera);
            this.gb_camera_init.Controls.Add(this.bt_CloseCamera);
            this.gb_camera_init.Controls.Add(this.bt_OpenCamera);
            this.gb_camera_init.Font = new System.Drawing.Font("宋体", 12F);
            this.gb_camera_init.Location = new System.Drawing.Point(13, 14);
            this.gb_camera_init.Margin = new System.Windows.Forms.Padding(2);
            this.gb_camera_init.Name = "gb_camera_init";
            this.gb_camera_init.Padding = new System.Windows.Forms.Padding(2);
            this.gb_camera_init.Size = new System.Drawing.Size(220, 277);
            this.gb_camera_init.TabIndex = 0;
            this.gb_camera_init.TabStop = false;
            this.gb_camera_init.Text = "设备管理";
            // 
            // gb_image_grab
            // 
            this.gb_image_grab.Controls.Add(this.bt_StopGrab);
            this.gb_image_grab.Controls.Add(this.bt_StartGrab);
            this.gb_image_grab.Font = new System.Drawing.Font("宋体", 12F);
            this.gb_image_grab.Location = new System.Drawing.Point(1, 186);
            this.gb_image_grab.Name = "gb_image_grab";
            this.gb_image_grab.Size = new System.Drawing.Size(215, 82);
            this.gb_image_grab.TabIndex = 8;
            this.gb_image_grab.TabStop = false;
            this.gb_image_grab.Text = "采集图像";
            // 
            // bt_StopGrab
            // 
            this.bt_StopGrab.Enabled = false;
            this.bt_StopGrab.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_StopGrab.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_StopGrab.Location = new System.Drawing.Point(112, 35);
            this.bt_StopGrab.Name = "bt_StopGrab";
            this.bt_StopGrab.Size = new System.Drawing.Size(100, 30);
            this.bt_StopGrab.TabIndex = 3;
            this.bt_StopGrab.Text = "停止采集";
            this.bt_StopGrab.UseVisualStyleBackColor = true;
            // 
            // bt_StartGrab
            // 
            this.bt_StartGrab.Enabled = false;
            this.bt_StartGrab.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_StartGrab.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_StartGrab.Location = new System.Drawing.Point(6, 35);
            this.bt_StartGrab.Name = "bt_StartGrab";
            this.bt_StartGrab.Size = new System.Drawing.Size(100, 30);
            this.bt_StartGrab.TabIndex = 2;
            this.bt_StartGrab.Text = "开始采集";
            this.bt_StartGrab.UseVisualStyleBackColor = true;
            // 
            // cb_DeviceList
            // 
            this.cb_DeviceList.FormattingEnabled = true;
            this.cb_DeviceList.Location = new System.Drawing.Point(15, 29);
            this.cb_DeviceList.Margin = new System.Windows.Forms.Padding(2);
            this.cb_DeviceList.Name = "cb_DeviceList";
            this.cb_DeviceList.Size = new System.Drawing.Size(192, 24);
            this.cb_DeviceList.TabIndex = 7;
            // 
            // bt_DiscoverCamera
            // 
            this.bt_DiscoverCamera.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_DiscoverCamera.Location = new System.Drawing.Point(53, 75);
            this.bt_DiscoverCamera.Margin = new System.Windows.Forms.Padding(2);
            this.bt_DiscoverCamera.Name = "bt_DiscoverCamera";
            this.bt_DiscoverCamera.Size = new System.Drawing.Size(100, 40);
            this.bt_DiscoverCamera.TabIndex = 2;
            this.bt_DiscoverCamera.Text = "初始化相机";
            this.bt_DiscoverCamera.UseVisualStyleBackColor = true;
            this.bt_DiscoverCamera.Click += new System.EventHandler(this.bt_DiscoverCamera_Click);
            // 
            // bt_CloseCamera
            // 
            this.bt_CloseCamera.Enabled = false;
            this.bt_CloseCamera.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_CloseCamera.Location = new System.Drawing.Point(110, 129);
            this.bt_CloseCamera.Margin = new System.Windows.Forms.Padding(2);
            this.bt_CloseCamera.Name = "bt_CloseCamera";
            this.bt_CloseCamera.Size = new System.Drawing.Size(100, 30);
            this.bt_CloseCamera.TabIndex = 1;
            this.bt_CloseCamera.Text = "关闭相机";
            this.bt_CloseCamera.UseVisualStyleBackColor = true;
            this.bt_CloseCamera.Click += new System.EventHandler(this.bt_CloseCamera_Click);
            // 
            // bt_OpenCamera
            // 
            this.bt_OpenCamera.Enabled = false;
            this.bt_OpenCamera.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_OpenCamera.Location = new System.Drawing.Point(6, 129);
            this.bt_OpenCamera.Margin = new System.Windows.Forms.Padding(2);
            this.bt_OpenCamera.Name = "bt_OpenCamera";
            this.bt_OpenCamera.Size = new System.Drawing.Size(100, 30);
            this.bt_OpenCamera.TabIndex = 0;
            this.bt_OpenCamera.Text = "打开相机";
            this.bt_OpenCamera.UseVisualStyleBackColor = true;
            this.bt_OpenCamera.Click += new System.EventHandler(this.bt_OpenCamera_Click);
            // 
            // tp_process_option
            // 
            this.tp_process_option.Controls.Add(this.gb_Test);
            this.tp_process_option.Controls.Add(this.gb_measurement);
            this.tp_process_option.Controls.Add(this.groupBox3);
            this.tp_process_option.Location = new System.Drawing.Point(4, 22);
            this.tp_process_option.Margin = new System.Windows.Forms.Padding(2);
            this.tp_process_option.Name = "tp_process_option";
            this.tp_process_option.Padding = new System.Windows.Forms.Padding(2);
            this.tp_process_option.Size = new System.Drawing.Size(501, 299);
            this.tp_process_option.TabIndex = 1;
            this.tp_process_option.Text = "处理设置";
            this.tp_process_option.UseVisualStyleBackColor = true;
            // 
            // gb_Test
            // 
            this.gb_Test.Controls.Add(this.rbt_outLineMode);
            this.gb_Test.Controls.Add(this.rbt_inLineMode);
            this.gb_Test.Controls.Add(this.bt_StartTest);
            this.gb_Test.Controls.Add(this.bt_StopTest);
            this.gb_Test.Font = new System.Drawing.Font("宋体", 12F);
            this.gb_Test.Location = new System.Drawing.Point(17, 103);
            this.gb_Test.Margin = new System.Windows.Forms.Padding(2);
            this.gb_Test.Name = "gb_Test";
            this.gb_Test.Padding = new System.Windows.Forms.Padding(2);
            this.gb_Test.Size = new System.Drawing.Size(215, 110);
            this.gb_Test.TabIndex = 9;
            this.gb_Test.TabStop = false;
            this.gb_Test.Text = "测试指令";
            // 
            // bt_StartTest
            // 
            this.bt_StartTest.Enabled = false;
            this.bt_StartTest.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_StartTest.Location = new System.Drawing.Point(11, 25);
            this.bt_StartTest.Margin = new System.Windows.Forms.Padding(2);
            this.bt_StartTest.Name = "bt_StartTest";
            this.bt_StartTest.Size = new System.Drawing.Size(91, 40);
            this.bt_StartTest.TabIndex = 7;
            this.bt_StartTest.Text = "开始测试";
            this.bt_StartTest.UseVisualStyleBackColor = true;
            this.bt_StartTest.Click += new System.EventHandler(this.bt_StartTest_Click);
            // 
            // bt_StopTest
            // 
            this.bt_StopTest.Enabled = false;
            this.bt_StopTest.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_StopTest.Location = new System.Drawing.Point(110, 25);
            this.bt_StopTest.Margin = new System.Windows.Forms.Padding(2);
            this.bt_StopTest.Name = "bt_StopTest";
            this.bt_StopTest.Size = new System.Drawing.Size(91, 40);
            this.bt_StopTest.TabIndex = 8;
            this.bt_StopTest.Text = "停止测试";
            this.bt_StopTest.UseVisualStyleBackColor = true;
            this.bt_StopTest.Click += new System.EventHandler(this.bt_StopTest_Click);
            // 
            // gb_measurement
            // 
            this.gb_measurement.Controls.Add(this.rbt_Measure18);
            this.gb_measurement.Controls.Add(this.rbt_Measure9);
            this.gb_measurement.Font = new System.Drawing.Font("宋体", 12F);
            this.gb_measurement.Location = new System.Drawing.Point(17, 20);
            this.gb_measurement.Margin = new System.Windows.Forms.Padding(2);
            this.gb_measurement.Name = "gb_measurement";
            this.gb_measurement.Padding = new System.Windows.Forms.Padding(2);
            this.gb_measurement.Size = new System.Drawing.Size(215, 80);
            this.gb_measurement.TabIndex = 6;
            this.gb_measurement.TabStop = false;
            this.gb_measurement.Text = "测量项";
            // 
            // rbt_Measure9
            // 
            this.rbt_Measure9.AutoSize = true;
            this.rbt_Measure9.Enabled = false;
            this.rbt_Measure9.Font = new System.Drawing.Font("宋体", 10F);
            this.rbt_Measure9.Location = new System.Drawing.Point(17, 38);
            this.rbt_Measure9.Margin = new System.Windows.Forms.Padding(2);
            this.rbt_Measure9.Name = "rbt_Measure9";
            this.rbt_Measure9.Size = new System.Drawing.Size(88, 18);
            this.rbt_Measure9.TabIndex = 1;
            this.rbt_Measure9.Text = "FAI-9(2D)";
            this.rbt_Measure9.UseVisualStyleBackColor = true;
            this.rbt_Measure9.CheckedChanged += new System.EventHandler(this.rbt_Measure9_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bt_ClearData);
            this.groupBox3.Controls.Add(this.bt_SaveCSV);
            this.groupBox3.Controls.Add(this.bt_SaveBmp);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox3.Location = new System.Drawing.Point(247, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(230, 128);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据";
            // 
            // bt_ClearData
            // 
            this.bt_ClearData.Enabled = false;
            this.bt_ClearData.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_ClearData.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_ClearData.Location = new System.Drawing.Point(17, 80);
            this.bt_ClearData.Name = "bt_ClearData";
            this.bt_ClearData.Size = new System.Drawing.Size(91, 40);
            this.bt_ClearData.TabIndex = 2;
            this.bt_ClearData.Text = "清空数据";
            this.bt_ClearData.UseVisualStyleBackColor = true;
            this.bt_ClearData.Click += new System.EventHandler(this.bt_ClearData_Click);
            // 
            // bt_SaveCSV
            // 
            this.bt_SaveCSV.Enabled = false;
            this.bt_SaveCSV.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_SaveCSV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_SaveCSV.Location = new System.Drawing.Point(17, 26);
            this.bt_SaveCSV.Name = "bt_SaveCSV";
            this.bt_SaveCSV.Size = new System.Drawing.Size(91, 40);
            this.bt_SaveCSV.TabIndex = 1;
            this.bt_SaveCSV.Text = "保存数据";
            this.bt_SaveCSV.UseVisualStyleBackColor = true;
            this.bt_SaveCSV.Click += new System.EventHandler(this.bt_SaveCSV_Click);
            // 
            // bt_SaveBmp
            // 
            this.bt_SaveBmp.Enabled = false;
            this.bt_SaveBmp.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_SaveBmp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_SaveBmp.Location = new System.Drawing.Point(121, 26);
            this.bt_SaveBmp.Name = "bt_SaveBmp";
            this.bt_SaveBmp.Size = new System.Drawing.Size(90, 40);
            this.bt_SaveBmp.TabIndex = 0;
            this.bt_SaveBmp.Text = "保存BMP";
            this.bt_SaveBmp.UseVisualStyleBackColor = true;
            this.bt_SaveBmp.Click += new System.EventHandler(this.bt_SaveBmp_Click);
            // 
            // lv_AllFrameData
            // 
            this.lv_AllFrameData.HideSelection = false;
            this.lv_AllFrameData.Location = new System.Drawing.Point(509, 460);
            this.lv_AllFrameData.Margin = new System.Windows.Forms.Padding(2);
            this.lv_AllFrameData.Name = "lv_AllFrameData";
            this.lv_AllFrameData.Size = new System.Drawing.Size(671, 302);
            this.lv_AllFrameData.TabIndex = 3;
            this.lv_AllFrameData.UseCompatibleStateImageBehavior = false;
            // 
            // tp_3Dcamera_option
            // 
            this.tp_3Dcamera_option.Controls.Add(this.groupBox1);
            this.tp_3Dcamera_option.Location = new System.Drawing.Point(4, 22);
            this.tp_3Dcamera_option.Name = "tp_3Dcamera_option";
            this.tp_3Dcamera_option.Size = new System.Drawing.Size(501, 299);
            this.tp_3Dcamera_option.TabIndex = 2;
            this.tp_3Dcamera_option.Text = "3D相机设置";
            this.tp_3Dcamera_option.UseVisualStyleBackColor = true;
            // 
            // rbt_Measure18
            // 
            this.rbt_Measure18.AutoSize = true;
            this.rbt_Measure18.Font = new System.Drawing.Font("宋体", 10F);
            this.rbt_Measure18.Location = new System.Drawing.Point(108, 38);
            this.rbt_Measure18.Margin = new System.Windows.Forms.Padding(2);
            this.rbt_Measure18.Name = "rbt_Measure18";
            this.rbt_Measure18.Size = new System.Drawing.Size(102, 18);
            this.rbt_Measure18.TabIndex = 2;
            this.rbt_Measure18.Text = "FAI-18C(3D)";
            this.rbt_Measure18.UseVisualStyleBackColor = true;
            this.rbt_Measure18.CheckedChanged += new System.EventHandler(this.rbt_Measure18_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bt_ClearBufferData);
            this.groupBox1.Controls.Add(this.bt_StopGrab_3D);
            this.groupBox1.Controls.Add(this.bt_StartGrab_3D);
            this.groupBox1.Controls.Add(this.bt_InitCamera3D);
            this.groupBox1.Controls.Add(this.bt_CloseCamera_3D);
            this.groupBox1.Controls.Add(this.bt_OpenCamera_3D);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox1.Location = new System.Drawing.Point(13, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(220, 277);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备管理";
            // 
            // bt_StopGrab_3D
            // 
            this.bt_StopGrab_3D.Enabled = false;
            this.bt_StopGrab_3D.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_StopGrab_3D.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_StopGrab_3D.Location = new System.Drawing.Point(114, 88);
            this.bt_StopGrab_3D.Name = "bt_StopGrab_3D";
            this.bt_StopGrab_3D.Size = new System.Drawing.Size(100, 30);
            this.bt_StopGrab_3D.TabIndex = 3;
            this.bt_StopGrab_3D.Text = "停止采集";
            this.bt_StopGrab_3D.UseVisualStyleBackColor = true;
            // 
            // bt_StartGrab_3D
            // 
            this.bt_StartGrab_3D.Enabled = false;
            this.bt_StartGrab_3D.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_StartGrab_3D.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bt_StartGrab_3D.Location = new System.Drawing.Point(114, 34);
            this.bt_StartGrab_3D.Name = "bt_StartGrab_3D";
            this.bt_StartGrab_3D.Size = new System.Drawing.Size(100, 30);
            this.bt_StartGrab_3D.TabIndex = 2;
            this.bt_StartGrab_3D.Text = "开始采集";
            this.bt_StartGrab_3D.UseVisualStyleBackColor = true;
            // 
            // bt_InitCamera3D
            // 
            this.bt_InitCamera3D.Font = new System.Drawing.Font("宋体", 10F);
            this.bt_InitCamera3D.Location = new System.Drawing.Point(9, 34);
            this.bt_InitCamera3D.Margin = new System.Windows.Forms.Padding(2);
            this.bt_InitCamera3D.Name = "bt_InitCamera3D";
            this.bt_InitCamera3D.Size = new System.Drawing.Size(100, 30);
            this.bt_InitCamera3D.TabIndex = 2;
            this.bt_InitCamera3D.Text = "初始化相机";
            this.bt_InitCamera3D.UseVisualStyleBackColor = true;
            // 
            // bt_CloseCamera_3D
            // 
            this.bt_CloseCamera_3D.Enabled = false;
            this.bt_CloseCamera_3D.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_CloseCamera_3D.Location = new System.Drawing.Point(9, 143);
            this.bt_CloseCamera_3D.Margin = new System.Windows.Forms.Padding(2);
            this.bt_CloseCamera_3D.Name = "bt_CloseCamera_3D";
            this.bt_CloseCamera_3D.Size = new System.Drawing.Size(100, 30);
            this.bt_CloseCamera_3D.TabIndex = 1;
            this.bt_CloseCamera_3D.Text = "关闭相机";
            this.bt_CloseCamera_3D.UseVisualStyleBackColor = true;
            // 
            // bt_OpenCamera_3D
            // 
            this.bt_OpenCamera_3D.Enabled = false;
            this.bt_OpenCamera_3D.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_OpenCamera_3D.Location = new System.Drawing.Point(9, 88);
            this.bt_OpenCamera_3D.Margin = new System.Windows.Forms.Padding(2);
            this.bt_OpenCamera_3D.Name = "bt_OpenCamera_3D";
            this.bt_OpenCamera_3D.Size = new System.Drawing.Size(100, 30);
            this.bt_OpenCamera_3D.TabIndex = 0;
            this.bt_OpenCamera_3D.Text = "打开相机";
            this.bt_OpenCamera_3D.UseVisualStyleBackColor = true;
            // 
            // bt_ClearBufferData
            // 
            this.bt_ClearBufferData.Enabled = false;
            this.bt_ClearBufferData.Font = new System.Drawing.Font("宋体", 12F);
            this.bt_ClearBufferData.Location = new System.Drawing.Point(113, 143);
            this.bt_ClearBufferData.Margin = new System.Windows.Forms.Padding(2);
            this.bt_ClearBufferData.Name = "bt_ClearBufferData";
            this.bt_ClearBufferData.Size = new System.Drawing.Size(100, 30);
            this.bt_ClearBufferData.TabIndex = 7;
            this.bt_ClearBufferData.Text = "清除内存";
            this.bt_ClearBufferData.UseVisualStyleBackColor = true;
            // 
            // rbt_outLineMode
            // 
            this.rbt_outLineMode.AutoSize = true;
            this.rbt_outLineMode.Checked = true;
            this.rbt_outLineMode.Font = new System.Drawing.Font("宋体", 12F);
            this.rbt_outLineMode.Location = new System.Drawing.Point(112, 75);
            this.rbt_outLineMode.Margin = new System.Windows.Forms.Padding(2);
            this.rbt_outLineMode.Name = "rbt_outLineMode";
            this.rbt_outLineMode.Size = new System.Drawing.Size(90, 20);
            this.rbt_outLineMode.TabIndex = 10;
            this.rbt_outLineMode.TabStop = true;
            this.rbt_outLineMode.Text = "离线模式";
            this.rbt_outLineMode.UseVisualStyleBackColor = true;
            this.rbt_outLineMode.CheckedChanged += new System.EventHandler(this.rbt_outLineMode_CheckedChanged_1);
            // 
            // rbt_inLineMode
            // 
            this.rbt_inLineMode.AutoSize = true;
            this.rbt_inLineMode.Font = new System.Drawing.Font("宋体", 12F);
            this.rbt_inLineMode.Location = new System.Drawing.Point(17, 75);
            this.rbt_inLineMode.Margin = new System.Windows.Forms.Padding(2);
            this.rbt_inLineMode.Name = "rbt_inLineMode";
            this.rbt_inLineMode.Size = new System.Drawing.Size(90, 20);
            this.rbt_inLineMode.TabIndex = 9;
            this.rbt_inLineMode.Text = "在线模式";
            this.rbt_inLineMode.UseVisualStyleBackColor = true;
            this.rbt_inLineMode.CheckedChanged += new System.EventHandler(this.rbt_inLineMode_CheckedChanged_1);
            // 
            // MyWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1185, 757);
            this.Controls.Add(this.lv_AllFrameData);
            this.Controls.Add(this.tp_SetOption);
            this.Controls.Add(this.pb_out);
            this.Controls.Add(this.pb_in);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MyWindow";
            this.Text = "Application";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MyWindow_FormClosing);
            this.Resize += new System.EventHandler(this.MyWindow_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pb_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_out)).EndInit();
            this.tp_SetOption.ResumeLayout(false);
            this.tp_2Dcamera_option.ResumeLayout(false);
            this.gb_camera_parameter.ResumeLayout(false);
            this.gb_camera_parameter.PerformLayout();
            this.gb_camera_init.ResumeLayout(false);
            this.gb_image_grab.ResumeLayout(false);
            this.tp_process_option.ResumeLayout(false);
            this.gb_Test.ResumeLayout(false);
            this.gb_Test.PerformLayout();
            this.gb_measurement.ResumeLayout(false);
            this.gb_measurement.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tp_3Dcamera_option.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_in;
        private System.Windows.Forms.PictureBox pb_out;
        private System.Windows.Forms.TabControl tp_SetOption;
        private System.Windows.Forms.TabPage tp_2Dcamera_option;
        private System.Windows.Forms.TabPage tp_process_option;
        private System.Windows.Forms.GroupBox gb_camera_init;
        private System.Windows.Forms.Button bt_DiscoverCamera;
        private System.Windows.Forms.Button bt_CloseCamera;
        private System.Windows.Forms.Button bt_OpenCamera;
        private System.Windows.Forms.GroupBox gb_camera_parameter;
        private System.Windows.Forms.Button bt_SetParam;
        private System.Windows.Forms.Button bt_GetParam;
        private System.Windows.Forms.Label lb_FrameRate;
        private System.Windows.Forms.Label lb_Gain;
        private System.Windows.Forms.Label lb_Exposure;
        private System.Windows.Forms.TextBox tb_FrameRate;
        private System.Windows.Forms.TextBox tb_Gain;
        private System.Windows.Forms.TextBox tb_Exposure;
        private System.Windows.Forms.ListView lv_AllFrameData;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button bt_SaveBmp;
        private System.Windows.Forms.GroupBox gb_measurement;
        private System.Windows.Forms.Button bt_StartTest;
        private System.Windows.Forms.Button bt_StopTest;
        private System.Windows.Forms.GroupBox gb_Test;
        private System.Windows.Forms.Button bt_SaveCSV;
        private System.Windows.Forms.Button bt_ClearData;
        private System.Windows.Forms.ComboBox cb_DeviceList;
        private System.Windows.Forms.RadioButton rbt_Measure9;
        private System.Windows.Forms.GroupBox gb_image_grab;
        private System.Windows.Forms.Button bt_StopGrab;
        private System.Windows.Forms.Button bt_StartGrab;
        private System.Windows.Forms.TabPage tp_3Dcamera_option;
        private System.Windows.Forms.RadioButton rbt_Measure18;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bt_ClearBufferData;
        private System.Windows.Forms.Button bt_StopGrab_3D;
        private System.Windows.Forms.Button bt_StartGrab_3D;
        private System.Windows.Forms.Button bt_InitCamera3D;
        private System.Windows.Forms.Button bt_CloseCamera_3D;
        private System.Windows.Forms.Button bt_OpenCamera_3D;
        private System.Windows.Forms.RadioButton rbt_outLineMode;
        private System.Windows.Forms.RadioButton rbt_inLineMode;
    }
}

