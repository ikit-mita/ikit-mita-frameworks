using System;
using System.Collections.Generic;
using System.Linq;
using IkitMita.Mvvm.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace IkitMita.Mvvm.Views
{
    public abstract class ViewManagerBase<TVm> : IViewManager<TVm> where TVm : IShowableViewModel
    {
        protected readonly IServiceLocator ServiceLocator;

        protected ViewManagerBase(IViewModelManager<TVm> vmManager, IServiceLocator serviceLocator)
        {
            ServiceLocator = serviceLocator;
            vmManager.ViewModelShown += (sender, e) => ShowViewForViewModel(e.ViewModel);
            vmManager.ViewModelClosed += (sender, e) => CloseViewForViewModel(e.ViewModel);
        }

        public abstract void ShowViewForViewModel(TVm viewModel);

        public abstract void CloseViewForViewModel(TVm viewModel);

        protected IView ResolveView(Type viewModelType)
        {
            var contractName = GetViewContractName(viewModelType);
            var view = ResolveView(contractName);
            return view;
        }

        protected IView ResolveView(string contractName)
        {
            var view = ServiceLocator.GetInstance<IView>(contractName);
            return view;
        }

        protected string GetViewContractName(Type viewModelType)
        {
            string contractName = viewModelType.Name.Replace("ViewModel", "View");

            if (viewModelType.IsGenericType)
            {
                List<string> parts = viewModelType.GetGenericArguments()
                    .Select(t => t.Name)
                    .ToList();

                var indexOfGenParam = viewModelType.Name.IndexOf('`');
                var vmTypeName = viewModelType.Name.Substring(0, indexOfGenParam);
                parts.Add(vmTypeName.Replace("ViewModel", "View"));

                contractName = string.Join("_", parts);
            }

            return contractName;
        }
    }
}
