using System;
using Lx.Utilities.Contracts.Logging;

namespace Lx.Utilities.Services.Infrastructure
{
    public static class ExceptionExtensions
    {
        /// <summary>
        ///     Write exception message to log
        /// </summary>
        /// <param name="exception">Exception instance</param>
        /// <param name="logger">Logger</param>
        /// <param name="logReference">Unique reference to the logged exception</param>
        /// <returns></returns>
        public static Exception WriteToLog(this Exception exception, ILogger logger, string logReference = null)
        {
            logger?.LogException(exception, logReference);
            return exception;
        }

        /// <summary>
        ///     Attach a data object to the Exception instance
        /// </summary>
        /// <param name="exception">Exception instance</param>
        /// <param name="data">Data object to be attached to the Exception instance</param>
        /// <param name="dataObjectReference">Optional reference to the data object</param>
        /// <returns></returns>
        public static Exception WithData(this Exception exception, object data, string dataObjectReference = null)
        {
            if (!data.GetType().IsSerializable)
                return exception;

            var key = string.IsNullOrWhiteSpace(dataObjectReference)
                ? data.GetHashCode().ToString()
                : dataObjectReference;
            exception.Data.Add(key, data);
            return exception;
        }

        /// <summary>
        ///     Attach one or more data objects to the Exception instance
        /// </summary>
        /// <param name="exception">Exception instance</param>
        /// <param name="dataObjects">Data objects to be attached to the Exception instance</param>
        /// <returns></returns>
        public static Exception WithData(this Exception exception, params object[] dataObjects)
        {
            foreach (var dataObject in dataObjects)
                exception.WithData(dataObject);
            return exception;
        }
    }
}