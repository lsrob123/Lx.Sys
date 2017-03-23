using System;
using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Infrastructure.Dto;

namespace Lx.Utilities.Contract.Logging {
    /// <summary>
    ///     For general purposed loggers
    /// </summary>
    public interface ILogger : IMediatorMessageHandler<ProcessResult> {
        /// <summary>
        ///     Log with normal message
        /// </summary>
        /// <param name="message"></param>
        void LogInfo(string message);

        /// <summary>
        ///     Log with trace message
        /// </summary>
        /// <param name="message"></param>
        void LogTrace(string message);

        /// <summary>
        ///     Log exception
        /// </summary>
        /// <param name="ex">Exception instance</param>
        /// <param name="logReference">Unique reference to the exception</param>
        void LogException(Exception ex, string logReference = null);
    }
}