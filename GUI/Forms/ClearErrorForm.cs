//----------------------------------------------------------------------------- 
// <copyright file="ClearErrorForm.cs" company="KEYENCE">
//	 Copyright (c) 2019 KEYENCE CORPORATION. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------- 
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace LJX8_DllSampleAll.Forms
{
	public partial class ClearErrorForm : Form
	{
		#region Field
		/// <summary>
		/// Error code of the error to clear
		/// </summary>
		private short _errCode;
		#endregion

		#region Property
		/// <summary>
		/// The error code for the error you wish to clear
		/// </summary>
		public short ErrCode
		{
			get { return _errCode; }
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
				_errCode = Convert.ToInt16(_textBoxErrCode.Text, 16);
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
		public ClearErrorForm()
		{
			InitializeComponent();

			// Field initialization
			_errCode = 0;
		}
		#endregion
	}
}
