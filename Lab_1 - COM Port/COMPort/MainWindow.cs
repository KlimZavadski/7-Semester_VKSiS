using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO.Ports;


namespace COMPort
{
    public partial class MainWindow : Form
    {
        SerialPort comPort;


        public MainWindow()
        {
            InitializeComponent();
            comPort = null;
            ComboBox_Name.DataSource = SerialPort.GetPortNames().OrderBy(x => x).ToList();
            ComboBox_Name.SelectedIndex = 0;
            ComboBox_Rate.SelectedIndex = 7;
        }

        private bool InitializePort(String name, int rate)
        {
            try
            {
                if (comPort != null)
                {
                    return false;
                }
                comPort = new SerialPort(name, rate, Parity.None, 8, StopBits.One);
                comPort.Open();
            }
            catch (System.Exception ex)
            {
                comPort = null;
                TextBox_Chat.AppendText("Error: " + ex.Message + Environment.NewLine);
                return false;
            }
            comPort.ErrorReceived += new SerialErrorReceivedEventHandler(ErrorReceived);
            comPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);

            return true;
        }

        private void ErrorReceived(object sender, EventArgs e)
        {
            TextBox_Chat.AppendText("Error Received!" + Environment.NewLine);
        }

        private void DataReceived(object sender, EventArgs e)
        {
            TextBox_Chat.AppendText(comPort.ReadExisting() + Environment.NewLine);
        }

        private void Send(String message)
        {
            if (comPort != null)
            {
                comPort.RtsEnable = true;
                comPort.Write(message);
                comPort.RtsEnable = false;
            }
        }


        private void Button_Send_Click(object sender, EventArgs e)
        {
            String message = TextBox_Message.Text;
            Send(message);
            TextBox_Chat.AppendText(message + Environment.NewLine);
            TextBox_Message.Clear();
        }

        private void Button_Connect_Click(object sender, EventArgs e)
        {
            int i = ComboBox_Name.SelectedIndex;
            int j = ComboBox_Rate.SelectedIndex;
            bool isConnected = InitializePort(ComboBox_Name.Items[i].ToString(), Convert.ToInt32(ComboBox_Rate.Items[j]));
            if (isConnected == true)
            {
                TextBox_Chat.AppendText("You are connected." + Environment.NewLine);
            }
        }

        private void Button_Disconnect_Click(object sender, EventArgs e)
        {
            if (comPort != null)
            {
                comPort.Close();
                comPort = null;
                TextBox_Chat.AppendText("You are disconnected." + Environment.NewLine);
            }
        }

        private void TextBox_Message_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Button_Send_Click(null, null);
            }
        }

        private void TextBox_Message_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox_Message.Clear();
            }
        }
    }
}
