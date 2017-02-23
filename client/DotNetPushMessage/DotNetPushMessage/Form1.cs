using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Quobject.SocketIoClientDotNet.Client;

namespace DotNetPushMessage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private DateTime st;

        private void btnPush_Click(object sender, EventArgs e)
        {

            var msg = this.txtMsg.Text;
            if (string.IsNullOrWhiteSpace(msg))
            {
                MessageBox.Show("请输入要推送的内容！");
                return;
            }

            SendMessage(msg);

        }

        void SendMessage(string msg)
        {

            var message = new { UserId = "guest1", Message = msg };

            var options = CreateOptions();
            var socket = IO.Socket("http://localhost:8888/", options);

            socket.Emit("sendtousers", JsonConvert.SerializeObject(message));

            //socket.On(Socket.EVENT_CONNECT, () =>
            //{
            //    socket.Emit("sendtousers", JsonConvert.SerializeObject(message));
            //   //Socket.Disconnect();
            //});

            //socket.On(Socket.EVENT_CONNECT_TIMEOUT, () =>
            //{
            //    MessageBox.Show("SocketIoClientDotNet：连接Node.js服务端超时...");
            //});

            //socket.On(Socket.EVENT_CONNECT_ERROR, () =>
            //{
            //    MessageBox.Show("SocketIoClientDotNet:连接Node.js服务端发生错误...");
            //});

            //socket.On("hi", (data) =>
            //{
            //    socket.Disconnect();
            //});

        }


        private IO.Options CreateOptions()
        {
            //var op = new IO.Options
            //{
            //    AutoConnect = true,
            //    Reconnection = true,
            //    ReconnectionAttempts = 5,
            //    ReconnectionDelay = 5,
            //    Timeout = 20,
            //    Secure = true,
            //    ForceNew = true,
            //    Multiplex = true
            //};

            var op = new IO.Options
            {
                AutoConnect = true,
                Reconnection = false
            };
            return op;
        }

    }
}
