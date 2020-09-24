//----------------------------------------------------------------------------- 
// <copyright file="DepthProgramSelectForm.cs" company="KEYENCE">
//	 Copyright (c) 2019 KEYENCE CORPORATION. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------- 
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace LJX8_DllSampleAll.Forms
{
	public partial class DepthProgramSelectForm : Form
	{
		#region Field
		/// <summary>
		/// Level
		/// </summary>
		private byte _depth;

		/// <summary>
		/// Specified program
		/// </summary>
		private byte _target;
		#endregion

		#region Property

		/// <summary>
		/// Specify to what level the settings written for modification will be reflected (LJX8IF_SETTING_DEPTH).
		/// </summary>
		public byte Depth
		{
			get { return _depth; }
		}

		/// <summary>
		/// Specify the setting that is the target for initialization (LJX8IF_INIT_SETTING_TARGET).
		/// </summary>
		public byte Target
		{
			get { return _target; }
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
				_depth = Convert.ToByte(_textBoxDepth.Text, 16);
				_target = Convert.ToByte(_textBoxTargetProgram.Text, 16);
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
		/// <param name="isDepthVisible">Level control display setting</param>
		/// <param name="isTargetVisible">Specified program control display setting</param>
		public DepthProgramSelectForm(bool isDepthVisible, bool isTargetVisible)
		{
			InitializeComponent();

			// Field initialization
			_depth = 0;
			_target = 0;

			// Control display control
			_panelLevel.Visible = isDepthVisible;
			_panelTarget.Visible = isTargetVisible;
		}
		#endregion
	}
}
