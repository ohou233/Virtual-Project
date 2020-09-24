//----------------------------------------------------------------------------- 
// <copyright file="GetErrorForm.cs" company="KEYENCE">
//	 Copyright (c) 2019 KEYENCE CORPORATION. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------- 
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace LJX8_DllSampleAll.Forms
{
	public partial class GetErrorForm : Form
	{
		#region Field
		/// <summary>
		/// Maximum number of system errors to receive
		/// </summary>
		private byte _receivedMax;
		#endregion

		#region Property
		
		/// <summary>
		/// Specify the maximum amount of system error information to receive.
		/// </summary>
		public byte ReceivedMax
		{
			get { return _receivedMax; }
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
				_receivedMax = Convert.ToByte(_textBoxErrCnt.Text);
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
		public GetErrorForm()
		{
			InitializeComponent();

			// Field initialization
			_receivedMax = 0;
		}
		#endregion
	}
}
