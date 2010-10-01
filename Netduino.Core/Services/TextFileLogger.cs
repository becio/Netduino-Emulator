using System;
using System.IO;
using Caliburn.Micro;

namespace Netduino.Core.Services
{
    public class TextFileLogger : ILog
    {
        public void Error(Exception exception)
        {
            File.AppendAllText("log.txt", exception.Message);
        }

        public void Info(string format, params object[] args)
        {
            File.AppendAllText("log.txt", string.Format(format,args));
            File.AppendAllText("log.txt", Environment.NewLine);
        }

        public void Warn(string format, params object[] args)
        {
            File.AppendAllText("log.txt", string.Format(format, args));
            File.AppendAllText("log.txt", Environment.NewLine);
        }
    }
}
