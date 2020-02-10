using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Bsoft.Common
{
    public static class LogTraceToFile
    {
        // Fields
        private static int _logInfoCnt = 0;

        private static Queue _logQueue = Queue.Synchronized(new Queue());
        private static int _logSizeLimit = 5;
        public static string defTraceID = (Application.ProductName + "_log");
        private static TimerCallback dlgThreadProc = new TimerCallback(LogTraceToFile.OnTimerChange);
        private static bool m_bWrittingToLogFile = false;
        private static Encoding m_encoding = Encoding.UTF8;
        private static Hashtable m_htTraceFileInfo = Hashtable.Synchronized(new Hashtable());
        private static System.Threading.Timer m_tmrPool = new System.Threading.Timer(dlgThreadProc, null, 50, 50);
        public static bool ShowInConsole = false;
        public static eTRACELEVEL TraceLogLevel = eTRACELEVEL.DEBUG;

        // Methods
        public static void AddTraceFile()
        {
            AddTraceFile(defTraceID, "");
        }

        public static void AddTraceFile(string TraceID)
        {
            AddTraceFile(TraceID, "");
        }

        public static void AddTraceFile(string TraceID, string TraceLogPath)
        {
            lock (m_htTraceFileInfo.SyncRoot)
            {
                if (m_htTraceFileInfo.ContainsKey(TraceID))
                {
                    m_htTraceFileInfo[TraceID] = TraceLogPath;
                }
                else
                {
                    m_htTraceFileInfo.Add(TraceID, TraceLogPath);
                }
            }
        }

        private static bool deleteTraceLogFile(TraceLogInfo logInfo)
        {
            string traceFileName = GetTraceFileName(logInfo.TraceID);
            if ((traceFileName == null) || (traceFileName.Trim().Length == 0))
            {
                return false;
            }
            FileInfo info = new FileInfo(traceFileName);
            if ((info != null) && info.Exists)
            {
                try
                {
                    info.Delete();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        private static TraceLogInfo DequeueLogInfo()
        {
            TraceLogInfo info = null;
            if (_logInfoCnt > 0)
            {
                lock (_logQueue.SyncRoot)
                {
                    info = (TraceLogInfo)_logQueue.Dequeue();
                    Interlocked.Decrement(ref _logInfoCnt);
                }
            }
            return info;
        }

        private static void EnqueueLogInfo(TraceLogInfo logInfo)
        {
            lock (_logQueue.SyncRoot)
            {
                if (IsLogFileSizeLimitCrossed(logInfo))
                {
                    deleteTraceLogFile(logInfo);
                }
                _logQueue.Enqueue(logInfo);
                Interlocked.Increment(ref _logInfoCnt);
            }
        }

        private static string GetTraceFileName(string TraceID)
        {
            if (TraceID.Trim().Length == 0)
            {
                TraceID = defTraceID;
            }
            string path = null;
            try
            {
                if (m_htTraceFileInfo.ContainsKey(TraceID))
                {
                    path = (string)m_htTraceFileInfo[TraceID];
                    string str2 = Application.StartupPath + @"\LogTrace";
                    if (path.Trim() == string.Empty)
                    {
                        if (!Directory.Exists(str2))
                        {
                            Directory.CreateDirectory(str2);
                        }
                        str2 = str2 + @"\" + TraceID;
                        if (!Directory.Exists(str2))
                        {
                            Directory.CreateDirectory(str2);
                        }
                        return string.Format(@"{0}\{1}_{2:yyyyMMdd}.log", str2, TraceID, DateTime.Now);
                    }
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    return string.Format(@"{0}\{1}_{2:yyyyMMdd}.log", path, TraceID, DateTime.Now);
                }
                AddTraceFile(TraceID);
                GetTraceFileName(TraceID);
            }
            catch (Exception)
            {
            }
            return path;
        }

        private static bool IsLogFileSizeLimitCrossed(TraceLogInfo logInfo)
        {
            if (_logSizeLimit <= 0)
            {
                return false;
            }
            string traceFileName = GetTraceFileName(logInfo.TraceID);
            if ((traceFileName == null) || (traceFileName.Trim().Length == 0))
            {
                return false;
            }
            FileInfo info = new FileInfo(traceFileName);
            return (((info != null) && info.Exists) && (((info.Length / 0x400L) / 0x400L) >= _logSizeLimit));
        }

        private static void OnTimerChange(object state)
        {
            if (!m_bWrittingToLogFile)
            {
                m_bWrittingToLogFile = true;
                try
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(LogTraceToFile.ThreadProcWriteLog));
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }
            }
        }

        public static void RemoveTraceFile(string TraceID)
        {
            lock (m_htTraceFileInfo.SyncRoot)
            {
                if (m_htTraceFileInfo.ContainsKey(TraceID))
                {
                    m_htTraceFileInfo.Remove(TraceID);
                }
            }
        }

        public static void SetEncoding(Encoding pEncoding)
        {
            m_encoding = pEncoding;
        }

        private static void ThreadProcWriteLog()
        {
            try
            {
                do
                {
                    TraceLogInfo logInfo = DequeueLogInfo();
                    if (IsLogFileSizeLimitCrossed(logInfo))
                    {
                        logInfo = null;
                    }
                    if (logInfo != null)
                    {
                        if (logInfo.TraceID.Trim().Length == 0)
                        {
                            logInfo.TraceID = defTraceID;
                        }
                        string traceFileName = GetTraceFileName(logInfo.TraceID);
                        if (traceFileName != string.Empty)
                        {
                            StreamWriter writer = null;
                            try
                            {
                                if (!File.Exists(traceFileName))
                                {
                                    WriteAppDetail(logInfo);
                                }
                                writer = new StreamWriter(traceFileName, true, m_encoding);
                                string str2 = string.Format("{0:yyyy/MM/dd HH:mm:ss} {1} {2:000} {3}", new object[] { DateTime.Now, (int)logInfo.TraceLevel, Convert.ToInt32(logInfo.ThreadID), logInfo.Message });
                                if (ShowInConsole)
                                {
                                    Console.WriteLine("[{0}] :: {1}", logInfo.TraceID, str2);
                                }
                                writer.WriteLine(str2 + " Memory Used:" + ((double)((Process.GetCurrentProcess().WorkingSet64 / 0x400L) / 0x400L)).ToString("#0.00") + "MB");
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception.ToString());
                            }
                            finally
                            {
                                if (writer != null)
                                {
                                    writer.Flush();
                                    writer.Close();
                                }
                            }
                        }
                    }
                }
                while (_logInfoCnt > 0);
            }
            catch
            {
            }
            finally
            {
                m_bWrittingToLogFile = false;
            }
        }

        private static void ThreadProcWriteLog(object state)
        {
            ThreadProcWriteLog();
        }

        private static void WriteAppDetail(TraceLogInfo logInfo)
        {
            string traceFileName = GetTraceFileName(logInfo.TraceID);
            if (traceFileName != string.Empty)
            {
                StreamWriter writer = null;
                try
                {
                    if (!File.Exists(traceFileName) && (writer == null))
                    {
                        writer = new StreamWriter(traceFileName, true, m_encoding);
                    }
                    if (writer == null)
                    {
                        writer = new StreamWriter(traceFileName, true, m_encoding);
                    }
                    FileInfo info = new FileInfo(Application.ExecutablePath);
                    string str2 = string.Format("{0:yyyy/MM/dd HH:mm:ss} {1} {2:000} Application Name [{3}] Version No [{4}] Last Build Date Time [{5:yyyy/MM/dd HH:mm:ss}]", new object[] { DateTime.Now, (int)logInfo.TraceLevel, Convert.ToInt32(logInfo.ThreadID), info.Name, Application.ProductVersion, info.CreationTime });
                    if (ShowInConsole)
                    {
                        Console.WriteLine("[{0}] :: {1}", logInfo.TraceID, str2);
                    }
                    writer.WriteLine();
                    writer.WriteLine(str2);
                    writer.WriteLine();
                    writer.WriteLine();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Flush();
                        writer.Close();
                    }
                }
            }
        }

        public static void WriteLog(TraceLogInfo logInfo)
        {
            if ((logInfo.TraceLevel != eTRACELEVEL.DISABLED) && (logInfo.TraceLevel <= TraceLogLevel))
            {
                logInfo.ThreadID = Thread.CurrentThread.ManagedThreadId.ToString();
                EnqueueLogInfo(logInfo);
            }
        }

        // Properties
        public static int LogSizeLimit
        {
            get
            {
                return _logSizeLimit;
            }
            set
            {
                _logSizeLimit = value;
            }
        }
    }

    public enum eTRACELEVEL
    {
        DEBUG = 3,
        DISABLED = -1,
        ERROR = 0,
        INFORMATION = 2,
        WARNING = 1
    }

    public static class LogTrace
    {
        // Methods
        private static void DisplayErrorMessage(string errorMessage)
        {
            errorMessage = "Following Error Occurred:\r\n" + errorMessage;
            errorMessage = errorMessage + "\r\nThe error was successfully logged into the error log file.";
            MessageBox.Show(errorMessage);
            errorMessage = string.Empty;
        }

        public static void WriteDebugLog(string Msg)
        {
            WriteLog(string.Empty, string.Empty, Msg, eTRACELEVEL.DEBUG, string.Empty);
        }

        public static void WriteDebugLog(string className, string Msg)
        {
            WriteLog(className, string.Empty, Msg, eTRACELEVEL.DEBUG, string.Empty);
        }

        public static void WriteDebugLog(string className, string functionName, string Msg)
        {
            WriteLog(className, functionName, Msg, eTRACELEVEL.DEBUG, string.Empty);
        }

        public static void WriteErrorLog(string Msg)
        {
            WriteLog(string.Empty, string.Empty, Msg, eTRACELEVEL.ERROR, string.Empty);
        }

        public static void WriteErrorLog(string Msg, bool DisplayErrMsg)
        {
            WriteLog(string.Empty, string.Empty, Msg, eTRACELEVEL.ERROR, string.Empty);
            if (DisplayErrMsg)
            {
                DisplayErrorMessage(Msg);
            }
        }

        public static void WriteErrorLog(string className, string Msg)
        {
            WriteLog(className, string.Empty, Msg, eTRACELEVEL.ERROR, string.Empty);
        }

        public static void WriteErrorLog(string className, string Msg, bool DisplayErrMsg)
        {
            WriteLog(className, string.Empty, Msg, eTRACELEVEL.ERROR, string.Empty);
            if (DisplayErrMsg)
            {
                DisplayErrorMessage(Msg);
            }
        }

        public static void WriteErrorLog(string className, string functionName, string Msg)
        {
            WriteLog(className, functionName, Msg, eTRACELEVEL.ERROR, string.Empty);
        }

        public static void WriteErrorLog(string className, string functionName, string Msg, bool DisplayErrMsg)
        {
            WriteLog(className, functionName, Msg, eTRACELEVEL.ERROR, string.Empty);
            if (DisplayErrMsg)
            {
                DisplayErrorMessage(Msg);
            }
        }

        public static void WriteInfoLog(string Msg)
        {
            WriteLog(string.Empty, string.Empty, Msg, eTRACELEVEL.INFORMATION, string.Empty);
        }

        public static void WriteInfoLog(string className, string Msg)
        {
            WriteLog(className, string.Empty, Msg, eTRACELEVEL.INFORMATION, string.Empty);
        }

        public static void WriteInfoLog(string className, string functionName, string Msg)
        {
            WriteLog(className, functionName, Msg, eTRACELEVEL.INFORMATION, string.Empty);
        }

        public static void WriteLog(string Msg, eTRACELEVEL traceLevel)
        {
            WriteLog(string.Empty, string.Empty, Msg, traceLevel, string.Empty);
        }

        public static void WriteLog(string Msg, eTRACELEVEL traceLevel, string TraceID)
        {
            WriteLog(string.Empty, string.Empty, Msg, traceLevel, TraceID);
        }

        public static void WriteLog(string className, string Msg, eTRACELEVEL traceLevel)
        {
            WriteLog(className, string.Empty, Msg, traceLevel, string.Empty);
        }

        public static void WriteLog(string className, string Msg, eTRACELEVEL traceLevel, string TraceID)
        {
            WriteLog(className, string.Empty, Msg, traceLevel, TraceID);
        }

        public static void WriteLog(string className, string functionName, string Msg, eTRACELEVEL traceLevel, string TraceID)
        {
            StringBuilder builder = new StringBuilder();
            if (className.Trim().Length > 0)
            {
                builder.AppendFormat("[{0}]::", className);
            }
            if (functionName.Trim().Length > 0)
            {
                builder.AppendFormat("[{0}]::", functionName);
            }
            builder.Append(Msg);
            TraceID = TraceID.Trim();
            LogTraceToFile.WriteLog(new TraceLogInfo(builder.ToString(), traceLevel, (TraceID.Length == 0) ? string.Empty : TraceID));
        }

        public static void WriteWarningLog(string Msg)
        {
            WriteLog(string.Empty, string.Empty, Msg, eTRACELEVEL.WARNING, string.Empty);
        }

        public static void WriteWarningLog(string className, string Msg)
        {
            WriteLog(className, string.Empty, Msg, eTRACELEVEL.WARNING, string.Empty);
        }

        public static void WriteWarningLog(string className, string functionName, string Msg)
        {
            WriteLog(className, functionName, Msg, eTRACELEVEL.WARNING, string.Empty);
        }
    }

    public class TraceLogInfo
    {
        // Fields
        public string Message;

        public string ThreadID;
        public string TraceID;
        public eTRACELEVEL TraceLevel;

        // Methods
        public TraceLogInfo(string pMessage)
        {
            this.ThreadID = string.Empty;
            this.TraceID = LogTraceToFile.defTraceID;
            this.TraceLevel = eTRACELEVEL.INFORMATION;
            this.Message = pMessage;
        }

        public TraceLogInfo(string pMessage, eTRACELEVEL pTraceLevel)
        {
            this.ThreadID = string.Empty;
            this.TraceID = LogTraceToFile.defTraceID;
            this.TraceLevel = pTraceLevel;
            this.Message = pMessage;
        }

        public TraceLogInfo(string pMessage, eTRACELEVEL pTraceLevel, string pTraceID)
        {
            this.ThreadID = string.Empty;
            this.TraceID = pTraceID;
            this.TraceLevel = pTraceLevel;
            this.Message = pMessage;
        }
    }
}