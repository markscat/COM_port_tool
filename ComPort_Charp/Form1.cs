#define BuConnect_Click_three



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ComPort_Charp
{ /// <summary>
  /// 主窗體類別，負責串口通訊與 UI 交互
  /// </summary>
    public partial class Form1 : Form
    {

        private SerialPort _serialPort;
        private StreamWriter _logWriter;
        private bool isRecording = false;
        // 變數用來記錄使用者輸入的內容
      

        /// <summary>
        /// 構造函數，初始化組件與串口
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            InitializeSerialPort();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.DataReceived -= SerialPort_DataReceived;
                _serialPort.Close();
            }
            _logWriter?.Close(); // 確保紀錄檔案關閉
        }
        /*
        private void Form1_Load_1(object sender, EventArgs e)
        {

        }*/

        /// <summary>
        /// 初始化串口與 UI 元件
        /// </summary>
        private void InitializeSerialPort() {
            this.Text = "串口通訊工具"; // 設定視窗標題

            _serialPort = new SerialPort();
            // 統一使用字串填充波特率
            board_rat.Items.AddRange(new object[] { 9600, 19200, 38400, 57600, 115200, 256000 });
            comport.Items.AddRange(SerialPort.GetPortNames());
            board_rat.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            Data_Bit.Items.AddRange(new object[] { "5", "6", "7", "8" });
            Stop_Bits.Items.AddRange(new object[] { "1", "1.5", "2" });
            //Stop_Bits.Items.AddRange(new object[] { "One", "One5", "Two" });
            Parity.Items.AddRange(new object[] { "None", "Odd", "Even", "Mark", "Space" });
            Flow_Control.Items.AddRange(new object[] { "None", "XOnXOff", "RTS/CTS", "DTR/DSR" });
            ReadTimeout.Items.AddRange(new object[] { "100", "500", "1000", "2000", "5000" });
            WriteTimeout.Items.AddRange(new object[] { "100", "500", "1000", "2000", "5000" });

            if (comport.Items.Count > 0) comport.SelectedIndex = 0;
            board_rat.SelectedIndex = 0;
            Data_Bit.SelectedIndex = 3;
            Stop_Bits.SelectedIndex = 0;
            Parity.SelectedIndex = 0;
            Flow_Control.SelectedIndex = 1;
            //board_rat.SelectedIndex = 0;
            ReadTimeout.SelectedIndex = 0;
            WriteTimeout.SelectedIndex = 0;
            //textBoxOutput.KeyDown += TextBoxOutput_KeyDown;
            TextBoxIn.KeyDown += TextBoxIn_KeyDown;

        }

        private void TextBoxOutput_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;  // 防止任何鍵盤輸入影響 TextBoxOutput
        }

        private void TextBoxIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;  // 防止 TextBox 自動換行

                if (_serialPort.IsOpen)
                {
                    string message = TextBoxIn.Text.Trim();
                    if (!string.IsNullOrEmpty(message))  // 避免發送空訊息
                    {
                        //_serialPort.WriteLine(message);
                        //textBoxOutput.AppendText(">> " + message + Environment.NewLine);  // 顯示發送的內容

                        _serialPort.Write(message + "\r\n"); // **手動添加 \r\n**
                        TextBoxIn.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("請先連接串口！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            /** 在ComboBox中填充所有串口名稱*/
            comport.Items.AddRange(SerialPort.GetPortNames());

        }


        /// <summary>
        /// 連接/斷開串口
        /// </summary>
        /// 

        private void BuConnect_Click(object sender, EventArgs e)
        {
            if (comport.SelectedItem == null)
            {
                MessageBox.Show("請選擇一個可用的 COM 埠！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (board_rat.SelectedItem == null)
            {
                MessageBox.Show("請選擇鮑率！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_serialPort == null)
            {
                _serialPort = new SerialPort();
            }

            if (!_serialPort.IsOpen)
            {
                try
                {
                    /**設定通訊埠*/
                    _serialPort.PortName = comport.SelectedItem.ToString();
                    /**設定baud rate;*/
                    _serialPort.BaudRate = int.Parse(board_rat.SelectedItem.ToString());
                    /**設定資料位元*/
                    _serialPort.DataBits = int.Parse(Data_Bit.SelectedItem.ToString());
                    /**設定停止位元*/
                    // 處理停止位元轉換

                    string stopBitsText = Stop_Bits.SelectedItem.ToString();
                    string stopBitsMapped;
                    switch (stopBitsText)
                    {
                        case "1":
                            stopBitsMapped = "One";
                            break;
                        case "1.5":
                            stopBitsMapped = "One5";
                            break;
                        case "2":
                            stopBitsMapped = "Two";
                            break;
                        default:
                            throw new ArgumentException("無效的停止位設定");
                    }
                    _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), stopBitsMapped);

                    
                    /**設定流量控制*/

                    // 處理流量控制

                    string handshakeText = Flow_Control.SelectedItem.ToString();
                    Handshake handshake = Handshake.None;
                    switch (handshakeText)
                    {
                        case "XOnXOff":
                            handshake = Handshake.XOnXOff;
                            break;
                        case "RTS/CTS":
                            handshake = Handshake.RequestToSend;
                            break;
                        default:
                            MessageBox.Show("不支援此流量控制模式");
                            return;
                    }
                    _serialPort.Handshake = handshake;

                    //不考慮ＲＴＳ／ＣＴＳ的狀況
                    //_serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), Flow_Control.SelectedItem.ToString());


                    /**設定同位位元*/
                    _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), Parity.SelectedItem.ToString());

                    // 設定讀取與寫入的超時時間
                    _serialPort.ReadTimeout = int.Parse(ReadTimeout.SelectedItem.ToString());
                    _serialPort.WriteTimeout = int.Parse(WriteTimeout.SelectedItem.ToString());

                    // 避免事件重複綁定
                    _serialPort.DataReceived -= SerialPort_DataReceived;
                    _serialPort.DataReceived += SerialPort_DataReceived;

                    _serialPort.Open();
                    BuConnect.Text = "DisConnect";
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("無法開啟端口：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                _serialPort.DataReceived -= SerialPort_DataReceived;
                _serialPort.Close();
                _serialPort.Dispose();
                _serialPort = new SerialPort(); // 重新初始化
                BuConnect.Text = "Connect";

            }
        }


        /// <summary>
        /// 當串口接收到數據時的事件處理
        /// </summary>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
           try
           {
            // 接收到數據後顯示
            string data = _serialPort.ReadExisting();

             // 使用 BeginInvoke 避免 UI 卡住
            BeginInvoke(new Action(() => HandleReceivedData(data)));
            }
                catch (Exception ex)
            {
                MessageBox.Show("讀取串口數據時發生錯誤：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        /// <summary>
        /// 處理接收到的數據，顯示並記錄（若錄製中）
        /// </summary>
        /// <param name="data">接收到的數據</param>

        private void HandleReceivedData(string data)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => HandleReceivedData(data)));
                return;
            }

            textBoxOutput.AppendText(data); // 顯示接收到的訊息
            //textBoxOutput.AppendText(data + Environment.NewLine); // **確保換行**


            // 如果錄製中，則寫入日誌
            if (isRecording && _logWriter != null)
            {
                _logWriter.Write(data);
            }
        }

        private void BuRecord_Click(object sender, EventArgs e)
        {
            if (!_serialPort.IsOpen)
            {
                MessageBox.Show("請先連接串口再開始錄製！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!isRecording)
            {
                try
                {
                    string logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logfile");
                    Directory.CreateDirectory(logFolder);
                    string logFileName = Path.Combine(logFolder,
                        $"log_{DateTime.Now.ToString("MM_dd_HH_mm_ss", CultureInfo.InvariantCulture)}.txt");

                    // 处理文件访问冲突
                    try
                    {
                        _logWriter = new StreamWriter(logFileName, true);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("日志文件被其他程序占用，请关闭后重试。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _logWriter.AutoFlush = true;

                    // 安全更新 UI
                    BeginInvoke(new Action(() =>
                    {
                        isRecording = true;
                        BuRecord.Text = "Stop";
                    }));
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"文件寫入失敗: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show($"無權限訪問日誌目錄: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"無法開始錄製：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                _logWriter?.Dispose(); // 确保资源释放
                _logWriter = null;

                // 安全更新 UI
                BeginInvoke(new Action(() =>
                {
                    isRecording = false;
                    BuRecord.Text = "Record";
                }));
            }
        }

        /// <summary>
        /// 清除 TextBox 內容，並彈出確認對話框
        /// </summary>
        private void BuClear_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "確定要清除畫面嗎？",
                "確認清除",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                textBoxOutput.Clear();
            }
        }

        private void TextBoxIn_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
