using System;
using System.ComponentModel;
using XMvvmApp.Utils;

namespace XMvvmApp.Mvvm.Binders
{
    public class PropertyChangedEventTriggerBinder : EventTriggerBinder<PropertyChangedEventArgs>
    {
        public static PropertyChangedEventTriggerBinder Create(Action<PropertyChangedEventHandler> addDelegate, Action<PropertyChangedEventHandler> removeDelegate)
        {
            return new PropertyChangedEventTriggerBinder(new PropertyChangedEventHandlerAdapter(addDelegate, removeDelegate));
        }

        private PropertyChangedEventTriggerBinder(PropertyChangedEventHandlerAdapter adapter)
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
                _propertyChanged.TriggerSafely(sender, args);
            }
        }
    }
}
