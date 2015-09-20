using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Java.IO;
using XMvvmApp.Mvvm;

namespace XMvvmApp.Android.Mvvm.Views
{
    public abstract class BaseViewModelFragment<TViewModel> : Fragment
        where TViewModel : IViewModel
    {
        private const string STATE_KEY_VIEW_MODEL = "viewModel";

        protected abstract TViewModel MakeViewModel();
        protected abstract void OnBindViewsWithViewModel();
        protected abstract View OnSetupView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState, out bool retHasOptionsMenu);
        protected abstract void OnBindMenuItemsWithViewModel();
        protected abstract void OnSetupOptionsMenu(IMenu menu, MenuInflater inflater);

        protected TViewModel ViewModel { get; private set; }
        protected BindingCollection Bindings { get; private set; }
        protected IMenu Menu { get; private set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.ViewModel = MakeViewModel();
            this.Bindings = new BindingCollection();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            this.Bindings.DetachAll();
        }

        public override void OnStart()
        {
            base.OnStart();

            this.ViewModel.WakeupCommand.Execute(null);
        }

        public override void OnStop()
        {
            base.OnStop();

            this.ViewModel.SleepCommand.Execute(null);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            bool hasOptionsMenu = false;
            var view = OnSetupView(inflater, container, savedInstanceState, out hasOptionsMenu);

            if (hasOptionsMenu)
            {
                this.HasOptionsMenu = hasOptionsMenu;
            }

            OnBindViewsWithViewModel();

            _ProcessSavedInstanceState(savedInstanceState);

            return view;
        }

        private void _ProcessSavedInstanceState(Bundle savedInstanceState)
        {
            if (savedInstanceState != null)
            {
                var vmSavedState = savedInstanceState.GetSerializable(STATE_KEY_VIEW_MODEL);
                if (vmSavedState != null)
                {
                    this.ViewModel.RestoreSavedState(vmSavedState);
                }
            }
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);

            this.Menu = menu;

            OnSetupOptionsMenu(menu, inflater);

            OnBindMenuItemsWithViewModel();
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            outState.PutSerializable(STATE_KEY_VIEW_MODEL, (ISerializable)this.ViewModel.GetSavedState());
        }
    }
}