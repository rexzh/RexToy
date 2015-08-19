using System;
using System.Collections.Generic;

namespace RexToy.ORM.Dialect
{
    public abstract class AbstractDatabaseMeta : IDatabaseMeta
    {
        protected ITypeMap _map;
        protected Dictionary<string, List<IColumn>> _dict;
        public AbstractDatabaseMeta(ITypeMap map)
        {
            _map = map;
            _tables = new List<ITable>();
            _dict = new Dictionary<string, List<IColumn>>();
        }

        public void AddTable(string dbName)
        {
            _dict[dbName] = new List<IColumn>();
        }

        public void AddColumn(string tblDbName, string colDbName, string dbType, bool nullable, bool isPrimaryKey, int length)
        {
            if (_dict.ContainsKey(tblDbName))
            {
                var clrName = CLRName(colDbName);
                var clrType = _map.MapDbToClr(dbType);
                var c = new Column(colDbName, clrName, dbType, clrType, isPrimaryKey, nullable, length);
                _dict[tblDbName].Add(c);
            }
        }

        protected abstract string CLRName(string dbName);
        protected abstract ITable BuildTable(string dbName, string clrName, IEnumerable<IColumn> columns);

        public void Build()
        {
            foreach (var kvp in _dict)
            {
                var clrName = CLRName(kvp.Key);
                _tables.Add(BuildTable(kvp.Key, clrName, kvp.Value));
            }
        }

        protected List<ITable> _tables;
        public IEnumerable<ITable> Tables
        {
            get { return _tables; }
        }
    }
}
