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
using System.IO;

namespace Chef
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        public StreamReader STR;
        public StreamWriter STW;
        public string receive;
        public string text_to_send;
        public string gg;
        public string hh;
        public Form1()
        {
            InitializeComponent();

            IPAddress[] localIP = Dns.GetHostAddresses(Dns.GetHostName());       //get own ip
            foreach (IPAddress address in localIP)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    textBox2.Text = address.ToString();
                }
            }
        }
    

     

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (client.Connected)
            {
                try
                {
                    receive = STR.ReadLine();
                    this.listBox1.Invoke(new MethodInvoker(delegate () { listBox1.Items.Add(receive + "\n"); }));
                    receive = "";
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message.ToString());
                }

            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            if (client.Connected)
            {
                STW.WriteLine(text_to_send);
                this.listBox1.Invoke(new MethodInvoker(delegate () { listBox1.Items.Add(text_to_send + "\n"); }));
            }
            else
            {
                MessageBox.Show("Send Failed");
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox7.Text != "")
            {
                text_to_send = textBox7.Text;
                backgroundWorker2.RunWorkerAsync();
            }
            textBox7.Text = "";
        }
        private void button6_Click_1(object sender, EventArgs e)
        {
            client = new TcpClient();
            IPEndPoint IP_End = new IPEndPoint(IPAddress.Parse(textBox2.Text), int.Parse(textBox5.Text));

            try
            {
                client.Connect(IP_End);
                if (client.Connected)
                {
                    textBox6.AppendText("Connected to Sever  " + "\n");
                    STW = new StreamWriter(client.GetStream());
                    STR = new StreamReader(client.GetStream());
                    STW.AutoFlush = true;

                    backgroundWorker1.RunWorkerAsync();                      //start Receving data in BackGround
                    backgroundWorker3.RunWorkerAsync();
                    backgroundWorker2.WorkerSupportsCancellation = true;    //Ability to cancel this thread
                    backgroundWorker4.WorkerSupportsCancellation = true;
                }

            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message.ToString());
            }
        }

        private void button4_Click_1(object sender, EventArgs e)

        {
            TcpListener listener = new TcpListener(IPAddress.Any, int.Parse(textBox3.Text));
            listener.Start();
            client = listener.AcceptTcpClient();
            STR = new StreamReader(client.GetStream());
            STW = new StreamWriter(client.GetStream());
            STW.AutoFlush = true;

            backgroundWorker1.RunWorkerAsync();                      //start Receving data in BackGround
            backgroundWorker3.RunWorkerAsync();
            backgroundWorker2.WorkerSupportsCancellation = true;    //Ability to cancel this thread
            backgroundWorker4.WorkerSupportsCancellation = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            text_to_send = "Cheese Burger";
            backgroundWorker2.RunWorkerAsync();
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            while (client.Connected)
            {
                try
                {
                    receive = STR.ReadLine();
                    this.textBox6.Invoke(new MethodInvoker(delegate () { textBox6.AppendText(receive + "\n"); }));
                    receive = "";
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message.ToString());
                }

            }
        }

        private void backgroundWorker3_DoWork_1(object sender, DoWorkEventArgs e)
        {
            while (client.Connected)
            {
                try
                {
                    receive = STR.ReadLine();
                    this.textBox6.Invoke(new MethodInvoker(delegate () { textBox6.AppendText(gg + "\n"); }));
                    receive = "";
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message.ToString());
                }

            }
        }

        private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            if (client.Connected)
            {
                STW.WriteLine(hh);
                this.textBox6.Invoke(new MethodInvoker(delegate () { textBox6.AppendText(hh + "\n"); }));
            }
            else
            {
                MessageBox.Show("Send Failed");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

        }
    }
}
