using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUtilities.Logger
{
    public class CustomLogger: ICustomLogger
    {
        private readonly string _logFilePathError;
        private readonly string _logFilePathWarning;
        private readonly string _logFilePathInfo;
        private readonly string _logFilePath;

        public CustomLogger()
        {

            _logFilePath = "C:\\My Web Sites\\LogFiles";
            _logFilePathError = "Error-Logs.txt";
            _logFilePathWarning = "Warning-Logs.txt";
            _logFilePathInfo = "Info-Logs.txt";
        }

        public void WriteLog(string Log_Level, String Data, String Message, String LogFilePath)
        {
            var logEntry = $@"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{Log_Level}]  
Content: [  {Data}  ]  
{Message} 

------------------------------------------------------------";

            string dateFolder = DateTime.Now.ToString("yyyy-MM-dd");
            string FullPath = Path.Combine(_logFilePath, dateFolder);
            var directoryPath = Path.GetDirectoryName(FullPath);
            if (FullPath != null)
            {
                Directory.CreateDirectory(FullPath);
            }
            var Filedirectory = Path.Combine(FullPath, LogFilePath);
            if (!File.Exists(Filedirectory))
            {
                File.Create(Filedirectory).Dispose();
            }

            File.AppendAllText(Filedirectory, logEntry + Environment.NewLine);
        }

        public void LogInformation(string message, string className, string methodName, string Linenumber)
        {
            WriteLog("INFO", $"Class: {className}, Method: {methodName}, LineNo: {Linenumber}", $"Message: {message}", _logFilePathInfo);
        }

        public void LogWarning(string message, string className, string methodName, string Linenumber)
        {
            WriteLog("WARNING", $"Class: {className}, Method: {methodName}, LineNo: {Linenumber}", $"Message: {message}", _logFilePathWarning);
        }

        public void LogError(Exception exception, string className, string methodName, string Linenumber)
        {
            WriteLog("Error", $"Class: {className}, Method: {methodName}, LineNo: {Linenumber}", $"Message: {exception}", _logFilePathError);
        }
    }
}
