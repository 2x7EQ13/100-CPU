using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace _100_CPU
{
    public partial class Form1 : Form
    {
        //Sử dụng Singleton cho việc ghi log
        private static Form1 _instance;
        public static Form1 Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                {
                    _instance = new Form1();//nếu return null thì sẽ gây ra Exception
                }
                return _instance;
            }
        }
        Database database = new Database();
        public Form1()
        {
            _instance = this;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.richTextLog.ReadOnly = true;
            richTextLog.HideSelection = false;
            //LoadProcessNames();
            //bool runResult = database.InitDatabase();
            //database.ImportCsvToDatabase(@".\Logfile.CSV");
        }
        private void LoadProcessNames()
        {
            comboBoxProcName.Items.Clear();
            List<string> processNames = database.ProcessEnum(); //Process Name | PID
            if (processNames.Count == 0)
            {
                Log("Error when running enum processes", Color.Red);
                return;
            }
            comboBoxProcName.Items.Add("Auto Detect");
            comboBoxProcName.SelectedIndex = 0;
            foreach (string process in processNames)
            {
                comboBoxProcName.Items.Add(process);
            }
        }
        public void Log(string text, Color color)
        {
            text = text.Trim() + Environment.NewLine;
            richTextLog.SelectionColor = color;
            richTextLog.AppendText(text);
            richTextLog.SelectionColor = richTextLog.ForeColor;

        }

        private void toolStripMenuCopy_Click(object sender, EventArgs e)
        {
            richTextLog.Copy();
        }

        bool RunCaptureLog(ProcessHelp processHelp)
        {
            int runtime = 15;
            if (textBoxRuntime.Text.Trim().Length > 0)
            {
                try
                {
                    runtime = Int32.Parse(textBoxRuntime.Text.Trim());
                }
                catch (Exception) { }

            }
            bool runResult = false;
            runResult = processHelp.RunProcMon(runtime.ToString());
            if (!runResult)
            {
                Log("Error when running Process Monitor. Exiting", Color.Red);
                return false;
            }
            runResult = processHelp.ConvertToCSV();
            if (!runResult)
            {
                Log("Error when parsing log file. Exiting", Color.Red);
                return false;
            }
            //load database
            if(database.connection != null)
            {
                database.connection.Close();
            }
            runResult = database.InitDatabase();
            if (!runResult)
            {
                Log("Error when initial database. Exiting", Color.Red);
                return false;
            }
            runResult = database.ImportCsvToDatabase(@".\Logfile.CSV");
            if (!runResult)
            {
                Log("Error when importing CSV into database. Exiting", Color.Red);
                return false;
            }
            return true;
        }
        private void buttonAnalyze_Click(object sender, EventArgs e)
        {   
            if(comboBoxProcName.Items.Count == 0)
            {
                Log("No database. Run collecting log first.", Color.Red);
                return;
            }
            //process database
            string processName = comboBoxProcName.SelectedItem.ToString();
            processName = processName.Split('|')[0].Trim();
            if (processName.Equals("Auto Detect"))
            {
                LogRecord logRecord = database.SelectTopPath();
                if (logRecord != null)
                {
                    Log($"Top 1 Most Prevalent:", Color.Purple);
                    Log($"ProcessName:  {logRecord.ProcessName}", Color.Black);
                    Log($"PID:  {logRecord.PID}", Color.Black);
                    Log($"Operation:  {logRecord.Operation}", Color.Black);
                    Log($"Path:  {logRecord.Path}", Color.Black);
                    Log($"Result:  {logRecord.Result}", Color.Black);
                    Log($"Detail:  {logRecord.Detail}", Color.Black);
                    Log($"Number of occurrences:  {logRecord.Count}{Environment.NewLine}", Color.Black);
                }
            }else
            {
                LogRecord logRecord = database.SelectTopPathWithProcName(processName);
                if (logRecord != null)
                {
                    Log($"Top 1 Most Prevalent:", Color.Purple);
                    Log($"ProcessName:  {logRecord.ProcessName}", Color.Black);
                    Log($"PID:  {logRecord.PID}", Color.Black);
                    Log($"Operation:  {logRecord.Operation}", Color.Black);
                    Log($"Path:  {logRecord.Path}", Color.Black);
                    Log($"Result:  {logRecord.Result}", Color.Black);
                    Log($"Detail:  {logRecord.Detail}", Color.Black);
                    Log($"Number of occurrences:  {logRecord.Count}{Environment.NewLine}", Color.Black);
                }
            }
            //
        }

        private void buttonCollect_Click(object sender, EventArgs e)
        {
            ProcessHelp processHelp = new ProcessHelp();
            bool runResult = false;
            runResult = RunCaptureLog(processHelp);
            if (runResult)
            {
                LoadProcessNames();
            }
        }
    }
}
