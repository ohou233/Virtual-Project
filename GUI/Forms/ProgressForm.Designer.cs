

namespace LJX8_DllSampleAll.Forms
{
	partial class ProgressForm
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}


		private void InitializeComponent()
		{
            this._labelStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this._labelStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this._labelStatus.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._labelStatus.Location = new System.Drawing.Point(0, 0);
            this._labelStatus.Name = "_labelStatus";
            this._labelStatus.Size = new System.Drawing.Size(284, 66);
            this._labelStatus.TabIndex = 0;
            this._labelStatus.Text = "Processing";
            this._labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 66);
            this.ControlBox = false;
            this.Controls.Add(this._labelStatus);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ProgressForm";
            this.ShowIcon = false;
            this.Text = "ProgressForm";
            this.ResumeLayout(false);

		}


		private System.Windows.Forms.Label _labelStatus;
	}
}
