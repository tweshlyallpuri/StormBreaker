using Prism.Regions;
using StormBreaker.Modules.DatasourceManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace StormBreaker.Modules.DatasourceManager.Views
{
    /// <summary>
    /// Interaction logic for DataSourceManagerMainView.xaml
    /// </summary>
    public partial class DatasourceManagerMainView : UserControl, INavigationAware
    {


        public List<Datasource> Datasources { get; set; }


        public DatasourceManagerMainView()
        {
            Thread.Sleep(1000);
            InitializeComponent();
            //DataTree1.DataContext = Process.GetProcesses().Where(p => p.ProcessName.StartsWith("chr"));
            var categories = XDocument.Load("DataSources.xml").Root.Elements();//.Select(row => new Datasource(row.Element("Name").Value, new Da));
            List<Datasource> dsList = new List<Datasource>();
            foreach(var category in categories)
            {
                //var x = ds.Element("ServerInfo");
                //ServerInfo serverInfo = new ServerInfo(x.Element("HostName").Value, x.Element("AliasName").Value, x.Element("Role").Value, x.Element("Port").Value, x.Element("DatabaseName").Value, x.Element("Provider").Value, x.Element("ExtraInfo").Value, x.Element("Environment").Value);
                var y = category.Elements("DsInfo");
                List<DsInfo> dsInfos = new List<DsInfo>();

                foreach(var datasource in y)
                {
                    var z = datasource.Elements("Credential");
                    List<Credential> credentials = new List<Credential>();

                    foreach (var credential in z)
                    {
                        var cred = new Credential(credential.Element("Username").Value, credential.Element("Password").Value, credential.Element("AdditionalField1").Value, credential.Element("Description").Value);
                        credentials.Add(cred);
                    }
                    var dsInfo = new DsInfo(datasource.Element("HostName").Value, datasource.Element("AliasName").Value, datasource.Element("Port").Value, datasource.Element("DatabaseName").Value, datasource.Element("ExtraInfo").Value, credentials.ToArray());
                    dsInfos.Add(dsInfo);
                }
                dsList.Add(new Datasource(category.Element("Name").Value, dsInfos));
            }
            Datasources = dsList.ToList();
            DataTree.DataContext = Datasources;
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

        private void OnDataTreeSelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is Datasource d)
            {
                Datasource dataSource = d;
                string content = d.CategoryName + ": \n";
                foreach (var x in d.DsInfos)
                {
                    content += x.AliasName + "\n";
                }
                TextBlock.Text = content;

            }
            else if (e.NewValue is DsInfo ds)
            {
                DsInfo dsInfo = ds;
                string content = "AliasName : " + dsInfo.AliasName + "\n";
                content += "HostName : " + dsInfo.HostName + "\n";
                content += "Port : " + dsInfo.Port + "\n";
                content += "DatabaseName : " + dsInfo.DatabaseName + "\n";
                content += "ExtraInfo : " + dsInfo.ExtraInfo + "\n";
                string credentials = "";
                bool isFirst = true;
                foreach (var credential in dsInfo.Credentials)
                {
                    if (isFirst)
                    {
                        credentials += "-----------------Credentials---------------" + "\n";
                        isFirst = false;
                    }
                    credentials += "Username : " + credential.Username + "\n";
                    credentials += "Password : " + credential.Password + "\n";
                    credentials += "Description : " + credential.Description + "\n";
                    credentials += "AdditionalField : " + credential.AdditionalField1 + "\n";
                    credentials += "-------------------------------------------" + "\n";
                }
                content += credentials;
                TextBlock.Text = content;
            }
            else
                TextBlock.Text = "";
        }

        private void FilterTextChanged(object sender, TextChangedEventArgs e)
        {
            string s = FilterTextBox.Text;
            if (string.IsNullOrWhiteSpace(s))
                DataTree.DataContext = Datasources;
            List<Datasource> newList = new List<Datasource>();
            foreach(var x in Datasources)
            {
                List<DsInfo> dsInfos = new List<DsInfo>();
                foreach(var y in x.DsInfos)
                {
                    if (y.AliasName.Contains(s, StringComparison.OrdinalIgnoreCase) || y.DatabaseName.Contains(s, StringComparison.OrdinalIgnoreCase) || y.ExtraInfo.Contains(s, StringComparison.OrdinalIgnoreCase) || y.HostName.Contains(s, StringComparison.OrdinalIgnoreCase) || y.Port.Contains(s, StringComparison.OrdinalIgnoreCase))
                        dsInfos.Add(y);
                }
                if (dsInfos.Count > 0)
                    newList.Add(new Datasource(x.CategoryName, dsInfos));
            }
            DataTree.DataContext = newList;
        }
    }
}
