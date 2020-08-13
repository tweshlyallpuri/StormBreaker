using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity;

namespace StormBreaker.Views
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : RibbonWindow
    {
        IRegionManager _regionManager;
        IUnityContainer _container;
        public ShellWindow(IRegionManager regionManager, IUnityContainer container)
        {
            InitializeComponent();
            _regionManager = regionManager;
            _container = container;
        }

        private void OnRibbonTabSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tab = ShellRibbon?.SelectedItem as RibbonTab;
            if(tab!=null)
            {
                //logic for activating views of module selected
                //var view = _container.Resolve( , );
                //var region = _regionManager.Regions["MainRegion"];
                ////var moduleSelected = _moduleCatalog.Modules.FirstOrDefault(x => x.ModuleName == tab.Name + "Module");
                //if(region!=null)
                //{
                //    region.Activate(region.Views.FirstOrDefault(v=>v.))
                //}

            }
        }
    }
}
