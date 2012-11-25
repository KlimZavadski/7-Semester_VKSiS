using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;

namespace ButeStuffing
{
    public partial class MainWindow : Form
    {
        int dataLength;
        int packageLength;        
        char flagSymbol;

        public MainWindow()
        {
            InitializeComponent();
            dataLength = 19;
            packageLength = dataLength + 4;
            flagSymbol = '~';  // 0x7
        }

        #region [Handlers]
        private void button_Send_Click(object sender, EventArgs e)
        {
            if (textBox_Input.Text == "") return;

            Thread.Sleep(500);
            String transportString = PackageData(textBox_Input.Text, flagSymbol);
            ShowPackages(transportString, flagSymbol);
            Thread.Sleep(500);
            textBox_Output.Text = textBox_Input.Text;
        }

        private void textBox_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_Send_Click(null, null);
            }
        }
        #endregion


        private String GetSubstring(String input, int position, int length)
        {
            return (input.Length > position + length) ?
                input.Substring(position, length) :
                input.Substring(position);
        }

        private String PackageData(String input, char flag)
        {
            int position = 0;
            String output = "";
            String header = "";
            header += flag;
            header += (char)128;
            header += (char)192;

            // Escaping.
            // Replace escape symbol '~' and escaped '/'.
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '/')
                {
                    input = input.Insert(i, "/");
                    i++;
                }
            }
            input = input.Replace(new String(flag, 1), "/0");

            while (input.Length > position)
            {
                output += header;
                output += GetSubstring(input, position, dataLength);
                output += (char)(output.Length % packageLength);
                position += dataLength;
            }
            return output;
        }

        private void ShowPackages(String input, char flag)
        {
            textBox_Result.Text += Environment.NewLine + " # Flag  Source  Destination  Data\t\t   CheckSum" + Environment.NewLine;

            int count = 1;
            foreach (String package in input.Split(new char[] {flag}, StringSplitOptions.RemoveEmptyEntries))
            {
                textBox_Result.AppendText(String.Format("{0,2}", count++));
                textBox_Result.AppendText(String.Format("{0,5:X}", (int)flag));
                textBox_Result.AppendText(String.Format("{0,8:X}", (int)package[0]));
                textBox_Result.AppendText(String.Format("{0,13:X}  ", (int)package[1]));
                int last = package.Length - 1;
                textBox_Result.AppendText(String.Format("{0,-21}", package.Substring(2, last - 2)));
                textBox_Result.AppendText(String.Format("{0}", (int)package[last]) + Environment.NewLine);
            }
        }
    }
}