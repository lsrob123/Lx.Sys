using System;
using Lx.Utilities.Contract.Infrastructure.Common;
using Lx.Utilities.Contract.Infrastructure.Dto;
using Lx.Utilities.Contract.Logging;
using Moq;

namespace Lx.Utilities.Services.Logging.Moq {
    public static class LoggerMockFactory {
        public static Mock<ILogger> Make(Action<Exception, string> callback) {
            var mock = new Mock<ILogger>();

            mock.Setup(x => x.LogException(It.IsAny<Exception>(), It.IsAny<string>()))
                .Callback<Exception, string>((exception, logReference) => {
                    Console.WriteLine($"Logging exception {exception.ToString()}");
                    Console.WriteLine($"With reference {logReference}");
                    callback(exception, logReference);
                });

            mock.Setup(x => x.Handle(It.IsAny<ProcessResult>()))
                .Callback<ProcessResult>(processResult => {
                    Console.WriteLine("Writing to log ...");
                    processResult.WriteExceptionsToLog(mock.Object);
                });

            Mediator.Default.Subscribe(mock.Object);
            return mock;
        }
    }
}