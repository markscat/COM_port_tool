using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComPort_Charp
{
    public partial class Form1 : Form
    {
        private SerialPort _serialPort;
        //private ComboBox cmbPorts;


        public Form1()
        {
            InitializeComponent();
            this.Text = "串口通訊工具"; // 設定視窗標題

            _serialPort = new SerialPort();
            board_rat.Items.AddRange(new object[] { 9600, 19200, 38400, 57600, 115200, 256000 });
            board_rat.SelectedIndex = 0;



            string[] ports = SerialPort.GetPortNames();  // 獲取可用的 COM 端口
            if (ports.Length == 0)
            {
                MessageBox.Show("未偵測到任何可用的 COM 端口！請檢查設備連接。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            

            // 在 ComboBox 中填充所有串口名稱
            //cmbPorts.Items.AddRange(ports);
            comport.Items.AddRange(SerialPort.GetPortNames());
            if (comport.Items.Count > 0)
                comport.SelectedIndex = 0;  // 預設選擇第一個端口

            //Controls.Add(textBoxOutput);  // 添加到 Form 控件中


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 在ComboBox中填充所有串口名稱
            comport.Items.AddRange(SerialPort.GetPortNames());
            


        }

        private void BuConnect_Click(object sender, EventArgs e)
        {
            if (_serialPort.IsOpen) // 如果串口已開啟，則執行關閉操作
            {
                try
                {
                    _serialPort.Close();
                    BuConnect.Text = "Connect"; // 按鈕顯示「Connect」
                    MessageBox.Show("串口已關閉！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("無法關閉串口：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else // 如果串口未開啟，則執行開啟操作
            {
                if (comport.SelectedItem == null)
                {
                    MessageBox.Show("請先選擇一個 COM 端口！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (board_rat.SelectedItem == null)
                {
                    MessageBox.Show("請選擇鮑率！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string selectedPort = comport.SelectedItem.ToString();
                int selectedBaudRate = Convert.ToInt32(board_rat.SelectedItem);

                try
                {
                    _serialPort.PortName = selectedPort;
                    _serialPort.BaudRate = selectedBaudRate;
                    _serialPort.Open();
                    _serialPort.DataReceived += SerialPort_DataReceived;
                    BuConnect.Text = "Disconnect"; // 按鈕顯示「Disconnect」
                    MessageBox.Show("串口已成功開啟！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("無法開啟串口：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

#if BuConnect_Click_one
        private void BuConnect_Click(object sender, EventArgs e)
        {
            if (comport.SelectedItem == null)
            {
                MessageBox.Show("請先選擇一個 COM 端口！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (board_rat.SelectedItem == null)
            {
                MessageBox.Show("請選擇鮑率！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string selectedPort = comport.SelectedItem.ToString();
            int selectedBaudRate = Convert.ToInt32(board_rat.SelectedItem);

            MessageBox.Show("嘗試開啟端口：" + selectedPort, "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                _serialPort.PortName = selectedPort;
                _serialPort.BaudRate = selectedBaudRate;
                _serialPort.Open();
                _serialPort.DataReceived += SerialPort_DataReceived;
                MessageBox.Show("串口已成功開啟！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("無法開啟串口：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


#endif
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // 接收到數據後顯示
            string data = _serialPort.ReadExisting();
            Invoke(new Action(() => textBoxOutput.AppendText(data)));
        }
        private void btnClosePort_Click(object sender, EventArgs e)
        {

        }

        private void textBoxOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

}
