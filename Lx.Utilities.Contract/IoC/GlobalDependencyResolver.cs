using System;

namespace Lx.Utilities.Contract.IoC
{
    public class GlobalDependencyResolver
    {
        protected static readonly GlobalDependencyResolver DefaultInstance = new GlobalDependencyResolver();
        protected Func<Type, object> ResolveAction;
        public static GlobalDependencyResolver Default => DefaultInstance;
        public bool CanResolve => ResolveAction != null;

        public void SetResolveAction(Func<Type, object> resolveAction)
        {
            ResolveAction = resolveAction;
        }

        public T Resolve<T>()
        {
            if (!CanResolve)
                return default(T);

            var o = (T) ResolveAction(typeof (T));
            return o;
        }

        public T ResolveRequiredDependency<T>() where T : class
        {
            if (!CanResolve)
                return default(T);

            var o = Resolve<T>();
            if (o == null)
                throw new NullReferenceException(typeof (T).FullName);

            return o;
        }
    }
}