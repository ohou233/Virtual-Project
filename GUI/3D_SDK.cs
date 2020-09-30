        #region function of 3DCamerm
        private void PreLJX()
        {
            _timerHighSpeedReceive.Interval = 500;
            _timerHighSpeedReceive.Start();
            writeLog("成功开启定时器",true);
        }

        private void InitializeLjxControlValue()
        {
            _comboBoxLjxMeasureX.DataSource = GetLjxHeadMeasureRangeList();
            _comboBoxLjxMeasureX.DisplayMember = "Key";
            _comboBoxLjxMeasureX.ValueMember = "Value";
            _comboBoxLjxSamplingPeriod.DataSource = GetLjxSamplingPeriodRangeList();
            _comboBoxLjxSamplingPeriod.DisplayMember = "Key";
            _comboBoxLjxSamplingPeriod.ValueMember = "Value";
            _comboBoxLjxMeasureX.SelectedValue = Define.LjxHeadMeasureRangeFull;
            _comboBoxLjxSamplingPeriod.SelectedValue = Define.LjxHeadSamplingPeriod.LjxHeadSamplingPeriod1KHz;
        }

        private List<DictionaryEntry> GetLjxHeadMeasureRangeList()
        {
            List<DictionaryEntry> list = new List<DictionaryEntry>()
            {
                (new DictionaryEntry(Resources.IDS_MEASURE_RANGE_FULL, Define.LjxHeadMeasureRangeFull)),
                (new DictionaryEntry(Resources.IDS_MEASURE_RANGE_THREE_FOURTHS, Define.LjxHeadMeasureRangeThreeFourth)),
                (new DictionaryEntry(Resources.IDS_MEASURE_RANGE_HALF, Define.LjxHeadMeasureRangeHalf)),
                (new DictionaryEntry(Resources.IDS_MEASURE_RANGE_QUARTER, Define.LjxHeadMeasureRangeQuarter))
            };
            return list;
        }

        private List<DictionaryEntry> GetLjxSamplingPeriodRangeList()
        {
            List<DictionaryEntry> list = new List<DictionaryEntry>()
            {
                (new DictionaryEntry(Resources.IDS_SAMPLING_PERIOD_10Hz, Define.LjxHeadSamplingPeriod.LjxHeadSamplingPeriod10Hz)),
                (new DictionaryEntry(Resources.IDS_SAMPLING_PERIOD_20Hz, Define.LjxHeadSamplingPeriod.LjxHeadSamplingPeriod20Hz)),
                (new DictionaryEntry(Resources.IDS_SAMPLING_PERIOD_50Hz, Define.LjxHeadSamplingPeriod.LjxHeadSamplingPeriod50Hz)),
                (new DictionaryEntry(Resources.IDS_SAMPLING_PERIOD_100Hz, Define.LjxHeadSamplingPeriod.LjxHeadSamplingPeriod100Hz)),
                (new DictionaryEntry(Resources.IDS_SAMPLING_PERIOD_200Hz, Define.LjxHeadSamplingPeriod.LjxHeadSamplingPeriod200Hz)),
                (new DictionaryEntry(Resources.IDS_SAMPLING_PERIOD_500Hz, Define.LjxHeadSamplingPeriod.LjxHeadSamplingPeriod500Hz)),
                (new DictionaryEntry(Resources.IDS_SAMPLING_PERIOD_1kHz, Define.LjxHeadSamplingPeriod.LjxHeadSamplingPeriod1KHz)),
                (new DictionaryEntry(Resources.IDS_SAMPLING_PERIOD_2kHz, Define.LjxHeadSamplingPeriod.LjxHeadSamplingPeriod2KHz)),
                (new DictionaryEntry(Resources.IDS_SAMPLING_PERIOD_4kHz, Define.LjxHeadSamplingPeriod.LjxHeadSamplingPeriod4KHz)),
                (new DictionaryEntry(Resources.IDS_SAMPLING_PERIOD_8kHz, Define.LjxHeadSamplingPeriod.LjxHeadSamplingPeriod8KHz)),
                (new DictionaryEntry(Resources.IDS_SAMPLING_PERIOD_16kHz, Define.LjxHeadSamplingPeriod.LjxHeadSamplingPeriod16KHz))
            };
            return list;
        }

        private bool InitLJX()
        {
            int rc = NativeMethods.LJX8IF_Initialize();
            if (rc == (int)Rc.Ok)
            {
                writeLog("Success to init LJX8IF", true);
                return true;
            }
            else
            {
                writeLog("Fail to init LJX8IF", true);
                return false;
            }
        }

        private int finalizeLJX()
        {
            int rc = NativeMethods.LJX8IF_Finalize();
            return rc;
        }

        private bool openEthernet()
        {
            ethernetConfig = new LJX8IF_ETHERNET_CONFIG();
            ethernetConfig.abyIpAddress = new byte[]
            {
                Convert.ToByte(m_IP1.Text),
                Convert.ToByte(m_IP2.Text),
                Convert.ToByte(m_IP3.Text),
                Convert.ToByte(m_IP4.Text)
            };
            ethernetConfig.wPortNo = Convert.ToUInt16(m_Port.Text);
            int rc = NativeMethods.LJX8IF_EthernetOpen(0, ref ethernetConfig);
            if (rc != (int)Rc.Ok)
            {
                writeLog("Fail to open Ethernet!", true);
                _deviceData.EthernetConfig = ethernetConfig;
                return false;
            }
            else
            {
                _deviceData.EthernetConfig = ethernetConfig;
                writeLog("Success to open Ethernet!", true);
                return true;
            }
        }

        private void commClose()
        {
            int rc = NativeMethods.LJX8IF_EthernetOpen(0, ref ethernetConfig);
            if (rc != (int)Rc.Ok)
            {
                MessageBox.Show("Fail to close communication!");
            }
        }

        private bool StartMeasure()
        {
            int rc = NativeMethods.LJX8IF_StartMeasure(0);
            if (rc != (int)Rc.Ok)
            {
                writeLog("Fail to StartMeasure", true);
                return false;
            }
            else
            {
                writeLog("Success to StartMeasure", true);
                return true;
            }
        }

        private void StopMeasure()
        {
            int rc = NativeMethods.LJX8IF_StopMeasure(0);
            if (rc != (int)Rc.Ok)
                writeLog("Fail to StopMeasure", true);
            else
                writeLog("Success to StopMeasure", true);
        }

        private void ClearMemory()
        {
            int rc = NativeMethods.LJX8IF_ClearMemory(0);
        }

        private bool InitializeHighSpeedDataCommunication()
        {
            ThreadSafeBuffer.ClearBuffer(0);  //Clear the retained profile data.
            _deviceData.SimpleArrayDataHighSpeed.Clear();
            ushort _highSpeedPortNo = (ushort)(ethernetConfig.wPortNo + 1);
            int rc = NativeMethods.LJX8IF_InitializeHighSpeedDataCommunicationSimpleArray(0, ref ethernetConfig,
                    _highSpeedPortNo, _callbackSimpleArray, _profileCount, 0);
            if (rc == (int)Rc.Ok)
            {
                _deviceData.Status = DeviceStatus.EthernetFast;
                _deviceData.EthernetConfig = ethernetConfig;
                writeLog("Success to InitHighSpeedCommunication", true);
                return true;
            }
            else
            {
                writeLog("Fail to InitHighSpeedCommunication", true);
                return false;
            }
        }

        private bool PreStartHighSpeedDataCommunication()
        {
            LJX8IF_HIGH_SPEED_PRE_START_REQUEST request = new LJX8IF_HIGH_SPEED_PRE_START_REQUEST();
            request.bySendPosition = Convert.ToByte("2");
            LJX8IF_PROFILE_INFO profileInfo = new LJX8IF_PROFILE_INFO();
            int rc = NativeMethods.LJX8IF_PreStartHighSpeedDataCommunication(0, ref request, ref profileInfo);
            _deviceData.SimpleArrayDataHighSpeed.Clear();
            _deviceData.SimpleArrayDataHighSpeed.DataWidth = profileInfo.nProfileDataCount;
            _deviceData.SimpleArrayDataHighSpeed.IsLuminanceEnable = profileInfo.byLuminanceOutput == 1;
            _profileInfo = profileInfo;
            if (rc == (int)Rc.Ok)
            {
                writeLog("Success to PreStartHighSpeedDataCommunication", true);
                return true;
            }
            else
            {
                writeLog("Fail to PreStartHighSpeedDataCommunication", true);
                return false ;
            }
        }

        private bool StartHighSpeedDataCommunication()
        {
            ThreadSafeBuffer.ClearBuffer(0);
            _isBufferFull = false;
            _isStopCommunicationByError = false;
            int rc = NativeMethods.LJX8IF_StartHighSpeedDataCommunication(0);
            if (rc == (int)Rc.Ok)
            {
                writeLog("Succeed to start high speed data communication", true);
                return true;
            }
            else
            {
                writeLog("Fail to start high speed data communication", true);
                return false;
            }
        }

        private void StopHighSpeedDataCommunication()
        {
            NativeMethods.LJX8IF_StopHighSpeedDataCommunication(0);
        }

        private void FinalizeHighSpeedDataCommunication()
        {
            NativeMethods.LJX8IF_FinalizeHighSpeedDataCommunication(0);
        }

        private void GetProfileByte()
        {
            byte[] _profileImage = _deviceData.SimpleArrayDataHighSpeed.GetImageByte(0, (int)_profileCount);
            m_3DWidth = _deviceData.SimpleArrayDataHighSpeed.DataWidth;
            m_3DHeight = _profileImage.Count() / 2 /m_3DWidth;
            IntPtr ptr = Marshal.AllocHGlobal(_profileImage.Length);
            Marshal.Copy(_profileImage, 0, ptr, _profileImage.Length);
            m_3DDLL.byte2HObject(m_3DWidth, m_3DHeight, ptr);
        }

        private void SetSamplingPeriod()
        {
            LJX8IF_TARGET_SETTING _targetSetting = new LJX8IF_TARGET_SETTING();
            _targetSetting.byType = 16;
            _targetSetting.byCategory = 0;
            _targetSetting.byItem = 2;
            _targetSetting.byTarget1 = 0;
            _targetSetting.byTarget2 = 0;
            _targetSetting.byTarget3 = 0;
            _targetSetting.byTarget4 = 0;
            byte[] _data = new byte[1];
            _data[0] = (byte)_comboBoxLjxSamplingPeriod.SelectedIndex;
            using (PinnedObject pin = new PinnedObject(_data))
            {
                uint error = 0;
                NativeMethods.LJX8IF_SetSetting(0, 2, targetSetting, pin.Pointer, 1, ref error);
            }
        }

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
                int rc = NativeMethods.LJX8IF_SetSetting(0, 2, targetSetting,
                    pin.Pointer, 4, ref error);
            }
        }

        private void SetIntervalPoints()
        {
            int m_CountProfile = Convert.ToInt32(numericUpDownIntervalPoints.Text);
            String strA = m_CountProfile.ToString("x8");
            byte[] _data = new byte[2];
            string[] parameterTexts = new string[2];
            parameterTexts[0] = strA.Substring(4, 2);
            parameterTexts[1] = strA.Substring(6, 2);
            if (0 < parameterTexts.Length)
            {
                _data = Array.ConvertAll(parameterTexts,
                    delegate (string text) { return Convert.ToByte(text, 16); });
            }
            Array.Resize(ref _data, 2);
            uint error = 0;
            LJX8IF_TARGET_SETTING _targetSetting = new LJX8IF_TARGET_SETTING();
            _targetSetting.byType = 16;
            _targetSetting.byCategory = 0;
            _targetSetting.byItem = 9;
            _targetSetting.byTarget1 = 0;
            _targetSetting.byTarget2 = 0;
            _targetSetting.byTarget3 = 0;
            _targetSetting.byTarget4 = 0;
            using (PinnedObject pin = new PinnedObject(_data))
            {
                int rc = NativeMethods.LJX8IF_SetSetting(0, 2, _targetSetting,
                    pin.Pointer, 2, ref error);
            }
        }

        private void GetParameter()
        {
            //采样周期
            byte[] data = new byte[1];
            using (PinnedObject pin = new PinnedObject(data))
            {
                LJX8IF_TARGET_SETTING _targetSetting = new LJX8IF_TARGET_SETTING();
                _targetSetting.byType = 16;
                _targetSetting.byCategory = 0;
                _targetSetting.byItem = 2;
                _targetSetting.byTarget1 = 0;
                _targetSetting.byTarget2 = 0;
                _targetSetting.byTarget3 = 0;
                _targetSetting.byTarget4 = 0;
                int rc = NativeMethods.LJX8IF_GetSetting(0, 2, targetSetting,
                    pin.Pointer, 1);
                if (rc == (int)Rc.Ok)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(string.Format("{0:x2} ", data[0]));
                    MessageBox.Show(stringBuilder.ToString());
                }
                else
                {
                    writeLog("获取采样周期失败", false);
                }
                       
            }

            //批处理条数
            byte[] data1 = new byte[4];
            using (PinnedObject pin = new PinnedObject(data1))
            {
                LJX8IF_TARGET_SETTING _targetSetting = new LJX8IF_TARGET_SETTING();
                _targetSetting.byType = 16;
                _targetSetting.byCategory = 0;
                _targetSetting.byItem = 10;
                _targetSetting.byTarget1 = 0;
                _targetSetting.byTarget2 = 0;
                _targetSetting.byTarget3 = 0;
                _targetSetting.byTarget4 = 0;
                int rc = NativeMethods.LJX8IF_GetSetting(0, 2, targetSetting,
                    pin.Pointer, 4);
                if (rc == (int)Rc.Ok)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < 4; i++)
                    {
                        if ((i % 8) == 0) stringBuilder.Append(string.Format("  [0x{0:x4}] ", i));

                        stringBuilder.Append(string.Format("{0:x2} ", data1[i]));
                        if (((i % 8) == 7) || (i == 3))
                        {
                            MessageBox.Show(stringBuilder.ToString());
                            stringBuilder.Remove(0, stringBuilder.Length);
                        }
                    }
                }
                else
                {
                    writeLog("获取采样周期失败", false);
                }

            }

            //间隔点数
            byte[] data2 = new byte[4];
            using (PinnedObject pin = new PinnedObject(data2))
            {
                LJX8IF_TARGET_SETTING _targetSetting = new LJX8IF_TARGET_SETTING();
                _targetSetting.byType = 16;
                _targetSetting.byCategory = 0;
                _targetSetting.byItem = 9;
                _targetSetting.byTarget1 = 0;
                _targetSetting.byTarget2 = 0;
                _targetSetting.byTarget3 = 0;
                _targetSetting.byTarget4 = 0;
                int rc = NativeMethods.LJX8IF_GetSetting(0, 2, targetSetting,
                    pin.Pointer, 4);
                if (rc == (int)Rc.Ok)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < 4; i++)
                    {
                        if ((i % 8) == 0) stringBuilder.Append(string.Format("  [0x{0:x4}] ", i));

                        stringBuilder.Append(string.Format("{0:x2} ", data2[i]));
                        if (((i % 8) == 7) || (i == 3))
                        {
                            MessageBox.Show(stringBuilder.ToString());
                            stringBuilder.Remove(0, stringBuilder.Length);
                        }
                    }
                }
                else
                {
                    writeLog("获取采样周期失败", false);
                }

            }
        }

        private void ReceiveHighSpeedSimpleArray(IntPtr headBuffer, IntPtr profileBuffer, IntPtr luminanceBuffer, uint isLuminanceEnable, uint profileSize, uint count, uint notify, uint user)
        {
            _isBufferFull = _deviceData.SimpleArrayDataHighSpeed.AddReceivedData(profileBuffer, luminanceBuffer, count);
            _deviceData.SimpleArrayDataHighSpeed.Notify = notify;
        }

        private void _timerHighSpeedReceive_Tick(object sender, EventArgs e)
        {
            uint notify;
            int batchNo;
            SoftWareState.Text = _deviceData.SimpleArrayDataHighSpeed.Count.ToString();
            notify = _deviceData.SimpleArrayDataHighSpeed.Notify;
            batchNo = _deviceData.SimpleArrayDataHighSpeed.BatchNo;
        }

        private void Show3DImage()
        {
            while (true)
            {
                if (_deviceData.SimpleArrayDataHighSpeed.Count >= (uint)_profileCount && startMeasure3D)
                {
                    StopMeasure();
                    //GetProfileByte();
                    bool result = _deviceData.SimpleArrayDataHighSpeed.SaveDataAsImages(@".\data\ngImage\22.tif", 0, (int)_profileCount);
                    writeLog(result ? "Succeed to save image." : "Fail to save image.", true);
                    StopHighSpeedDataCommunication();
                    SoftWareState.Text = "待料中";
                    SoftWareState.ForeColor = Color.Blue;
                    startMeasure3D = false;
                }
            }
        }

        private void _timerBufferError_Tick(object sender, EventArgs e)
        {
            if (_isBufferFull && !_isStopCommunicationByError)
            {
                _isStopCommunicationByError = true;
                NativeMethods.LJX8IF_StopHighSpeedDataCommunication(0);
                NativeMethods.LJX8IF_FinalizeHighSpeedDataCommunication(0);
                Invoke(new InvokeDelagate(ShowBufferFullMessage));
            }
        }

        private void ShowBufferFullMessage()
        {
            MessageBox.Show(this, "Receive buffer is full.");
        }

        protected override void OnShown(EventArgs e)
        {
            GetBufferSizeText();
            base.OnShown(e);
        }
        private string GetBufferSizeText()
        {
            return GetOneProfileDataSize().ToString();
        }

        private uint GetOneProfileDataSize()
        {
            uint oneProfileBufferSize = 6400;
            oneProfileBufferSize *= (uint)Marshal.SizeOf(typeof(uint));
            oneProfileBufferSize += (uint)Marshal.SizeOf(typeof(LJX8IF_PROFILE_HEADER));
            oneProfileBufferSize += (uint)Marshal.SizeOf(typeof(LJX8IF_PROFILE_FOOTER));
            return oneProfileBufferSize;
        }

        public bool Open3DDevice()
        {
            PreLJX();
            bool flag = InitLJX();
            if (!flag)
                return false;
            flag = openEthernet();
            if (!flag)
                return false;
            flag = InitializeHighSpeedDataCommunication();
            if (!flag)
                return false;
            return true;
        }

        public void Close3DDevice()
        {
            StopHighSpeedDataCommunication();
            FinalizeHighSpeedDataCommunication();
            commClose();
        }

        #endregion