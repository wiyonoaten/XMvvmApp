namespace XMvvmApp.Mvvm.Converters
{
    public class TypeCastValueConverter<TSource, TTarget> : DelegateValueConverter<TSource, TTarget>
    {
        public TypeCastValueConverter() : base(
            (sourceVal) => (TTarget)(object)sourceVal,
            (targetVal) => (TSource)(object)targetVal)
        {
        }
    }
}
