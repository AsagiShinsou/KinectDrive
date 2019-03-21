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
            public string neck_x { get; set; }
            [DataMember]
            public string neck_y { get; set; }
            [DataMember]
            public string neck_z { get; set; }
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
            
            [DataMember]
            public string spineShoulder_x { get; set; }
            [DataMember]
            public string spineShoulder_y { get; set; }
            [DataMember]
            public string spineShoulder_z { get; set; }

            [DataMember]
            public string shoulderLeft_x { get; set; }
            [DataMember]
            public string shoulderLeft_y { get; set; }
            [DataMember]
            public string shoulderLeft_z { get; set; }


            [DataMember]
            public string shoulderRight_x { get; set; }
            [DataMember]
            public string shoulderRight_y { get; set; }
            [DataMember]
            public string shoulderRight_z { get; set; }


            [DataMember]
            public string elbowLeft_x { get; set; }
            [DataMember]
            public string elbowLeft_y { get; set; }
            [DataMember]
            public string elbowLeft_z { get; set; }

            [DataMember]
            public string elbowRight_x { get; set; }
            [DataMember]
            public string elbowRight_y { get; set; }
            [DataMember]
            public string elbowRight_z { get; set; }

            [DataMember]
            public string wristLeft_x { get; set; }
            [DataMember]
            public string wristLeft_y { get; set; }
            [DataMember]
            public string wristLeft_z { get; set; }

            [DataMember]
            public string wristRight_x { get; set; }
            [DataMember]
            public string wristRight_y { get; set; }
            [DataMember]
            public string wristRight_z { get; set; }

            [DataMember]
            public string hip_x { get; set; }
            [DataMember]
            public string hip_y { get; set; }
            [DataMember]
            public string hip_z { get; set; }

            

            [DataMember]
            public string action { get; set; }

            public kinectJson()
            {
                this.action = "kinect_json";
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
                //this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;

                this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
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
            //Console.WriteLine(json);
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

                            if (joint.JointType.ToString() == "Neck")
                            {
                                //klog.Text = klog.Text + "HandR x:" + joint.Position.X.ToString() + " y:" + joint.Position.Y.ToString() + " z:" + joint.Position.Z.ToString();
                                this.kinectData.neck_x = joint.Position.X.ToString();
                                this.kinectData.neck_y = joint.Position.Y.ToString();
                                this.kinectData.neck_z = joint.Position.Z.ToString();
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

                            if (joint.JointType.ToString() == "SpineShoulder")
                            {
                                //klog.Text = klog.Text + "HandL x:" + joint.Position.X.ToString() + " y:" + joint.Position.Y.ToString() + " z:" + joint.Position.Z.ToString();
                                this.kinectData.spineShoulder_x = joint.Position.X.ToString();
                                this.kinectData.spineShoulder_y = joint.Position.Y.ToString();
                                this.kinectData.spineShoulder_z = joint.Position.Z.ToString();
                            }

                            if (joint.JointType.ToString() == "ShoulderLeft")
                            {
                                //klog.Text = klog.Text + "HandL x:" + joint.Position.X.ToString() + " y:" + joint.Position.Y.ToString() + " z:" + joint.Position.Z.ToString();
                                this.kinectData.shoulderLeft_x = joint.Position.X.ToString();
                                this.kinectData.shoulderLeft_y = joint.Position.Y.ToString();
                                this.kinectData.shoulderLeft_z = joint.Position.Z.ToString();
                            }

                            if (joint.JointType.ToString() == "ShoulderRight")
                            {
                                //klog.Text = klog.Text + "HandL x:" + joint.Position.X.ToString() + " y:" + joint.Position.Y.ToString() + " z:" + joint.Position.Z.ToString();
                                this.kinectData.shoulderRight_x = joint.Position.X.ToString();
                                this.kinectData.shoulderRight_y = joint.Position.Y.ToString();
                                this.kinectData.shoulderRight_z = joint.Position.Z.ToString();
                            }

                            if (joint.JointType.ToString() == "ElbowLeft")
                            {
                                //klog.Text = klog.Text + "HandL x:" + joint.Position.X.ToString() + " y:" + joint.Position.Y.ToString() + " z:" + joint.Position.Z.ToString();
                                this.kinectData.elbowLeft_x = joint.Position.X.ToString();
                                this.kinectData.elbowLeft_y = joint.Position.Y.ToString();
                                this.kinectData.elbowLeft_z = joint.Position.Z.ToString();
                            }

                            if (joint.JointType.ToString() == "ElbowRight")
                            {
                                //klog.Text = klog.Text + "HandL x:" + joint.Position.X.ToString() + " y:" + joint.Position.Y.ToString() + " z:" + joint.Position.Z.ToString();
                                this.kinectData.elbowRight_x = joint.Position.X.ToString();
                                this.kinectData.elbowRight_y = joint.Position.Y.ToString();
                                this.kinectData.elbowRight_z = joint.Position.Z.ToString();
                            }


                            if (joint.JointType.ToString() == "WristLeft")
                            {
                                //klog.Text = klog.Text + "HandL x:" + joint.Position.X.ToString() + " y:" + joint.Position.Y.ToString() + " z:" + joint.Position.Z.ToString();
                                this.kinectData.wristLeft_x = joint.Position.X.ToString();
                                this.kinectData.wristLeft_y = joint.Position.Y.ToString();
                                this.kinectData.wristLeft_z = joint.Position.Z.ToString();
                            }

                            if (joint.JointType.ToString() == "WristRight")
                            {
                                //klog.Text = klog.Text + "HandL x:" + joint.Position.X.ToString() + " y:" + joint.Position.Y.ToString() + " z:" + joint.Position.Z.ToString();
                                this.kinectData.wristRight_x = joint.Position.X.ToString();
                                this.kinectData.wristRight_y = joint.Position.Y.ToString();
                                this.kinectData.wristRight_z = joint.Position.Z.ToString();
                            }

                            if (joint.JointType.ToString() == "Hip")
                            {
                                //klog.Text = klog.Text + "HandL x:" + joint.Position.X.ToString() + " y:" + joint.Position.Y.ToString() + " z:" + joint.Position.Z.ToString();
                                this.kinectData.hip_x = joint.Position.X.ToString();
                                this.kinectData.hip_y = joint.Position.Y.ToString();
                                this.kinectData.hip_z = joint.Position.Z.ToString();
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
