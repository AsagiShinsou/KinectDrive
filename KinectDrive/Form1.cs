using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using KinectManagement;
using Microsoft.Kinect;


namespace KinectDrive
{

    public partial class MainForm : Form
    {
        private KinectSensor sensor;
        public KinectForm kform;
        public MainForm()
        {
            InitializeComponent();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            kform = new KinectForm();


            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

            if (null != this.sensor)
            {
                this.sensor.SkeletonStream.Enable();
                StatusLabel.Text = "Kinect connected!";
                this.sensor.Start();

                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;
                this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;

                //this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
            }

        }

        private void просмотретьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            kform.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var socket = new ClientWebSocket();
            socket.ConnectAsync(new Uri(ServerUrl.Text), CancellationToken.None);
            var encoded = Encoding.UTF8.GetBytes("hi there");
            var buffer = new ArraySegment<Byte>(encoded, 0, encoded.Length);
            socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);

            //socket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private void обновитьСтатусToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != this.sensor)
            {
                this.sensor.Stop();
            }
        }

        private void KinectTimer_Tick(object sender, EventArgs e)
        {
            klog.Text = "";
        }


        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {

            Skeleton[] skeletons = new Skeleton[0];



            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);


                    foreach (Skeleton skel in skeletons)
                    {
                        foreach (Joint joint in skel.Joints)
                        {
                            if (joint.JointType.ToString() == "Head")
                            {
                                klog.Text = "Head x:" +joint.Position.X.ToString()+" y:"+ joint.Position.Y.ToString()+" z:"+joint.Position.Z.ToString();   
                            }

                            if (joint.JointType.ToString() == "HandRight")
                            {
                                //klog.Text = klog.Text + "HandR x:" + joint.Position.X.ToString() + " y:" + joint.Position.Y.ToString() + " z:" + joint.Position.Z.ToString();
                            }

                            if (joint.JointType.ToString() == "HandLeft")
                            {
                                //klog.Text = klog.Text + "HandL x:" + joint.Position.X.ToString() + " y:" + joint.Position.Y.ToString() + " z:" + joint.Position.Z.ToString();
                            }

                            Application.DoEvents();


                        }

                    }
                }
            }


        }

        private void klog_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
