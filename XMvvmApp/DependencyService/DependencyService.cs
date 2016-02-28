using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace XMvvmApp.DependencyService
{
    public class DependencyService
    {
        private static readonly IDictionary<Type, object> _registry = new ConcurrentDictionary<Type, object>();

        public static void Register<T>(T service)
        {
            _registry[typeof(T)] = service;
        }

        public static void ResolveDependencies(object obj)
        {
            var type = obj.GetType();
            var properties = type.GetProperties();
            foreach (var prop in properties)
            {
                if (prop.CanWrite)
                {
                    var dependencyAttr = prop.GetCustomAttributes(typeof(DependencyAttribute), true).FirstOrDefault() as DependencyAttribute;
                    if (dependencyAttr != null)
                    {
                        object service = null;
                        if (_registry.ContainsKey(dependencyAttr.ServiceType))
                        {
                            service = _registry[dependencyAttr.ServiceType];
                        }

                        if (service == null && !dependencyAttr.IsOptional)
                        {
                            throw new DependencyNotResolvedException(dependencyAttr.ServiceType);
                        }

                        prop.SetValue(obj, service);
                    }
                }
            }
        }
    }
}
