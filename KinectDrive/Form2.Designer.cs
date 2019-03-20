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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.LabelHead = new System.Windows.Forms.Label();
            this.LabelLHand = new System.Windows.Forms.Label();
            this.LabelRHand = new System.Windows.Forms.Label();
            this.LabelRightLeg = new System.Windows.Forms.Label();
            this.LabelLeftLeg = new System.Windows.Forms.Label();
            this.LabelBody = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // LabelHead
            // 
            this.LabelHead.AutoSize = true;
            this.LabelHead.Location = new System.Drawing.Point(12, 34);
            this.LabelHead.Name = "LabelHead";
            this.LabelHead.Size = new System.Drawing.Size(64, 13);
            this.LabelHead.TabIndex = 1;
            this.LabelHead.Text = "Голова: NA";
            // 
            // LabelLHand
            // 
            this.LabelLHand.AutoSize = true;
            this.LabelLHand.Location = new System.Drawing.Point(12, 47);
            this.LabelLHand.Name = "LabelLHand";
            this.LabelLHand.Size = new System.Drawing.Size(86, 13);
            this.LabelLHand.TabIndex = 2;
            this.LabelLHand.Text = "Левая рука: NA";
            // 
            // LabelRHand
            // 
            this.LabelRHand.AutoSize = true;
            this.LabelRHand.Location = new System.Drawing.Point(12, 60);
            this.LabelRHand.Name = "LabelRHand";
            this.LabelRHand.Size = new System.Drawing.Size(92, 13);
            this.LabelRHand.TabIndex = 3;
            this.LabelRHand.Text = "Правая рука: NA";
            // 
            // LabelRightLeg
            // 
            this.LabelRightLeg.AutoSize = true;
            this.LabelRightLeg.Location = new System.Drawing.Point(12, 73);
            this.LabelRightLeg.Name = "LabelRightLeg";
            this.LabelRightLeg.Size = new System.Drawing.Size(92, 13);
            this.LabelRightLeg.TabIndex = 4;
            this.LabelRightLeg.Text = "Правая нога: NA";
            // 
            // LabelLeftLeg
            // 
            this.LabelLeftLeg.AutoSize = true;
            this.LabelLeftLeg.Location = new System.Drawing.Point(12, 86);
            this.LabelLeftLeg.Name = "LabelLeftLeg";
            this.LabelLeftLeg.Size = new System.Drawing.Size(86, 13);
            this.LabelLeftLeg.TabIndex = 5;
            this.LabelLeftLeg.Text = "Левая нога: NA";
            // 
            // LabelBody
            // 
            this.LabelBody.AutoSize = true;
            this.LabelBody.Location = new System.Drawing.Point(12, 99);
            this.LabelBody.Name = "LabelBody";
            this.LabelBody.Size = new System.Drawing.Size(53, 13);
            this.LabelBody.TabIndex = 6;
            this.LabelBody.Text = "Тело: NA";
            // 
            // KinectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 156);
            this.Controls.Add(this.LabelBody);
            this.Controls.Add(this.LabelLeftLeg);
            this.Controls.Add(this.LabelRightLeg);
            this.Controls.Add(this.LabelRHand);
            this.Controls.Add(this.LabelLHand);
            this.Controls.Add(this.LabelHead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "KinectForm";
            this.Text = "Информация с Kinect";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label LabelHead;
        private System.Windows.Forms.Label LabelLHand;
        private System.Windows.Forms.Label LabelRHand;
        private System.Windows.Forms.Label LabelRightLeg;
        private System.Windows.Forms.Label LabelLeftLeg;
        private System.Windows.Forms.Label LabelBody;
    }
}