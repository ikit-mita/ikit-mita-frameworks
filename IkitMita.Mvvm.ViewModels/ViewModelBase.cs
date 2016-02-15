using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JetBrains.Annotations;

namespace IkitMita.Mvvm.ViewModels
{
    public abstract class ViewModelBase : IViewModel, INotifyPropertyChanged
    {
        private static readonly ConcurrentDictionary<Type, Dictionary<string, List<PropertyInfo>>> _propertiesDependenciesCashe = new ConcurrentDictionary<Type, Dictionary<string, List<PropertyInfo>>>();
        private readonly ConcurrentStack<int> _operationsInProggress = new ConcurrentStack<int>();
        private IThreadSafeInvoker _threadSafeInvoker;

        public event PropertyChangedEventHandler PropertyChanged;

        protected IThreadSafeInvoker ThreadSafeInvoker
        {
            get { return _threadSafeInvoker ?? (_threadSafeInvoker = NotThreadSafeInvoker.Instance); }
            set { _threadSafeInvoker = value; }
        }

        public bool IsBusy => _operationsInProggress.Count > 0;

        [DependsOn(nameof(IsBusy))]
        public bool IsFree => _operationsInProggress.Count == 0;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            propertyName = Check.NotNullOrEmpty(propertyName, nameof(propertyName));

            //UI thread save
            ThreadSafeInvoker.Invoke(() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)));

            CheckDependentProperties(propertyName);
        }

        private void CheckDependentProperties([NotNull] string propertyName)
        {
            var propDeps = _propertiesDependenciesCashe.GetOrAdd(GetType(), GetPropertiesDependencies);
            var dependentProperties = propDeps.GetValueSafe(propertyName, () => new List<PropertyInfo>());

            foreach (var property in dependentProperties)
            {
                if (property.PropertyType.Is<ICommand>())
                {
                    var command = (ICommand)property.GetValue(this);
                    var delegateCommand = command as DelegateCommandBase;

                    if (delegateCommand != null)
                    {
                        delegateCommand.RaiseCanExecuteChanged();
                    }
                    else
                    {
                        command.RaiseEvent("CanExecuteChanged", EventArgs.Empty);
                    }
                }
                else
                {
                    OnPropertyChanged(property.Name);
                }
            }
        }

        private static Dictionary<string, List<PropertyInfo>> GetPropertiesDependencies([NotNull]Type type)
        {
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var dependencies = new Dictionary<string, List<PropertyInfo>>(properties.Length);

            foreach (var property in properties)
            {
                var dependsOnAttributes = property.GetCustomAttributes<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    var propertyInfos = dependencies.GetOrAdd(dependsOnAttribute.PropertyName, () => new List<PropertyInfo>());
                    propertyInfos.Add(property);
                }
            }

            return dependencies;
        }

        protected OperationBusier StartOperation()
        {
            _operationsInProggress.Push(1);
            OnPropertyChanged(nameof(IsBusy));

            return new OperationBusier(this, vm =>
                {
                    int i;
                    _operationsInProggress.TryPop(out i);
                    OnPropertyChanged(nameof(IsBusy));
                });
        }

        protected class OperationBusier : IDisposable
        {
            private bool _isDisposed;
            private readonly ViewModelBase _vm;
            private readonly Action<ViewModelBase> _onDispose;
            public OperationBusier(ViewModelBase vm, Action<ViewModelBase> onDispose)
            {
                _vm = vm;
                _onDispose = onDispose;
            }

            public void End()
            {
                Dispose();
            }

            public void Dispose()
            {
                if (!_isDisposed)
                {
                    _onDispose(_vm);
                    _isDisposed = true;
                }
            }
        }
    }
}
