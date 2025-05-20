/**
 * @file Form.c
 * @brief 串口通訊工具
 * @author Chergpt and Ethan 
 * @date Feb 16, 2025
 *
 * Layout如下
 *  ┌──────────────────────────────────┐
 *  │ [通訊埠] [鮑率]      {連接} {清除}{紀錄}{RTC}    [ ] HEX 模式      │   
 *  ├────────────┬────────────┬────────┤
 *  │ ASCII 顯示             │ HEX 顯示               │ [Data bit]     │
 *  │                        │                        │ [stop bit]     │
 *  │                        │                        │ [Parity]       │
 *  │                        │                        │ [Flow Control] │
 *  │                        │                        │ [Read Timeout] │
 *  │                        │                        │ [Write Timeout]│
 *  └────────────┴────────────┴────────┘
 *  │                        輸出訊息                                    │
 *  └──────────────────────────────────┘
 *
 *
 *2025/5/1 更新紀錄
 *  -新增ＨＥＸ轉換功能.
 *   點擊右上方『HEX』核取方塊,TextBox會分成兩個;一個顯示ascii，一個顯示十六進位值
 *  -目前問題：
 * 　在按下『HEX』核取方塊之後，雖然畫面確實有切割，但是HEX顯示區塊會跑到右邊的設定區塊
 * 　在取消『HEX』模式之後,ascii區塊水平垂直的區塊會擴大為整個視窗
 * 　雖然如此,我又不收你錢;堪用就好
 *   蛤?看不懂中文？
 *   你以為我在看你們那些英文法文德文日文克林貢文的時候就很輕鬆嗎？
 *   告訴我！我看不懂的時候，我可以嗆作者：你為什麼不用中文？
 *   
 *  
 *  
 *  
 * 串列傳輸桌面應用程式
 * 可設定連接埠、鮑率、傳送位元、流量控制、同位位元
 * 可清除、紀錄訊息框的紀錄
 * 支援Unicode
 * 
 * 其實這就是個爛大街的東西，github上一堆
 * 我會做這個的目的是想要做一個測試治具。
 * 這個程式不是很完美，應該還會有其他的問題；但這個就交給時間來決定吧。
 *
 *版權：
 *GNU GENERAL PUBLIC LICENSE
 * Version 3, 29 June 2007
 * Copyright (C) [2025] [Ethan]
 * 本程式是一個自由軟體：您可以依照 **GNU 通用公共授權條款（GPL）** 發佈和/或修改，
 * GPL 版本 3 或（依您選擇）任何更新版本。
 * 
 * 本程式的發佈目的是希望它對您有幫助，但 **不提供任何擔保**，甚至不包含適銷性或特定用途適用性的默示擔保。
 * 請參閱 **GNU 通用公共授權條款** 以獲取更多詳細資訊。
 * 您應當已經收到一份 **GNU 通用公共授權條款** 副本。
 * 如果沒有，請參閱 <https://www.gnu.org/licenses/gpl-3.0.html>。  
 */
#define Checkbox_0428
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Text.RegularExpressions;
using System.Security.Policy;

namespace ComPort_Charp
{ /// <summary>
  /// 主窗體類別，負責串口通訊與 UI 交互
  /// </summary>
  /// 
    public partial class Form1 : Form
    {
        /// <summary>
        /// 串口通訊的實體，用於收發資料。
        /// </summary>
        private SerialPort _serialPort;

        /// <summary>
        /// 寫入日誌檔的 StreamWriter 實例。
        /// </summary>
        private StreamWriter _logWriter;

        /// <summary>
        /// 表示是否正在記錄使用者輸入。
        /// </summary>
        private bool isRecording = false;

        /// <summary>
        /// 用於同步處理日誌寫入的鎖定物件。
        /// </summary>
        private readonly object _logLock = new object(); // 日誌寫入鎖

        /// <summary>
        /// 日誌檔案所儲存的資料夾路徑。
        /// </summary>
        private readonly string _debugFolderPath; // 日誌目錄路徑

        private readonly StringBuilder _hexBuffer = new StringBuilder();




        /// <summary>
        /// 構造函數，初始化組件與串口
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            InitializeSerialPort();
            //_logLock = new object(); // 新增此行

            //<0410 測試碼>
            //debug報告
            //當出錯的時候,可以把錯誤報告丟到執行檔案之下的debug資料夾中

            // CurrentDomain.BaseDirectory 執行檔所在目錄
            _debugFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"debug");

            // 自動建立日誌目錄（如果不存在）
            Directory.CreateDirectory(_debugFolderPath);
            //</0410 測試碼>
            //SplitView(); // <--- 新增這一行

            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBoxHex_CheckedChanged);


        }


        //<0410 新增;寫入除錯資訊>

        private void WriteDebugLog(string fileName, string message)
        {
            try
            {
                
                string logPath = Path.Combine(_debugFolderPath, fileName);

                // 使用 File.AppendAllText 自動處理檔案開啟/關閉
                File.AppendAllText(logPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {message}\n");
            }
            catch (Exception ex)
            {
                // 日誌寫入失敗時可在此處理（例如顯示警告）
                MessageBox.Show($"日誌寫入失敗: {ex.Message}");
            }
        }
        //</0410 新增;寫入除錯資訊>

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.DataReceived -= SerialPort_DataReceived;
                _serialPort.Close();
            }
            _logWriter?.Close(); // 確保紀錄檔案關閉
        }
 

        /// <summary>
        /// 初始化串口與 UI 元件
        /// </summary>
        /*!
         *  Initializes the serial port.
         */
        private void InitializeSerialPort() {
            this.Text = "串口通訊工具"; // 設定視窗標題

            _serialPort = new SerialPort
            {
                Encoding = Encoding.UTF8
            };

            // 統一使用字串填充波特率
            board_rat.Items.AddRange(new object[] { 9600, 19200, 38400, 57600, 115200, 256000 });
            comport.Items.AddRange(SerialPort.GetPortNames());
            //board_rat.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            Data_Bit.Items.AddRange(new object[] { "5", "6", "7", "8" });
            Stop_Bits.Items.AddRange(new object[] { "1", "1.5", "2" });
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
            UpdateLayoutByMode();
            
        }


       


        private void UpdateLayoutByMode()
        {
            
            if (panelDisplayArea == null) return; // 防禦性程式碼

            if (checkBox1.Checked)
            {
                // 切換到 HEX 模式，畫面分一半
                int halfWidth = this.panelDisplayArea.Width / 2;

                textBoxOutput.SetBounds(0, 0, halfWidth, panelDisplayArea.ClientSize.Height);
                textBoxHex.SetBounds(halfWidth, 0, halfWidth, panelDisplayArea.ClientSize.Height);

                //textBoxOutput.SetBounds(0, textBoxOutput.Top, halfWidth, textBoxOutput.Height);
                //textBoxHex.SetBounds(halfWidth, textBoxHex.Top, halfWidth, textBoxHex.Height);

                textBoxHex.Visible = true;
            }
            else
            {

                // textBoxOutput 填滿 panelDisplayArea
                textBoxOutput.SetBounds(0, 0, panelDisplayArea.ClientSize.Width, panelDisplayArea.ClientSize.Height);
                
                // 切換到 ASCII 模式，Output 佔滿寬度
                //textBoxOutput.SetBounds(0, textBoxOutput.Top, this.ClientSize.Width, textBoxOutput.Height);
                textBoxHex.Visible = false;
            }
        }


        /// <summary>
        /// 
        /**  @brief	 BuConnect_Click 連接/斷開串口
         *  </summary>
        */

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
                    //string stopBitsMapped;

                    switch (stopBitsText)
                    {
                        case "1":
                            _serialPort.StopBits = StopBits.One;
                        break;
                        case "1.5":
                            _serialPort.StopBits = StopBits.OnePointFive;  // 正確枚舉值
                            break;
                        case "2":
                            _serialPort.StopBits = StopBits.Two;
                            break;

                    }
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
                        case "None": // 新增處理「None」的情況
                            handshake = Handshake.None;
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
        /// 

        //</0411 修改>


        private readonly object _bufferLock = new object();  // 緩衝區操作鎖
        private readonly StringBuilder _receiveBuffer = new StringBuilder();

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //參數設定
                int bytesToRead = _serialPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                int bytesActuallyRead = _serialPort.Read(buffer, 0, bytesToRead); // 獲取實際讀取的字節數


                if (bytesToRead == 0) {
                    // 沒有數據可讀
                    return; 
                }

                if (bytesActuallyRead == 0) {
                    return; // 再次確認是否真的讀到數據
                }

                // 1. 將原始數據轉換為 UTF8 (或您期望的串口編碼) 字串，用於後續處理和可能的日誌記錄
                //    這裡使用 UTF8 是因為您的 _serialPort.Encoding 預設為 UTF8
                string rawDataString = Encoding.UTF8.GetString(buffer, 0, bytesActuallyRead);

                // 2. 處理換行符，得到用於顯示和記錄的 ASCII (處理後的) 字符串
                string processedAsciiForDisplayAndLog = ProcessLineBreaks(rawDataString);

                // 3. 處理 HEX 顯示 (僅用於 UI)
                string processedHexForDisplay = BitConverter.ToString(buffer, 0, bytesActuallyRead).Replace("-", " ") + Environment.NewLine;

                lock (_bufferLock)
                {
                    //_receiveBuffer.Append(processedAscii);
                    //_hexBuffer.Append(processedHex);

                    _receiveBuffer.Append(processedAsciiForDisplayAndLog);
                    _hexBuffer.Append(processedHexForDisplay);
                }

#if modify_0516
                // 1. 從串列埠讀取原始數據;把原始資料放到buffer中
                _serialPort.Read(buffer, 0, bytesToRead);
                //string processedData;


                /**
                 * 2. 解碼並處理換行符
                 * 
                 * 先把Buffer中的資料轉換之後，放到rawData
                 * 然後把rawData中的資料轉換成ASCII的編碼
                 */
                string rawData = Encoding.UTF8.GetString(buffer);
                string processedAscii = ProcessLineBreaks(rawData);

                /**
                 * 將buffer中的資料轉換成字串
                 */
                // HEX 顯示格式
                string processedHex = BitConverter.ToString(buffer).Replace("-", " ") + Environment.NewLine;
                lock (_bufferLock)
                {
                    _receiveBuffer.Append(processedAscii);
                    _hexBuffer.Append(processedHex);
                }
#endif

                // 5. 調用 HandleReceivedData 將 ASCII 數據寫入日誌 (如果正在錄製)
                //    這個方法內部會處理 InvokeRequired 和 _logLock
                HandleReceivedData(processedAsciiForDisplayAndLog);

                // 6. 觸發 UI 更新
                BeginInvoke(new Action(ProcessReceivedData));
            }
            // 4. 觸發單一 UI 更新
            catch (Exception ex)
            {
                // 5. 統一錯誤處理
                WriteDebugLog("error.log", $"ERROR in SerialPort_DataReceived: {ex.Message} \nStackTrace: {ex.StackTrace}");

            }
        }

        private void HandleReceivedData(string asciiDataToLog)
        {
            if (string.IsNullOrEmpty(asciiDataToLog)) // 如果數據為空，則不記錄
            {
                return;
            }

            // 確保在正確的線程操作 _logWriter (雖然 StreamWriter 本身是線程安全的，但 isRecording 的讀取和 _logWriter 的賦值可能需要同步)
            // 但考慮到 isRecording 和 _logWriter 主要在 UI 線程修改，這裡的檢查和寫入可以認為在 DataReceived 線程是相對安全的，
            // 除非 BuRecord_Click 被非常頻繁地調用。 _logLock 主要保護 _logWriter 的寫操作。
            if (isRecording && _logWriter != null)
            {
                lock (_logLock)
                {
                    // 再次檢查，以防在進入 lock 前狀態改變
                    if (isRecording && _logWriter != null)
                    {
                        try
                        {
                            _logWriter.Write(asciiDataToLog);
                            // _logWriter.Flush(); // AutoFlush = true 時，這行可以省略，但保留也無妨
                        }
                        catch (Exception ex)
                        {
                            // 處理日誌寫入錯誤，可以寫入 debug 日誌
                            WriteDebugLog("log_write_error.log", $"Error writing to log via HandleReceivedData: {ex.Message}");
                        }
                    }
                }
            }
        }







        private void CheckBoxHex_CheckedChanged(object sender, EventArgs e)
        {
            // 清除現有顯示內容
            //textBoxOutput.Clear();
            if (checkBox1.Checked)
            {
                // 顯示 HEX 模式（分欄）
                textBoxHex.Visible = true;
                textBoxOutput.Width = this.ClientSize.Width / 2;
                textBoxHex.Width = this.ClientSize.Width / 2;
                textBoxHex.Left = textBoxOutput.Right;
            }
            else
            {
                // 關閉 HEX 模式，只顯示 ASCII
                textBoxHex.Visible = false;
                //textBoxOutput.Width = this.ClientSize.Width - textBoxOutput.Left;
                textBoxOutput.Width = this.ClientSize.Width - textBoxOutput.Left;
            }
            //ProcessReceivedData();
        }

        /**
         * 同時更新textBox
         * 因為兩個textBox，所以需要兩個緩衝區_receiveBuffer和_hexBuffer；
         * 所以，同樣的事情必須要做兩次
        */
        private void ProcessReceivedData()
        {
            lock (_bufferLock)
            {
                if (_receiveBuffer.Length > 0){
                    textBoxOutput.AppendText(_receiveBuffer.ToString());
                    _receiveBuffer.Clear();
                }

                if (_hexBuffer.Length > 0)
                {
                    textBoxHex.AppendText(_hexBuffer.ToString());
                    _hexBuffer.Clear();
                }


                if (checkBox1.Checked && _hexBuffer.Length > 0)
                {
                    textBoxHex.AppendText(_hexBuffer.ToString());
                    _hexBuffer.Clear();
                }
                else if (!checkBox1.Checked && _hexBuffer.Length > 0)
                {
                    // 如果不是 HEX 模式，但 HEX buffer 裡有東西，清空它
                    _hexBuffer.Clear();
                }
            }
        }



        // 7. 換行符統一處理方法
        private string ProcessLineBreaks(string input)
        {
            return input
                .Replace("\r\n", "\n")  // 統一為 LF
                .Replace("\r", "\n")    // 處理舊版 Mac 換行符
                .Replace("\n", "\r\n"); // 轉為 Windows 標準換行符
        }
        //</0411 修改>

#if modify_0516
        /// <summary>
        /// 處理接收到的數據，顯示並記錄（若錄製中）
        /// </summary>
        /// <param name="data">接收到的數據</param>

        private void HandleReceivedData(string asciiDataToLog)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => HandleReceivedData(data)));
                return;
            }

            textBoxOutput.AppendText(data); // 顯示接收到的訊息


            lock (_logLock) // 需在類別中定義 private readonly object _logLock = new object();
            {
                if (isRecording && _logWriter != null)
                {
                    _logWriter.Write(data);
                    _logWriter.Flush(); // 強制寫入檔案
                }
            }
        }


#endif
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
                                                $"log_{DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture)}.txt"); // 建議的檔名格式，按字母排序即按時間排序

                    // $"log_{DateTime.Now.ToString("MM_dd_HH_mm_ss", CultureInfo.InvariantCulture)}.txt");

                    // 处理文件访问冲突
                    try
                    {
                        _logWriter = new StreamWriter(logFileName, true, Encoding.UTF8)
                        {
                            AutoFlush = true // 在成功創建後設置 AutoFlush
                        };
                        /**等同於
                         * 
                         * _logWriter = new StreamWriter(logFileName, true, Encoding.UTF8);
                         * _logWriter.AutoFlush = true; // 在成功創建後設置 AutoFlush
                         */
                    }
                    catch (IOException ioEx)
                    {

                        MessageBox.Show($"日誌文件 '{Path.GetFileName(logFileName)}' 被其他程序占用或無法訪問，请关闭后重试。\n錯誤: {ioEx.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // 不需要再賦值給 _logWriter，因為它未能成功創建
                        return; // 結束錄製流程
                    }


                    isRecording = true;
                    BuRecord.Text = "Stop";

                    // 創建 StreamWriter
                    //logWriter = new StreamWriter(logFileName, true, Encoding.UTF8); // true for append, 明確指定編碼
                    //_logWriter.AutoFlush = true;

                    //isRecording = true; // 在成功打開 StreamWriter 後再設置 isRecording
                    // BuRecord.Text = "Stop"; // 更新按鈕文字 (建議在UI線程更新，但此處通常問題不大)
                                            // BeginInvoke(new Action(() => BuRecord.Text = "Stop")); // 更安全的UI更新方式

                    //_logWriter.AutoFlush = true;

                    // 安全更新 UI
                    /*
                    BeginInvoke(new Action(() =>
                    {
                        isRecording = true;
                        BuRecord.Text = "Stop";
                    }));*/

                }
                catch (IOException ex)
                {
                    MessageBox.Show($"文件寫入失敗: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // 清理可能已部分創建的 _logWriter
                    _logWriter?.Dispose();
                    _logWriter = null;
                    isRecording = false; // 重置狀態
                }


                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show($"無權限訪問日誌目錄: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //isRecording = false; // 重置狀態
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"無法開始錄製：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _logWriter?.Dispose();
                    _logWriter = null;
                    //isRecording = false;
                }
            }
            else // 停止錄製
            {
                isRecording = false; // 先改變狀態，避免在關閉過程中仍有數據嘗試寫入
                //BuRecord.Text = "Record"; // 更新按鈕文字 (建議在UI線程更新)
                BeginInvoke(new Action(() => BuRecord.Text = "Record"));



                //使用 lock 確保在 Dispose 時沒有其他線程在 Write
                lock (_logLock)
                {
                    _logWriter?.Flush(); // 確保所有緩衝數據已寫入
                    _logWriter?.Dispose(); // 确保资源释放
                    _logWriter = null;
                }

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
            //File.AppendAllText("debug_text_changed.log", $"[{DateTime.Now}] Text 變更\n");
        }

        private void BtnSyncTime_Click(object sender, EventArgs e)
        {
            if (!_serialPort.IsOpen)
            {
                MessageBox.Show("请先连接串口！", "警告",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DateTime now = DateTime.Now;
                byte[] packet = CreateTimeSyncPacket(now);

                _serialPort.Write(packet, 0, packet.Length);
                textBoxOutput.AppendText($"已发送时间同步数据：{now:yyyy-MM-dd(dddd) HH:mm:ss}\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"时间同步失败：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /**
         * @brief 設定時間同步的封包格式
         * 格式:, ,checksum,\r\n
         * checksum 由 CalculateChecksum(payload);產生
         */
        private byte[] CreateTimeSyncPacket(DateTime time)
        {

            // 协议格式：SYNC_TIME
            string payload = $"{time.Year}," +
                 $"{time.Month:D2}," +
                 $"{time.Day:D2}," +
                 $"{time.Hour:D2}," +
                 $"{time.Minute:D2}," +
                 $"{time.Second:D2}," +
                 $"{(int)time.DayOfWeek}";  // 加入星期（日=0, 一=1, ... 六=6）;
          

            // 计算校验和（简单求和校验）
            byte checksum = CalculateChecksum(payload);

            // 组合完整数据包
            string fullPacket = $"{payload},{checksum}\r\n";

            return Encoding.ASCII.GetBytes(fullPacket);
        }

        private byte CalculateChecksum(string data)
        {
            byte sum = 0;
            foreach (char c in data)
            {
                sum += (byte)c;
            }
            return sum;
        }
    }


    ///舊碼區
    ///


}
