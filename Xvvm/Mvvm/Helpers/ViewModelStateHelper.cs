using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Xvvm.Utils;

namespace Xvvm.Mvvm.Helpers
{
    public class ViewModelStateHelper
    {
        private readonly IViewModel _viewModel;

        private List<PropertyInfo> _stateProps;

        public ViewModelStateHelper(IViewModel viewModel)
        {
            _viewModel = viewModel;

            _stateProps = null;
        }

        private void _BuildStatePropertiesCache()
        {
            _stateProps = new List<PropertyInfo>();

            var type = _viewModel.GetType();

            _AppendStateProperties(type);
        }

        private void _AppendStateProperties(Type type)
        {
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var prop in props)
            {
                // Not specified to be IgnoredProperty
                var attrs = prop.GetCustomAttributes(typeof(IgnoredPropertyAttribute), true);
                if (attrs.Count() > 0)
                {
                    continue;
                }

                // Not a command
                if (typeof(ICommand).IsAssignableFrom(prop.PropertyType))
                {
                    continue;
                }

                // Not a delegate
                if (typeof(Delegate).IsAssignableFrom(prop.PropertyType))
                {
                    continue;
                }

                _stateProps.Add(prop);
            }
        }

        public object GetState()
        {
            lock(this)
            {
                if (_stateProps == null)
                {
                    _BuildStatePropertiesCache();
                }

                var jObj = new JObject();

                foreach (var prop in _stateProps)
                {
                    var value = prop.GetValue(_viewModel);

                    JToken jTok = null;
                    if (value != null)
                    {
                        jTok = JToken.FromObject(value);
                    }

                    jObj.Add(prop.Name, jTok); 
                }

                return jObj.ToString(Formatting.None);
            }
        }

        public void RestoreState(object state)
        {
            lock(this)
            {
                if (_stateProps == null)
                {
                    _BuildStatePropertiesCache();
                }

                if (!(state is string))
                {
                    return;
                }

                JObject jObj = JObject.Parse(state as string);

                foreach (var prop in _stateProps)
                {
                    var jTok = jObj.GetValue(prop.Name);
                    var valueJsonStr = jTok.ToString(Formatting.None);

                    if (
#if (WINDOWS_UWP)
                        prop.PropertyType.GetTypeInfo().IsGenericType
#else
                        prop.PropertyType.IsGenericType
#endif
                        && prop.PropertyType.GetGenericTypeDefinition().IsWithin(
                            typeof(IObservableReadOnlyList<>), 
                            typeof(IObservableList<>)))
                    {
                        var elementType = prop.PropertyType.GetGenericArguments().First();
                        var enumerableType = typeof(IEnumerable<>).MakeGenericType(elementType);
                        var items = JsonConvert.DeserializeObject(valueJsonStr, enumerableType);

                        var concretePropType = typeof(ObservableList<>).MakeGenericType(elementType);
                        var resetToMethod = concretePropType.GetMethod(ObservableList<object>.ResetToMethodName, BindingFlags.Instance | BindingFlags.Public);
                        resetToMethod.Invoke(prop.GetValue(_viewModel), new object[] { items });
                    }
                    else
                    {
                        var value = JsonConvert.DeserializeObject(valueJsonStr, prop.PropertyType);
                        prop.SetValue(_viewModel, value);
                    }
                }
            }
        }
    }
}
