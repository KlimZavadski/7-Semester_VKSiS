namespace COMPort
{
    partial class MainWindow
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextBox_Chat = new System.Windows.Forms.TextBox();
            this.TextBox_Message = new System.Windows.Forms.TextBox();
            this.Button_Send = new System.Windows.Forms.Button();
            this.ComboBox_Name = new System.Windows.Forms.ComboBox();
            this.Button_Connect = new System.Windows.Forms.Button();
            this.ComboBox_Rate = new System.Windows.Forms.ComboBox();
            this.Button_Disconnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextBox_Chat
            // 
            this.TextBox_Chat.Location = new System.Drawing.Point(12, 39);
            this.TextBox_Chat.Multiline = true;
            this.TextBox_Chat.Name = "TextBox_Chat";
            this.TextBox_Chat.ReadOnly = true;
            this.TextBox_Chat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox_Chat.Size = new System.Drawing.Size(360, 180);
            this.TextBox_Chat.TabIndex = 4;
            // 
            // TextBox_Message
            // 
            this.TextBox_Message.Location = new System.Drawing.Point(12, 225);
            this.TextBox_Message.Multiline = true;
            this.TextBox_Message.Name = "TextBox_Message";
            this.TextBox_Message.Size = new System.Drawing.Size(360, 90);
            this.TextBox_Message.TabIndex = 5;
            this.TextBox_Message.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_Message_KeyDown);
            this.TextBox_Message.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_Message_KeyUp);
            // 
            // Button_Send
            // 
            this.Button_Send.Location = new System.Drawing.Point(13, 321);
            this.Button_Send.Name = "Button_Send";
            this.Button_Send.Size = new System.Drawing.Size(359, 29);
            this.Button_Send.TabIndex = 6;
            this.Button_Send.Text = "Send";
            this.Button_Send.UseVisualStyleBackColor = true;
            this.Button_Send.Click += new System.EventHandler(this.Button_Send_Click);
            // 
            // ComboBox_Name
            // 
            this.ComboBox_Name.FormattingEnabled = true;
            this.ComboBox_Name.Location = new System.Drawing.Point(12, 12);
            this.ComboBox_Name.Name = "ComboBox_Name";
            this.ComboBox_Name.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ComboBox_Name.Size = new System.Drawing.Size(90, 21);
            this.ComboBox_Name.TabIndex = 0;
            // 
            // Button_Connect
            // 
            this.Button_Connect.Location = new System.Drawing.Point(204, 12);
            this.Button_Connect.Name = "Button_Connect";
            this.Button_Connect.Size = new System.Drawing.Size(75, 21);
            this.Button_Connect.TabIndex = 3;
            this.Button_Connect.Text = "Connect";
            this.Button_Connect.UseVisualStyleBackColor = true;
            this.Button_Connect.Click += new System.EventHandler(this.Button_Connect_Click);
            // 
            // ComboBox_Rate
            // 
            this.ComboBox_Rate.FormattingEnabled = true;
            this.ComboBox_Rate.Items.AddRange(new object[] {
            "110",
            "150",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.ComboBox_Rate.Location = new System.Drawing.Point(108, 12);
            this.ComboBox_Rate.Name = "ComboBox_Rate";
            this.ComboBox_Rate.Size = new System.Drawing.Size(90, 21);
            this.ComboBox_Rate.TabIndex = 1;
            // 
            // Button_Disconnect
            // 
            this.Button_Disconnect.Location = new System.Drawing.Point(297, 12);
            this.Button_Disconnect.Name = "Button_Disconnect";
            this.Button_Disconnect.Size = new System.Drawing.Size(75, 21);
            this.Button_Disconnect.TabIndex = 7;
            this.Button_Disconnect.Text = "Disconnect";
            this.Button_Disconnect.UseVisualStyleBackColor = true;
            this.Button_Disconnect.Click += new System.EventHandler(this.Button_Disconnect_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 362);
            this.Controls.Add(this.Button_Disconnect);
            this.Controls.Add(this.ComboBox_Rate);
            this.Controls.Add(this.Button_Connect);
            this.Controls.Add(this.ComboBox_Name);
            this.Controls.Add(this.Button_Send);
            this.Controls.Add(this.TextBox_Message);
            this.Controls.Add(this.TextBox_Chat);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBox_Chat;
        private System.Windows.Forms.TextBox TextBox_Message;
        private System.Windows.Forms.Button Button_Send;
        private System.Windows.Forms.ComboBox ComboBox_Name;
        private System.Windows.Forms.Button Button_Connect;
        private System.Windows.Forms.ComboBox ComboBox_Rate;
        private System.Windows.Forms.Button Button_Disconnect;
    }
}

