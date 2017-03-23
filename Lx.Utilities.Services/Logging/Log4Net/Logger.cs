using System;
using System.Reflection;
using log4net;
using log4net.Config;
using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Logging;
using Lx.Utilities.Contract.Mediator;

namespace Lx.Utilities.Services.Logging.Log4Net {
    public class Logger : ILogger {
        protected static readonly ILog Log;

        static Logger() {
            XmlConfigurator.Configure();
            Log = LogManager.GetLogger(Assembly.GetCallingAssembly().FullName);
        }

        public Logger() {
            Mediator.Default.Subscribe(this);
        }

        public void LogInfo(string message) {
            Log.Info(message);
        }

        public void LogTrace(string message) {
            Log.Debug(message);
        }

        public void LogException(Exception ex, string logReference = null) {
            if (string.IsNullOrWhiteSpace(logReference))
                Log.Error(ex);
            else
                Log.Error(logReference, ex);
        }

        public void Handle(ProcessResult message) {
            message.WriteExceptionsToLog(this);
        }
    }
}