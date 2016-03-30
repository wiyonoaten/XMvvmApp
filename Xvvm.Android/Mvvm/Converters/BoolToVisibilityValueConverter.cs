using Android.Views;
using Xvvm.Mvvm;

namespace Xvvm.Android.Mvvm.Converters
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