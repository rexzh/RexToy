using System;
using System.Collections.Generic;

using RexToy;

namespace RexToy.ORM.Dialect
{
    public abstract class AbstractTable : ITable
    {
        public AbstractTable(string dbName, string clrName, IEnumerable<IColumn> columns)
        {
            dbName.ThrowIfNullArgument(nameof(dbName));
            columns.ThrowIfNullArgument(nameof(columns));
            clrName.ThrowIfNullArgument(nameof(clrName));

            _dbName = dbName;
            _clrName = clrName;
            _columns = columns;

            //Note: Normally pk will only have one column, so not use StringBuilder here.
            _pk = string.Empty;
            foreach (var c in _columns)
            {
                if (c.IsPrimaryKey)
                    _pk += c.CLRName + ';';
            }
            _pk = _pk.RemoveEnd(';');
        }

        public abstract string PKGenerate { get; }

        protected IEnumerable<IColumn> _columns;
        public IEnumerable<IColumn> Columns
        {
            get { return _columns; }
        }

        protected string _dbName;
        public string DbName
        {
            get { return _dbName; }
        }

        protected string _clrName;
        public string CLRName
        {
            get
            {
                return CamelCase(_clrName);
            }
        }

        public string InstanceName
        {
            get
            {
                string name = CamelCase(_clrName);
                return name.Substring(0, 1).ToLower() + name.Substring(1);
            }
        }

        protected string _pk;
        public string PrimaryKey
        {
            get { return _pk; }
        }

        private string CamelCase(string name)
        {
            string[] segs = name.Split('_', StringSplitOptions.RemoveEmptyEntries);
            string result = string.Empty;
            foreach (string seg in segs)
            {
                result += seg.Substring(0, 1).ToUpper() + seg.Substring(1).ToLower();
            }
            return result;
        }
    }
}
