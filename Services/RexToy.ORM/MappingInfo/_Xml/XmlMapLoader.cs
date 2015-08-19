using System;
using System.Collections.Generic;

using RexToy.Logging;
using RexToy.Xml;
using RexToy.ORM.Configuration;

namespace RexToy.ORM.MappingInfo
{
    class XmlMapLoader
    {
        private static ILog _log = LogContext.GetLogger<XmlMapLoader>();

        private Dictionary<string, ClrClassPath> _clrDict;
        private Dictionary<string, DbPath> _dbDict;

        private IObjectMapInfoCache _cache;
        private string _file;

        public XmlMapLoader(IObjectMapInfoCache cache, string file)
        {
            cache.ThrowIfNullArgument(nameof(cache));
            file.ThrowIfNullArgument(nameof(file));

            _cache = cache;
            _file = file;

            _clrDict = new Dictionary<string, ClrClassPath>();
            _dbDict = new Dictionary<string, DbPath>();
        }

        public void Load()
        {
            _log.Info("Try load mapping information from [{0}]...", _file);

            XDoc doc = XDoc.LoadFromFile(_file);
            Load(doc);

            _log.Info("Load mapping information from [{0}] success.", _file);
        }

        private void Load(XDoc doc)
        {
            Assertion.AreEqual("ormap", doc.Node.Name, "Root element should be [ormap].");
            foreach (XAccessor x in doc.Attributes)
            {
                Assertion.AreEqual("xmlns", x.Prefix, "Only xmlns attributes allowed in root element.");
                string tag = x.LocalName;
                string target = x.GetStringValue();

                if (target.StartsWith(DbPath.DB_SCHEMA))
                {
                    DbPath dbPath = target.RemoveBegin(DbPath.DB_SCHEMA);
                    Assertion.IsTrue(ORMConfig.ORMConfiguration.ExistDatabase(dbPath.DatabaseId), "Database [{0}] is not defined.", dbPath.DatabaseId);

                    _dbDict.Add(tag, dbPath);
                }
                else if (target.StartsWith(ClrClassPath.CLR_SCHEME))
                {
                    ClrClassPath path = target.RemoveBegin(ClrClassPath.CLR_SCHEME);
                    _clrDict.Add(tag, path);
                }
                else
                {
                    Assertion.Fail("Unknown schema of uri [{0}].", target);
                }
            }

            var mapList = doc.NavigateToList("object-table-map");
            foreach (var xMap in mapList)
            {
                PrefixedName c = new PrefixedName(xMap.GetStringValue("@class"));
                PrefixedName t = new PrefixedName(xMap.GetStringValue("@table"));
                string primaryKey = xMap.GetStringValue("@primaryKey");
                string pkGenerate = xMap.GetStringValue("@primaryKeyGenerate");

                Assertion.IsTrue(_clrDict.ContainsKey(c.Prefix), "Prefix [{0}] is not defined.", c.Prefix);
                Type entityType = _clrDict[c.Prefix].MakeType(c.LocalName);

                if (entityType == null)
                    MappingInfoExceptionHelper.ThrowLoadEntityTypeFail(c.Prefix, c.LocalName);

                var fields = xMap.NavigateToList("field");
                List<IPropertyMapInfo> list = new List<IPropertyMapInfo>();
                foreach (var xField in fields)
                {
                    string property = xField.GetStringValue("@property");
                    string column = xField.GetStringValue("@column");

                    int length = xField.GetValue<int>("@length") ?? 0;
                    bool nullable = xField.GetValue<bool>("@nullable") ?? true;

                    IPropertyMapInfo pInfo = new PropertyMapInfo(property, column, length, nullable);
                    list.Add(pInfo);
                }

                IObjectMapInfo info = new ObjectMapInfo(t, entityType, primaryKey, pkGenerate, list);
                _cache.SetMapInfo(entityType, info);
            }
        }
    }
}
