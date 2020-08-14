using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace StormBreaker.Modules.DatasourceManager
{
    class XmlHelper
    {
        public XmlHelper()
        {
            XmlReader r = XmlReader.Create(@"D:\Dev\GitHub\StormBreaker\StormBreaker.Modules.DataSourceManager\Resources\DataSources.xml");
            while (r.NodeType != XmlNodeType.Element)
                r.Read();
            XElement e = XElement.Load(r);
        }
        public void CreateRandomXml()
        {

        }
    }
}
