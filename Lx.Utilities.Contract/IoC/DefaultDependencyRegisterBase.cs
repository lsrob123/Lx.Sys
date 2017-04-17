using System;

namespace Lx.Utilities.Contract.IoC
{
    public abstract class DefaultDependencyRegisterBase
    {
        private Action<Type, object, bool> _registerInstanceAction;
        private Action<Type, Type, bool, bool> _registerTypeAction;

        public void SetActions(Action<Type, Type, bool, bool> registerType, Action<Type, object, bool> registerInstance)
        {
            _registerTypeAction = registerType;
            _registerInstanceAction = registerInstance;
        }

        public abstract void AddRegistrations();

        protected virtual void Register<TInterface, TInstance>(Func<TInstance> instanceFactory = null,
            bool externallyOwned = false, bool singleInstance = false)
            where TInstance : TInterface
        {
            if (instanceFactory == null)
                _registerTypeAction(typeof (TInterface), typeof (TInstance), externallyOwned, singleInstance);
            else
                _registerInstanceAction(typeof (TInterface), instanceFactory(), externallyOwned);
        }

        protected virtual void Register<TInterface>(Func<TInterface> instanceFactory, bool externallyOwned = false)
        {
            _registerInstanceAction(typeof (TInterface), instanceFactory(), externallyOwned);
        }
    }
}