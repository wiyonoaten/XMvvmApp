using Android.Views;
using System;

namespace Xvvm.Android.EventListeners
{
    public class DelegateMenuItemOnMenuItemClickListener : Java.Lang.Object, IMenuItemOnMenuItemClickListener
    {
        private readonly Func<IMenuItem, bool> _dlgt;

        public DelegateMenuItemOnMenuItemClickListener(Func<IMenuItem, bool> dlgt)
        {
            _dlgt = dlgt;
        }

        public bool OnMenuItemClick(IMenuItem item)
        {
            return _dlgt.Invoke(item);
        }
    }
}