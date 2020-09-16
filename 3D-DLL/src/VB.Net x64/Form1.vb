Imports LjxaSample.DriverDotNet
Public Class Form1
    '实例化简化版DLL
    Private ljxa As LJX8000A = New LJX8000A()
    '用于保存高度/浓淡数据的缓存
    Private _heightdata As UShort() = New UShort() {}
    Private _luminancedata As UShort() = New UShort() {}
    '图像的宽度
    Private imgwidth As Integer = 0
    '标志位，判断是否图像接收ok
    Private image_received As Boolean = False
    Private image_save_path As String = ""

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '初始化3D显示窗口
        ljxaWindows3D1.Init()
        '确认保存的文件夹是否存在,没有的话创建
        Dim StartTime As Date = Date.Now
        image_save_path = Application.StartupPath & "\" & StartTime.Year & "-" & StartTime.Month & "-" & StartTime.Day & "\"
        If (Not IO.Directory.Exists(image_save_path)) Then
            IO.Directory.CreateDirectory(image_save_path)
        End If
        _logBox.AppendText("默认保存路径：" + vbCrLf)
        _logBox.AppendText(image_save_path + vbCrLf)
        '使用计时器定时判断是否接收到图像（避免跨线程调用控件）
        timerwaitimage.Enabled = True
    End Sub

    Private Sub _button_openDevice_Click(sender As Object, e As EventArgs) Handles _button_openDevice.Click
        '新建通信设定然后打开设备连接
        Dim _comset As CommunicationSetting = New CommunicationSetting(communicationSetting1.IP, 24691, 24692, communicationSetting1.YSize, communicationSetting1.UseExternalBatchStart, communicationSetting1.OutputLiminanceData)
        Dim result As Boolean = ljxa.OpenDevice(0, _comset, AddressOf _acquireFinish)
        AddLog("OpenDevice：   " & result, Date.Now)
    End Sub
    Private Sub _button_acquire_start_Click(sender As Object, e As EventArgs) Handles _button_acquire_start.Click
        '打开高速通信并等待数据传回
        Dim result As Boolean = ljxa.AcquireStart(0)
        AddLog("AcquireStart:   " & result, Date.Now)
    End Sub
    Private Sub _button_acquire_stop_Click(sender As Object, e As EventArgs) Handles _button_acquire_stop.Click
        '中断批处理处理，然后处理已获得的数据
        Dim result As Boolean = ljxa.AcquireStop(0)
        AddLog("AcquireStop:   " & result, Date.Now)
    End Sub
    Private Sub _button_close_device_Click(sender As Object, e As EventArgs) Handles _button_close_device.Click
        '关闭设备连接
        Dim result As Boolean = ljxa.CloseDevice(0)
        AddLog("CloseDevice:   " & result, DateTime.Now)
    End Sub
    '回调函数，数据接收完成时DLL会调用
    Private Sub _acquireFinish(_ID As Integer, _notify As Integer)
        image_received = True
    End Sub
    '以上数据获取部分已结束，以下部分是显示+保存相关的内容


    Private Sub timerwaitimage_Tick(sender As Object, e As EventArgs) Handles timerwaitimage.Tick
        '如果接收到图像了，就保存 + 显示
        If image_received Then
            image_received = False
            Dim _pitch As Integer = 0
            ljxa.GetHeightData(0, _heightdata, imgwidth, _pitch)
            ljxa.GetLuminanceData(0, _luminancedata)
            If (imgwidth > 0 And _heightdata.Length > imgwidth) Then
                AddLog("Image File Received", Date.Now)
                If (Not _heightdata.Length = _luminancedata.Length) Then
                    _luminancedata = New UShort() {}
                End If
                Dim _imgheight = _heightdata.Length / imgwidth
                '计算3D显示的网格细化比例（设定过小会造成系统严重卡顿）
                Dim Scale As Decimal = imgwidth / 400
                ljxaWindows3D1.BinningSize = Convert.ToInt32(Math.Ceiling(Scale))
                '将3D数据传递给显示空间
                ljxaWindows3D1.SetImage(_heightdata, _luminancedata, imgwidth)
                '保存图像数据
                Dim NowTime As Date = Date.Now
                Dim fileName As String = NowTime.Hour & "-" & NowTime.Minute & "-" & NowTime.Second
                LJX8000A.SaveReceiveDataAsFile.SaveAsImage(_heightdata, imgwidth, image_save_path + fileName + "_height.tif", LJX8000A.SaveReceiveDataAsFile.ImageSaveType.Tiff)
                LJX8000A.SaveReceiveDataAsFile.SaveAsImage(_luminancedata, imgwidth, image_save_path + fileName + "_luminance.tif", LJX8000A.SaveReceiveDataAsFile.ImageSaveType.Tiff)
                LJX8000A.SaveReceiveDataAsFile.SaveAsImage(_heightdata, imgwidth, image_save_path + fileName + "_height.bmp", LJX8000A.SaveReceiveDataAsFile.ImageSaveType.Bmp565)
                LJX8000A.SaveReceiveDataAsFile.SaveAsImage(_luminancedata, imgwidth, image_save_path + fileName + "_luminance.bmp", LJX8000A.SaveReceiveDataAsFile.ImageSaveType.Bmp565)
                LJX8000A.SaveReceiveDataAsFile.SaveAsCsv(_heightdata, imgwidth, image_save_path + fileName + "_height.csv")
                LJX8000A.SaveReceiveDataAsFile.SaveAsCsv(_luminancedata, imgwidth, image_save_path + fileName + "_luminance.csv")
                AddLog("Image File Saved:  " & fileName & "(.tif|.bmp|.csv)", Date.Now)
            End If
        End If
    End Sub
    '在logBox中添加带时间戳的记录
    Private Sub AddLog(_text As String, _time As Date)
        Dim timestr As String = _time.Year & "-" & _time.Month & "-" & _time.Day & "   " & _time.Hour & ":" & _time.Minute & ":" & _time.Second & ":" & _time.Millisecond & "    "
        _logBox.AppendText(timestr & _text & vbCrLf)
    End Sub
End Class
