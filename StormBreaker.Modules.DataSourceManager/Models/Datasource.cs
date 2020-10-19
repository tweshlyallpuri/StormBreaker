using System.Collections.Generic;
using System.Net;

namespace StormBreaker.Modules.DatasourceManager.Models
{
    public class Datasource
    {
        public Datasource(string categoryName, List<DsInfo> dsInfos)
        {
            CategoryName = categoryName;
            DsInfos = dsInfos;
        }

        public string CategoryName { get; private set; }
        public List<DsInfo> DsInfos { get; private set; }
        }
    public class DsInfo
    {
        public DsInfo(string hostName, string aliasName, string port, string databaseName, string extraInfo, Credential[] credentials)
        {
            HostName = hostName;
            AliasName = aliasName;
            Port = port;
            DatabaseName = databaseName;
            ExtraInfo = extraInfo;
            Credentials = credentials;
        }

        public string HostName { get; private set; }
        public string AliasName { get; private set; }
        public string Port { get; private set; }
        public string DatabaseName { get; private set; }
        public string ExtraInfo { get; private set; }
        public Credential[] Credentials { get; private set; }
    }

    public class Credential
    {
        public Credential(string username, string password, string additionalField1, string description)
        {
            Username = username;
            Password = password;
            AdditionalField1 = additionalField1;
            Description = description;
        }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string AdditionalField1 { get; private set; }
        public string Description { get; private set; }
    }
}
