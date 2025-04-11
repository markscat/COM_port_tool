using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComPort_Charp
{
    internal static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //<0401 新增>
            // 設定全域異常處理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, e) =>
            {
                HandleFatalError(e.Exception);
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                HandleFatalError(e.ExceptionObject as Exception);
            };

            //</0401 新增>
            Application.Run(new Form1());
        }

        //<0401 新增>
        private static void HandleFatalError(Exception ex)
        {
            string errorLogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SerialPortLogs", "crash.log");
            string errorMessage = $"[{DateTime.Now}] 嚴重錯誤:\n{ex}\n\n";
            File.AppendAllText(errorLogPath, errorMessage);
            MessageBox.Show("程式發生嚴重錯誤，請檢查 crash.log", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(1);
        }
        //</0401 新增>
    }
}
