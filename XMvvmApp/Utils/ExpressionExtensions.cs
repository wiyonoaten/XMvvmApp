using System;
using System.Linq.Expressions;
using System.Reflection;

namespace XMvvmApp.Utils
{
    public static class ExpressionExtensions
    {
        public static string GetPropertyName<T>(this Expression<Func<T>> propertyExp)
        {
            if (propertyExp == null)
            {
                throw new ArgumentNullException(nameof(propertyExp));
            }

            var body = propertyExp.Body as MemberExpression;

            if (body == null)
            {
                throw new ArgumentException("Not a valid property expression", nameof(propertyExp));
            }

            var property = body.Member as PropertyInfo;

            if (property == null)
            {
                throw new ArgumentException("Not a valid property expression", nameof(propertyExp));
            }

            return property.Name;
        }
    }
}
