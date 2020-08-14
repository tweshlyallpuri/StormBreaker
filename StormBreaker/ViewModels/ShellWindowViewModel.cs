using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Controls.Ribbon;

namespace StormBreaker.ViewModels
{
    class ShellWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand<Object[]> NavigateCommand { get; private set; }
        public ShellWindowViewModel(IRegionManager regionManager)
        {
            NavigateCommand = new DelegateCommand<Object[]>(Navigate);
            _regionManager = regionManager;
        }
        private void Navigate(Object[] ribbonTabs)
        {
            if (ribbonTabs != null && ribbonTabs.Count()>0)
            {
                string viewName = (ribbonTabs[0] as RibbonTab)?.Name + "MainView";
                //var mainRegion = _regionManager.Regions["MainRegion"];
                //// Activate Module A view
                //var targetViewName = mainRegion.GetView("viewName");
                //mainRegion.Activate(targetViewName);
                //// Remove existing views
                //foreach (var oldViewName in mainRegion.Views)
                //{
                //    var oldView = mainRegion.GetView(oldViewName);
                //    mainRegion.Deactivate(oldView);
                //}

                _regionManager.RequestNavigate("MainRegion", viewName, Callback);
            }
        }

        private void Callback(NavigationResult result)
        {
            if (result.Error != null)
            {
                //handle error
            }
        }
    }
}
