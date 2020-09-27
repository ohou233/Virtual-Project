using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LJX8_DllSampleAll;

namespace MyWindow.Forms
{
    public partial class OpenEthernetForm : Form
    {
        #region Field
        /// <summary>
        /// Ethernet communication settings
        /// </summary>
        private LJX8IF_ETHERNET_CONFIG _ethernetConfig;
        #endregion

        #region Property
        /// <summary>
        /// Ethernet communication settings
        /// </summary>
        public LJX8IF_ETHERNET_CONFIG EthernetConfig
        {
            get { return _ethernetConfig; }
            set
            {
                _ethernetConfig = value;
                if (_ethernetConfig.abyIpAddress != null)
                {
                    Utility.UpdateTextFromEthernetSetting(_ethernetConfig, _textBoxIpFirstSegment, _textBoxIpSecondSegment, _textBoxIpThirdSegment, _textBoxIpFourthSegment);
                }
                _textBoxPort.Text = _ethernetConfig.wPortNo.ToString();
            }
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
                _ethernetConfig.abyIpAddress = Utility.GetIpAddressFromTextBox(_textBoxIpFirstSegment, _textBoxIpSecondSegment, _textBoxIpThirdSegment, _textBoxIpFourthSegment);
                _ethernetConfig.wPortNo = Convert.ToUInt16(_textBoxPort.Text);
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

        public OpenEthernetForm()
        {
            InitializeComponent();
            _ethernetConfig = new LJX8IF_ETHERNET_CONFIG();
        }
    }
}
