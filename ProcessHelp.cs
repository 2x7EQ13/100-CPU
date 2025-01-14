using System;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using Microsoft.Win32;

namespace _100_CPU
{
    internal class ProcessHelp
    {
        public bool RunProcMon(string runTime)
        {
            string procmonPath = @".\Procmon64.exe";
            // Create the command line arguments
            string arguments = $@"/AcceptEula /Quiet /Minimized /BackingFile .\LogFile.PML /LoadConfig .\ProcmonConfiguration.pmc /Runtime {runTime}";
            Process procmonProcess = new Process();
            procmonProcess.StartInfo.FileName = procmonPath;
            procmonProcess.StartInfo.Arguments = arguments;
            procmonProcess.StartInfo.UseShellExecute = false;
            procmonProcess.StartInfo.CreateNoWindow = true;
            try
            {
                //must delete all logfile to suppress overwrite popup
                if(System.IO.File.Exists(@".\LogFile.PML"))
                {
                    System.IO.File.Delete(@".\LogFile.PML");
                }
                ClearProcMonKey();
                procmonProcess.Start();
                Thread.Sleep(Int32.Parse(runTime) * 1000);//runTime in second
                if (!procmonProcess.HasExited)
                {
                    Thread.Sleep(1000); //wait more 1s and force to exit
                    //procmonProcess.Kill();
                }
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("has exited"))
                {
                    return true;
                }
                Form1.Instance.Log($"RunProcMon: An error occurred: {ex.Message}", Color.Red);
                return false;
            }
            return true;
        }
        private void ClearProcMonKey()
        {
            string registryKeyPath = @"SOFTWARE\Sysinternals\Process Monitor";
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryKeyPath, true))
                {
                    if (key != null)
                    {
                        Registry.CurrentUser.DeleteSubKeyTree(registryKeyPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Form1.Instance.Log($"ClearProcMonKey: An error occurred: {ex.Message}", Color.Red);
            }
        }
        public bool ConvertToCSV()
        {
            //wait capture process complete exit
            string processName = "Procmon64"; // Process name without the .exe extension
            int tmp = 0;
            try
            {
                while (true)
                {
                    Process[] processes = Process.GetProcessesByName(processName);
                    if (processes.Length == 0)
                    {
                        Console.WriteLine("Procmon64.exe has exited.");
                        break;
                    }
                    Thread.Sleep(1000);
                    if(tmp == 30) //30s timeout
                    {
                        foreach (Process process in processes)
                        {
                            process.Kill(); // Terminate the process
                            process.WaitForExit(); // Optional: wait for the process to exit
                        }
                    }
                    tmp++;
                }
            }
            catch (Exception ex)
            {
                Form1.Instance.Log($"ConvertToCSV: An error occurred: {ex.Message}", Color.Red);
                return false;
            }

            string procmonPath = @".\Procmon64.exe";
            // Create the command line arguments
            string arguments = $@"/AcceptEula /Quiet /Minimized /OpenLog .\Logfile.PML /SaveAs .\Logfile.CSV /SaveApplyFilter";
            Process procmonProcess = new Process();
            procmonProcess.StartInfo.FileName = procmonPath;
            procmonProcess.StartInfo.Arguments = arguments;
            procmonProcess.StartInfo.UseShellExecute = false;
            procmonProcess.StartInfo.CreateNoWindow = true;
            try
            {
                procmonProcess.Start();
                while (!procmonProcess.HasExited)
                {
                    Thread.Sleep(1000);
                }
                ClearProcMonKey();
            }
            catch (Exception ex)
            {
                Form1.Instance.Log($"RunProcMon: An error occurred: {ex.Message}", Color.Red);
                return false;
            }
            return true;
        }
    }
}
