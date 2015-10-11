using System;

namespace XMvvmApp.Mvvm.Converters
{
    public class StringFormatValueConverter<T> : IValueConverter<T, string>
    {
        private readonly string _formatString;
        private readonly Func<T, object>[] _argFunctors;

        public StringFormatValueConverter(string formatString)
        {
            _formatString = formatString;
            _argFunctors = null;
        }

        public StringFormatValueConverter(string formatString, params Func<T, object>[] argFunctors)
        {
            _formatString = formatString;
            _argFunctors = argFunctors;
        }

        public string Convert(T source)
        {
            if (_argFunctors == null)
            {
                return string.Format(_formatString, source);
            }

            var args = new object[_argFunctors.Length];
            for (int i=0; i < _argFunctors.Length; i++)
            {
                args[i] = _argFunctors[i].Invoke(source);
            }

            return string.Format(_formatString, args);
        }

        public T ConvertBack(string target)
        {
            throw new NotSupportedException(nameof(ConvertBack) + " is not supported");
        }
    }
}
