using System;

namespace XMvvmApp.DependencyService
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DependencyAttribute : Attribute
    {
        public readonly Type ServiceType;

        public DependencyAttribute(Type serviceType)
        {
            ServiceType = serviceType;
        }

        public bool IsOptional { get; set; }
    }
}
