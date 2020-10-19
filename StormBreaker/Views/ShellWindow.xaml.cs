using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
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
        Thread _t;
        static CancellationTokenSource c = new CancellationTokenSource();
        static CancellationToken token = c.Token;
        private event Action CloseSplashWindow = delegate { };

        public ShellWindow(IRegionManager regionManager, IUnityContainer container)
        {
            //var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            //Task reportTask = Task.Factory.StartNew(() =>
            //{
            //    Dispatcher.CurrentDispatcher.InvokeAsync((Action)(() => new SplashWindow().Show()));
            //    Dispatcher.Run();
            //},token);
            _t = new Thread(() => ShowSplashWindow(ref CloseSplashWindow));
            //_t = new Thread((Object token) =>
            //{
            //    var t = (CancellationToken)token;
            //    var s = new SplashWindow();
            //    if (!t.IsCancellationRequested)
            //    {

            //        Dispatcher.CurrentDispatcher.BeginInvoke((Action)(() => s.Show()));
            //        Dispatcher.Run();
            //    }
            //    while(!t.IsCancellationRequested)
            //    {
            //        Thread.Sleep(500);
            //    }
            //    Dispatcher.CurrentDispatcher.BeginInvoke((Action)(() => s.Close()));
            //    Dispatcher.Run();               
                
            //});
            _t.SetApartmentState(ApartmentState.STA);
            _t.IsBackground = true;
            _t.Start();
            //_s.Show();
            InitializeComponent();
            _regionManager = regionManager;
            _container = container;
            this.Loaded += OnLoaded;
        }

        private void ShowSplashWindow(ref Action CloseSplashWindow)
        {
            var s = new SplashWindow();
            Dispatcher.CurrentDispatcher.BeginInvoke((Action)(() => s.Show()));
            CloseSplashWindow += () => s.Dispatcher.BeginInvoke(new ThreadStart(() => s.Close()));
            Dispatcher.Run();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //c.Cancel();
            CloseSplashWindow();
            this.Activate();
            //ShellRibbon.RaiseEvent(new SelectionChangedEventArgs(Ribbon.SelectionChangedEvent,null,null));
            _regionManager.RequestNavigate("MainRegion", "DatasourceManagerMainView");
        }

        //private void CloseSplashWindow(object sender, EventArgs e)
        //{
        //    _s.Close();
        //    _d.Stop();
        //}

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
