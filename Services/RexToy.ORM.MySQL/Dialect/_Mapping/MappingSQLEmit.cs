using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect.MySQL
{
    class MappingSQLEmit : AbstractMappingSQLEmit
    {
        public MappingSQLEmit(IObjectMapInfoCache cache, IMappingColumnsBuilder cb, ISQLTranslator tr, IMappingConditionExpressionVisitor cv, IMappingOrderExpressionVisitor ov)
            : base(cache, cb, tr, cv, ov)
        {
        }

        public override string FindIdentity<T>()
        {
            var map = _cache.GetMapInfo(typeof(T), true);
            Assertion.IsTrue(map.PrimaryKeyGenerate == PrimaryKeyGenerate.Auto, "Not auto generate primary key.");

            try
            {
                return string.Format("{0}@@IDENTITY", _tr.Select);
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }

        public override string FindBy<T>(Expression<Func<T, object>> order, int top, OrderType type = OrderType.Asc)
        {
            order.ThrowIfNullArgument(nameof(order));
            var map = _cache.GetMapInfo(typeof(T), true);
            StringBuilder str = new StringBuilder(this.FindBy<T>());
            str.Append(_tr.OrderBy).Append(_ov.Translate(order, map));
            str.Append(type == OrderType.Asc ? _tr.Asc : _tr.Desc);
            str.AppendFormat(" LIMIT 0, {0}", top);
            return str.ToString();
        }

        public override string FindBy<T>(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, int top, OrderType type = OrderType.Asc)
        {
            order.ThrowIfNullArgument(nameof(order));
            where.ThrowIfNullArgument(nameof(where));
            var map = _cache.GetMapInfo(typeof(T), true);
            StringBuilder str = new StringBuilder(this.FindBy(where));
            str.Append(_tr.OrderBy).Append(_ov.Translate(order, map));
            str.Append(type == OrderType.Asc ? _tr.Asc : _tr.Desc);
            str.AppendFormat(" LIMIT 0, {0}", top);
            return str.ToString();
        }
    }
}
