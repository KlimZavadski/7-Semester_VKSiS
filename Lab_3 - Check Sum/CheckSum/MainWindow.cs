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

        int dataLengthBit;
        int controlBitCount;

        public MainWindow()
        {
            InitializeComponent();
            dataLength = 19;
            packageLength = dataLength + 4;
            flagSymbol = "~";  // 0x7
            escapeSymbol = "/"; // 0x
            flagEscape = "/0";

            dataLengthBit = dataLength * 8;
            controlBitCount = 8;
        }

        #region [Handlers]
        private void button_Send_Click(object sender, EventArgs e)
        {
            if (textBox_Input.Text == "") return;

            Thread.Sleep(500);
            String transportString = PackageData(textBox_Input.Text);
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
        private String CodeHamming(String data)
        {
            StringBuilder buffer = new StringBuilder(new String('0', dataLengthBit - controlBitCount));
            StringBuilder output = new StringBuilder();
            // Alignmenting output string.
            String empty = new String(' ', dataLength - 1 - data.Length);
            data += empty;
            
            // Generate Hamming code.
            String code = "";
            for (int degree = 0; degree < controlBitCount; degree++)
            {
                int mask = Convert.ToInt32(Math.Pow(2, degree));
                int count = 0;

                for (int number = 1; number <= dataLengthBit - controlBitCount; number++)
                {
                    // If number belong this group.
                    if ((number & mask) != 0)
                    {
                        int byteNumber = (number - 1) / 8;
                        int bitNumber = (number - 1) % 8;
                        
                        // Add bit if it = 1.
                        int value = ((int)data[byteNumber] >> bitNumber) & 1;
                        count += value;
                        // Build output string.
                        buffer[number - 1] = (char)(value + '0');
                    }
                }
                // Get XOR and put value.
                code += count % 2;
            }

            // Insert code bits.
            for (int i = 0; i < controlBitCount; i++)
            {
                buffer.Insert(Convert.ToInt32(Math.Pow(2, i)) - 1, code[i]);
            }

            // Set mistake.
            buffer[29] = (buffer[29] == '1') ? '0' : '1';

            // Convert to bytes string.
            int symbol = 0;
            for (int i = 0; i < dataLengthBit; i++)
            {
                if (buffer[i] == '1')
                {
                    symbol += 1 << (i % 8);
                }
                if (i % 8 == 7)
                {
                    output.Append((char)symbol);
                    symbol = 0;
                }
            }
            return output.ToString();
        }

        private String DecodeHamming(String input)
        {
            StringBuilder buffer = new StringBuilder();
            StringBuilder output = new StringBuilder(dataLength);
            String code = "";
            
            int errorPosition = 0;
            int alignment = 0;

            // Convert to bit string.
            foreach (char c in input)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (((c >> i) & 1) != 0)
                    {
                        buffer.Append("1");
                    }
                    else
                    {
                        buffer.Append("0");
                    }
                }
            }
            // Cut code bits.
            for (int i = 0; i < controlBitCount; i++)
            {
                int position = Convert.ToInt32(Math.Pow(2, i)) - 1 - i;
                code += buffer[position];
                buffer.Remove(position, 1);
            }

            // Generate data string.
            for (int degree = 0; degree < controlBitCount; degree++)
            {
                int mask = Convert.ToInt32(Math.Pow(2, degree));
                int count = (code[degree] == '1') ? 1 : 0;

                for (int number = 1; number <= dataLengthBit - controlBitCount; number++)
                {
                    // If number belong this group.
                    if ((number & mask) != 0)
                    {
                        if (buffer[number - 1] == '1')
                        {
                            count += 1;
                        }
                    }
                }
                // Get XOR and put value.
                if (count % 2 == 1)
                {
                    errorPosition += 1 << degree;
                    alignment = degree + 1;
                }
            }

            // Convert to bytes string.
            int symbol = 0;
            for (int i = 0; i < dataLengthBit - controlBitCount; i++)
            {
                if (buffer[i] == '1')
                {
                    symbol += 1 << (i % 8);
                }
                if (i % 8 == 7)
                {
                    output.Append((char)symbol);
                    symbol = 0;
                }
            }
            // Show bad symbol.
            if (errorPosition != 0)
            {
                output[(errorPosition + alignment) / 8] = '?';
            }

            return output.ToString();
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
                output += CodeHamming(data);
                output += (char)(dataLength);
                
                position += dataLength;
            }

            return output;
        }

        private String UnpackageData(String input)
        {
            StringBuilder output = new StringBuilder(input.Length);

            foreach (String package in input.Split(new String[] { flagSymbol }, StringSplitOptions.RemoveEmptyEntries))
            {
                int len = (int)package[package.Length - 1];

                String data = DecodeHamming(package.Substring(2, len))
                    .Replace(flagEscape, flagSymbol)
                    .Replace("//", escapeSymbol)
                    .Trim();
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