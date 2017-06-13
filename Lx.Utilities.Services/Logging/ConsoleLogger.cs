using System;
using Lx.Utilities.Contracts.Infrastructure.DTOs;
using Lx.Utilities.Contracts.Logging;
using Lx.Utilities.Contracts.Mediator;

namespace Lx.Utilities.Services.Logging
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger()
        {
            Mediator.Default.Subscribe(this);
        }

        public void LogInfo(string message)
        {
            Console.WriteLine(message);
        }

        public void LogTrace(string message)
        {
            Console.WriteLine(message);
        }

        public void LogException(Exception ex, string logReference = null)
        {
            if (!string.IsNullOrWhiteSpace(logReference))
                Console.WriteLine($"Reference: {logReference}");
            Console.WriteLine(ex.ToString());
        }

        public void Handle(ProcessResult message)
        {
            message.WriteExceptionsToLog(this);
        }
    }
}