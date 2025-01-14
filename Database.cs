using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Drawing;
using CsvHelper;
using System.Globalization;

namespace _100_CPU
{
    internal class Database
    {
        public SQLiteConnection connection;
        public bool InitDatabase()
        {
            try
            {
                string databaseFile = "TroubleShooting.db";
                // Check if the database file already exists
                if (File.Exists(databaseFile))
                {
                    File.Delete(databaseFile);
                }
                SQLiteConnection.CreateFile(databaseFile);
                connection = new SQLiteConnection($"Data Source={databaseFile};Version=3;");
                connection.Open();
                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS LogData (
                    ProcessName TEXT,
                    PID TEXT,
                    Operation TEXT,
                    Path TEXT,
                    Result TEXT,
                    Detail TEXT
                );";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                    Form1.Instance.Log("InitDatabase: Init complete", Color.Green);
                }
            }
            catch (Exception ex)
            {
                Form1.Instance.Log("InitDatabase: " + ex.Message, Color.Red);
                return false;
            }
            return true;
        }
        public bool ImportCsvToDatabase(string csvFilePath)
        {
            try
            {
                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "INSERT INTO LogData (ProcessName, PID, Operation, Path, Result, Detail) VALUES (@ProcessName, @PID, @Operation, @Path, @Result, @Detail)";
                        command.Parameters.Add(new SQLiteParameter("@ProcessName"));
                        command.Parameters.Add(new SQLiteParameter("@PID"));  // Keep as TEXT for better performance
                        command.Parameters.Add(new SQLiteParameter("@Operation"));
                        command.Parameters.Add(new SQLiteParameter("@Path"));
                        command.Parameters.Add(new SQLiteParameter("@Result"));
                        command.Parameters.Add(new SQLiteParameter("@Detail"));
                        using (var reader = new StreamReader(csvFilePath))
                        {
                            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                            {
                                csv.Read();
                                csv.ReadHeader();
                                while (csv.Read())
                                {
                                    command.Parameters["@ProcessName"].Value = csv.GetField<string>("Process Name");
                                    command.Parameters["@PID"].Value = csv.GetField<string>("PID");  // Read as TEXT
                                    command.Parameters["@Operation"].Value = csv.GetField<string>("Operation");
                                    command.Parameters["@Path"].Value = csv.GetField<string>("Path");
                                    command.Parameters["@Result"].Value = csv.GetField<string>("Result");
                                    command.Parameters["@Detail"].Value = csv.GetField<string>("Detail");                                    
                                    command.ExecuteNonQuery();
                                }
                            }                         
                        }
                    }
                    transaction.Commit();
                    Form1.Instance.Log("ImportCsvToDatabase: Import CSV successfully", Color.Green);
                }
                //
            }
            catch (Exception ex)
            {
                Form1.Instance.Log("ImportCsvToDatabase: " + ex.Message, Color.Red);
                return false;
            }
            return true;
        }

        public LogRecord SelectTopPathWithProcName(string procName)
        {
            string selectTopPathQuery = $@"
                SELECT ProcessName, PID, Operation, Path, Result, Detail, COUNT(*) AS Count
                FROM LogData
                WHERE ProcessName LIKE '%{procName}%'
                GROUP BY Path
                ORDER BY Count DESC
                LIMIT 1;";
            try
            {
                using (var command = new SQLiteCommand(selectTopPathQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Check if there is at least one result
                        {
                            string processName = reader["ProcessName"].ToString();
                            string pid = reader["PID"].ToString();
                            string operation = reader["Operation"].ToString();
                            string path = reader["Path"].ToString();
                            string result = reader["Result"].ToString();
                            string detail = reader["Detail"].ToString();
                            int count = Convert.ToInt32(reader["Count"]);
                            LogRecord logRecord = new LogRecord(processName, pid, operation, path, result, detail, count);
                            return logRecord;
                        }
                        else
                        {
                            Form1.Instance.Log("SelectTopPath: No data found in LogData table.", Color.Brown);
                        }
                    }
                }
                //
            }
            catch (Exception ex)
            {
                Form1.Instance.Log("SelectTopPath: " + ex.Message, Color.Red);
                return null;
            }
            return null;
        }
        public LogRecord SelectTopPath()
        {
            string selectTopPathQuery = @"
                SELECT ProcessName, PID, Operation, Path, Result, Detail, COUNT(*) AS Count
                FROM LogData
                GROUP BY Path
                ORDER BY Count DESC
                LIMIT 1;";
            try
            {
                using (var command = new SQLiteCommand(selectTopPathQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Check if there is at least one result
                        {
                            string processName = reader["ProcessName"].ToString();
                            string pid = reader["PID"].ToString();
                            string operation = reader["Operation"].ToString();
                            string path = reader["Path"].ToString();
                            string result = reader["Result"].ToString();
                            string detail = reader["Detail"].ToString();
                            int count = Convert.ToInt32(reader["Count"]);
                            LogRecord logRecord = new LogRecord(processName, pid, operation, path, result ,detail, count);
                            return logRecord;
                        }
                        else
                        {
                            Form1.Instance.Log("SelectTopPath: No data found in LogData table.", Color.Brown);
                        }
                    }
                }
                //
            }
            catch (Exception ex)
            {
                Form1.Instance.Log("SelectTopPath: " + ex.Message, Color.Red);
                return null;
            }
            return null;
        }
        public List<string> ProcessEnum()
        {
            List<string> list = new List<string>();
            string selectQuery = @"
                SELECT ProcessName, PID
                FROM LogData
                GROUP BY PID;";
            try
            {
                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string processName = reader["ProcessName"].ToString();
                            string pid = reader["PID"].ToString();
                            list.Add(processName + " | " + pid);
                        }
                    }
                }
                //
            }
            catch (Exception ex)
            {
                Form1.Instance.Log("SelectTopPath: " + ex.Message, Color.Red);
            }
            return list;
        }
    }
}
