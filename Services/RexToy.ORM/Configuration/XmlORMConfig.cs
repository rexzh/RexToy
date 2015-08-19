using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using RexToy.Configuration;
using RexToy.Xml;

namespace RexToy.ORM.Configuration
{
    internal class XmlORMConfig : ModuleConfig, IORMConfig
    {
        private const string ORM = "orm";
        private const string DATABASE = "database";
        private const string OBJECT_MAP = "object-map";
        private const char Delimiter = ';';

        private const string DIALECT_PROVIDER_ATTR = "{0}.dialectProvider";
        private const string PROVIDER_ATTR = "{0}.provider";
        private const string DIALECT_ID_ATTR = "{0}.dialectId";
        private const string CNN_STR_ATTR = "{0}.cnnstr";

        private Dictionary<string, IDatabaseInfo> _databases;

        public XmlORMConfig()
        {
            _databases = new Dictionary<string, IDatabaseInfo>();

            Initialize();
        }

        private void Initialize()
        {
            _dbs = GlobalConfig.ReadValue(ORM, DATABASE).Split(Delimiter, StringSplitOptions.RemoveEmptyEntries);

            foreach (var db in _dbs)
            {
                string id = db.Trim();
                if (string.IsNullOrEmpty(id))
                    ConfigExceptionHelper.ThrowDatabaseIdNotDefined();

                string dialectId = GlobalConfig.ReadValue(ORM, string.Format(DIALECT_ID_ATTR, id));
                if (string.IsNullOrEmpty(dialectId))
                    ConfigExceptionHelper.ThrowDialectIdForDatabaseNotDefined(id);
                string cnnstr = GlobalConfig.ReadValue(ORM, string.Format(CNN_STR_ATTR, id));
                string provider = null;
                string dialectProvider = null;
                string providerKey = string.Format(PROVIDER_ATTR, id);
                string providerDialectKey = string.Format(DIALECT_PROVIDER_ATTR, id);
                if (GlobalConfig.ExistsKey(ORM, providerKey))
                    provider = GlobalConfig.ReadValue(ORM, providerKey);
                if (GlobalConfig.ExistsKey(ORM, providerDialectKey))
                    dialectProvider = GlobalConfig.ReadValue(ORM, providerDialectKey);

                _databases.Add(id, new DatabaseInfo(cnnstr, provider, dialectId, dialectProvider));
            }
        }

        #region IORMConfig Members
        private string[] _dbs;
        public IEnumerable<string> GetAllDbIds()
        {
            return _dbs;
        }

        public IDatabaseInfo GetDatabaseInfo(string dbId)
        {
            dbId.ThrowIfNullArgument(nameof(dbId));
            IDatabaseInfo result;
            if (!_databases.TryGetValue(dbId, out result))
            {
                ConfigExceptionHelper.ThrowDatabaseNotDefined(dbId);
                return null;
            }
            else
                return result;
        }

        public bool ExistDatabase(string dbId)
        {
            return _databases.ContainsKey(dbId);
        }

        public int DatabaseCount
        {
            get { return _databases.Count; }
        }

        public string[] GetObjectMapPaths()
        {
            string files = GlobalConfig.ReadValue(ORM, OBJECT_MAP);

            string[] paths;
            if (string.IsNullOrEmpty(files))
                paths = new string[] { };
            else
                paths = files.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < paths.Length; i++)
            {
                string path = Runtime.GetPath(paths[i]);
                if (!File.Exists(path))
                    ConfigExceptionHelper.ThrowMapFileNotExist(path);
                paths[i] = path;
            }

            return paths;
        }

        #endregion
    }
}
