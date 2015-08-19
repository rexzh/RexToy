using System;
using System.Collections.Generic;
using System.Xml;

using RexToy.Collections;

namespace RexToy.Xml
{
    public partial class XAccessor
    {
        public const string XMLNS = "xmlns";

        private XmlNamespaceManager _nsmgr;

        private XmlNode _node;
        public XmlNode Node
        {
            get { return _node; }
        }

        internal XAccessor(XmlNode node, XmlNamespaceManager nsmgr)
        {
            nsmgr.ThrowIfNullArgument(nameof(nsmgr));
            node.ThrowIfNullArgument(nameof(node));

            _node = node;
            _nsmgr = nsmgr;
        }

        internal XAccessor(XmlNode node)
        {
            node.ThrowIfNullArgument(nameof(node));

            _node = node;
            _nsmgr = new XmlNamespaceManager(node.OwnerDocument.NameTable);
        }

        public void AddNamespace(string prefix, string uri)
        {

            prefix.ThrowIfNullArgument(nameof(prefix));
            uri.ThrowIfNullArgument(nameof(uri));

            _nsmgr.AddNamespace(prefix, uri);
        }

        public string Prefix
        {
            get { return _node.Prefix; }
        }

        public string LocalName
        {
            get { return _node.LocalName; }
        }

        public XmlNodeType NodeType
        {
            get { return _node.NodeType; }
        }

        public XAccessor NavigateToSingle(string xpath)
        {
            XAccessor x = NavigateToSingleOrNull(xpath);
            if (x != null)
                return x;
            else
                ExceptionHelper.ThrowNodeNotFoundException(xpath);
            return null;
        }

        public XAccessor NavigateToSingleOrNull(string xpath)
        {
            try
            {
                XmlNode node = _node.SelectSingleNode(xpath, _nsmgr);
                if (node != null)
                    return new XAccessor(node, _nsmgr);
                else
                    return null;
            }
            catch (Exception e)
            {
                throw e.CreateWrapException<XmlAccessException>();
            }
        }

        public IReadOnlyList<XAccessor> NavigateToList(string xpath)
        {
            try
            {
                XmlNodeList list = _node.SelectNodes(xpath, _nsmgr);
                List<XAccessor> xlist = new List<XAccessor>();
                foreach (XmlNode node in list)
                {
                    xlist.Add(new XAccessor(node, _nsmgr));
                }
                return xlist;
            }
            catch (Exception e)
            {
                throw e.CreateWrapException<XmlAccessException>();
            }
        }

        public XAccessor Parent
        {
            get
            {
                if (_node.ParentNode == null)
                    return null;
                else
                    return new XAccessor(_node.ParentNode, _nsmgr);
            }
        }

        public XAccessor NextSibling
        {
            get
            {
                if (_node.NextSibling == null)
                    return null;
                else
                    return new XAccessor(_node.NextSibling, _nsmgr);
            }
        }

        public XAccessor PreviousSibling
        {
            get
            {
                if (_node.PreviousSibling == null)
                    return null;
                else
                    return new XAccessor(_node.PreviousSibling, _nsmgr);
            }
        }

        public IReadOnlyList<XAccessor> Children
        {
            get
            {
                List<XAccessor> xlist = new List<XAccessor>();
                foreach (XmlNode node in _node.ChildNodes)
                    xlist.Add(new XAccessor(node, _nsmgr));
                return xlist;
            }
        }

        public IReadOnlyList<XAccessor> Attributes
        {
            get
            {
                List<XAccessor> xlist = new List<XAccessor>();
                foreach (XmlAttribute attr in _node.Attributes)
                    xlist.Add(new XAccessor(attr, _nsmgr));
                return xlist;
            }
        }

        public bool HasChildNodes
        {
            get { return _node.HasChildNodes; }
        }

        public bool IsComment
        {
            get { return _node.NodeType == XmlNodeType.Comment; }
        }
    }
}