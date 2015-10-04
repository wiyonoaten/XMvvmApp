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

        public static T GetPropertyValue<T>(this Expression<Func<T>> propertyExp)
        {
            if (propertyExp == null)
            {
                throw new ArgumentNullException(nameof(propertyExp));
            }

            if (!(propertyExp.Body is MemberExpression))
            {
                throw new ArgumentException("Not a valid property expression", nameof(propertyExp));
            }

            return propertyExp.Compile()();
        }

        public static O GetPropertyOwner<T, O>(this Expression<Func<T>> propertyExp)
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

            var objExp = body.Expression;

            if (!typeof(O).IsAssignableFrom(objExp.Type))
            {
                throw new ArgumentException("Not a valid property expression", nameof(propertyExp));
            }

            var lambdaObjExp = Expression.Lambda<Func<O>>(objExp);

            return lambdaObjExp.Compile()();
        }
    }
}
