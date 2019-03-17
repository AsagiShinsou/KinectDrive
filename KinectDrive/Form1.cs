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


namespace KinectDrive
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void просмотретьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var KinectForm = new KinectForm();
            KinectForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var socket = new ClientWebSocket();
            socket.ConnectAsync(new Uri(ServerUrl.Text), CancellationToken.None);
            socket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
