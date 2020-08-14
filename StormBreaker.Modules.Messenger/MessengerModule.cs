using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using StormBreaker.Modules.Messenger.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace StormBreaker.Modules.Messenger
{
    class MessengerModule : IModule
    {
        private IRegionManager _regionManager;
        public MessengerModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions["RibbonRegion"];
            var dsView = containerProvider.Resolve<MessengerRibbonView>();
            region.Add(dsView);
            region = _regionManager.Regions["MainRegion"];
            //var view = containerProvider.Resolve<MessengerMainView>();
            //region.Add(view);
            //region.Deactivate(view);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MessengerMainView>();
        }
    }
}
