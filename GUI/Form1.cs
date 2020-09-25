using System;
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
using LJX8_DllSampleAll.Forms;
using LJX8_DllSampleAll.Properties;
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

        #region Enum

        /// <summary>
        /// Send command definition
        /// </summary>
        /// <remark>Defined for separate return code distinction</remark>
        private enum SendCommand
        {
            /// <summary>None</summary>
            None,
            /// <summary>Restart</summary>
            RebootController,
            /// <summary>Trigger</summary>
            Trigger,
            /// <summary>Start measurement</summary>
            StartMeasure,
            /// <summary>Stop measurement</summary>
            StopMeasure,
            /// <summary>Get profiles</summary>
            GetProfile,
            /// <summary>Get batch profiles</summary>
            GetBatchProfile,
            /// <summary>Initialize Ethernet high-speed data communication</summary>
            InitializeHighSpeedDataCommunication,
            /// <summary>Request preparation before starting high-speed data communication</summary>
            PreStartHighSpeedDataCommunication,
            /// <summary>Start high-speed data communication</summary>
            StartHighSpeedDataCommunication,
        }

        #endregion

        #region Field

        /// <summary>Ethernet settings structure </summary>
        private LJX8IF_ETHERNET_CONFIG _ethernetConfig;
        /// <summary>Current device ID</summary>
        private int _currentDeviceId;
        /// <summary>Send command</summary>
        private SendCommand _sendCommand;
        /// <summary>Callback function used during high-speed communication</summary>
        private HighSpeedDataCallBack _callback;
        /// <summary>Callback function used during high-speed communication (count only)</summary>
        private HighSpeedDataCallBack _callbackOnlyCount;
        /// <summary>Callback function used during high-speed communication (simple array)</summary>
        private HighSpeedDataCallBackForSimpleArray _callbackSimpleArray;
        /// <summary>Callback function used during high-speed communication (simple array) (count only)</summary>
        private HighSpeedDataCallBackForSimpleArray _callbackSimpleArrayOnlyCount;
        /// The following are maintained in arrays to support multiple controllers.
        /// <summary>Array of profile information structures</summary>
        private LJX8IF_PROFILE_INFO[] _profileInfo;
        /// <summary>Array of controller information</summary>
        private DeviceData[] _deviceData;
        /// <summary>Array of labels that indicate the controller status</summary>
        private Label[] _deviceStatusLabels;
        /// <summary>Array of labels that indicate the number of received profiles </summary>
        private Label[] _receivedProfileCountLabels;
        /// <summary>Array of value of receive buffer is full</summary>
        private static bool[] _isBufferFull = new bool[NativeMethods.DeviceCount];
        /// <summary>Array of value of stop processing has done by buffer full error</summary>
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
        #endregion

        public MyWindow()
        {
            InitializeComponent();
            changeSize();
            HAlgorithm.CreateInWindow(pb_in.Handle, pb_in.Width, pb_in.Height);
            HAlgorithm.CreateOutWindow(pb_out.Handle, pb_out.Width, pb_out.Height);
            
            Control.CheckForIllegalCrossThreadCalls = false;
            OutLineModeState();

            UpdateHighSpeedProfileSaveEnable();



            thread_OutLineTest = new Thread(new ThreadStart(thread_OutLineTest_Start));
        }

        private void UpdateHighSpeedProfileSaveEnable()
        {
            //bool isOnlyProfileCountChecked = _checkBoxOnlyProfileCount.Checked;
            bool isOnlyProfileCountChecked = false;
            //bool isUseSimpleArray = _checkBoxUseSimpleArray.Checked;
            bool isUseSimpleArray = true;

            _textBoxHighSpeedProfileFilePath.Enabled = !isOnlyProfileCountChecked;
            //_numericUpDownProfileNo.Enabled = !isOnlyProfileCountChecked;
            _numericUpDownProfileSaveCount.Enabled = !isOnlyProfileCountChecked;
            //_buttonHighSpeedSave.Enabled = !isOnlyProfileCountChecked && !isUseSimpleArray;
            _buttonHighSpeedProfileFileSave.Enabled = !isOnlyProfileCountChecked;
            _buttonHighSpeedSaveAsBitmapFile.Enabled = !isOnlyProfileCountChecked && isUseSimpleArray;
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
                         out DistanceX1, out DistanceY1, OutLineFilePath);

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
                              out DistanceX1, out DistanceY1, OutLineFilePath);
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
            rbt_Measure18.Enabled = true;

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

                    HAlgorithm.DispBuffer(m_BufForDriver, stFrameInfo.nWidth, stFrameInfo.nHeight);
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

            //控件操作 
        }

        //选择测试项
        #region

        private void rbt_Measure9_CheckedChanged(object sender, EventArgs e)
        {
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
            HAlgorithm.InitListView2D(lv_AllFrameData);
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
                        stFrameInfo.nHeight, out Radius, out PositionDegree, out RunTime, out DistanceX1, out DistanceY1);
                    break;
                }
            }

            //保存异常图像
            if(PositionDegree <0 || PositionDegree >0.06 || Radius <9.40 || Radius > 9.50)
            {
                SaveErrorBMP();
            }
            //设置控件状态
            bt_StopGrab.Enabled = false;
            bt_StopTest.Enabled = true;
            bt_StartTest.Enabled = false;
            rbt_Measure9.Enabled = false;
            rbt_Measure18.Enabled = false;

            bt_ClearData.Enabled = false;
            bt_SaveCSV.Enabled = false;
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

        private void rbt_Measure18_CheckedChanged(object sender, EventArgs e)
        {
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

        private void rbt_inLineMode_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbt_inLineMode.Checked == true)
            {
                bt_StartTest.Enabled = false;
                bt_DiscoverCamera.Enabled = true;
            }
        }

        private void rbt_outLineMode_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbt_outLineMode.Checked == true)
            {
                bt_StartTest.Enabled = true;
                bt_DiscoverCamera.Enabled = false;
            }
        }

        // 控件大小随窗体大小等比例缩放
        #region 
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

        private float x;//定义当前窗体的宽度
        private float y;//定义当前窗体的高度

        private void _textBoxHighSpeedProfileFilePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void _checkBoxUseSimpleArray_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBatchSimpleArrayEnable();
        }

        private void UpdateBatchSimpleArrayEnable()
        {
            Color colorEnabled = Color.FromArgb(255, 192, 218, 255);
            Color colorDisabled = Color.LightGray;


            //bool isUseSimpleArray = _checkBoxUseSimpleArray.Checked;
            bool isUseSimpleArray = true;

            //bool isOnlyProfileCountChecked = _checkBoxOnlyProfileCount.Checked;
            bool isOnlyProfileCountChecked = false;

            //_buttonInitializeHighSpeedDataCommunication.Enabled = !isUseSimpleArray;
            //_buttonInitializeHighSpeedDataCommunication.BackColor = !isUseSimpleArray ? colorEnabled : colorDisabled;
            _buttonInitializeHighSpeedDataCommunicationSimpleArray.Enabled = isUseSimpleArray;
            _buttonInitializeHighSpeedDataCommunicationSimpleArray.BackColor = isUseSimpleArray ? colorEnabled : colorDisabled;

            //_buttonHighSpeedSave.Enabled = !isUseSimpleArray && !isOnlyProfileCountChecked;
            _buttonHighSpeedSaveAsBitmapFile.Enabled = isUseSimpleArray && !isOnlyProfileCountChecked;
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
            //_checkBoxOnlyProfileCount.Enabled = !isStart;
        }

        private void _buttonInitialize_Click(object sender, EventArgs e)
        {
            int rc = NativeMethods.LJX8IF_Initialize();
            AddLogResult(rc, Resources.IDS_INITIALIZE);

            for (int i = 0; i < _deviceData.Length; i++)
            {
                _deviceData[i].Status = DeviceStatus.NoConnection;
                _deviceStatusLabels[i].Text = _deviceData[i].GetStatusString();
                _receivedProfileCountLabels[i].Text = "0";
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
                    _deviceStatusLabels[_currentDeviceId].Text = _deviceData[_currentDeviceId].GetStatusString();
                    _receivedProfileCountLabels[_currentDeviceId].Text = "0";
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

        private void _buttonEthernetOpen_Click(object sender, EventArgs e)
        {
            using (OpenEthernetForm openEthernetForm = new OpenEthernetForm())
            {
                if (DialogResult.OK != openEthernetForm.ShowDialog()) return;

                LJX8IF_ETHERNET_CONFIG ethernetConfig = openEthernetForm.EthernetConfig;
                // @Point
                // # Enter the "_currentDeviceId" set here for the communication settings into the arguments of each DLL function.
                // # If you want to get data from multiple controllers, prepare and set multiple "_currentDeviceId" values,
                //   enter these values into the arguments of the DLL functions, and then use the functions.

                int rc = NativeMethods.LJX8IF_EthernetOpen(_currentDeviceId, ref ethernetConfig);
                AddLogResult(rc, Resources.IDS_ETHERNET_OPEN);

                if (rc == (int)Rc.Ok)
                {
                    _deviceData[_currentDeviceId].Status = DeviceStatus.Ethernet;
                    _deviceData[_currentDeviceId].EthernetConfig = ethernetConfig;
                }
                else
                {
                    _deviceData[_currentDeviceId].Status = DeviceStatus.NoConnection;
                }
                _deviceStatusLabels[_currentDeviceId].Text = _deviceData[_currentDeviceId].GetStatusString();
                _receivedProfileCountLabels[_currentDeviceId].Text = "0";
            }
        }

        private void _buttonInitializeHighSpeedDataCommunicationSimpleArray_Click(object sender, EventArgs e)
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
                    highSpeedInitializeForm.HighSpeedPortNo, _callbackSimpleArrayOnlyCount,
                    highSpeedInitializeForm.ProfileCount, (uint)_currentDeviceId);
                // @Point
                // # When the frequency of calls is low, the callback function may not be called once per specified number of profiles.
                // # The callback function is called when both of the following conditions are met.
                //   * There is one packet of received data.
                //   * The specified number of profiles have been received by the time the call frequency has been met.

                AddLogResult(rc, Resources.IDS_INITIALIZE_HIGH_SPEED_DATA_ETHERNET_COMMUNICATION_SIMPLE_ARRAY);

                if (rc == (int)Rc.Ok)
                {
                    _deviceData[_currentDeviceId].Status = DeviceStatus.EthernetFast;
                    _deviceData[_currentDeviceId].EthernetConfig = ethernetConfig;
                }
                _deviceStatusLabels[_currentDeviceId].Text = _deviceData[_currentDeviceId].GetStatusString();
                _receivedProfileCountLabels[_currentDeviceId].Text = "0";
            }
        }

        private void _buttonPreStartHighSpeedDataCommunication_Click(object sender, EventArgs e)
        {
            _sendCommand = SendCommand.PreStartHighSpeedDataCommunication;

            using (PreStartHighSpeedForm preStartHighSpeedForm = new PreStartHighSpeedForm())
            {
                if (DialogResult.OK != preStartHighSpeedForm.ShowDialog()) return;

                LJX8IF_HIGH_SPEED_PRE_START_REQUEST request = preStartHighSpeedForm.Request;
                // @Point
                // # SendPosition is used to specify which profile to start sending data from during high-speed communication.
                // # When "Overwrite" is specified for the operation when memory full and 
                //   "0: From previous send complete position" is specified for the send start position,
                //    if the LJ-X continues to accumulate profiles, the LJ-X memory will become full,
                //    and the profile at the previous send complete position will be overwritten with a new profile.
                //    In this situation, because the profile at the previous send complete position is not saved, an error will occur.

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

            _receivedProfileCountLabels[_currentDeviceId].Text = "0";
            int rc = NativeMethods.LJX8IF_StartHighSpeedDataCommunication(_currentDeviceId);

            AddLogResult(rc, Resources.IDS_START_HIGH_SPEED_DATA_COMMUNICATION);

        }

        private void _buttonStartMeasure_Click(object sender, EventArgs e)
        {
            _sendCommand = SendCommand.StartMeasure;

            int rc = NativeMethods.LJX8IF_StartMeasure(_currentDeviceId);
            AddLogResult(rc, Resources.IDS_START_MEASURE);
        }

        private void _buttonStopMeasure_Click(object sender, EventArgs e)
        {
            _sendCommand = SendCommand.StopMeasure;

            int rc = NativeMethods.LJX8IF_StopMeasure(_currentDeviceId);
            AddLogResult(rc, Resources.IDS_STOP_MEASURE);
        }

        private void _buttonStopHighSpeedDataCommunication_Click(object sender, EventArgs e)
        {
            int rc = NativeMethods.LJX8IF_StopHighSpeedDataCommunication(_currentDeviceId);
            AddLogResult(rc, Resources.IDS_STOP_HIGH_SPEED_DATA_COMMUNICATION);
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
            _deviceStatusLabels[_currentDeviceId].Text = _deviceData[_currentDeviceId].GetStatusString();
            _receivedProfileCountLabels[_currentDeviceId].Text = "0";
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

            //int startIndex = (int)_numericUpDownProfileNo.Value;
            int startIndex = 0;
            int dataCount = (int)_numericUpDownProfileSaveCount.Value;
            bool result = _deviceData[_currentDeviceId].SimpleArrayDataHighSpeed.SaveDataAsImages(_textBoxHighSpeedProfileFilePath.Text, startIndex, dataCount);

            AddLog(result ? "Succeed to save image." : "Failed to save image.");

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
