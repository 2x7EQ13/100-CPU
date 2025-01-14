
namespace _100_CPU
{
    internal class LogRecord
    {
        public LogRecord(string processName, string pID, string operation, string path, string result, string detail, int count = 0)
        {
            ProcessName = processName;
            PID = pID;
            Operation = operation;
            Path = path;
            Result = result;
            Detail = detail;
            Count = count;
        }
        public string ProcessName { get; set; }
        public string PID { get; set; }
        public string Operation { get; set; }
        public string Path { get; set; }
        public string Result { get; set; }
        public string Detail { get; set; }
        public int Count { get; set; }
    }
}
