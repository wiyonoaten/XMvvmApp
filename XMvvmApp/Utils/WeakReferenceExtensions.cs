using System;

namespace XMvvmApp.Utils
{
    public static class WeakReferenceExtensions
    {
        public static T Get<T>(this WeakReference<T> weakSelf)
            where T : class
        {
            T strongSelf;
            if (!weakSelf.TryGetTarget(out strongSelf))
            {
                return null;
            }
            return strongSelf;
        }
    }
}
