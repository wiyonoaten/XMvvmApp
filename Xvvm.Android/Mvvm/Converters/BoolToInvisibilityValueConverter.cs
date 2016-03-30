using Android.Views;
using Xvvm.Mvvm;

namespace Xvvm.Android.Mvvm.Converters
{
    public class BoolToInvisibilityValueConverter : DelegateValueConverter<bool, ViewStates>
    {
        public BoolToInvisibilityValueConverter(bool useGoneState = false) : base(
            (boolVal) => boolVal ? (useGoneState ? ViewStates.Gone : ViewStates.Invisible) : ViewStates.Visible, 
            (viewStatesVal) => viewStatesVal != ViewStates.Visible ? true : false)
        {
        }
    }
}