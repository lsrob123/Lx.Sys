using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Lx.Utilities.Contracts.Infrastructure.Enumerations;
using Lx.Utilities.Contracts.Infrastructure.Exceptions;
using Lx.Utilities.Contracts.Infrastructure.Interfaces;
using Lx.Utilities.Contracts.Logging;
using Newtonsoft.Json;

namespace Lx.Utilities.Contracts.Infrastructure.DTOs
{
    public class ProcessResult : IDto
    {
        /// <summary>
        ///     The following exception types can not be deserialized with JSON serializer used in NServiceBus and
        ///     hence are required to convert to general exception type
        /// </summary>
        protected static readonly Type[] ReplicationRequiringExceptions =
        {
            typeof(EntitySqlException),
            typeof(PropertyConstraintException),
            typeof(DbUpdateConcurrencyException),
            typeof(DbUpdateException),
            typeof(DbEntityValidationException)
        };

        public ProcessResult()
        {
        }

        public ProcessResult(ProcessResultType type, Exception exception,
            string resultReference = null, bool logExcetions = true)
            : this(type, new List<Exception> {exception}, resultReference, logExcetions)
        {
        }

        public ProcessResult(ProcessResultType type, IEnumerable<Exception> exceptions = null,
            string resultReference = null, bool logExcetions = true)
        {
            Type = type;
            LogExcetions = logExcetions;
            SetExceptions(exceptions);

            if (!string.IsNullOrWhiteSpace(resultReference))
            {
                ResultReference = resultReference.Trim();
                return;
            }

            ResultReference = Guid.NewGuid().ToString();

            if (HasExceptions && LogExcetions)
                Mediator.Mediator.Default.Publish(this);
            // TODO: Assess the possibility of asynchronizing this mediator publish
        }

        /// <summary>
        ///     Type is defaulted to ProcessResultType.MultiStatus
        /// </summary>
        /// <param name="exceptions"></param>
        /// <param name="resultReference"></param>
        /// <param name="logExcetions"></param>
        public ProcessResult(IReadOnlyCollection<Exception> exceptions, string resultReference = null,
            bool logExcetions = true)
            : this(GetProcessResultType(exceptions),
                exceptions, resultReference, logExcetions)
        {
        }

        /// <summary>
        ///     Type is defaulted to ProcessResultType.InternalServerError
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="resultReference"></param>
        /// <param name="logExcetions"></param>
        public ProcessResult(Exception exception, string resultReference = null, bool logExcetions = true)
            : this(ProcessResultType.InternalServerError, new List<Exception> {exception}, resultReference, logExcetions
            )
        {
        }

        public bool LogExcetions { get; set; }
        public ProcessResultType Type { get; set; }
        public ICollection<Exception> Exceptions { get; set; }
        public string Reason { get; set; }

        [NotMapped]
        [IgnoreDataMember]
        [JsonIgnore]
        public bool HasExceptions => Exceptions != null && Exceptions.Any();

        public string ResultReference { get; set; }

        public ProcessResult WithReason(string reason)
        {
            Reason = reason;
            return this;
        }

        private static ProcessResultType GetProcessResultType(IReadOnlyCollection<Exception> exceptions)
        {
            if (exceptions == null || !exceptions.Any())
                return ProcessResultType.Ok;

            if (exceptions.Count > 1)
                return ProcessResultType.InternalServerError;

            var exception = exceptions.First();

            if (exception is ForbiddenException)
                return ProcessResultType.Forbidden;

            if (exception is ArgumentNullException || exception is ArgumentException)
                return ProcessResultType.BadRequest;

            if (exception is KeyNotFoundException || exception is ObjectNotFoundException)
                return ProcessResultType.NotFound;

            if (exception is NotImplementedException)
                return ProcessResultType.NotImplemented;

            return ProcessResultType.InternalServerError;
        }

        public override string ToString()
        {
            if (!HasExceptions)
                return Type.IsSuccess ? Type.Name : base.ToString();

            var aggregateException = new AggregateException(Exceptions);
            return aggregateException.ToString();
        }

        public static Exception ReplicateToGeneralException<TException>(TException source)
            where TException : Exception
        {
            if (source == null)
                return null;

            var mostDerivedType = source.GetType();
            if (ReplicationRequiringExceptions.All(x => x != mostDerivedType))
                return source;

            var destination = new Exception(source.ToString());
            return destination;
        }

        public ProcessResult DisableExceptionLogging()
        {
            LogExcetions = false;
            return this;
        }

        public ProcessResult EnableExceptionLogging()
        {
            LogExcetions = true;
            return this;
        }

        public ProcessResult SetExceptions(IEnumerable<Exception> exceptions)
        {
            Exceptions = exceptions?.Select(ReplicateToGeneralException).ToList();
            return this;
        }

        public ProcessResult SetException(Exception exception)
        {
            if (exception != null)
                SetExceptions(new List<Exception> {exception});
            return this;
        }

        public ProcessResult WithProcessResultType(ProcessResultType processResultType)
        {
            Type = processResultType;
            return this;
        }

        public static implicit operator ProcessResult(ProcessResultType type)
        {
            return new ProcessResult(type);
        }

        public bool Equals(ProcessResultType type)
        {
            return Type.Equals(type);
        }

        public static implicit operator ProcessResult(Exception exception)
        {
            return new ProcessResult(exception);
        }

        public static implicit operator ProcessResult(List<Exception> exceptions)
        {
            return new ProcessResult(exceptions);
        }

        public static implicit operator ProcessResult(HashSet<Exception> exceptions)
        {
            return new ProcessResult(exceptions);
        }

        public static implicit operator ProcessResult(Exception[] exceptions)
        {
            return new ProcessResult(exceptions);
        }

        public static implicit operator ProcessResult(ConcurrentBag<Exception> exceptions)
        {
            return new ProcessResult(exceptions);
        }

        public void WriteExceptionsToLog(ILogger logger)
        {
            if (!LogExcetions || !HasExceptions)
                return;

            if (Exceptions.Count == 1)
            {
                logger.LogException(Exceptions.First(), ResultReference);
            }
            else
            {
                var aggregateException = new AggregateException(Exceptions);
                logger.LogException(aggregateException, ResultReference);
            }
        }
    }
}