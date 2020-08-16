using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace StormBreaker.Modules.DatasourceManager
{
    public enum DataSourceElements
    {

    }
    class XmlHelper
    {
        public XmlHelper()
        {
            //XmlReader r = XmlReader.Create();
            //while (r.NodeType != XmlNodeType.Element)
            //    r.Read();
            XElement e = XElement.Load("test.xml");
        }
        public void CreateRandomXml()
        {
            XDocument myDoc = new XDocument(
                //new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Datasources"));
            var r = new Random();
            for (int i = 0; i <= 25; i++)
            {

                myDoc.Root.Add(
                    new XElement("Datasource",
                    new XElement("Name","DS" + i),
                        new XElement("ServerInfo",
                            new XElement("HostName", "Patrick Hines" + i),
                            new XElement("AliasName", "206-555-0144" + i),
                            new XElement("Role", "asdasd" + i),
                            new XElement("Port", "123 Main St"),
                            new XElement("DatabaseName", "Mercer Island" + i),
                            new XElement("Provider", "WA"),
                            new XElement("ExtraInfo", "68042"),
                            new XElement("Environment", "Prod")),
                        new XElement("Credential",
                        new XElement("Username", "Patrick Hines" + i),
                        new XElement("Password", "pwd" + i),
                        new XElement("AdditionalField1", "blah"),
                        new XElement("Description", "gggg"))));
            }
            //myDoc.Save("test.xml");
        }

    }
}
