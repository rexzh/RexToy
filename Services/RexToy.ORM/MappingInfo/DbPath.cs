using System;
using System.Collections.Generic;

namespace RexToy.ORM.MappingInfo
{
    class DbPath
    {
        public const string DB_SCHEMA = "db://";

        public static implicit operator DbPath(string databaseId)
        {
            return new DbPath(databaseId);
        }

        private string _databaseId;
        public string DatabaseId
        {
            get { return _databaseId; }
        }

        public DbPath(string databaseId)
        {
            _databaseId = databaseId;
        }
    }
}
