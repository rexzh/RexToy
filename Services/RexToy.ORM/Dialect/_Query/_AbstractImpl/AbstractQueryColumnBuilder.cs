using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Collections;
using RexToy.ORM.MappingInfo;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public abstract class AbstractQueryColumnBuilder : IQueryColumnBuilder
    {
        protected StringBuilder _str;
        protected ISQLTranslator _tr;
        protected IObjectMapInfoCache _cache;
        protected AbstractQueryColumnBuilder(ISQLTranslator tr, IObjectMapInfoCache cache)
        {
            cache.ThrowIfNullArgument(nameof(cache));
            tr.ThrowIfNullArgument(nameof(tr));

            _tr = tr;
            _cache = cache;
            _str = new StringBuilder();
        }

        public virtual StringBuilder BuildSelectColumns(View view)
        {
            Visit(view);
            return _str.RemoveEnd(_tr.ColumnDelimiter);
        }

        private void Visit(View view)
        {
            switch (view.ViewType)
            {
                case ViewType.Join:
                    Visit((JoinView)view);
                    break;

                case ViewType.Single:
                    Visit((SingleEntityView)view);
                    break;
            }
        }

        private void Visit(JoinView j)
        {
            Visit(j.Left);
            Visit(j.Right);
        }

        private void Visit(SingleEntityView s)
        {
            var map = _cache.GetMapInfo(s.EntityType, true);

            string prefix = s.Alias ?? map.Table.LocalName;
            foreach (var pMap in map.PropertyMaps)
            {
                _str.Append(_tr.GetEscapedTableName(prefix)).Append(_tr.MemberAccess).Append(_tr.GetEscapedColumnName(pMap.ColumnName))
                    .Append(_tr.As).Append(_tr.GetEscapedColumnName(prefix + pMap.ColumnName)).Append(_tr.ColumnDelimiter);
            }
        }
    }
}
