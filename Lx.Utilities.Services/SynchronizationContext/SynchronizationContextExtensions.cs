using System;

namespace Lx.Utilities.Services.SynchronizationContext {
    public static class SynchronizationContextExtensions {
        public static void Post(this System.Threading.SynchronizationContext synchronizationContext, Action action) {
            synchronizationContext.Post(x => { action?.Invoke(); }, null);
        }
    }
}