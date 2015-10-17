using System;
using System.ComponentModel;

namespace XMvvmApp.Mvvm.Binders
{
    public class PropertyChangedEventRaiseBinder : EventRaiseBinder<PropertyChangedEventArgs>
    {
        public static PropertyChangedEventRaiseBinder Create(Action<PropertyChangedEventHandler> addDelegate, Action<PropertyChangedEventHandler> removeDelegate)
        {
            return new PropertyChangedEventRaiseBinder(new PropertyChangedEventHandlerAdapter(addDelegate, removeDelegate));
        }

        private PropertyChangedEventRaiseBinder(PropertyChangedEventHandlerAdapter adapter)
            : base(_ => adapter.PropertyChanged += _, _ => adapter.PropertyChanged -= _)
        {
        }

        private class PropertyChangedEventHandlerAdapter
        {
            private readonly Action<PropertyChangedEventHandler> _addDelegate;
            private readonly Action<PropertyChangedEventHandler> _removeDelegate;

            private readonly object _propertyChangedEventLock = new object();
            private event EventHandler<PropertyChangedEventArgs> _propertyChanged;

            public PropertyChangedEventHandlerAdapter(Action<PropertyChangedEventHandler> addDelegate, Action<PropertyChangedEventHandler> removeDelegate)
            {
                _addDelegate = addDelegate;
                _removeDelegate = removeDelegate;
            }

            public event EventHandler<PropertyChangedEventArgs> PropertyChanged
            {
                add
                {
                    lock (_propertyChangedEventLock)
                    {
                        _propertyChanged += value;
                        _addDelegate(_HandlePropertyChanged);
                    }
                }
                remove
                {
                    lock (_propertyChangedEventLock)
                    {
                        _propertyChanged -= value;
                        _removeDelegate(_HandlePropertyChanged);
                    }
                }
            }

            private void _HandlePropertyChanged(object sender, PropertyChangedEventArgs args)
            {
                if (_propertyChanged != null)
                {
                    _propertyChanged(sender, args);
                }
            }
        }
    }
}
