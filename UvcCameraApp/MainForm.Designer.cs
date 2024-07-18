namespace UvcCameraApp
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelVideo = new System.Windows.Forms.Panel();
            this.buttonStart = new System.Windows.Forms.Button();
            this.comboBoxCameras = new System.Windows.Forms.ComboBox();
            this.panelVideo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelVideo
            // 
            this.panelVideo.Controls.Add(this.buttonStart);
            this.panelVideo.Controls.Add(this.comboBoxCameras);
            this.panelVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVideo.Location = new System.Drawing.Point(0, 0);
            this.panelVideo.Name = "panelVideo";
            this.panelVideo.Size = new System.Drawing.Size(800, 450);
            this.panelVideo.TabIndex = 0;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(696, 37);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(92, 37);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // comboBoxCameras
            // 
            this.comboBoxCameras.FormattingEnabled = true;
            this.comboBoxCameras.Location = new System.Drawing.Point(570, 4);
            this.comboBoxCameras.Name = "comboBoxCameras";
            this.comboBoxCameras.Size = new System.Drawing.Size(227, 26);
            this.comboBoxCameras.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelVideo);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.panelVideo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelVideo;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.ComboBox comboBoxCameras;
    }
}

