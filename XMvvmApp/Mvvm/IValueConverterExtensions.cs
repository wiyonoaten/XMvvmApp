namespace XMvvmApp.Mvvm
{
    public static class IValueConverterExtensions
    {
        public static V GetConvertedValue<T, V>(this IValueConverter<T, V> valueConverter, T value)
        {
            if (valueConverter == null)
            {
                return default(V);
            }
            return valueConverter.Convert(value);
        }

        public static T FromConvertedValue<T, V>(this IValueConverter<T, V> valueConverter, V convertedValue)
        {
            if (valueConverter == null)
            {
                return default(T);
            }
            return valueConverter.ConvertBack(convertedValue);
        }

        public static string GetStringValue<T>(this IValueConverter<T, string> valueConverter, T value)
        {
            if (value != null && valueConverter == null)
            {
                return value.ToString();
            }
            return valueConverter.GetConvertedValue(value);
        }

        public static T FromStringValue<T>(this IValueConverter<T, string> valueConverter, string stringValue)
        {
            if (stringValue != null && valueConverter == null)
            {
                if (typeof(string).IsAssignableFrom(typeof(T)))
                {
                    return (T)(object)stringValue;
                }
                return default(T);
            }
            return valueConverter.FromConvertedValue(stringValue);
        }

        public static bool GetBoolValue<T>(this IValueConverter<T, bool> valueConverter, T value)
        {
            if (value != null && valueConverter == null)
            {
                if (typeof(bool).IsAssignableFrom(typeof(T)))
                {
                    return (bool)(object)value;
                }
                return true;
            }
            return valueConverter.GetConvertedValue(value);
        }

        public static T FromBoolValue<T>(this IValueConverter<T, bool> valueConverter, bool booleanValue)
        {
            if (valueConverter == null)
            {
                if (typeof(bool).IsAssignableFrom(typeof(T)))
                {
                    return (T)(object)booleanValue;
                }
                return default(T);
            }
            return valueConverter.FromConvertedValue(booleanValue);
        }

        public static IValueConverter<T1, V> ChainWith<T1, T2, V>(this IValueConverter<T1, T2> valueConverter, IValueConverter<T2, V> anotherValueConverter)
        {
            return new DelegateValueConverter<T1, V>(
                (t1Val) => anotherValueConverter.Convert(valueConverter.Convert(t1Val)),
                (vVal) => valueConverter.ConvertBack(anotherValueConverter.ConvertBack(vVal)));
        }
    }
}
