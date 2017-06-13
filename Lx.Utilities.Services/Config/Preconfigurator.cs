using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Lx.Utilities.Contracts.Configuration;
using Lx.Utilities.Services.Infrastructure;
using Lx.Utilities.Services.Mapping.AutoMapper;

namespace Lx.Utilities.Services.Config
{
    public static class Preconfigurator
    {
        private static readonly ConcurrentBag<Action> Actions = new ConcurrentBag<Action>();

        private static readonly ReaderWriterLockSlim Lock =
            new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        private static bool _isPreConfigurationDone;

        public static void RegisterTasks(params Action[] actions)
        {
            AddPreconfigurationTasks(actions);
        }

        private static void AddPreconfigurationTasks(IEnumerable<Action> actions)
        {
            foreach (var action in actions.ToList())
                Actions.Add(action);
        }

        /// <summary>
        ///     Call all methods annotated with PreconfigurationAttribute
        /// </summary>
        /// <remarks>
        ///     Preconfigurator.Configure() is thread safe and idempotent
        /// </remarks>
        public static void Configure(bool forcedExecutionRequired = false)
        {
            Lock.EnterUpgradeableReadLock();
            try
            {
                if (_isPreConfigurationDone && !forcedExecutionRequired)
                    return;

                Lock.EnterWriteLock();
                try
                {
                    var actions = AssemblyHelper.GetTypesInReferencedAssemblies()
                        .SelectMany(type => type.GetMethods())
                        .Where(method => method.IsStatic &&
                                         method.GetCustomAttribute<PreconfigurationAttribute>() != null)
                        .Select(method => new Action(() => method.Invoke(null, new object[0])))
                        .ToList();

                    AddPreconfigurationTasks(actions);

                    foreach (var action in Actions)
                        action();

                    ConfigureMapping();

                    _isPreConfigurationDone = true;
                }
                finally
                {
                    Lock.ExitWriteLock();
                }
            }
            finally
            {
                Lock.ExitUpgradeableReadLock();
            }
        }

        public static void ConfigureMapping()
        {
            MappingService.Configure();
        }
    }
}