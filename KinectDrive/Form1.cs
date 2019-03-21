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

using System.Threading;
using KinectManagement;
using Microsoft.Kinect;
using WebSocket4Net;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using Newtonsoft.Json;




namespace KinectDrive
{
   
   

    public partial class MainForm : Form
    {
        public WebSocket websocket;
        private KinectSensor sensor;
        public KinectForm kform;
        public kinectJson kinectData;


        public class kinectJson
        {
            [DataMember]
            public string head_x { get; set; }
            [DataMember]
            public string head_y { get; set; }
            [DataMember]
            public string head_z { get; set; }

            [DataMember]
            public string armRight_x { get; set; }
            [DataMember]
            public string armRight_y { get; set; }
            [DataMember]
            public string armRight_z { get; set; }

            [DataMember]
            public string armLeft_x { get; set; }
            [DataMember]
            public string armLeft_y { get; set; }
            [DataMember]
            public string armLeft_z { get; set; }
            

            public kinectJson()
            {

            }


        }
        
        /*
                Person person1 = new Person("Tom", 29, new Company("Microsoft"));
                Person person2 = new Person("Bill", 25, new Company("Apple"));
                Person[] people = new Person[] { person1, person2 };

                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Person[]));
             */


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

            this.kinectData = new kinectJson();

        }

        private void просмотретьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kform.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            websocket = new WebSocket("ws://mailshark.ru:3000/");//создаем вебсокет
            
           /* websocket.Opened += new EventHandler(websocket_Opened);//событие возникающее в момент открытия
            websocket.Error += new EventHandler<ErrorEventArgs>(websocket_Error); //событие возникающее при ошибке
            websocket.Closed += new EventHandler(websocket_Closed); //событие закрытия
            websocket.MessageReceived += new EventHandler(websocket_MessageReceived);//получение сообщений*/
            websocket.Open();//подключиться
            while (websocket.State == WebSocketState.Connecting) { };
            KinectTimer.Enabled = true;


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
            
            string json = JsonConvert.SerializeObject(this.kinectData);
           
            websocket.Send(json);
           
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
                                
                                this.kinectData.head_x = joint.Position.X.ToString();
                                this.kinectData.head_y = joint.Position.Y.ToString();
                                this.kinectData.head_z = joint.Position.Z.ToString();
                            }

                            if (joint.JointType.ToString() == "HandRight")
                            {
                                //klog.Text = klog.Text + "HandR x:" + joint.Position.X.ToString() + " y:" + joint.Position.Y.ToString() + " z:" + joint.Position.Z.ToString();
                                this.kinectData.armRight_x = joint.Position.X.ToString();
                                this.kinectData.armRight_y = joint.Position.Y.ToString();
                                this.kinectData.armRight_z = joint.Position.Z.ToString();
                            }

                            if (joint.JointType.ToString() == "HandLeft")
                            {
                                //klog.Text = klog.Text + "HandL x:" + joint.Position.X.ToString() + " y:" + joint.Position.Y.ToString() + " z:" + joint.Position.Z.ToString();
                                this.kinectData.armLeft_x = joint.Position.X.ToString();
                                this.kinectData.armLeft_y = joint.Position.Y.ToString();
                                this.kinectData.armLeft_z = joint.Position.Z.ToString();
                            }
                        }

                            Application.DoEvents();
 

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
