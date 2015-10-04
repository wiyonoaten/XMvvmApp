using System.ComponentModel;

namespace XMvvmApp.Mvvm.Bindings
{
    public class ViewModelPropertyChangedBinding : Binding
    {
        private readonly INotifyPropertyChanged _viewModel;

        public ViewModelPropertyChangedBinding(IViewModel viewModel, PropertyChangedEventHandler evHandler)
            : base(evHandler)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += evHandler;
        }

        public override void Detach()
        {
            base.Detach();

            _viewModel.PropertyChanged -= base.Connection as PropertyChangedEventHandler;
        }
    }
}
