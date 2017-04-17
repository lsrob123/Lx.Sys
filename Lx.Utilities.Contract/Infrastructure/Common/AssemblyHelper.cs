using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Lx.Utilities.Contract.Infrastructure.Common
{
    public static class AssemblyHelper
    {
        private static readonly ReaderWriterLockSlim Lock =
            new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        private static readonly List<Assembly> ReferencedAssemblies = new List<Assembly>();

        public static IReadOnlyCollection<Assembly> GetReferencedAssemblies(ICollection<string> namespaceKeywords = null)
        {
            namespaceKeywords = namespaceKeywords ?? new List<string>();
            namespaceKeywords.Add(typeof (AssemblyHelper).Namespace?.Split('.')[0]);

            var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

            Lock.EnterUpgradeableReadLock();
            try
            {
                if (!ReferencedAssemblies.Any())
                {
                    Lock.EnterWriteLock();
                    try
                    {
                        var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");

                        ReferencedAssemblies.AddRange(
                            referencedPaths.Select(
                                path => AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path)))
                                .Where(x => namespaceKeywords.Any(y => x.FullName.Contains(y))));
                    }
                    finally
                    {
                        Lock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                Lock.ExitUpgradeableReadLock();
            }

            var allAssemblies = new List<Assembly>(domainAssemblies);
            allAssemblies.AddRange(ReferencedAssemblies);

            return allAssemblies;
        }

        public static IReadOnlyCollection<Type> GetTypesInReferencedAssemblies(
            ICollection<string> namespaceKeywords = null, Func<Type, bool> typeFilter = null)
        {
            var types = GetReferencedAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeFilter?.Invoke(t) ?? true)
                .ToList();
            return types;
        }
    }
}