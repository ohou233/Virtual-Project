using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LjxaSample.DriverDotNet;

namespace LjxaDllSampleProgramCS
{
    public partial class Form1 : Form
    {
        //实例化简化版DLL
        private LJX8000A ljxa = new LJX8000A();
        //用于保存高度/浓淡数据的缓存
        private ushort[] _heightdata = new ushort[] { };
        private ushort[] _luminancedata = new ushort[] { };
        //图像的宽度
        private int imgwidth = 0;
        //标志位，判断是否图像接收ok
        private bool image_received = false;
        private string image_save_path = "";

        public Form1()
        {
            InitializeComponent();
            //初始化3D显示窗口
            ljxaWindows3D1.Init();
            //确认保存的文件夹是否存在,没有的话创建
            DateTime StartTime = DateTime.Now;
            image_save_path = Application.StartupPath + "\\" + StartTime.Year + "-" + StartTime.Month + "-" + StartTime.Day + "\\";
            if (!(System.IO.Directory.Exists(image_save_path)))
            {
                System.IO.Directory.CreateDirectory(image_save_path);
            }
            _logBox.AppendText("默认保存路径：" + "\n");
            _logBox.AppendText(image_save_path + "\n");
            //使用计时器定时判断是否接收到图像（避免跨线程调用控件）
           timerwaitimage.Enabled = true;
        }

        private void _button_openDevice_Click(object sender, EventArgs e)
        {
            //新建通信设定然后打开设备连接
            CommunicationSetting _comset = new CommunicationSetting(communicationSetting1.IP, 24691, 24692, communicationSetting1.YSize, communicationSetting1.UseExternalBatchStart, communicationSetting1.OutputLiminanceData);
            bool result = ljxa.OpenDevice(0, _comset, _acquireFinish);
            AddLog("OpenDevice：   " + result + "\n",DateTime.Now);
        }

        private void _button_acquire_start_Click(object sender, EventArgs e)
        {
            //打开高速通信并等待数据传回
            bool result = ljxa.AcquireStart(0);
            AddLog("AcquireStart:   " + result + "\n",DateTime.Now);
        }

        private void _button_acquire_stop_Click(object sender, EventArgs e)
        {
            //中断批处理，然后处理已获得的数据
            bool result = ljxa.AcquireStop(0);
            AddLog("AcquireStop:   " + result + "\n",DateTime.Now);
        }

        private void _button_close_device_Click(object sender, EventArgs e)
        {
            //关闭设备连接
            bool result = ljxa.CloseDevice(0);
            AddLog("CloseDevice:   " + result + "\n",DateTime.Now);
        }

        //回调函数，数据接收完成时DLL会调用
        private void _acquireFinish(int _ID, int _notify)
        {
            image_received = true;
        }

        //以上数据获取部分已结束，以下部分是显示+保存相关的内容
        private void timerwaitimage_Tick(object sender, EventArgs e)
        {
            //如果接收到图像了，就保存 + 显示
            if (image_received)
            {
                image_received = false;
               
                int _pitch = 0;
                ljxa.GetHeightData(0, ref _heightdata, ref imgwidth, ref _pitch);
                ljxa.GetLuminanceData(0, ref _luminancedata);
                if (imgwidth > 0 & _heightdata.Length > imgwidth)
                {
                    AddLog("Image File Received" + "\n",DateTime.Now);
                    if (!(_heightdata.Length == _luminancedata.Length))
                    {
                        _luminancedata = new ushort[] { };
                    }
                    int _imgheight = _heightdata.Length / imgwidth;
                    //计算3D显示的网格细化比例（设定过小会造成系统严重卡顿）
                    decimal scale = imgwidth / 400;
                    ljxaWindows3D1.BinningSize = Convert.ToInt32(Math.Ceiling(scale));
                    //将3D数据传递给显示空间
                    ljxaWindows3D1.SetImage(_heightdata, _luminancedata, imgwidth);
                    //保存图像数据
                    DateTime NowTime = DateTime.Now;
                    String fileName = NowTime.Hour + "-" + NowTime.Minute + "-" + NowTime.Second;
                    LJX8000A.SaveReceiveDataAsFile.SaveAsImage(_heightdata, imgwidth, image_save_path + fileName + "_height.tif",LJX8000A.SaveReceiveDataAsFile.ImageSaveType.Tiff);
                    LJX8000A.SaveReceiveDataAsFile.SaveAsImage(_luminancedata, imgwidth, image_save_path + fileName + "_luminance.tif", LJX8000A.SaveReceiveDataAsFile.ImageSaveType.Tiff);
                    LJX8000A.SaveReceiveDataAsFile.SaveAsImage(_heightdata, imgwidth, image_save_path + fileName + "_height.bmp", LJX8000A.SaveReceiveDataAsFile.ImageSaveType.Bmp565);
                    LJX8000A.SaveReceiveDataAsFile.SaveAsImage(_luminancedata, imgwidth, image_save_path + fileName + "_luminance.bmp", LJX8000A.SaveReceiveDataAsFile.ImageSaveType.Bmp565);
                    LJX8000A.SaveReceiveDataAsFile.SaveAsCsv(_heightdata, imgwidth, image_save_path + fileName + "_height.csv");
                    LJX8000A.SaveReceiveDataAsFile.SaveAsCsv(_luminancedata, imgwidth, image_save_path + fileName + "_luminance.csv");
                    AddLog("Image File Saved:  " + fileName + "(.tif|.bmp|.csv)" + "\n",DateTime.Now);
                }    
            }
        }

        //在logBox中添加带时间戳的记录
        private void AddLog(string _text,DateTime _time)
        {
            string timestr =_time.Year + "-" + _time.Month + "-" + _time.Day + "   "+  _time.Hour + ":" + _time.Minute + ":" + _time.Second + ":"+  _time.Millisecond +"    ";
            _logBox.AppendText(timestr + _text + "\n");
        }
    }
    }
