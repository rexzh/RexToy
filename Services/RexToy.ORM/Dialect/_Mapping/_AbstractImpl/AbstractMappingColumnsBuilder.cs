using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect
{
    public class AbstractMappingColumnsBuilder : IMappingColumnsBuilder
    {
        protected ISQLTranslator _tr;
        protected AbstractMappingColumnsBuilder(ISQLTranslator tr)
        {
            tr.ThrowIfNullArgument(nameof(tr));
            _tr = tr;
        }

        #region IColumnsBuilder Members

        public virtual StringBuilder BuildSelectColumns(IObjectMapInfo map)
        {
            StringBuilder b = new StringBuilder();
            foreach (var pmi in map.PropertyMaps)
            {
                b.Append(_tr.GetEscapedColumnName(pmi.ColumnName)).Append(_tr.ColumnDelimiter);
            }
            b.RemoveEnd(_tr.ColumnDelimiter);
            return b;
        }

        public virtual StringBuilder BuildUpdateSets(IObjectMapInfo map, object entity)
        {
            StringBuilder b = new StringBuilder();
            IReflector r = Reflector.Bind(entity);
            foreach (var pmi in map.NonPKPropertyMaps)
            {
                b.Append(_tr.GetEscapedColumnName(pmi.ColumnName));
                b.Append(_tr.Equal).Append(_tr.GetValueString(r.GetPropertyValue(pmi.PropertyName)));
                b.Append(_tr.ColumnDelimiter);
            }
            b.RemoveEnd(_tr.ColumnDelimiter);
            return b;
        }

        public virtual StringBuilder BuildInsertColumns(IObjectMapInfo map, object entity)
        {
            StringBuilder b = new StringBuilder();
            foreach (var pmi in map.PropertyMaps)
            {
                if (map.PrimaryKeyGenerate == PrimaryKeyGenerate.Auto && map.PrimaryKeyMaps.Contains(pmi))
                    continue;

                b.Append(_tr.GetEscapedColumnName(pmi.ColumnName)).Append(_tr.ColumnDelimiter);
            }
            b.RemoveEnd(_tr.ColumnDelimiter);
            return b;
        }

        public virtual StringBuilder BuildInsertValues(IObjectMapInfo map, object entity)
        {
            StringBuilder b = new StringBuilder();
            IReflector r = Reflector.Bind(entity);
            foreach (var pmi in map.PropertyMaps)
            {
                if (map.PrimaryKeyGenerate == PrimaryKeyGenerate.Auto && map.PrimaryKeyMaps.Contains(pmi))
                    continue;

                //Note:special handling Guid when insert, must use '*', not same as WHERE {Guid{*}}
                object val = r.GetPropertyValue(pmi.PropertyName);
                if (val is Guid)
                    b.Append(_tr.GetValueString(val.ToString())).Append(_tr.ColumnDelimiter);
                else
                    b.Append(_tr.GetValueString(val)).Append(_tr.ColumnDelimiter);
            }
            b.RemoveEnd(_tr.ColumnDelimiter);
            return b;
        }

        public virtual StringBuilder BuildWherePrimaryKey(IObjectMapInfo map, object entity)
        {
            StringBuilder b = new StringBuilder();
            IReflector r = Reflector.Bind(entity);
            foreach (var pki in map.PrimaryKeyMaps)
            {
                b.Append(_tr.GetEscapedColumnName(pki.ColumnName));
                object right = r.GetPropertyValue(pki.PropertyName);
                if (right == null)
                    b.Append(_tr.Is);
                else
                    b.Append(_tr.Equal);
                b.Append(_tr.GetValueString(right));
                b.Append(_tr.And);
            }
            b.RemoveEnd(_tr.And);
            return b;
        }

        #endregion
    }
}
