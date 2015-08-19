using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace RexToy.Xml
{
    public class XDoc : XAccessor
    {
        protected XDoc(XmlNode node, XmlNamespaceManager nsmgr)
            : base(node, nsmgr)
        {
        }

        public static XDoc LoadFromFile(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);

            return new XDoc(doc.DocumentElement, nsmgr);
        }

        public static XDoc LoadFromString(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);

            return new XDoc(doc.DocumentElement, nsmgr);
        }

        public static XDoc LoadFromStream(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(reader.ReadToEnd());
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
                return new XDoc(doc.DocumentElement, nsmgr);
            }
        }
    }
}
