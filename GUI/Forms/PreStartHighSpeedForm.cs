//----------------------------------------------------------------------------- 
// <copyright file="PreStartHighSpeedForm.cs" company="KEYENCE">
//	 Copyright (c) 2019 KEYENCE CORPORATION. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------- 
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace LJX8_DllSampleAll.Forms
{
	public partial class PreStartHighSpeedForm : Form
	{
		#region Field
		/// <summary>
		/// High-speed communication start request structure
		/// </summary>
		private LJX8IF_HIGH_SPEED_PRE_START_REQUEST _request;
		#endregion

		#region Property
		/// <summary>
		/// High-speed communication start request structure
		/// </summary>
		public LJX8IF_HIGH_SPEED_PRE_START_REQUEST Request
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
			if (DialogResult != DialogResult.OK) return;

			try
			{
				_request.bySendPosition = Convert.ToByte(_textBoxSendPos.Text);
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
		public PreStartHighSpeedForm()
		{
			InitializeComponent();

			// Field initialization
			_request = new LJX8IF_HIGH_SPEED_PRE_START_REQUEST();
		}
		#endregion
	}
}
