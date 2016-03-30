using System;

namespace Xvvm.Mvvm
{
    public class DelegateValueConverter<TSource, TTarget> : IValueConverter<TSource, TTarget>
    {
        private readonly Func<TSource, TTarget> _convertDlgt;
        private readonly Func<TTarget, TSource> _convertBackDlgt;

        public DelegateValueConverter(Func<TSource, TTarget> convertDlgt, Func<TTarget, TSource> convertBackDlgt)
        {
            _convertDlgt = convertDlgt;
            _convertBackDlgt = convertBackDlgt;
        }

        public DelegateValueConverter(Func<TSource, TTarget> convertDlgt)
        {
            _convertDlgt = convertDlgt;
            _convertBackDlgt = (targetValue) => { throw new NotSupportedException("convertBack is unsupported"); };
        }

        public TTarget Convert(TSource source)
        {
            return _convertDlgt(source);
        }

        public TSource ConvertBack(TTarget target)
        {
            return _convertBackDlgt(target);
        }
    }
}
