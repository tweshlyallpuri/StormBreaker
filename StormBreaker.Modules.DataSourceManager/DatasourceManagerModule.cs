using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using StormBreaker.Modules.DatasourceManager.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace StormBreaker.Modules.DatasourceManager
{
    class DatasourceManagerModule : IModule
    {
        private IRegionManager _regionManager;
        public DatasourceManagerModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            //new XmlHelper();
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //do some initialization stuff here
            IRegion region = _regionManager.Regions["RibbonRegion"];
            var dsView = containerProvider.Resolve<DatasourceManagerRibbonView>();
            region.Add(dsView);
            _regionManager.RequestNavigate("MainRegion", "DatasourceManagerMainView");
            // region = _regionManager.Regions["MainRegion"];
            //region.Add(containerProvider.Resolve<DatasourceManagerMainView>());
            //region.Add(containerProvider.Resolve<DatasourceManagerMainView>());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DatasourceManagerMainView>();
        }
    }
}
