//----------------------------------------------------------------------------- 
// <copyright file="GetBatchProfileForm.cs" company="KEYENCE">
//	 Copyright (c) 2019 KEYENCE CORPORATION. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------- 
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace LJX8_DllSampleAll.Forms
{
	public partial class GetBatchProfileForm : Form
	{
		#region Field
		/// <summary>
		/// Get profile request structure (batch measurement: on)
		/// </summary>
		private LJX8IF_GET_BATCH_PROFILE_REQUEST _request;
		#endregion

		#region Property
		/// <summary>
		/// Get profile request structure (batch measurement: on)
		/// </summary>
		public LJX8IF_GET_BATCH_PROFILE_REQUEST Request
		{
			get { return _request; }
		}
		#endregion

		#region Event

		/// <summary>
		/// Close start event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnClosing(CancelEventArgs e)
		{
			if (DialogResult != DialogResult.OK)return;

			try
			{
				_request.byTargetBank = Convert.ToByte(_textBoxTargetBank.Text, 16);
				_request.byPositionMode = Convert.ToByte(_textBoxPositionMode.Text, 16);
				_request.dwGetBatchNo = Convert.ToUInt32(_textBoxGetBatchNo.Text);
				_request.dwGetProfileNo = Convert.ToUInt32(_textBoxGetProfileNo.Text);
				_request.byGetProfileCount = Convert.ToByte(_textBoxGetProfileCount.Text);
				_request.byErase = Convert.ToByte(_textBoxErase.Text);
				if (_request.byGetProfileCount <= 0)
				{
					MessageBox.Show(this, "Specify a value from 1 to 255.");
					e.Cancel = true;
					return;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message);
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
		public GetBatchProfileForm()
		{
			InitializeComponent();

			// Field initialization
			_request = new LJX8IF_GET_BATCH_PROFILE_REQUEST();
		}
		#endregion
	}
}
