using System;

namespace Xvvm.DependencyService
{
    [Serializable]
    public class DependencyNotResolvedException : Exception
    {
        public DependencyNotResolvedException(Type serviceType)
             : base("Dependency of type '" + serviceType.FullName + "' could not be resolved and is not an optional dependency.")
        {
        }
    }
}