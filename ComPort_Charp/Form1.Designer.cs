using System;

namespace ComPort_Charp
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.BuConnect = new System.Windows.Forms.Button();
            this.board_rat = new System.Windows.Forms.ComboBox();
            this.BuClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.comport = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Data_Bit = new System.Windows.Forms.ComboBox();
            this.Stop_Bits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Parity = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Flow_Control = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ReadTimeout = new System.Windows.Forms.ComboBox();
            this.WriteTimeout = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.BuRecord = new System.Windows.Forms.Button();
            this.TextBoxIn = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(877, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Selected COM Port";
            // 
            // BuConnect
            // 
            this.BuConnect.Location = new System.Drawing.Point(306, 60);
            this.BuConnect.Name = "BuConnect";
            this.BuConnect.Size = new System.Drawing.Size(75, 23);
            this.BuConnect.TabIndex = 0;
            this.BuConnect.Text = "Connect";
            this.BuConnect.UseVisualStyleBackColor = true;
            this.BuConnect.Click += new System.EventHandler(this.BuConnect_Click);
            // 
            // board_rat
            // 
            this.board_rat.FormattingEnabled = true;
            this.board_rat.IntegralHeight = false;
            this.board_rat.Location = new System.Drawing.Point(134, 60);
            this.board_rat.Name = "board_rat";
            this.board_rat.Size = new System.Drawing.Size(109, 20);
            this.board_rat.TabIndex = 3;
            // 
            // BuClear
            // 
            this.BuClear.Location = new System.Drawing.Point(387, 59);
            this.BuClear.Name = "BuClear";
            this.BuClear.Size = new System.Drawing.Size(75, 22);
            this.BuClear.TabIndex = 1;
            this.BuClear.Text = "clear";
            this.BuClear.UseVisualStyleBackColor = true;
            this.BuClear.Click += new System.EventHandler(this.BuClear_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Selected BaudRate";
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutput.Location = new System.Drawing.Point(4, 89);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(727, 406);
            this.textBoxOutput.TabIndex = 4;
            this.textBoxOutput.SizeChanged += new System.EventHandler(this.Form1_Load);
            //this.textBoxOutput.TextChanged += new System.EventHandler(this.TextBoxOutput_TextChanged);
            // 
            // comport
            // 
            this.comport.FormattingEnabled = true;
            this.comport.Location = new System.Drawing.Point(7, 60);
            this.comport.Name = "comport";
            this.comport.Size = new System.Drawing.Size(121, 20);
            this.comport.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(767, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Data Bits";
            // 
            // Data_Bit
            // 
            this.Data_Bit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Data_Bit.FormattingEnabled = true;
            this.Data_Bit.Location = new System.Drawing.Point(747, 112);
            this.Data_Bit.Name = "Data_Bit";
            this.Data_Bit.Size = new System.Drawing.Size(121, 20);
            this.Data_Bit.TabIndex = 8;
            // 
            // Stop_Bits
            // 
            this.Stop_Bits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Stop_Bits.FormattingEnabled = true;
            this.Stop_Bits.Location = new System.Drawing.Point(747, 159);
            this.Stop_Bits.Name = "Stop_Bits";
            this.Stop_Bits.Size = new System.Drawing.Size(121, 20);
            this.Stop_Bits.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(767, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Stop Bits";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(767, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "Parity";
            // 
            // Parity
            // 
            this.Parity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Parity.FormattingEnabled = true;
            this.Parity.Location = new System.Drawing.Point(747, 204);
            this.Parity.Name = "Parity";
            this.Parity.Size = new System.Drawing.Size(121, 20);
            this.Parity.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(767, 241);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "Flow Control";
            // 
            // Flow_Control
            // 
            this.Flow_Control.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Flow_Control.FormattingEnabled = true;
            this.Flow_Control.Location = new System.Drawing.Point(747, 256);
            this.Flow_Control.Name = "Flow_Control";
            this.Flow_Control.Size = new System.Drawing.Size(121, 20);
            this.Flow_Control.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(767, 301);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "Read Timeout";
            // 
            // ReadTimeout
            // 
            this.ReadTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ReadTimeout.FormattingEnabled = true;
            this.ReadTimeout.Location = new System.Drawing.Point(747, 316);
            this.ReadTimeout.Name = "ReadTimeout";
            this.ReadTimeout.Size = new System.Drawing.Size(121, 20);
            this.ReadTimeout.TabIndex = 16;
            // 
            // WriteTimeout
            // 
            this.WriteTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WriteTimeout.FormattingEnabled = true;
            this.WriteTimeout.Location = new System.Drawing.Point(747, 374);
            this.WriteTimeout.Name = "WriteTimeout";
            this.WriteTimeout.Size = new System.Drawing.Size(121, 20);
            this.WriteTimeout.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(767, 359);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "Write Timeout ";
            // 
            // BuRecord
            // 
            this.BuRecord.Location = new System.Drawing.Point(498, 60);
            this.BuRecord.Name = "BuRecord";
            this.BuRecord.Size = new System.Drawing.Size(75, 23);
            this.BuRecord.TabIndex = 19;
            this.BuRecord.Text = "Record";
            this.BuRecord.UseVisualStyleBackColor = true;
            this.BuRecord.Click += new System.EventHandler(this.BuRecord_Click);
            // 
            // TextBoxIn
            // 
            this.TextBoxIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TextBoxIn.Location = new System.Drawing.Point(4, 522);
            this.TextBoxIn.Name = "TextBoxIn";
            this.TextBoxIn.Size = new System.Drawing.Size(727, 22); 
            this.TextBoxIn.TabIndex = 20;
            this.TextBoxIn.TextChanged += new System.EventHandler(this.TextBoxIn_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 504);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 12);
            this.label9.TabIndex = 21;
            this.label9.Text = "Message Out";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(877, 581);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.TextBoxIn);
            this.Controls.Add(this.BuRecord);
            this.Controls.Add(this.WriteTimeout);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ReadTimeout);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Flow_Control);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Parity);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Stop_Bits);
            this.Controls.Add(this.Data_Bit);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comport);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.board_rat);
            this.Controls.Add(this.BuClear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BuConnect);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            //this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comport;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BuClear;
        private System.Windows.Forms.ComboBox board_rat;
        private System.Windows.Forms.Button BuConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Data_Bit;
        private System.Windows.Forms.ComboBox Stop_Bits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox Parity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox Flow_Control;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox ReadTimeout;
        private System.Windows.Forms.ComboBox WriteTimeout;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button BuRecord;
        private System.Windows.Forms.TextBox TextBoxIn;
        private System.Windows.Forms.Label label9;
    }
}

