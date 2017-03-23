using System;

namespace Lx.Utilities.Contract.Infrastructure.Exceptions {
    public class ForbiddenException : Exception {
        public ForbiddenException() {}

        public ForbiddenException(string message, Exception innerException = null) : base(message, innerException) {}
    }
}