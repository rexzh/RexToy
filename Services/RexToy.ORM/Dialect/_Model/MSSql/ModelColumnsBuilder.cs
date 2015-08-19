using System;
using System.Collections.Generic;
using System.Text;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect.MSSql
{
    class ModelColumnsBuilder : AbstractModelColumnsBuilder
    {
        public ModelColumnsBuilder(ISQLTranslator tr, ITypeMap tm)
            : base(tr, tm)
        {
        }

        protected override void BuildCreateTableColumns()
        {
            IReflector r = Reflector.Bind(_map.EntityType, ReflectorPolicy.CreateInstance(false, false, false, false));

            foreach (var pkInfo in _map.PrimaryKeyMaps)
            {
                BuildColumnBasic(r, pkInfo, true);
                //Note:GUID is not supported
                if (_map.PrimaryKeyGenerate == PrimaryKeyGenerate.Auto)
                    _str.Append("IDENTITY(1, 1) ");
                _str.Append(_tr.Not).Append(_tr.Null).Append(_tr.ColumnDelimiter);
            }

            foreach (var nonPkInfo in _map.NonPKPropertyMaps)
            {
                BuildColumnBasic(r, nonPkInfo, false);
                _str.Append(_tr.ColumnDelimiter);
            }

            _str.Append(_tr.Constraint).Append(_tr.GetEscapedColumnName(PK_PREFIX + _map.Table.LocalName)).Append(_tr.PrimaryKey);
            _str.Append(StringPair.Parenthesis.Begin);
            foreach (var pkInfo in _map.PrimaryKeyMaps)
            {
                _str.Append(_tr.GetEscapedColumnName(pkInfo.ColumnName)).Append(_tr.ColumnDelimiter);
            }
            _str.RemoveEnd(_tr.ColumnDelimiter);
            _str.Append(StringPair.Parenthesis.End);
        }

        private void BuildColumnBasic(IReflector r, IPropertyMapInfo propertyMapInfo, bool pk)
        {
            Type propType = r.GetPropertyType(propertyMapInfo.PropertyName);
            _str.Append(_tr.GetEscapedColumnName(propertyMapInfo.ColumnName)).Append(_tm.MapClrToDb(propType));

            if (propType == typeof(string) || propType == typeof(char) || propType == typeof(char?))
            {
                int length = (propertyMapInfo.Length == 0) ? DEFAULT_COL_LENGTH : propertyMapInfo.Length;
                _str.Append(length.ToString().Bracketing(StringPair.Parenthesis)).Append(SPACE);
            }

            if (!pk)
            {
                bool nullable = (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>));
                _str.Append(nullable ? _tr.Null : _tr.Not + _tr.Null);
            }
        }
    }
}
