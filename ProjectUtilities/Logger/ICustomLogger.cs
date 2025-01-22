using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUtilities.Logger
{
    public interface ICustomLogger
    {
        void WriteLog(string Log_Level, String Data, String Message, String LogFilePath);
        void LogInformation(string message, string className, string methodName, string Linenumber);
        void LogWarning(string message, string className, string methodName, string Linenumber);
        void LogError(Exception exception, string className, string methodName, string Linenumber);
    }
}
