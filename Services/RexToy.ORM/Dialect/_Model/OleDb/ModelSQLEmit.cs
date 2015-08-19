using System;
using System.Collections.Generic;
using System.Text;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect.OleDb
{
    class ModelSQLEmit : AbstractModelSQLEmit
    {
        public ModelSQLEmit(IObjectMapInfoCache cache, ISQLTranslator tr, ITypeMap tm, IModelColumnsBuilder cb)
            : base(cache, tr, tm, cb)
        {
        }

        public override string CreateTable<T>()
        {
            var map = _cache.GetMapInfo(typeof(T), true);
            StringBuilder str = new StringBuilder();
            str.Append(_tr.CreateTable).Append(_tr.GetEscapedTableName(map.Table.LocalName));
            str.Append(_cb.BuildCreateTableColumns(map).Bracketing(StringPair.Parenthesis));
            return str.ToString();
        }
    }
}
