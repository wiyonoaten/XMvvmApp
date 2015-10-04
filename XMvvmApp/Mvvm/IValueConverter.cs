namespace XMvvmApp.Mvvm
{
    public interface IValueConverter<TSource, TTarget>
    {
        TTarget Convert(TSource source);
        TSource ConvertBack(TTarget target);
    }
}
