using Android.Views;
using XMvvmApp.Mvvm;

namespace XMvvmApp.Android.Mvvm.Converters
{
    public class BoolToVisibilityValueConverter : DelegateValueConverter<bool, ViewStates>
    {
        public BoolToVisibilityValueConverter(bool useGoneState = false) : base(
            (boolVal) => boolVal ? ViewStates.Visible : (useGoneState ? ViewStates.Gone : ViewStates.Invisible), 
            (viewStatesVal) => viewStatesVal == ViewStates.Visible ? true : false)
        {
        }
    }
}