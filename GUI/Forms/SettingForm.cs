//----------------------------------------------------------------------------- 
// <copyright file="SettingForm.cs" company="KEYENCE">
//	 Copyright (c) 2019 KEYENCE CORPORATION. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------- 
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace LJX8_DllSampleAll.Forms
{
	public partial class SettingForm : Form
	{
		#region Constant
		private const int BaseNumber = 16; 
		#endregion

		#region Field
		/// <summary>
		/// Specify to what level the sent settings will be reflected (LJX8IF_SETTING_DEPTH).
		/// </summary>
		private byte _depth;

		/// <summary>
		/// Identify the item that is the target to send.
		/// </summary>
		private LJX8IF_TARGET_SETTING _targetSetting;

		/// <summary>
		/// Specify the buffer that stores the setting data to send.
		/// </summary>
		private byte[] _data;
		#endregion

		#region Property
		/// <summary>
		/// Specify to what level the sent settings will be reflected (LJX8IF_SETTING_DEPTH).
		/// </summary>
		public byte Depth
		{
			get { return _depth; }
		}

		/// <summary>
		/// Identify the item that is the target to send.
		/// </summary>
		public LJX8IF_TARGET_SETTING TargetSetting
		{
			get { return _targetSetting; }
		}
		public int DataLength
		{
			get { return Convert.ToInt32(_textBoxDataLength.Text); }
		}
		/// <summary>
		/// Specify the buffer that stores the setting data to send.
		/// </summary>
		public byte[] Data
		{
			get { return _data; }
		}
		#endregion

		#region Event
		/// <summary>
		/// Load event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			_textBoxDepth.Text = Convert.ToString(_depth, BaseNumber);
			_textBoxType.Text = Convert.ToString(_targetSetting.byType, BaseNumber);
			_textBoxCategor.Text = Convert.ToString(_targetSetting.byCategory, BaseNumber);
			_textBoxItem.Text = Convert.ToString(_targetSetting.byItem, BaseNumber);
			_textBoxTarget1.Text = Convert.ToString(_targetSetting.byTarget1, BaseNumber);
			_textBoxTarget2.Text = Convert.ToString(_targetSetting.byTarget2, BaseNumber);
			_textBoxTarget3.Text = Convert.ToString(_targetSetting.byTarget3, BaseNumber);
			_textBoxTarget4.Text = Convert.ToString(_targetSetting.byTarget4, BaseNumber);
			_textBoxParameter.Text = Convert.ToString(_data[0], BaseNumber);

			base.OnLoad(e);
		}

		/// <summary>
		/// Close start event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClosing(CancelEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;

			try
			{
				_depth = Convert.ToByte(_textBoxDepth.Text, BaseNumber);
				_targetSetting.byType = Convert.ToByte(_textBoxType.Text, BaseNumber);
				_targetSetting.byCategory = Convert.ToByte(_textBoxCategor.Text, BaseNumber);
				_targetSetting.byItem = Convert.ToByte(_textBoxItem.Text, BaseNumber);
				_targetSetting.byTarget1 = Convert.ToByte(_textBoxTarget1.Text, BaseNumber);
				_targetSetting.byTarget2 = Convert.ToByte(_textBoxTarget2.Text, BaseNumber);
				_targetSetting.byTarget3 = Convert.ToByte(_textBoxTarget3.Text, BaseNumber);
				_targetSetting.byTarget4 = Convert.ToByte(_textBoxTarget4.Text, BaseNumber);

				char[] trimChars = { ' ', ',' };
				string trimStrings = _textBoxParameter.Text.Trim(trimChars);
				if (0 < trimStrings.Length)
				{
					string[] parameterTexts = trimStrings.Split(',');
					if (0 < parameterTexts.Length)
					{
						_data = Array.ConvertAll(parameterTexts,
							delegate(string text) { return Convert.ToByte(text, BaseNumber); });
					}
				}
				Array.Resize(ref _data, Convert.ToInt32(_textBoxDataLength.Text));
			}
			catch (Exception exception)
			{
				MessageBox.Show(this, exception.Message);
				e.Cancel = true;
				return;
			}

			base.OnClosing(e);
		}
		#endregion

		#region Method
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="isSetSetting">True when sending settings, false when getting settings</param>
		public SettingForm(bool isSetSetting)
		{
			InitializeComponent();

			// Control display control
			_textBoxParameter.Visible = isSetSetting;
			_labelParameter.Visible = isSetSetting;

			// Settings that change the sampling period of the running area to "100 Hz"
			_depth = 0x01;						// Setting depth: Running settings area
			_targetSetting.byType = 0x10;		// Setting type: Program number 0
			_targetSetting.byCategory = 0x0;	// Category: Trigger settings
			_targetSetting.byItem = 0x02;		// Setting item: Sampling period
			_targetSetting.byTarget1 = 0x0;		// Setting target 1: None
			_targetSetting.byTarget2 = 0x0;		// Setting target 2: None
			_targetSetting.byTarget3 = 0x0;		// Setting target 3: None
			_targetSetting.byTarget4 = 0x0;		// Setting target 4: None


			_data = new byte[(int)NativeMethods.ProgramSettingSize];
			// Settings that change the sampling period "100 Hz"
			_data[0] = 0x3;
		}
		#endregion

	}
}
