using Prism.Regions;
using StormBreaker.Modules.DatasourceManager.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Xml.Linq;

namespace StormBreaker.Modules.DatasourceManager.Views
{
    /// <summary>
    /// Interaction logic for DataSourceManagerMainView.xaml
    /// </summary>
    public partial class DatasourceManagerMainView : UserControl, INavigationAware
    {
        public DatasourceManagerMainView()
        {
            InitializeComponent();
            //DataTree1.DataContext = Process.GetProcesses().Where(p => p.ProcessName.StartsWith("chr"));
            var datasources = XDocument.Load("test.xml").Root.Elements();//.Select(row => new Datasource(row.Element("Name").Value, new Da));
            List<Datasource> dsList = new List<Datasource>();
            foreach(var ds in datasources)
            {
                var x = ds.Element("ServerInfo");
                ServerInfo serverInfo = new ServerInfo(x.Element("HostName").Value, x.Element("AliasName").Value, x.Element("Role").Value, x.Element("Port").Value, x.Element("DatabaseName").Value, x.Element("Provider").Value, x.Element("ExtraInfo").Value, x.Element("Environment").Value);
                var y = ds.Elements("Credential");
                List<Credential> credentials = new List<Credential>();

                foreach(var credential in y)
                {
                    var cred = new Credential(credential.Element("Username").Value, credential.Element("Password").Value, credential.Element("AdditionalField1").Value, credential.Element("Description").Value);
                    credentials.Add(cred);
                }
                dsList.Add(new Datasource(ds.Element("Name").Value, serverInfo, credentials.ToArray()));
            }

            DataTree.DataContext = dsList.Where(x=>true);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }
    }
}
