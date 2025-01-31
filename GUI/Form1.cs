﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MvCamCtrl.NET;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Xml.Linq;
using HalconAlgorithm;
using System.Drawing.Imaging;
using LJX8_DllSampleAll.Data;
using MyWindow.Forms;
using MyWindow.Properties;
using LJX8_DllSampleAll;

namespace MyWindow
{
    public partial class MyWindow : Form
    {
        #region 3D Pagram

        #region Constant
        private const uint NoErrorValue = 0u;
        private const int ProfileDataMinCount = 200;
        private const int DataCountInOneLine = 8;
        private const int NotAccessValue = 0;
        private const char NullChar = '\0';
        #endregion

        #region 3D Enum
        private enum SendCommand
        {
            None,
            RebootController,
            Trigger,
            StartMeasure,
            StopMeasure,
            GetProfile,
            GetBatchProfile,
            InitializeHighSpeedDataCommunication,
            PreStartHighSpeedDataCommunication,
            StartHighSpeedDataCommunication,
        }
        #endregion

        #region Field
        private int _currentDeviceId = 0;
        private SendCommand _sendCommand;
        private HighSpeedDataCallBack _callback;
        private HighSpeedDataCallBackForSimpleArray _callbackSimpleArray;
        private HighSpeedDataCallBackForSimpleArray _callbackSimpleArrayOnlyCount;
        private LJX8IF_PROFILE_INFO[] _profileInfo;
        private DeviceData[] _deviceData;
        private static bool[] _isBufferFull = new bool[NativeMethods.DeviceCount];
        private static bool[] _isStopCommunicationByError = new bool[NativeMethods.DeviceCount];
        #endregion

        #region Delegate
        private delegate void InvokeDelagate();
        #endregion
        #endregion

        #region 2D Pagram
        MyCamera.MV_CC_DEVICE_INFO_LIST m_stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
        private MyCamera m_MyCamera = new MyCamera();
        bool m_bGrabbing = false;
        Thread m_hReceiveThread = null;
        MyCamera.MV_FRAME_OUT_INFO_EX m_stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();

        //用于从驱动获取图像的缓存 
        UInt32 m_nBufSizeForDriver = 0;
        IntPtr m_BufForDriver;
        private static Object BufForDriverLock = new Object();

        //用于保存图像的缓存
        UInt32 m_nBufSizeForSaveImage = 0;
        IntPtr m_BufForSaveImage;
        #endregion

        #region Configure Pagram
        bool IsInLine = false;
        int MeasureProject = -1;
        Thread thread_OutLineTest;
        bool IsthreadLoadImageStop = false;
        double Radius, PositionDegree, RunTime, DistanceX1, DistanceY1;
        double Origin_Z_mm, E2_OffestZ_mm, E5_OffestZ_mm, E10_OffestZ_mm, E13_OffestZ_mm, Profile;
        #endregion

        public MyWindow()
        {
            InitializeComponent();
            changeSize();
            HAlgorithm.CreateInWindow(pb_in.Handle, pb_in.Width, pb_in.Height);
            HAlgorithm.CreateOutWindow(pb_out.Handle, pb_out.Width, pb_out.Height);
            
            Control.CheckForIllegalCrossThreadCalls = false;
            OutLineModeState();
            thread_OutLineTest = new Thread(new ThreadStart(thread_OutLineTest_Start));
            
            #region 3D初始化
            // Field initialization
            _sendCommand = SendCommand.None;
            _deviceData = new DeviceData[NativeMethods.DeviceCount];
            _callback = ReceiveHighSpeedData;
            _callbackSimpleArray = ReceiveHighSpeedSimpleArray;
            _callbackSimpleArrayOnlyCount = CountSimpleArrayReceive;

            for (int i = 0; i < NativeMethods.DeviceCount; i++)
            {
                _deviceData[i] = new DeviceData();
            }
            _profileInfo = new LJX8IF_PROFILE_INFO[NativeMethods.DeviceCount];

            UpdateBatchSimpleArrayEnable();
            UpdateHighSpeedProfileSaveEnable();
            #endregion
        }

        private void CountSimpleArrayReceive(IntPtr headBuffer, IntPtr profileBuffer, IntPtr luminanceBuffer, uint isLuminanceEnable, uint profileSize, uint count, uint notify, uint user)
        {
            _deviceData[(int)user].SimpleArrayDataHighSpeed.Count += count;
            _deviceData[(int)user].SimpleArrayDataHighSpeed.Notify = notify;
        }

        private void ReceiveHighSpeedSimpleArray(IntPtr headBuffer, IntPtr profileBuffer, IntPtr luminanceBuffer, uint isLuminanceEnable, uint profileSize, uint count, uint notify, uint user)
        {
            _isBufferFull[(int)user] = _deviceData[(int)user].SimpleArrayDataHighSpeed.AddReceivedData(profileBuffer, luminanceBuffer, count);
            _deviceData[(int)user].SimpleArrayDataHighSpeed.Notify = notify;
        }

        private static void ReceiveHighSpeedData(IntPtr buffer, uint size, uint count, uint notify, uint user)
        {
            uint profileSize = (uint)(size / Marshal.SizeOf(typeof(int)));
            List<int[]> receiveBuffer = new List<int[]>();
            int[] bufferArray = new int[(int)(profileSize * count)];
            Marshal.Copy(buffer, bufferArray, 0, (int)(profileSize * count));

            // Profile data retention
            for (int i = 0; i < (int)count; i++)
            {
                int[] oneProfile = new int[(int)profileSize];
                Array.Copy(bufferArray, i * profileSize, oneProfile, 0, profileSize);
                receiveBuffer.Add(oneProfile);
            }

            if (ThreadSafeBuffer.GetBufferDataCount((int)user) + receiveBuffer.Count < Define.BufferFullCount)
            {
                ThreadSafeBuffer.Add((int)user, receiveBuffer, notify);
            }
            else
            {
                _isBufferFull[(int)user] = true;
            }
        }

        private void UpdateHighSpeedProfileSaveEnable()
        {
            bool isOnlyProfileCountChecked = false;
            _textBoxHighSpeedProfileFilePath.Enabled = !isOnlyProfileCountChecked;
            _numericUpDownProfileSaveCount.Enabled = !isOnlyProfileCountChecked;
            _buttonHighSpeedProfileFileSave.Enabled = !isOnlyProfileCountChecked;
        }

        //打开相机
        private void bt_OpenCamera_Click(object sender, EventArgs e)
        {
            if (m_stDeviceList.nDeviceNum == 0 || cb_DeviceList.SelectedIndex == -1)
            {
                ShowErrorMsg("请选择要打开的设备", 0);
                return;
            }

            //获取选择的设备信息 
            MyCamera.MV_CC_DEVICE_INFO device =
                (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[cb_DeviceList.SelectedIndex],
                                                              typeof(MyCamera.MV_CC_DEVICE_INFO));

            //打开设备
            if (null == m_MyCamera)
            {
                m_MyCamera = new MyCamera();
                if (null == m_MyCamera)
                {
                    return;
                }
            }

            int nRet = m_MyCamera.MV_CC_CreateDevice_NET(ref device);
            if (MyCamera.MV_OK != nRet)
            {
                return;
            }

            nRet = m_MyCamera.MV_CC_OpenDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                m_MyCamera.MV_CC_DestroyDevice_NET();
                ShowErrorMsg("设备打开失败！", nRet);
                return;
            }

            //探测网络最佳包大小(只对GigE相机有效) 
            if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            {
                int nPacketSize = m_MyCamera.MV_CC_GetOptimalPacketSize_NET();
                if (nPacketSize > 0)
                {
                    nRet = m_MyCamera.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                    if (nRet != MyCamera.MV_OK)
                    {
                        ShowErrorMsg("设置网络最佳包大小失败！", nRet);
                    }
                }
                else
                {
                    ShowErrorMsg("获取网络最佳包大小失败！", nPacketSize);
                }
            }

            // ch:设置采集连续模式 | en:Set Continues Aquisition Mode
            m_MyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", (uint)MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
            m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);

            // 获取参数
            bt_GetParam_Click(null, null);

            //控件操作
            bt_OpenCamera.Enabled = false;
            bt_CloseCamera.Enabled = true;
            bt_GetParam.Enabled = true;
            bt_SetParam.Enabled = true;
            tb_Exposure.Enabled = true;
            tb_Gain.Enabled = true;
            tb_FrameRate.Enabled = true;
            bt_StartGrab.Enabled = true;
            bt_StopGrab.Enabled = false;
        }

        //关闭相机
        private void bt_CloseCamera_Click(object sender, EventArgs e)
        {
            // 取流标志位清零
            if (m_bGrabbing == true)
            {
                m_bGrabbing = false;
                m_hReceiveThread.Join();
            }

            if (m_BufForDriver != IntPtr.Zero)
            {
                Marshal.Release(m_BufForDriver);
            }
            if (m_BufForSaveImage != IntPtr.Zero)
            {
                Marshal.Release(m_BufForSaveImage);
            }

            //关闭设备
            m_MyCamera.MV_CC_CloseDevice_NET();
            m_MyCamera.MV_CC_DestroyDevice_NET();

            // 控件操作 
            bt_DiscoverCamera.Enabled = true;
            bt_OpenCamera.Enabled = true;
            bt_CloseCamera.Enabled = false;
            bt_GetParam.Enabled = false;
            bt_SetParam.Enabled = false;
            tb_Exposure.Enabled = false;
            tb_Gain.Enabled = false;
            tb_FrameRate.Enabled = false;
            bt_StartGrab.Enabled = false;
            bt_StopGrab.Enabled = false;
        }

        //设备初始化，检测相机
        private void bt_DiscoverCamera_Click(object sender, EventArgs e)
        {
            //创建设备列表
            System.GC.Collect();
            cb_DeviceList.Items.Clear();
            m_stDeviceList.nDeviceNum = 0;
            int nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_stDeviceList);
            if (0 != nRet || 0 == m_stDeviceList.nDeviceNum)
            {
                ShowErrorMsg("未检测到设备！", 0);
                return;
            }

            // 在窗体列表中显示设备名
            for (int i = 0; i < m_stDeviceList.nDeviceNum; i++)
            {
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));

                    if (gigeInfo.chUserDefinedName != "")
                    {
                        cb_DeviceList.Items.Add("GEV: " + gigeInfo.chUserDefinedName + " (" + gigeInfo.chSerialNumber + ")");
                    }
                    else
                    {
                        cb_DeviceList.Items.Add("GEV: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")");
                    }
                }
                else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(device.SpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                    if (usbInfo.chUserDefinedName != "")
                    {
                        cb_DeviceList.Items.Add("U3V: " + usbInfo.chUserDefinedName + " (" + usbInfo.chSerialNumber + ")");
                    }
                    else
                    {
                        cb_DeviceList.Items.Add("U3V: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")");
                    }
                }
            }

            // 选择第一项
            if (m_stDeviceList.nDeviceNum != 0)
            {
                cb_DeviceList.SelectedIndex = 0;
                //设置控件状态
                bt_OpenCamera.Enabled = true;
            }
        }

        // 显示错误信息
        private void ShowErrorMsg(string csMessage, int nErrorNum)
        {
            string errorMsg;
            if (0 == nErrorNum )
            {
                errorMsg = csMessage;
            }
            else
            {
                errorMsg = csMessage + ": Error =" + String.Format("{0:X}", nErrorNum);
            }

            switch (nErrorNum)
            {
                case MyCamera.MV_E_HANDLE: errorMsg += " 错误或无效的句柄 "; break;
                case MyCamera.MV_E_SUPPORT: errorMsg += " 不支持的功能 "; break;
                case MyCamera.MV_E_BUFOVER: errorMsg += " 缓存已满 "; break;
                case MyCamera.MV_E_CALLORDER: errorMsg += " 函数调用顺序错误 "; break;
                case MyCamera.MV_E_PARAMETER: errorMsg += " 参数错误 "; break;
                case MyCamera.MV_E_RESOURCE: errorMsg += " 应用资源失败 "; break;
                case MyCamera.MV_E_NODATA: errorMsg += " 没有数据 "; break;
                case MyCamera.MV_E_PRECONDITION: errorMsg += " 前提条件错误，或运行环境已更改 "; break;
                case MyCamera.MV_E_VERSION: errorMsg += " 版本不匹配 "; break;
                case MyCamera.MV_E_NOENOUGH_BUF: errorMsg += " 内存不足 "; break;
                case MyCamera.MV_E_UNKNOW: errorMsg += " 未知错误 "; break;
                case MyCamera.MV_E_GC_GENERIC: errorMsg += " 一般错误 "; break;
                case MyCamera.MV_E_GC_ACCESS: errorMsg += " 节点访问条件错误 "; break;
                case MyCamera.MV_E_ACCESS_DENIED: errorMsg += " 没有权限 "; break;
                case MyCamera.MV_E_BUSY: errorMsg += " 设备忙或网络断开 "; break;
                case MyCamera.MV_E_NETER: errorMsg += " 网络错误 "; break;
            }

            MessageBox.Show(errorMsg, "提示");
        }

        //获取相机参数
        private void bt_GetParam_Click(object sender, EventArgs e)
        {
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            int nRet = m_MyCamera.MV_CC_GetFloatValue_NET("ExposureTime", ref stParam);
            if (MyCamera.MV_OK == nRet)
            {
                tb_Exposure.Text = stParam.fCurValue.ToString("F1");
            }

            nRet = m_MyCamera.MV_CC_GetFloatValue_NET("Gain", ref stParam);
            if (MyCamera.MV_OK == nRet)
            {
                tb_Gain.Text = stParam.fCurValue.ToString("F1");
            }

            nRet = m_MyCamera.MV_CC_GetFloatValue_NET("ResultingFrameRate", ref stParam);
            if (MyCamera.MV_OK == nRet)
            {
                tb_FrameRate.Text = stParam.fCurValue.ToString("F1");
            }
        }

        //设置相机参数
        private void bt_SetParam_Click(object sender, EventArgs e)
        {
            try
            {
                float.Parse(tb_Exposure.Text);
                float.Parse(tb_Gain.Text);
                float.Parse(tb_FrameRate.Text);
            }
            catch
            {
                ShowErrorMsg("请输入正确的数据类型!", 0);
                return;
            }

            m_MyCamera.MV_CC_SetEnumValue_NET("ExposureAuto", 0);
            int nRet = m_MyCamera.MV_CC_SetFloatValue_NET("ExposureTime", float.Parse(tb_Exposure.Text));
            if (nRet != MyCamera.MV_OK)
            {
                ShowErrorMsg("设置曝光时间失败!", nRet);
            }

            m_MyCamera.MV_CC_SetEnumValue_NET("GainAuto", 0);
            nRet = m_MyCamera.MV_CC_SetFloatValue_NET("Gain", float.Parse(tb_Gain.Text));
            if (nRet != MyCamera.MV_OK)
            {
                ShowErrorMsg("设置增益失败！", nRet);
            }

            nRet = m_MyCamera.MV_CC_SetFloatValue_NET("AcquisitionFrameRate", float.Parse(tb_FrameRate.Text));
            if (nRet != MyCamera.MV_OK)
            {
                ShowErrorMsg("设置帧率失败！", nRet);
            }
        }

        //选择离线模式
        private void rbt_outLineMode_CheckedChanged(object sender, EventArgs e)
        {
            IsInLine = false;
            OutLineModeState();
        }

        //离线测试线程
        private void thread_OutLineTest_Start()
        {
            string OutLineFilePath = "";
            bool MeasureIsSucced;
            while (!IsthreadLoadImageStop)
            {
                //进行离线测试
                if (OutLineFilePath == string.Empty)
                {
                    FolderSelectDialog dialog = new FolderSelectDialog();
                    dialog.InitialDirectory = "./";
                    if (dialog.ShowDialog(this.Handle))
                    {
                        OutLineFilePath = dialog.FileName;
                        MeasureIsSucced = HAlgorithm.OutLineMeasure(MeasureProject, lv_AllFrameData, out Radius, out PositionDegree, out RunTime,
                         out DistanceX1, out DistanceY1, OutLineFilePath, 
                         out Origin_Z_mm, out E2_OffestZ_mm, out E5_OffestZ_mm, out E10_OffestZ_mm, out E13_OffestZ_mm, out Profile);
                    }
                    else
                    {
                        ShowErrorMsg("请选择文件路径", 0);
                        OutLineModeState();
                        return;
                    }
                }
                else
                {
                    MeasureIsSucced = HAlgorithm.OutLineMeasure(MeasureProject, lv_AllFrameData, out Radius, out PositionDegree, out RunTime,
                              out DistanceX1, out DistanceY1, OutLineFilePath, out Origin_Z_mm, out E2_OffestZ_mm, out E5_OffestZ_mm, out E10_OffestZ_mm, out E13_OffestZ_mm, out Profile);
                }

                if (MeasureIsSucced == false)
                {
                    //设置控件状态
                    bt_StopTest.Enabled = false;
                    bt_StartTest.Enabled = true;
                    rbt_Measure9.Enabled = true;
                    rbt_Measure18.Enabled = true;

                    break;
                }
                else
                {
                    Thread.Sleep(200);
                }
            }
        }

        //处于离线模式时的控件状态
        private void OutLineModeState()
        {
            bt_StartGrab.Enabled = false;
            bt_DiscoverCamera.Enabled = false;
            bt_OpenCamera.Enabled = false;
            bt_CloseCamera.Enabled = false;
            tb_Exposure.Enabled = false;
            tb_FrameRate.Enabled = false;
            tb_Gain.Enabled = false;
            bt_GetParam.Enabled = false;
            bt_SetParam.Enabled = false;
            bt_StopGrab.Enabled = false;
            bt_SaveBmp.Enabled = false;
            bt_StartTest.Enabled = true;
            rbt_Measure9.Enabled = true;
            rbt_Measure18.Enabled = true;
            bt_StopTest.Enabled = false;
        }

        //选择在线模式
        private void rbt_inLineMode_CheckedChanged(object sender, EventArgs e)
        {
            IsInLine = true;
            bt_DiscoverCamera.Enabled = true;
            bt_StopTest.Enabled = false;
            bt_StartTest.Enabled = false;
        }

        //开始采集
        private void bt_StartGrab_Click(object sender, EventArgs e)
        {
            //连续采集图像
            // 标志位置位true 
            m_bGrabbing = true;

            m_hReceiveThread = new Thread(ReceiveThreadProcess);
            m_hReceiveThread.Start();

            m_stFrameInfo.nFrameLen = 0;//取流之前先清除帧长度
            m_stFrameInfo.enPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Undefined;
            // 开始采集
            int nRet = m_MyCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                m_bGrabbing = false;
                m_hReceiveThread.Join();
                ShowErrorMsg("开始采集失败！", nRet);
                return;
            }

            // 控件操作
            bt_StartTest.Enabled = true;
            bt_StopGrab.Enabled = true;
            bt_SaveBmp.Enabled = true;
            rbt_Measure9.Enabled = true;
        }

        //实时显示线程函数
        public void ReceiveThreadProcess()
        {
            MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
            int nRet = m_MyCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Get PayloadSize failed", nRet);
                return;
            }

            UInt32 nPayloadSize = stParam.nCurValue;
            if (nPayloadSize > m_nBufSizeForDriver)
            {
                if (m_BufForDriver != IntPtr.Zero)
                {
                    Marshal.Release(m_BufForDriver);
                }
                m_nBufSizeForDriver = nPayloadSize;
                m_BufForDriver = Marshal.AllocHGlobal((Int32)m_nBufSizeForDriver);
            }

            if (m_BufForDriver == IntPtr.Zero)
            {
                return;
            }

            MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();

            while (m_bGrabbing)
            {
                lock (BufForDriverLock)
                {
                    nRet = m_MyCamera.MV_CC_GetOneFrameTimeout_NET(m_BufForDriver, nPayloadSize, ref stFrameInfo, 1000);
                    if (nRet == MyCamera.MV_OK)
                    {
                        m_stFrameInfo = stFrameInfo;
                    }
                }

                if (nRet == MyCamera.MV_OK)
                {
                    if (RemoveCustomPixelFormats(stFrameInfo.enPixelType))
                    {
                        continue;
                    }

                    HAlgorithm.DispBuffer(MeasureProject, m_BufForDriver, stFrameInfo.nWidth, stFrameInfo.nHeight);
                }
            }
        }

        //去除自定义的像素格式
        private bool RemoveCustomPixelFormats(MyCamera.MvGvspPixelType enPixelFormat)
        {
            Int32 nResult = ((int)enPixelFormat) & (unchecked((Int32)0x80000000));
            if (0x80000000 == nResult)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //停止采集
        private void bt_StopGrab_Click(object sender, EventArgs e)
        {
            // 标志位设为false
            m_bGrabbing = false;
            m_hReceiveThread.Join();

            // 停止采集 
            int nRet = m_MyCamera.MV_CC_StopGrabbing_NET();
            if (nRet != MyCamera.MV_OK)
            {
                ShowErrorMsg("Stop Grabbing Fail!", nRet);
            }
        }

        //选择测试项
        #region
        //选择测量项18
        private void rbt_Measure18_CheckedChanged(object sender, EventArgs e)
        {
            if (rbt_inLineMode.Checked == true)
            {
                _buttonInitialize.Enabled = true;
                _buttonStartHighSpeedDataCommunication.Enabled = true;
                _buttonStartMeasure.Enabled = true;

                bt_DiscoverCamera.Enabled = false;
            }

            if (true == rbt_Measure18.Checked)
            {
                MeasureProject = 18;
                HAlgorithm.ClearListView(lv_AllFrameData);
                HAlgorithm.InitListView3D(lv_AllFrameData);
            }
            else
            {
                MeasureProject = -1;
            }
        }

        private void rbt_Measure9_CheckedChanged(object sender, EventArgs e)
        {
            if(rbt_inLineMode.Checked ==true)
            {
                bt_DiscoverCamera.Enabled = true;
                _buttonInitialize.Enabled = false;
                _buttonStartHighSpeedDataCommunication.Enabled = false;
                _buttonStartMeasure.Enabled = false;
            }

            if (true == rbt_Measure9.Checked)
            {
                MeasureProject = 9;
                HAlgorithm.ClearListView(lv_AllFrameData);
                HAlgorithm.InitListView2D(lv_AllFrameData);
            }
            else
            {
                MeasureProject = -1;
            }
        }
        #endregion

        //清空测试数据
        private void bt_ClearData_Click(object sender, EventArgs e)
        {
            HAlgorithm.ClearListView(lv_AllFrameData);

            if(rbt_Measure9.Checked==true)
            {
                HAlgorithm.InitListView2D(lv_AllFrameData);
            }
            else if(rbt_Measure18.Checked == true)
            {
                HAlgorithm.InitListView3D(lv_AllFrameData);
            }
        }

        //保存CSV数据文件
        private void bt_SaveCSV_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Excel 文件(*.csv*) | *.csv* |文本文件(*.txt) | *.txt ";
                dialog.DefaultExt = ".csv";
                dialog.AddExtension = true;
                dialog.AutoUpgradeEnabled = false;
                dialog.Title = "请选择保存路径";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string SavePath = dialog.FileName;
                    DataWriteToCSV(SavePath, lv_AllFrameData);
                }
            }
            catch
            {
                ShowErrorMsg("保存失败！", 0);
            }
        }

        //安全退出
        private void MyWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            bt_CloseCamera_Click(null, null);
        }

        //保存BMP图像文件
        private void bt_SaveBmp_Click(object sender, EventArgs e)
        {
            if (false == m_bGrabbing)
            {
                ShowErrorMsg("不处于采集图像状态", 0);
                return;
            }

            if (RemoveCustomPixelFormats(m_stFrameInfo.enPixelType))
            {
                ShowErrorMsg("不支持的像素格式", 0);
                return;
            }

            IntPtr pTemp = IntPtr.Zero;
            MyCamera.MvGvspPixelType enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Undefined;
            if (m_stFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8 ||
                m_stFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_BGR8_Packed)
            {
                pTemp = m_BufForDriver;
                enDstPixelType = m_stFrameInfo.enPixelType;
            }
            else
            {
                UInt32 nSaveImageNeedSize = 0;
                MyCamera.MV_PIXEL_CONVERT_PARAM stConverPixelParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();

                lock (BufForDriverLock)
                {
                    if (m_stFrameInfo.nFrameLen == 0)
                    {
                        ShowErrorMsg("保存失败", 0);
                        return;
                    }

                    if (IsMonoData(m_stFrameInfo.enPixelType))
                    {
                        enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
                        nSaveImageNeedSize = (uint)m_stFrameInfo.nWidth * m_stFrameInfo.nHeight;
                    }
                    else
                    {
                        ShowErrorMsg("不存在的像素格式！", 0);
                        return;
                    }

                    if (m_nBufSizeForSaveImage < nSaveImageNeedSize)
                    {
                        if (m_BufForSaveImage != IntPtr.Zero)
                        {
                            Marshal.Release(m_BufForSaveImage);
                        }
                        m_nBufSizeForSaveImage = nSaveImageNeedSize;
                        m_BufForSaveImage = Marshal.AllocHGlobal((Int32)m_nBufSizeForSaveImage);
                    }

                    stConverPixelParam.nWidth = m_stFrameInfo.nWidth;
                    stConverPixelParam.nHeight = m_stFrameInfo.nHeight;
                    stConverPixelParam.pSrcData = m_BufForDriver;
                    stConverPixelParam.nSrcDataLen = m_stFrameInfo.nFrameLen;
                    stConverPixelParam.enSrcPixelType = m_stFrameInfo.enPixelType;
                    stConverPixelParam.enDstPixelType = enDstPixelType;
                    stConverPixelParam.pDstBuffer = m_BufForSaveImage;
                    stConverPixelParam.nDstBufferSize = m_nBufSizeForSaveImage;
                    int nRet = m_MyCamera.MV_CC_ConvertPixelType_NET(ref stConverPixelParam);
                    if (MyCamera.MV_OK != nRet)
                    {
                        ShowErrorMsg("转换像素类型失败！", nRet);
                        return;
                    }
                    pTemp = m_BufForSaveImage;
                }
            }

            lock (BufForDriverLock)
            {
                if (enDstPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                {
                    //************************Mono8 转 Bitmap*******************************
                    Bitmap bmp = new Bitmap(m_stFrameInfo.nWidth, m_stFrameInfo.nHeight, m_stFrameInfo.nWidth * 1, PixelFormat.Format8bppIndexed, pTemp);

                    ColorPalette cp = bmp.Palette;
                    // init palette
                    for (int i = 0; i < 256; i++)
                    {
                        cp.Entries[i] = Color.FromArgb(i, i, i);
                    }
                    // set palette back
                    SaveFileDialog dialog = new SaveFileDialog();
                    dialog.Filter = " 图像文件(*.bmp*) | *.bmp* ";
                    dialog.Title = "请选择保存路径";
                    dialog.DefaultExt = ".bmp";
                    if(dialog.ShowDialog() == DialogResult.OK)
                    {
                        bmp.Palette = cp;
                        bmp.Save(dialog.FileName, ImageFormat.Bmp);
                    }
                }
                else
                {
                    //*********************BGR8 转 Bitmap**************************
                    try
                    {
                        Bitmap bmp = new Bitmap(m_stFrameInfo.nWidth, m_stFrameInfo.nHeight, m_stFrameInfo.nWidth * 3, PixelFormat.Format24bppRgb, pTemp);
                        SaveFileDialog dialog = new SaveFileDialog();
                        dialog.Filter = " 图像文件(*.bmp*) | *.bmp* ";
                        dialog.Title = "请选择保存路径";
                        dialog.DefaultExt = ".bmp";
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            bmp.Save(dialog.FileName, ImageFormat.Bmp);
                        }
                    }
                    catch
                    {
                        ShowErrorMsg("保存失败！", 0);
                    }
                }
            }
            ShowErrorMsg("保存成功！", 0);
        }

        //判断读取的像素类型
        private Boolean IsMonoData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    return true;
                default:
                    return false;
            }
        }

        //开始测试
        private void bt_StartTest_Click(object sender, EventArgs e)
        {
            if (-1 == MeasureProject)
            {
                ShowErrorMsg("请选择测量项！", 0);
                return;
            }

            //设置控件状态
            bt_StopTest.Enabled = true;
            bt_StartTest.Enabled = false;
            rbt_Measure9.Enabled = false;
            rbt_Measure18.Enabled = false;

            if (false == IsInLine)
            {
                //进行离线测试
                if (thread_OutLineTest.ThreadState == ThreadState.Unstarted)
                {
                    thread_OutLineTest.Start();
                }

                if (thread_OutLineTest.ThreadState == ThreadState.Stopped || thread_OutLineTest.ThreadState == ThreadState.Aborted)
                {
                    thread_OutLineTest = new Thread(new ThreadStart(thread_OutLineTest_Start));
                    thread_OutLineTest.Start();
                }
            }
            else
            {
                //进行在线测试
                InLineTest();
            }
        }

        //在线测试
        private void InLineTest()
        {
            if(MeasureProject == 9)
            {
                MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
                int Ret = m_MyCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
                if (MyCamera.MV_OK != Ret)
                {
                    ShowErrorMsg("Get PayloadSize failed", Ret);
                    return;
                }

                UInt32 nPayloadSize = stParam.nCurValue;
                if (nPayloadSize > m_nBufSizeForDriver)
                {
                    if (m_BufForDriver != IntPtr.Zero)
                    {
                        Marshal.Release(m_BufForDriver);
                    }
                    m_nBufSizeForDriver = nPayloadSize;
                    m_BufForDriver = Marshal.AllocHGlobal((Int32)m_nBufSizeForDriver);
                }

                if (m_BufForDriver == IntPtr.Zero)
                {
                    return;
                }

                MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();

                while (true)
                {
                    lock (BufForDriverLock)
                    {
                        Ret = m_MyCamera.MV_CC_GetOneFrameTimeout_NET(m_BufForDriver, nPayloadSize, ref stFrameInfo, 1000);
                        if (Ret == MyCamera.MV_OK)
                        {
                            m_stFrameInfo = stFrameInfo;
                        }
                    }

                    if (Ret == MyCamera.MV_OK)
                    {
                        if (RemoveCustomPixelFormats(stFrameInfo.enPixelType))
                        {
                            continue;
                        }
                        HAlgorithm.InLineMeasure(MeasureProject, lv_AllFrameData, m_BufForDriver, stFrameInfo.nWidth,
                            stFrameInfo.nHeight, out Radius, out PositionDegree, out RunTime, out DistanceX1, out DistanceY1,
                            out Origin_Z_mm, out E2_OffestZ_mm, out E5_OffestZ_mm, out E10_OffestZ_mm, out E13_OffestZ_mm, out Profile);

                        break;
                    }
                }

                ////保存异常图像
                //if (PositionDegree < 0 || PositionDegree > 0.06 || Radius < 9.40 || Radius > 9.50)
                //{
                //    SaveErrorBMP();
                //}
                //设置控件状态
                bt_StopGrab.Enabled = false;
                bt_StopTest.Enabled = true;
                bt_StartTest.Enabled = false;
                rbt_Measure9.Enabled = false;
                rbt_Measure18.Enabled = false;
                bt_ClearData.Enabled = false;
                bt_SaveCSV.Enabled = false;
            }

            else if(MeasureProject == 18)
            {
                int m_3DWidth, m_3DHeight;
                IntPtr profilePtr = ProfileByte2ptr(out m_3DWidth, out m_3DHeight);

                HAlgorithm.InLineMeasure(MeasureProject, lv_AllFrameData, profilePtr, (ushort)m_3DWidth, (ushort)m_3DHeight,
                    out Radius, out PositionDegree, out RunTime, out DistanceX1, out DistanceY1, out Origin_Z_mm,
                    out E2_OffestZ_mm, out E5_OffestZ_mm, out E10_OffestZ_mm, out E13_OffestZ_mm, out Profile);

                //设置控件状态
                bt_StopTest.Enabled = true;
                bt_StartTest.Enabled = false;
                rbt_Measure9.Enabled = false;
                rbt_Measure18.Enabled = false;
                bt_ClearData.Enabled = false;
                bt_SaveCSV.Enabled = false;
            }
        }

        private void SaveErrorBMP()
        {
            if (false == m_bGrabbing)
            {
                ShowErrorMsg("不处于采集图像状态", 0);
                return;
            }
            if (RemoveCustomPixelFormats(m_stFrameInfo.enPixelType))
            {
                ShowErrorMsg("不支持的像素格式", 0);
                return;
            }

            IntPtr pTemp = IntPtr.Zero;
            MyCamera.MvGvspPixelType enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Undefined;
            if (m_stFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8 || m_stFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_BGR8_Packed)
            {
                pTemp = m_BufForDriver;
                enDstPixelType = m_stFrameInfo.enPixelType;
            }
            else
            {
                UInt32 nSaveImageNeedSize = 0;
                MyCamera.MV_PIXEL_CONVERT_PARAM stConverPixelParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();

                lock (BufForDriverLock)
                {
                    if (m_stFrameInfo.nFrameLen == 0)
                    {
                        ShowErrorMsg("保存失败", 0);
                        return;
                    }

                    if (IsMonoData(m_stFrameInfo.enPixelType))
                    {
                        enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
                        nSaveImageNeedSize = (uint)m_stFrameInfo.nWidth * m_stFrameInfo.nHeight;
                    }
                    else
                    {
                        ShowErrorMsg("不存在的像素格式！", 0);
                        return;
                    }

                    if (m_nBufSizeForSaveImage < nSaveImageNeedSize)
                    {
                        if (m_BufForSaveImage != IntPtr.Zero)
                        {
                            Marshal.Release(m_BufForSaveImage);
                        }
                        m_nBufSizeForSaveImage = nSaveImageNeedSize;
                        m_BufForSaveImage = Marshal.AllocHGlobal((Int32)m_nBufSizeForSaveImage);
                    }

                    stConverPixelParam.nWidth = m_stFrameInfo.nWidth;
                    stConverPixelParam.nHeight = m_stFrameInfo.nHeight;
                    stConverPixelParam.pSrcData = m_BufForDriver;
                    stConverPixelParam.nSrcDataLen = m_stFrameInfo.nFrameLen;
                    stConverPixelParam.enSrcPixelType = m_stFrameInfo.enPixelType;
                    stConverPixelParam.enDstPixelType = enDstPixelType;
                    stConverPixelParam.pDstBuffer = m_BufForSaveImage;
                    stConverPixelParam.nDstBufferSize = m_nBufSizeForSaveImage;
                    int nRet = m_MyCamera.MV_CC_ConvertPixelType_NET(ref stConverPixelParam);
                    if (MyCamera.MV_OK != nRet)
                    {
                        ShowErrorMsg("转换像素类型失败！", nRet);
                        return;
                    }
                    pTemp = m_BufForSaveImage;
                }
            }

            string SaveString = "./" + DateTime.Now.ToString() + ".bmp";
            lock (BufForDriverLock)
            {
                if (enDstPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                {
                    //************************Mono8 转 Bitmap*******************************
                    Bitmap bmp = new Bitmap(m_stFrameInfo.nWidth, m_stFrameInfo.nHeight, m_stFrameInfo.nWidth * 1, PixelFormat.Format8bppIndexed, pTemp);

                    ColorPalette cp = bmp.Palette;
                    // 初始化调色板
                    for (int i = 0; i < 256; i++)
                    {
                        cp.Entries[i] = Color.FromArgb(i, i, i);
                    }
                    bmp.Palette = cp;
                    bmp.Save(SaveString, ImageFormat.Bmp);
                }
                else
                {
                    //*********************BGR8 转 Bitmap**************************
                    try
                    {
                        Bitmap bmp = new Bitmap(m_stFrameInfo.nWidth, m_stFrameInfo.nHeight, m_stFrameInfo.nWidth * 3, PixelFormat.Format24bppRgb, pTemp);
                        bmp.Save(SaveString, ImageFormat.Bmp);
                    }
                    catch
                    {
                        ShowErrorMsg("保存失败！", 0);
                    }
                }
            }
            ShowErrorMsg("保存成功！", 0);
        }
        
        //停止测试
        private void bt_StopTest_Click(object sender, EventArgs e)
        {
            if(false == IsInLine)
            {
                thread_OutLineTest.Abort();
            }
            //设置控件状态
            bt_StopTest.Enabled = false;
            bt_StartTest.Enabled = true;
            rbt_Measure9.Enabled = true;
            rbt_Measure18.Enabled = true;
            bt_SaveCSV.Enabled = true;
            bt_ClearData.Enabled = true;
        }

        //ListView中数据保存为CSV
        public void DataWriteToCSV(string filePath, ListView listview)
        {
            string path = filePath;
            int rowNum = listview.Items.Count;
            int column = listview.Items[0].SubItems.Count;
            int rowIndex;
            int columnIndex = 0;

            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }

            StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);

            if (rowNum != 0)
            {
                //将ListView的列名导入Excel表的第一行
                foreach (ColumnHeader dc in listview.Columns)
                {
                    columnIndex++;
                    sw.Write(dc.Text + ",");
                }

                for (rowIndex = 2; rowIndex < listview.Items.Count + 2; rowIndex++)
                {
                    sw.Write("\r\n" + listview.Items[rowIndex - 2].Text + ",");
                    for (columnIndex = 2; columnIndex <= listview.Columns.Count; columnIndex++)
                    {
                        sw.Write(listview.Items[rowIndex - 2].SubItems[columnIndex - 1].Text + ",");
                    }
                }
            }
            sw.Flush();
            sw.Close();
        }

        //选择在线模式
        private void rbt_inLineMode_CheckedChanged_1(object sender, EventArgs e)
        {
            if(rbt_inLineMode.Checked==true)
            {
                IsInLine = true;
            }
            if (rbt_inLineMode.Checked == true && MeasureProject == 9)
            {
                bt_StartTest.Enabled = false;
                bt_DiscoverCamera.Enabled = true;
            }
            else if(rbt_inLineMode.Checked == true && MeasureProject == 18)
            {
                _buttonInitialize.Enabled = true;
                _buttonStartHighSpeedDataCommunication.Enabled = true;
                _buttonStartMeasure.Enabled = true;
            }
        }

        //选择离线模式
        private void rbt_outLineMode_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbt_outLineMode.Checked == true)
            {
                bt_StartTest.Enabled = true;
                bt_DiscoverCamera.Enabled = false;
                _buttonInitialize.Enabled = false;
                _buttonStartHighSpeedDataCommunication.Enabled = false;
                _buttonStartMeasure.Enabled = false;
            }
        }

        private void UpdateBatchSimpleArrayEnable()
        {
            Color colorEnabled = Color.FromArgb(255, 192, 218, 255);
            Color colorDisabled = Color.LightGray;
         }

        private void _checkBoxStartTimer_CheckedChanged(object sender, EventArgs e)
        {
            bool isStart = _checkBoxStartTimer.Checked;
            if (isStart)
            {
                _timerHighSpeedReceive.Interval = (int)_numericUpDownInterval.Value;
                _timerHighSpeedReceive.Start();
            }
            else
            {
                _timerHighSpeedReceive.Stop();
            }
            _numericUpDownInterval.Enabled = !isStart;
        }

        private void _buttonInitialize_Click(object sender, EventArgs e)
        {
            int rc = NativeMethods.LJX8IF_Initialize();
            AddLogResult(rc, Resources.IDS_INITIALIZE);

            for (int i = 0; i < _deviceData.Length; i++)
            {
                _deviceData[i].Status = DeviceStatus.NoConnection;
            }

            bool openEthernetSucceed = Open_Ethernet();

            //SetIntervalPoints();
            //SetNumProfile();

            if (openEthernetSucceed)
            {
                Initialize_HighSpeedData_Communication_SimpleArray();
            }

            btn_Set3DProgram.Enabled = true;
            btn_Get3DProgram.Enabled = true;
        }

        //设置细化点数
        private void SetIntervalPoints()
        {
            byte[] hex = new byte[4];
            using (PinnedObject pin = new PinnedObject(hex))
            {
                uint dwDataSize = 4;
                uint Error = 0;
                try
                {
                    LJX8IF_TARGET_SETTING Target_Setting = new LJX8IF_TARGET_SETTING()
                    {
                        byCategory = 0x0,
                        byItem = 0x09,
                        byTarget1 = 0x0,
                        byTarget2 = 0x0,
                        byTarget3 = 0x0,
                        byTarget4 = 0x0,
                        byType = 0X10,
                        reserve = 4
                    };
                    int line = Int32.Parse(numericUpDownIntervalPoints.Value.ToString());
                    hex[0] = (byte)(line & 0xff);
                    hex[1] = (byte)((line >> 8) & 0xff);   //先右移再与操作
                    hex[2] = (byte)((line >> 16) & 0xff);
                    hex[3] = (byte)((line >> 24) & 0xff);
                    int rc = NativeMethods.LJX8IF_SetSetting(0, 0x01, Target_Setting, pin.Pointer, dwDataSize, ref Error);

                    if( rc == (int)Rc.Ok)
                    {
                        AddLog("Set Refinement points Succeed");
                    }
                    else
                    {
                        AddLog("Set Refinement points failure");
                    }
                }
                catch
                {
                    AddLog("Set Refinement points failure");
                }
                }
        }
                
        //设置批处理点数
        private void SetNumProfile()
        {
            int m_CountProfile = Convert.ToInt32(m_NumOfProfile.Text);
            String strA = m_CountProfile.ToString("x8");
            byte[] _data = new byte[4];
            string[] parameterTexts = new string[4];
            parameterTexts[0] = strA.Substring(0, 2);
            parameterTexts[1] = strA.Substring(2, 2);
            parameterTexts[2] = strA.Substring(4, 2);
            parameterTexts[3] = strA.Substring(6, 2);
            if (0 < parameterTexts.Length)
            {
                _data = Array.ConvertAll(parameterTexts,
                    delegate (string text) { return Convert.ToByte(text, 16); });
            }
            Array.Resize(ref _data, 4);
            uint error = 0;
            LJX8IF_TARGET_SETTING _targetSetting = new LJX8IF_TARGET_SETTING();
            _targetSetting.byType = 16;
            _targetSetting.byCategory = 0;
            _targetSetting.byItem = 10;
            _targetSetting.byTarget1 = 0;
            _targetSetting.byTarget2 = 0;
            _targetSetting.byTarget3 = 0;
            _targetSetting.byTarget4 = 0;
            using (PinnedObject pin = new PinnedObject(_data))
            {
                int rc = NativeMethods.LJX8IF_SetSetting(0, 2, _targetSetting,
                    pin.Pointer, 4, ref error);
            }
        }

        private bool Open_Ethernet()
        {
            using (OpenEthernetForm openEthernetForm = new OpenEthernetForm())
            {
                if (DialogResult.OK != openEthernetForm.ShowDialog())
                {
                    return false;
                }

                LJX8IF_ETHERNET_CONFIG ethernetConfig = openEthernetForm.EthernetConfig;

                int rc = NativeMethods.LJX8IF_EthernetOpen(_currentDeviceId, ref ethernetConfig);
                AddLogResult(rc, Resources.IDS_ETHERNET_OPEN);

                if (rc == (int)Rc.Ok)
                {
                    _deviceData[_currentDeviceId].Status = DeviceStatus.Ethernet;
                    _deviceData[_currentDeviceId].EthernetConfig = ethernetConfig;
                    return true;
                }
                else
                {
                    _deviceData[_currentDeviceId].Status = DeviceStatus.NoConnection;
                    return false;
                }
            }
        }

        private void AddLogResult(int rc, string commandName)
        {
            if (rc == (int)Rc.Ok)
            {
                AddLog(string.Format(Resources.IDS_LOG_FORMAT, commandName, Resources.IDS_RESULT_OK, rc));
            }
            else
            {
                AddLog(string.Format(Resources.IDS_LOG_FORMAT, commandName, Resources.IDS_RESULT_NG, rc));
                AddErrorLog(rc);
            }
        }

        private void CommonErrorLog(int rc)
        {
            switch (rc)
            {
                case (int)Rc.Ok:
                    AddLog(string.Format(Resources.IDS_RC_FORMAT, Resources.IDS_RC_OK));
                    break;
                case (int)Rc.ErrOpenDevice:
                    AddLog(string.Format(Resources.IDS_RC_FORMAT, Resources.IDS_RC_ERR_OPEN_DEVICE));
                    break;
                case (int)Rc.ErrNoDevice:
                    AddLog(string.Format(Resources.IDS_RC_FORMAT, Resources.IDS_RC_ERR_NO_DEVICE));
                    break;
                case (int)Rc.ErrSend:
                    AddLog(string.Format(Resources.IDS_RC_FORMAT, Resources.IDS_RC_ERR_SEND));
                    break;
                case (int)Rc.ErrReceive:
                    AddLog(string.Format(Resources.IDS_RC_FORMAT, Resources.IDS_RC_ERR_RECEIVE));
                    break;
                case (int)Rc.ErrTimeout:
                    AddLog(string.Format(Resources.IDS_RC_FORMAT, Resources.IDS_RC_ERR_TIMEOUT));
                    _deviceData[_currentDeviceId].Status = DeviceStatus.NoConnection;
                    break;
                case (int)Rc.ErrParameter:
                    AddLog(string.Format(Resources.IDS_RC_FORMAT, Resources.IDS_RC_ERR_PARAMETER));
                    break;
                case (int)Rc.ErrNomemory:
                    AddLog(string.Format(Resources.IDS_RC_FORMAT, Resources.IDS_RC_ERR_NOMEMORY));
                    break;
                case (int)Rc.ErrHispeedNoDevice:
                    AddLog(string.Format(Resources.IDS_RC_FORMAT, Resources.IDS_RC_ERR_HISPEED_NO_DEVICE));
                    break;
                case (int)Rc.ErrHispeedOpenYet:
                    AddLog(string.Format(Resources.IDS_RC_FORMAT, Resources.IDS_RC_ERR_HISPEED_OPEN_YET));
                    break;
                case (int)Rc.ErrBufferShort:
                    AddLog(string.Format(Resources.IDS_RC_FORMAT, Resources.IDS_RC_ERR_BUFFER_SHORT));
                    break;
                default:
                    AddLog(string.Format(Resources.IDS_NOT_DEFINE_FROMAT, rc));
                    break;
            }
        }

        private bool ControllerErrorLog(int rc)
        {
            switch (rc)
            {
                case 0x8011:
                    return true;
                case 0x8021:
                    AddLog(string.Format(Resources.IDS_RC_FORMAT, @"Controller model difference"));
                    return true;
                case 0x8031:
                    AddLog(string.Format(Resources.IDS_RC_FORMAT, @"Undefined command"));
                    return true;
                case 0x8032:
                    AddLog(string.Format(Resources.IDS_RC_FORMAT, @"Command length error"));
                    return true;
                case 0x8041:
                    AddLog(string.Format(Resources.IDS_RC_FORMAT, @"Controller status error"));
                    return true;
                case 0x8042:
                    AddLog(string.Format(Resources.IDS_RC_FORMAT, @"Controller parameter error"));
                    return true;
            }
            return false;
        }

        private void AddErrorLog(int rc)
        {
            if (rc < 0x8000)
            {
                // Common return code
                CommonErrorLog(rc);
            }
            else
            {
                // Controller return code
                if (ControllerErrorLog(rc))
                {
                    return;
                }
                // Individual return code
                IndividualErrorLog(rc);
            }
        }

        private void IndividualErrorLog(int rc)
        {
            switch (_sendCommand)
            {
                case SendCommand.RebootController:
                    {
                        switch (rc)
                        {
                            case 0x80A0:
                                AddLog(string.Format(Resources.IDS_RC_FORMAT, @"Accessing the save area"));
                                break;
                            default:
                                AddLog(string.Format(Resources.IDS_NOT_DEFINE_FROMAT, rc));
                                break;
                        }
                    }
                    break;
                case SendCommand.Trigger:
                    {
                        switch (rc)
                        {
                            case 0x8080:
                                AddLog(string.Format(Resources.IDS_RC_FORMAT, @"The trigger mode is not [external trigger]"));
                                break;
                            default:
                                AddLog(string.Format(Resources.IDS_NOT_DEFINE_FROMAT, rc));
                                break;
                        }
                    }
                    break;
                case SendCommand.StartMeasure:
                case SendCommand.StopMeasure:
                    {
                        switch (rc)
                        {
                            case 0x8080:
                                AddLog(string.Format(Resources.IDS_RC_FORMAT, @"Batch measurements are off or several controller Sync function is Sync Slave"));
                                break;
                            case 0x80A0:
                                AddLog(string.Format(Resources.IDS_RC_FORMAT, @"Batch measurement start or stop processing could not be performed because laser off by command or the LASER_ON terminal is off"));
                                break;
                            default:
                                AddLog(string.Format(Resources.IDS_NOT_DEFINE_FROMAT, rc));
                                break;
                        }
                    }
                    break;
                case SendCommand.GetProfile:
                    {
                        switch (rc)
                        {
                            case 0x8081:
                                AddLog(string.Format(Resources.IDS_RC_FORMAT, @"Batch measurements on"));
                                break;
                            case 0x80A0:
                                AddLog(string.Format(Resources.IDS_RC_FORMAT, @"No profile data"));
                                break;
                            default:
                                AddLog(string.Format(Resources.IDS_NOT_DEFINE_FROMAT, rc));
                                break;
                        }
                    }
                    break;
                case SendCommand.GetBatchProfile:
                    {
                        switch (rc)
                        {
                            case 0x8081:
                                AddLog(string.Format(Resources.IDS_RC_FORMAT, @"Batch measurements off"));
                                break;
                            case 0x80A0:
                                AddLog(string.Format(Resources.IDS_RC_FORMAT, @"No batch data (batch measurements not run even once)"));
                                break;
                            default:
                                AddLog(string.Format(Resources.IDS_NOT_DEFINE_FROMAT, rc));
                                break;
                        }
                    }
                    break;
                case SendCommand.InitializeHighSpeedDataCommunication:
                    {
                        switch (rc)
                        {
                            case 0x80A1:
                                AddLog(string.Format(Resources.IDS_RC_FORMAT, @"Already performing high-speed communication error (for high-speed communication)"));
                                break;
                            default:
                                AddLog(string.Format(Resources.IDS_NOT_DEFINE_FROMAT, rc));
                                break;
                        }
                    }
                    break;
                case SendCommand.PreStartHighSpeedDataCommunication:
                case SendCommand.StartHighSpeedDataCommunication:
                    {
                        switch (rc)
                        {
                            case 0x8081:
                                AddLog(string.Format(Resources.IDS_RC_FORMAT, @"The data specified as the send start position does not exist"));
                                break;
                            case 0x80A0:
                                AddLog(string.Format(Resources.IDS_RC_FORMAT, @"A high-speed data communication connection was not established"));
                                break;
                            case 0x80A1:
                                AddLog(string.Format(Resources.IDS_RC_FORMAT, @"Already performing high-speed communication error (for high-speed communication)"));
                                break;
                            case 0x80A2:
                                AddLog(string.Format(Resources.IDS_RC_FORMAT, @"Command code does not match (for high-speed communication)"));
                                break;
                            case 0x80A3:
                                AddLog(string.Format(Resources.IDS_RC_FORMAT, @"Start code does not match (for high-speed communication)"));
                                break;
                            case 0x80A4:
                                AddLog(string.Format(Resources.IDS_RC_FORMAT, @"The send target data was cleared"));
                                break;
                            default:
                                AddLog(string.Format(Resources.IDS_NOT_DEFINE_FROMAT, rc));
                                break;
                        }
                    }
                    break;
                default:
                    AddLog(string.Format(Resources.IDS_NOT_DEFINE_FROMAT, rc));
                    break;
            }
        }

        private void AddLog(string strLog)
        {
            _textBoxLog.AppendText(strLog + Environment.NewLine);
            _textBoxLog.SelectionStart = _textBoxLog.Text.Length;
            _textBoxLog.Focus();
            _textBoxLog.ScrollToCaret();
        }

        private void Initialize_HighSpeedData_Communication_SimpleArray()
        {
            _sendCommand = SendCommand.InitializeHighSpeedDataCommunication;

            using (HighSpeedInitializeForm highSpeedInitializeForm = new HighSpeedInitializeForm())
            {
                highSpeedInitializeForm.Text = Resources.IDS_INITIALIZE_HIGH_SPEED_DATA_ETHERNET_COMMUNICATION_SIMPLE_ARRAY;
                highSpeedInitializeForm.EthernetConfig = _deviceData[_currentDeviceId].EthernetConfig;

                if (DialogResult.OK != highSpeedInitializeForm.ShowDialog()) return;

                ThreadSafeBuffer.ClearBuffer(_currentDeviceId);  //Clear the retained profile data.
                _deviceData[_currentDeviceId].ProfileDataHighSpeed.Clear();
                _deviceData[_currentDeviceId].SimpleArrayDataHighSpeed.Clear();

                LJX8IF_ETHERNET_CONFIG ethernetConfig = highSpeedInitializeForm.EthernetConfig;
                int rc = NativeMethods.LJX8IF_InitializeHighSpeedDataCommunicationSimpleArray(_currentDeviceId, ref ethernetConfig,
                    highSpeedInitializeForm.HighSpeedPortNo, _callbackSimpleArray,
                    highSpeedInitializeForm.ProfileCount, (uint)_currentDeviceId);
                
                AddLogResult(rc, Resources.IDS_INITIALIZE_HIGH_SPEED_DATA_ETHERNET_COMMUNICATION_SIMPLE_ARRAY);

                if (rc == (int)Rc.Ok)
                {
                    _deviceData[_currentDeviceId].Status = DeviceStatus.EthernetFast;
                    _deviceData[_currentDeviceId].EthernetConfig = ethernetConfig;
                }
            }
            Pre_Start_HighSpeedDataCommunication();
        }

        private void Pre_Start_HighSpeedDataCommunication()
        {
            _sendCommand = SendCommand.PreStartHighSpeedDataCommunication;

            using (PreStartHighSpeedForm preStartHighSpeedForm = new PreStartHighSpeedForm())
            {
                if (DialogResult.OK != preStartHighSpeedForm.ShowDialog()) return;

                LJX8IF_HIGH_SPEED_PRE_START_REQUEST request = preStartHighSpeedForm.Request;
                LJX8IF_PROFILE_INFO profileInfo = new LJX8IF_PROFILE_INFO();
 
                int rc = NativeMethods.LJX8IF_PreStartHighSpeedDataCommunication(_currentDeviceId, ref request, ref profileInfo);

                AddLogResult(rc, Resources.IDS_PRE_START_HIGH_SPEED_DATA_COMMUNICATION);
                if (rc != (int)Rc.Ok) return;

                // Response data display
                AddLog(Utility.ConvertProfileInfoToLogString(profileInfo).ToString());

                _deviceData[_currentDeviceId].SimpleArrayDataHighSpeed.Clear();
                _deviceData[_currentDeviceId].SimpleArrayDataHighSpeed.DataWidth = profileInfo.nProfileDataCount;
                _deviceData[_currentDeviceId].SimpleArrayDataHighSpeed.IsLuminanceEnable = profileInfo.byLuminanceOutput == 1;
                _profileInfo[_currentDeviceId] = profileInfo;
            }
        }

        private void _buttonStartHighSpeedDataCommunication_Click(object sender, EventArgs e)
        {
            _sendCommand = SendCommand.StartHighSpeedDataCommunication;

            ThreadSafeBuffer.ClearBuffer(_currentDeviceId);
            _deviceData[_currentDeviceId].ProfileDataHighSpeed.Clear();
            _isBufferFull[_currentDeviceId] = false;
            _isStopCommunicationByError[_currentDeviceId] = false;

            int rc = NativeMethods.LJX8IF_StartHighSpeedDataCommunication(_currentDeviceId);

            AddLogResult(rc, Resources.IDS_START_HIGH_SPEED_DATA_COMMUNICATION);

            if(_deviceData[_currentDeviceId].Status == DeviceStatus.EthernetFast)
            {
                _buttonStopHighSpeedDataCommunication.Enabled = true;
            }
        }

        private void _buttonStartMeasure_Click(object sender, EventArgs e)
        {
           if( _deviceData[_currentDeviceId].Status == DeviceStatus.EthernetFast)
            {
                _buttonStartMeasure.Enabled = false;
                _buttonStopMeasure.Enabled = true;
                _buttonHighSpeedSaveAsBitmapFile.Enabled = true;
            }
            _sendCommand = SendCommand.StartMeasure;
            int rc = NativeMethods.LJX8IF_StartMeasure(_currentDeviceId);
            AddLogResult(rc, Resources.IDS_START_MEASURE);
        }

        private void _buttonStopMeasure_Click(object sender, EventArgs e)
        {
            if (_deviceData[_currentDeviceId].Status == DeviceStatus.EthernetFast)
            {
                _buttonStartMeasure.Enabled = true;
                _buttonStopMeasure.Enabled = false;
                _buttonStopHighSpeedDataCommunication.Enabled = true;
            }
            _sendCommand = SendCommand.StopMeasure;
            int rc = NativeMethods.LJX8IF_StopMeasure(_currentDeviceId);
            AddLogResult(rc, Resources.IDS_STOP_MEASURE);

            Disp_ProfileByte2ptr();
            _buttonHighSpeedSaveAsBitmapFile.Enabled = true;
        }

        private void _buttonStopHighSpeedDataCommunication_Click(object sender, EventArgs e)
        {
            int rc = NativeMethods.LJX8IF_StopHighSpeedDataCommunication(_currentDeviceId);
            AddLogResult(rc, Resources.IDS_STOP_HIGH_SPEED_DATA_COMMUNICATION);

            if (_deviceData[_currentDeviceId].Status == DeviceStatus.EthernetFast)
            {
                _buttonStopHighSpeedDataCommunication.Enabled = false;
                _buttonFinalizeHighSpeedDataCommunication.Enabled = true;
                _buttonStartMeasure.Enabled = true;
            }
        }

        private void _buttonFinalizeHighSpeedDataCommunication_Click(object sender, EventArgs e)
        {
            int rc = NativeMethods.LJX8IF_FinalizeHighSpeedDataCommunication(_currentDeviceId);
            AddLogResult(rc, Resources.IDS_FINALIZE_HIGH_SPEED_DATA_COMMUNICATION);

            switch (_deviceData[_currentDeviceId].Status)
            {
                case DeviceStatus.EthernetFast:
                    LJX8IF_ETHERNET_CONFIG config = _deviceData[_currentDeviceId].EthernetConfig;
                    _deviceData[_currentDeviceId].Status = DeviceStatus.Ethernet;
                    _deviceData[_currentDeviceId].EthernetConfig = config;
                    break;
            }

            if (_deviceData[_currentDeviceId].Status == DeviceStatus.Ethernet)
            {
                _buttonFinalizeHighSpeedDataCommunication.Enabled = false;
                _buttonInitialize.Enabled = true;
            }              
        }

        private void _buttonHighSpeedProfileFileSave_Click(object sender, EventArgs e)
        {
            if (_profileOrBitmapFileSave.ShowDialog(this) == DialogResult.Cancel) return;

            _textBoxHighSpeedProfileFilePath.Text = _profileOrBitmapFileSave.FileName;
            _textBoxHighSpeedProfileFilePath.SelectionStart = _textBoxHighSpeedProfileFilePath.Text.Length;
        }

        private void _buttonHighSpeedSaveAsBitmapFile_Click(object sender, EventArgs e)
        {
            int width = _deviceData[_currentDeviceId].SimpleArrayDataHighSpeed.DataWidth;
            if (width == 0)
            {
                AddLog("No simple array data.");
                return;
            }

            if (string.IsNullOrEmpty(_textBoxHighSpeedProfileFilePath.Text))
            {
                AddLog("Failed to save image. (File path is empty.)");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            int startIndex = 0;
            int dataCount = (int)_numericUpDownProfileSaveCount.Value;
            bool result = _deviceData[_currentDeviceId].SimpleArrayDataHighSpeed.SaveDataAsImages(_textBoxHighSpeedProfileFilePath.Text, startIndex, dataCount);
            
            AddLog(result ? "Succeed to save image." : "Failed to save image.");
        }

        private void AddError(uint error)
        {
            _textBoxLog.AppendText("  ErrorCode : 0x" + error.ToString("x8") + Environment.NewLine);
            _textBoxLog.SelectionStart = _textBoxLog.Text.Length;
            _textBoxLog.Focus();
            _textBoxLog.ScrollToCaret();
        }

        private IntPtr ProfileByte2ptr(out int m_3DWidth, out int m_3DHeight)
        {
            byte[] _profileImage = _deviceData[_currentDeviceId].SimpleArrayDataHighSpeed.GetImageByte(0, (int)_deviceData[_currentDeviceId].SimpleArrayDataHighSpeed.Count);
            m_3DWidth = _deviceData[_currentDeviceId].SimpleArrayDataHighSpeed.DataWidth;
            m_3DHeight = _profileImage.Count() / 2 / m_3DWidth;
            IntPtr ptr = Marshal.AllocHGlobal(_profileImage.Length);
            Marshal.Copy(_profileImage, 0, ptr, _profileImage.Length);

            return ptr;
        }

        private void Disp_ProfileByte2ptr()
        {
            int m_3DWidth, m_3DHeight;
            IntPtr ptr = ProfileByte2ptr(out m_3DWidth, out m_3DHeight);

            HAlgorithm.DispBuffer( MeasureProject, ptr, (ushort)m_3DWidth, (ushort)m_3DHeight);
        }

        private void ClearMemory()
        {
            int rc = NativeMethods.LJX8IF_ClearMemory(0);
        }

        private void _timerHighSpeed_Tick(object sender, EventArgs e)
        {
            uint notify;
            int batchNo;
            List<int[]> data = ThreadSafeBuffer.Get(Define.DeviceId, out notify, out batchNo);

            int count = data.Count;

            if ((uint)(notify & 0xFFFF) != 0)
            {
                // If the lower 16 bits of the notification are not 0, high-speed communication was interrupted, so stop the timer.
                _timerHighSpeed.Stop();
                MessageBox.Show(this, string.Format("Finalize communication :Notify = 0x{0:x8}", notify));
            }

            if ((uint)(notify & 0x10000) != 0)
            {
                // A descriptor is included here if processing when batch measurement is finished is required.
            }
        }

        private void _timerBufferError_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < NativeMethods.DeviceCount; i++)
            {
                if ((_isBufferFull[i]) && (!_isStopCommunicationByError[i]))
                {
                    _isStopCommunicationByError[i] = true;
                    NativeMethods.LJX8IF_StopHighSpeedDataCommunication(i);
                    NativeMethods.LJX8IF_FinalizeHighSpeedDataCommunication(i);
                    Invoke(new InvokeDelagate(ShowBufferFullMessage));
                }
            }
        }

        private void btn_Set3DProgram_Click(object sender, EventArgs e)
        {
            byte[] hex = new byte[4];
            using (PinnedObject pin = new PinnedObject(hex))
            {
                uint dwDataSize = 4;
                uint Error = 0;

                LJX8IF_TARGET_SETTING Target_Setting = new LJX8IF_TARGET_SETTING()
                {
                    byCategory = 0x0,
                    byItem = 0x09,
                    byTarget1 = 0x0,
                    byTarget2 = 0x0,
                    byTarget3 = 0x0,
                    byTarget4 = 0x0,
                    byType = 0X10,
                    reserve = 4
                };
                int line = Int32.Parse(numericUpDownIntervalPoints.Text);
                hex[0] = (byte)(line & 0xff);
                hex[1] = (byte)((line >> 8) & 0xff);   //先右移再与操作
                hex[2] = (byte)((line >> 16) & 0xff);
                hex[3] = (byte)((line >> 24) & 0xff);
                int rc = NativeMethods.LJX8IF_SetSetting(0, 0x01, Target_Setting, pin.Pointer, dwDataSize, ref Error);
                //  AddLogResult(rc, Resources.IDS_SET_SETTING);

                //Enum.GetName(typeof(Samplingperiod), setting.ToInt32());
                Target_Setting.byItem = 0x0A;
                int rows = Int32.Parse(m_NumOfProfile.Text);
                hex[0] = (byte)(rows & 0xff);
                hex[1] = (byte)((rows >> 8) & 0xff);   //先右移再与操作
                hex[2] = (byte)((rows >> 16) & 0xff);
                hex[3] = (byte)((rows >> 24) & 0xff);
                rc = NativeMethods.LJX8IF_SetSetting(0, 0x02, Target_Setting, pin.Pointer, dwDataSize, ref Error);
                // AddLogResult(rc, Resources.IDS_SET_SETTING);

                //LineSpace = setting.ToInt32();
                //Target_Setting.byType = 0X10;采样周期
                //int HzSelect = Int32.Parse(_comboBoxLjxSamplingPeriod.SelectedIndex.ToString());
                //Target_Setting.byItem = 0x02;
                //hex[0] = (byte)(HzSelect & 0xff);
                // hex[1] = (byte)((HzSelect >> 8) & 0xff);   //先右移再与操作
                // hex[2] = (byte)((HzSelect >> 16) & 0xff);
                //hex[3] = (byte)((HzSelect >> 24) & 0xff);
                // rc = NativeMethods.LJX8IF_SetSetting(0, 0x01, Target_Setting, pin.Pointer, dwDataSize, ref Error);
                AddLogResult(rc, Resources.IDS_SET_SETTING);
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] hex = new byte[4];
            using (PinnedObject pin = new PinnedObject(hex))
            {
                uint dwDataSize = 4;

            LJX8IF_TARGET_SETTING Target_Setting = new LJX8IF_TARGET_SETTING()
            {
                byCategory = 0x0,
                byItem = 0x09,
                byTarget1 = 0x0,
                byTarget2 = 0x0,
                byTarget3 = 0x0,
                byTarget4 = 0x0,
                byType = 0X10
            };
            int rc = NativeMethods.LJX8IF_GetSetting(0, 0x02, Target_Setting, pin.Pointer, dwDataSize);
            if (rc == (int)Rc.Ok)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < dwDataSize; i++)
                {
                    stringBuilder.Insert(0, string.Format("{0:x2}", hex[i]));
                }
                numericUpDownIntervalPoints.Text = System.Convert.ToString(System.Convert.ToInt32(stringBuilder.ToString(), 16));
            }
            else
            {
                AddLogResult(rc, Resources.IDS_GET_SETTING);
                return;
            }
            ////Target_Setting.byType = 0X10;采样周期
            //Target_Setting.byItem = 0x02;
            //rc = NativeMethods.LJX8IF_GetSetting(0, 0x02, Target_Setting, pin.Pointer, dwDataSize);
            //if (rc == (int)Rc.Ok)
            //{
            //    StringBuilder stringBuilder = new StringBuilder();
            //    for (int i = 0; i < dwDataSize; i++)
            //    {
            //        stringBuilder.Insert(0, string.Format("{0:x2}", hex[i]));
            //    }
            //    _comboBoxLjxSamplingPeriod.SelectedItem = System.Convert.ToInt32(stringBuilder.ToString(), 16);

            //}
            //else
            //{
            //    AddLogResult(rc, Resources.IDS_GET_SETTING);
            //}
            Target_Setting.byItem = 0x0A;
            rc = NativeMethods.LJX8IF_GetSetting(0, 0x02, Target_Setting, pin.Pointer, dwDataSize);
            if (rc == (int)Rc.Ok)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < dwDataSize; i++)
                {
                    stringBuilder.Insert(0, string.Format("{0:x2}", hex[i]));
                }
                m_NumOfProfile.Text = System.Convert.ToString(System.Convert.ToInt32(stringBuilder.ToString(), 16));

            }
            else
            {
                AddLogResult(rc, Resources.IDS_GET_SETTING);
            }

            }
        }

        private void ShowBufferFullMessage()
        {
            MessageBox.Show(this, "Receive buffer is full.");
        }

        private void _timerHighSpeedReceive_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < NativeMethods.DeviceCount; i++)
            {
                uint notify;
                int batchNo;
                //Simple array data performed to conversion and storing on .
                notify = _deviceData[i].SimpleArrayDataHighSpeed.Notify;
                batchNo = _deviceData[i].SimpleArrayDataHighSpeed.BatchNo;
                if (notify == 0)  continue;
                AddLog(string.Format("  notify[{0}] = {1,0:x8}\tbatch[{0}] = {2}", i, notify, batchNo));
            }
        }

        // 控件大小随窗体大小等比例缩放
        #region 
        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度

        void changeSize()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲DoubleBuffer

            this.WindowState = FormWindowState.Maximized;
            x = this.Width;
            y = this.Height;
            setTag(this);
        }
        
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }
        //设置双缓冲区、解决闪屏问题
        public static void SetDouble(Control cc)
        {
            cc.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance |
                         System.Reflection.BindingFlags.NonPublic).SetValue(cc, true, null);
        }

        private void setControls(float newx, float newy, Control cons)
        {
            //遍历窗体中的控件，重新设置控件的值
            foreach (Control con in cons.Controls)
            {
                //获取控件的Tag属性值，并分割后存储字符串数组
                SetDouble(this);
                SetDouble(con);
                if (con.Tag != null)
                {
                    string[] mytag = con.Tag.ToString().Split(new char[] { ';' });
                    //根据窗体缩放的比例确定控件的值
                    con.Width = Convert.ToInt32(System.Convert.ToSingle(mytag[0]) * newx);//宽度
                    con.Height = Convert.ToInt32(System.Convert.ToSingle(mytag[1]) * newy);//高度
                    con.Left = Convert.ToInt32(System.Convert.ToSingle(mytag[2]) * newx);//左边距
                    con.Top = Convert.ToInt32(System.Convert.ToSingle(mytag[3]) * newy);//顶边距
                    Single currentSize = System.Convert.ToSingle(mytag[4]) * newy;//字体大小
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                    if (con.Controls.Count > 0)
                    {
                        setControls(newx, newy, con);
                    }
                }
            }
        }

        private void MyWindow_Resize(object sender, EventArgs e)
        {
            HAlgorithm.DisposeWindow();
            float newx = (this.Width) / x;
            float newy = (this.Height) / y;
            setControls(newx, newy, this);
                       
            HAlgorithm.CreateInWindow(pb_in.Handle, pb_in.Width, pb_in.Height);
            HAlgorithm.CreateOutWindow(pb_out.Handle, pb_out.Width, pb_out.Height);
        }
        #endregion
    }
}
