using System.Collections.Generic;

namespace StormBreaker.Modules.DatasourceManager.Models
{
    public class Datasource
    {
        public Datasource(string name, ServerInfo serverInformation, Credential[] credentials)
        {
            Name = name;
            ServerInformation = serverInformation;
            Credentials = credentials;
        }

        public string Name { get; private set; }
        public ServerInfo ServerInformation { get; private set; }
        public Credential[] Credentials { get; private set; }
        }
    public class ServerInfo
    {
        public ServerInfo(string hostName, string aliasName, string role, string port, string databaseName, string provider, string extraInfo, string environment)
        {
            HostName = hostName;
            AliasName = aliasName;
            Role = role;
            Port = port;
            DatabaseName = databaseName;
            Provider = provider;
            ExtraInfo = extraInfo;
            Environment = environment;
        }

        public string HostName { get; private set; }
        public string AliasName { get; private set; }
        public string Role { get; private set; }
        public string Port { get; private set; }
        public string DatabaseName { get; private set; }
        public string Provider { get; private set; }
        public string ExtraInfo { get; private set; }
        public string Environment { get; private set; }
    }

    public class Credential
    {
        public Credential(string username, string password, string additionalField1, string description)
        {
            Username = username;
            Password = password;
            AdditionalField1 = additionalField1;
            Description = description;
            AnotherCollection = new List<ServerInfo> { new ServerInfo("host", "aloas", "role", "pport", "databasenmae", "provider", "extra", "env"),
            new ServerInfo("host2", "aloas", "role", "pport", "databasenmae", "provider", "extra", "env")}.ToArray();
        }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string AdditionalField1 { get; private set; }
        public string Description { get; private set; }
        public ServerInfo[] AnotherCollection { get; private set; }
    }
}
