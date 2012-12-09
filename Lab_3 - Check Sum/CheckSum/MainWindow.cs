using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;

namespace CheckSum
{
    public partial class MainWindow : Form
    {
        int dataLength;
        int packageLength;
        String flagSymbol;
        String escapeSymbol;
        String flagEscape;

        public MainWindow()
        {
            InitializeComponent();
            dataLength = 19;
            packageLength = dataLength + 4;
            flagSymbol = "~";  // 0x7
            escapeSymbol = "/"; // 0x
            flagEscape = "/0";
        }

        #region [Handlers]
        private void button_Send_Click(object sender, EventArgs e)
        {
            if (textBox_Input.Text == "") return;

            Thread.Sleep(500);
            String transportString = PackageData(textBox_Input.Text);
            //String transportString = PackageData("qwertyuiop12345678");
            ShowPackages(transportString);
            Thread.Sleep(500);
            textBox_Output.Text = UnpackageData(transportString);
        }

        private void textBox_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_Send_Click(null, null);
            }
        }
        #endregion

        #region [Hamming]
        private String GetHammingCode(String data)
        {
            // Alignmenting output string.
            String empty = new String(' ', dataLength - 1 - data.Length);
            data += empty;
            
            // Generate Hamming code.
            int code = 0;
            for (int degree = 0; degree < 8; degree++)
            {
                int mask = Convert.ToInt32(Math.Pow(2, degree));
                int count = 0;

                for (int number = 1; number <= 144; number++)
                {
                    // If number belong this group.
                    if ((number & mask) != 0)
                    {
                        int byteNumber = (number - 1) / 8;
                        int bitNumber = (number - 1) % 8;
                        
                        // Add bit if it = 1.
                        count += ((int)data[byteNumber] >> bitNumber) & 1;
                    }
                }
                // Get XOR and put value.
                code += (count % 2) << degree;
            }

            return empty + ((char)code).ToString();
        }

        private Int32 IsDataCorrect()
        {
            return 0;
        }
        #endregion


        #region [Packaging]
        private String GetSubstring(String data, int position, int length)
        {
            return (data.Length > position + length) ?
                data.Substring(position, length) :
                data.Substring(position);
        }

        private String PackageData(String input)
        {
            int position = 0;
            String output = "";
            String header = "";
            header += flagSymbol;
            header += (char)128;
            header += (char)192;

            // Escape symbol '/'.
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == escapeSymbol[0])
                {
                    input = input.Insert(i, escapeSymbol);
                    i++;
                }
            }
            // Replace flag symbol '~'. 
            input = input.Replace(flagSymbol, flagEscape);

            while (input.Length > position)
            {
                String data = GetSubstring(input, position, dataLength - 1);

                output += header;
                output += data;
                output += GetHammingCode(data);
                output += (char)(data.Length);
                
                position += data.Length;
            }

            return output;
        }

        private String UnpackageData(String input)
        {
            StringBuilder output = new StringBuilder(input.Length);

            foreach (String package in input.Split(new String[] { flagSymbol }, StringSplitOptions.RemoveEmptyEntries))
            {
                int len = (int)package[package.Length - 1];
                int errorPosition = IsDataCorrect(package.Substring(2, dataLength));
                
                String data = package.Substring(2, len).Replace(flagEscape, flagSymbol).Replace("//", escapeSymbol);
                if (errorPosition != -1)
                {
                    data = data.Remove(errorPosition, 1).Insert(errorPosition, "?");
                }
                output.Append(data);
            }
            return output.ToString();
        }
        #endregion


        private void ShowPackages(String input)
        {
            textBox_Result.Text += Environment.NewLine + " # Flag  Source  Destination  Data\t\t   CheckSum" + Environment.NewLine;

            int count = 1;
            foreach (String package in input.Split(new String[] { flagSymbol }, StringSplitOptions.RemoveEmptyEntries))
            {
                textBox_Result.AppendText(String.Format("{0,2}", count++));
                textBox_Result.AppendText(String.Format("{0,5:X}", (int)flagSymbol[0]));
                textBox_Result.AppendText(String.Format("{0,8:X}", (int)package[0]));
                textBox_Result.AppendText(String.Format("{0,13:X}  ", (int)package[1]));
                int len = (int)package[package.Length - 1];
                textBox_Result.AppendText(String.Format("{0,-21}", package.Substring(2, len)));
                textBox_Result.AppendText(String.Format("{0}", len) + Environment.NewLine);
            }
        }
    }
}