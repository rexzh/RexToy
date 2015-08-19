using System;
using System.Collections.Generic;
using System.Linq;

using RexToy.Collections;

namespace RexToy.ORM.MappingInfo
{
    class ObjectMapInfo : IObjectMapInfo
    {
        public const char Composite_PK_Delimiter = ';';

        private List<string> _pkList;
        private IEnumerable<IPropertyMapInfo> _fieldList;
        private string _primaryKeyGenerate;
        public ObjectMapInfo(PrefixedName table, Type entityType, string primaryKey, string primaryKeyGenerate, IEnumerable<IPropertyMapInfo> fieldList)
        {
            _table = table;
            _entityType = entityType;
            _primaryKeyGenerate = primaryKeyGenerate;

            string[] pkList = primaryKey.Split(Composite_PK_Delimiter, StringSplitOptions.RemoveEmptyEntries);
            _pkList = new List<string>();
            foreach (string pk in pkList)
                _pkList.Add(pk.Trim());

            _fieldList = fieldList;
        }

        private PrefixedName _table;
        public PrefixedName Table
        {
            get { return _table; }
        }

        private Type _entityType;
        public Type EntityType
        {
            get { return _entityType; }
        }

        public IEnumerable<IPropertyMapInfo> PropertyMaps
        {
            get { return _fieldList; }
        }

        public IEnumerable<IPropertyMapInfo> PrimaryKeyMaps
        {
            get
            {
                return from f in _fieldList
                       where _pkList.Contains(f.PropertyName)
                       select f;
            }
        }

        public IEnumerable<IPropertyMapInfo> NonPKPropertyMaps
        {
            get
            {
                return from f in _fieldList
                       where !_pkList.Contains(f.PropertyName)
                       select f;
            }
        }

        public PrimaryKeyStatus PKStatus
        {
            get
            {
                switch (_pkList.Count)
                {
                    case 0:
                        return PrimaryKeyStatus.None;

                    case 1:
                        return PrimaryKeyStatus.Single;

                    default:
                        return PrimaryKeyStatus.Composite;
                }
            }
        }

        public PrimaryKeyGenerate PrimaryKeyGenerate
        {
            get
            {
                PrimaryKeyGenerate gen;
                if (Enum.TryParse<PrimaryKeyGenerate>(_primaryKeyGenerate, out gen))
                    return gen;
                else
                    return PrimaryKeyGenerate.Customized;
            }
        }

        public string PKGenerateString
        {
            get
            {
                return _primaryKeyGenerate;
            }
        }

        public string GetColumnFromProperty(string propertyName)
        {
            foreach (var map in this.PropertyMaps)
            {
                if (map.PropertyName == propertyName)
                    return map.ColumnName;
            }
            return null;
        }

        public string GetPropertyFromColumn(string columnName)
        {
            foreach (var map in this.PropertyMaps)
            {
                if (map.ColumnName == columnName)
                    return map.PropertyName;
            }
            return null;
        }

        public IPropertyMapInfo GetPropertyMapInfo(string propertyName)
        {
            foreach (var map in this.PropertyMaps)
            {
                if (map.PropertyName == propertyName)
                    return map;
            }
            return null;
        }
    }
}
