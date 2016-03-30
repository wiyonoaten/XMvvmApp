using System.Reflection;

namespace Xvvm.Mvvm
{
    public static class IValueConverterExtensions
    {
        public static T GetTargetValue<S, T>(this IValueConverter<S, T> vc, S sourceVal)
        {
            if (vc == null)
            {
                if (typeof(T).IsAssignableFrom(typeof(S)))
                {
                    return (T)(object)sourceVal; // upcast
                }
                return default(T);
            }
            return vc.Convert(sourceVal);
        }

        public static S FromTargetValue<S, T>(this IValueConverter<S, T> vc, T targetVal)
        {
            if (vc == null)
            {
                if (typeof(S).IsAssignableFrom(typeof(T)))
                {
                    return (S)(object)targetVal; // upcast
                }
                return default(S);
            }
            return vc.ConvertBack(targetVal);
        }

        public static string GetStringValue<S>(this IValueConverter<S, string> vc, S sourceVal)
        {
            if (sourceVal != null && vc == null)
            {
                return sourceVal.ToString();
            }
            return vc.GetTargetValue(sourceVal);
        }

        public static S FromStringValue<S>(this IValueConverter<S, string> vc, string stringVal)
        {
            return vc.FromTargetValue(stringVal);
        }

        public static bool GetBoolValue<S>(this IValueConverter<S, bool> vc, S sourceVal)
        {
            if (sourceVal != null && vc == null)
            {
                if (false == typeof(bool).IsAssignableFrom(typeof(S)))
                {
                    return true;
                }
            }
            return vc.GetTargetValue(sourceVal);
        }

        public static S FromBoolValue<S>(this IValueConverter<S, bool> vc, bool boolVal)
        {
            return vc.FromTargetValue(boolVal);
        }

        public static IValueConverter<S1, T> ChainWith<S1, S2, T>(this IValueConverter<S1, S2> vc, IValueConverter<S2, T> anotherVc)
        {
            return new DelegateValueConverter<S1, T>(
                (source1Val) => anotherVc.Convert(vc.Convert(source1Val)),
                (targetVal) => vc.ConvertBack(anotherVc.ConvertBack(targetVal)));
        }
    }
}
