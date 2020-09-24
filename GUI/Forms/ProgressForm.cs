//----------------------------------------------------------------------------- 
// <copyright file="ProgressForm.cs" company="KEYENCE">
//	 Copyright (c) 2019 KEYENCE CORPORATION. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------- 

using System.Windows.Forms;

namespace LJX8_DllSampleAll.Forms
{
	#region Enum
	/// <summary>
	/// Processing status
	/// </summary>
	public enum Status
	{
		Communicating = 0,
		Saving,
	};
	#endregion

	public partial class ProgressForm : Form
	{
		#region Field
		/// <summary>
		/// Processing status
		/// </summary>
		private Status _status;
		#endregion

		#region Properety
		/// <summary>
		/// Processing status
		/// </summary>
		public Status Status
		{
			set
			{
				_status = value;
				switch (_status)
				{
					case Status.Communicating:
						_labelStatus.Text = "Communicating";
						break;
					case Status.Saving:
						_labelStatus.Text = "Saving the file";
						break;
					default:
						return;
				}
			}
			get { return _status; }
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public ProgressForm()
		{
			InitializeComponent();
			_status = Status.Communicating;
		}
		#endregion
	}
}
