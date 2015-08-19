using System;
using System.Collections.Generic;

namespace RexToy.ORM.Dialect
{
    public class Column : IColumn
    {
        public Column(string dbName, string clrName, string dbType, Type clrType, bool isPrimaryKey, bool isNullable, int length)
        {
            _dbName = dbName;
            _clrName = clrName;
            _dbType = dbType;
            _clrType = clrType;
            _length = length;
            _isPrimaryKey = isPrimaryKey;
            _isNullable = isNullable;
        }

        protected string _dbName;
        public string DbName
        {
            get { return _dbName; }
        }

        protected string _clrName;
        public string CLRName
        {
            get { return _clrName; }
        }

        protected string _dbType;
        public string DbType
        {
            get { return _dbType; }
        }

        protected Type _clrType;
        public Type CLRType
        {
            get { return _clrType; }
        }

        protected int _length;
        public int Length
        {
            get { return _length; }
        }

        protected bool _isPrimaryKey;
        public bool IsPrimaryKey
        {
            get { return _isPrimaryKey; }
        }

        protected bool _isNullable;
        public bool IsNullable
        {
            get { return _isNullable; }
        }
    }
}
