namespace XMvvmApp.Mvvm.Converters
{
    public class IntToBoolValueConverter : DelegateValueConverter<int, bool>
    {
        public IntToBoolValueConverter() : base(
            (intVal) => intVal > 0 ? true : false,
            (boolVal) => boolVal ? 1 : 0)
        {
        }
    }
}
