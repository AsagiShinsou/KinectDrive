namespace KinectDrive
{
    partial class KinectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.KinectJoints = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // KinectJoints
            // 
            this.KinectJoints.Location = new System.Drawing.Point(12, 12);
            this.KinectJoints.Name = "KinectJoints";
            this.KinectJoints.Size = new System.Drawing.Size(550, 247);
            this.KinectJoints.TabIndex = 0;
            this.KinectJoints.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // KinectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 270);
            this.Controls.Add(this.KinectJoints);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "KinectForm";
            this.Text = "Информация с Kinect";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox KinectJoints;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}