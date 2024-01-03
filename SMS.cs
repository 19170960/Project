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
using System.Runtime.InteropServices;
using System.Security.Cryptography;


namespace Encrypted_Communication_Application
{
    public partial class SMS : Form

    {
        int destPort = 44444;
        UdpClient udpServer;
        IPEndPoint destIPAddress;
        string sID = Environment.GetEnvironmentVariable("ACa2cee9e266faa2972eeb0f023a101292");
        string authT = Environment.GetEnvironmentVariable("7119be5da10ca581319b8bf4d1cabb6b");

        RSACryptoServiceProvider encryptionRSA = new RSACryptoServiceProvider(4096);

        public SMS()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void SMS_Load(object sender, EventArgs e)
        {

        
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home home2 = new Home();
            this.Hide();
            home2.Show();
        }

        private void flowLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            udpServer = new UdpClient(55555);
            destIPAddress = new IPEndPoint(IPAddress.Parse(textBox4.Text), destPort);
            udpServer.BeginReceive(new AsyncCallback(messageReceive), udpServer);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        public void button2_Click(object sender, EventArgs e)
        {
            string keyPublic = encryptionRSA.ToXmlString(false);
            string keyPrivate = encryptionRSA.ToXmlString(true);

            byte[] encryptedPlaintext = Encoding.UTF8.GetBytes(textBox1.Text);
            string encryptedPlaintext64bytes = Convert.ToBase64String(encryptionRSA.Encrypt(encryptedPlaintext, false));

            byte[] encryptedPlainTextBytes = Convert.FromBase64String(encryptedPlaintext64bytes);
            string decryptedPlaintext = Encoding.UTF8.GetString(encryptionRSA.Decrypt(encryptedPlainTextBytes, false));

            udpServer.Connect(destIPAddress);
            udpServer.Send(encryptedPlainTextBytes, textBox1.Text.Length);
            textBox1.Clear();
        }

        public void messageReceive(IAsyncResult AR)
        {
            string keyPublic = encryptionRSA.ToXmlString(false);
            string keyPrivate = encryptionRSA.ToXmlString(true);

            byte[] message = udpServer.EndReceive(AR, ref destIPAddress);
            udpServer.BeginReceive(new AsyncCallback(messageReceive), udpServer)

            byte[] encryptedPlaintext = Encoding.UTF8.GetBytes(textBox1.Text);
            string encryptedPlaintext64bytes = Convert.ToBase64String(encryptionRSA.Encrypt(encryptedPlaintext, false));

            byte[] encryptedPlainTextBytes = Convert.FromBase64String(encryptedPlaintext64bytes);
            string decryptedPlaintext = Encoding.UTF8.GetString(encryptionRSA.Decrypt(encryptedPlainTextBytes, false));
            
            richTextBox1.AppendText("> " + decryptedPlaintext + "\n");
        }


        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
