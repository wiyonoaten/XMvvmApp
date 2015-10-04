namespace XMvvmApp.Mvvm.Converters
{
    public class InvertedBoolValueConverter : DelegateValueConverter<bool, bool>
    {
        public InvertedBoolValueConverter() : base(
            (sourceVal) => !sourceVal,
            (targetVal) => !targetVal)
        {
        }
    }
}
