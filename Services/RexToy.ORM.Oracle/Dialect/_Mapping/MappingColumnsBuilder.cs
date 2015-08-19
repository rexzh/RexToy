using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect.Oracle
{
    class MappingColumnsBuilder : AbstractMappingColumnsBuilder
    {
        internal MappingColumnsBuilder(ISQLTranslator tr)
            : base(tr)
        {
        }

        public override StringBuilder BuildInsertColumns(MappingInfo.IObjectMapInfo map, object entity)
        {
            StringBuilder b = new StringBuilder();
            foreach (var pmi in map.PropertyMaps)
            {
                b.Append(_tr.GetEscapedColumnName(pmi.ColumnName)).Append(_tr.ColumnDelimiter);
            }
            b.RemoveEnd(_tr.ColumnDelimiter);
            return b;
        }

        public override StringBuilder BuildInsertValues(MappingInfo.IObjectMapInfo map, object entity)
        {
            StringBuilder b = new StringBuilder();
            IReflector r = Reflector.Bind(entity);
            foreach (var pmi in map.PropertyMaps)
            {
                if (map.PrimaryKeyGenerate == PrimaryKeyGenerate.Customized && map.PrimaryKeyMaps.Contains(pmi))
                {
                    string sequenceName = map.PKGenerateString.RemoveBegin(ConstantString.SEQ_Prefix);
                    b.Append(sequenceName).Append(_tr.MemberAccess).Append(ConstantString.SEQ_NextVal).Append(_tr.ColumnDelimiter);
                    continue;
                }

                b.Append(_tr.GetValueString(r.GetPropertyValue(pmi.PropertyName))).Append(_tr.ColumnDelimiter);
            }
            b.RemoveEnd(_tr.ColumnDelimiter);
            return b;
        }
    }
}
