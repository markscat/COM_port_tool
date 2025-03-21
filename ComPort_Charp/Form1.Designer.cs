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
            this.BuConnect = new System.Windows.Forms.Button();
            this.BuClear = new System.Windows.Forms.Button();
            this.comport = new System.Windows.Forms.ComboBox();
            this.board_rat = new System.Windows.Forms.ComboBox();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.SuspendLayout();
            // 
            // BuConnect
            // 
            this.BuConnect.Location = new System.Drawing.Point(632, 39);
            this.BuConnect.Name = "BuConnect";
            this.BuConnect.Size = new System.Drawing.Size(75, 23);
            this.BuConnect.TabIndex = 0;
            this.BuConnect.Text = "Connect";
            this.BuConnect.UseVisualStyleBackColor = true;
            this.BuConnect.Click += new System.EventHandler(this.BuConnect_Click);
            // 
            // BuClear
            // 
            this.BuClear.Location = new System.Drawing.Point(722, 39);
            this.BuClear.Name = "BuClear";
            this.BuClear.Size = new System.Drawing.Size(75, 22);
            this.BuClear.TabIndex = 1;
            this.BuClear.Text = "clear";
            this.BuClear.UseVisualStyleBackColor = true;
            // 
            // comport
            // 
            this.comport.FormattingEnabled = true;
            this.comport.Location = new System.Drawing.Point(38, 39);
            this.comport.Name = "comport";
            this.comport.Size = new System.Drawing.Size(121, 23);
            this.comport.TabIndex = 2;
            // 
            // board_rat
            // 
            this.board_rat.FormattingEnabled = true;
            this.board_rat.IntegralHeight = false;
            this.board_rat.Location = new System.Drawing.Point(235, 39);
            this.board_rat.Name = "board_rat";
            this.board_rat.Size = new System.Drawing.Size(109, 23);
            this.board_rat.TabIndex = 3;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(24, 68);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(874, 409);
            this.textBoxOutput.TabIndex = 4;
            this.textBoxOutput.TextChanged += new System.EventHandler(this.textBoxOutput_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Selected COM Port";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Selected BaudRate";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(910, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(910, 489);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.board_rat);
            this.Controls.Add(this.comport);
            this.Controls.Add(this.BuClear);
            this.Controls.Add(this.BuConnect);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BuConnect;
        private System.Windows.Forms.Button BuClear;
        private System.Windows.Forms.ComboBox comport;
        private System.Windows.Forms.ComboBox board_rat;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}

