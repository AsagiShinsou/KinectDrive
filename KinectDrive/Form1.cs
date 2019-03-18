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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != this.sensor)
            {
                this.sensor.Stop();
            }
        }

        private void KinectTimer_Tick(object sender, EventArgs e)
        {
            if (null != this.sensor)
            {
                
            }
        }


        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            
            Skeleton[] skeletons = new Skeleton[0];
            if (skeletons.Length != 0)
            {
                klog.Text = "SKEL FOUND";
                foreach (Skeleton skel in skeletons)
                {
                    
                }
            }
        }
    }
}
