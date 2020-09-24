using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace LJX8_DllSampleAll.Forms
{
	public partial class ControlLaserForm : Form
	{
		#region Constants
		private const int BaseNumber = 16; 
		#endregion

		#region Field
		/// <summary>
		/// Laser status
		/// </summary>
		private byte _state;
		#endregion

		#region Property
		/// <summary>
		/// The error code for the error you wish to clear
		/// </summary>
		public byte State
		{
			get { return _state; }
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
				_state = Convert.ToByte(_textBoxLaserStatus.Text, BaseNumber);
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


		#region Methods
		/// <summary>
		/// Constructor
		/// </summary>
		public ControlLaserForm()
		{
			InitializeComponent();
		} 
		#endregion
	}
}
