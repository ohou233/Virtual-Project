<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me._button_close_device = New System.Windows.Forms.Button()
        Me._button_acquire_stop = New System.Windows.Forms.Button()
        Me._button_acquire_start = New System.Windows.Forms.Button()
        Me._button_openDevice = New System.Windows.Forms.Button()
        Me.communicationSetting1 = New LjxaDisp.CommunicationSetting()
        Me.ljxaWindows3D1 = New LjxaDisp.LjxaWindows3D()
        Me._logBox = New System.Windows.Forms.TextBox()
        Me.timerwaitimage = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        '_button_close_device
        '
        Me._button_close_device.Location = New System.Drawing.Point(12, 63)
        Me._button_close_device.Name = "_button_close_device"
        Me._button_close_device.Size = New System.Drawing.Size(94, 39)
        Me._button_close_device.TabIndex = 11
        Me._button_close_device.Text = "关闭设备 Close_Device"
        Me._button_close_device.UseVisualStyleBackColor = True
        '
        '_button_acquire_stop
        '
        Me._button_acquire_stop.Location = New System.Drawing.Point(129, 61)
        Me._button_acquire_stop.Name = "_button_acquire_stop"
        Me._button_acquire_stop.Size = New System.Drawing.Size(94, 42)
        Me._button_acquire_stop.TabIndex = 10
        Me._button_acquire_stop.Text = "停止获取 Acquire_Stop"
        Me._button_acquire_stop.UseVisualStyleBackColor = True
        '
        '_button_acquire_start
        '
        Me._button_acquire_start.Location = New System.Drawing.Point(129, 13)
        Me._button_acquire_start.Name = "_button_acquire_start"
        Me._button_acquire_start.Size = New System.Drawing.Size(94, 42)
        Me._button_acquire_start.TabIndex = 9
        Me._button_acquire_start.Text = "开始获取 Acquire_Start"
        Me._button_acquire_start.UseVisualStyleBackColor = True
        '
        '_button_openDevice
        '
        Me._button_openDevice.Location = New System.Drawing.Point(12, 12)
        Me._button_openDevice.Name = "_button_openDevice"
        Me._button_openDevice.Size = New System.Drawing.Size(94, 42)
        Me._button_openDevice.TabIndex = 8
        Me._button_openDevice.Text = "打开设备 Open_Device"
        Me._button_openDevice.UseVisualStyleBackColor = True
        '
        'communicationSetting1
        '
        Me.communicationSetting1.Location = New System.Drawing.Point(239, 20)
        Me.communicationSetting1.Name = "communicationSetting1"
        Me.communicationSetting1.Size = New System.Drawing.Size(325, 75)
        Me.communicationSetting1.TabIndex = 12
        '
        'ljxaWindows3D1
        '
        Me.ljxaWindows3D1.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ljxaWindows3D1.BinningSize = 4
        Me.ljxaWindows3D1.ColorHighValue = 65535
        Me.ljxaWindows3D1.ColorLowValue = 0
        Me.ljxaWindows3D1.GrayMixPercent = 0.5R
        Me.ljxaWindows3D1.Location = New System.Drawing.Point(12, 109)
        Me.ljxaWindows3D1.Name = "ljxaWindows3D1"
        Me.ljxaWindows3D1.Resolution_X = 0.0125R
        Me.ljxaWindows3D1.Resolution_Z = 0.0016R
        Me.ljxaWindows3D1.Size = New System.Drawing.Size(555, 450)
        Me.ljxaWindows3D1.TabIndex = 13
        Me.ljxaWindows3D1.ZoomScaleX = 1.0R
        Me.ljxaWindows3D1.ZoomScaleY = 1.0R
        Me.ljxaWindows3D1.ZoomScaleZ = 1.0R
        '
        '_logBox
        '
        Me._logBox.Location = New System.Drawing.Point(12, 571)
        Me._logBox.Multiline = True
        Me._logBox.Name = "_logBox"
        Me._logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me._logBox.Size = New System.Drawing.Size(555, 79)
        Me._logBox.TabIndex = 14
        '
        'timerwaitimage
        '
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 662)
        Me.Controls.Add(Me._logBox)
        Me.Controls.Add(Me.ljxaWindows3D1)
        Me.Controls.Add(Me.communicationSetting1)
        Me.Controls.Add(Me._button_close_device)
        Me.Controls.Add(Me._button_acquire_stop)
        Me.Controls.Add(Me._button_acquire_start)
        Me.Controls.Add(Me._button_openDevice)
        Me.Name = "Form1"
        Me.Text = "LJXA Simple Dll Sample Program"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents _button_close_device As Button
    Private WithEvents _button_acquire_stop As Button
    Private WithEvents _button_acquire_start As Button
    Private WithEvents _button_openDevice As Button
    Private WithEvents communicationSetting1 As LjxaDisp.CommunicationSetting
    Private WithEvents ljxaWindows3D1 As LjxaDisp.LjxaWindows3D
    Private WithEvents _logBox As TextBox
    Private WithEvents timerwaitimage As Timer
End Class
