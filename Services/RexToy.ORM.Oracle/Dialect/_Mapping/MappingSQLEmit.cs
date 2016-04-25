using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect.Oracle
{
    class MappingSQLEmit : AbstractMappingSQLEmit
    {
        public MappingSQLEmit(IObjectMapInfoCache cache, IMappingColumnsBuilder cb, ISQLTranslator tr, IMappingConditionExpressionVisitor cv, IMappingOrderExpressionVisitor ov)
            : base(cache, cb, tr, cv, ov)
        {
        }

        public override string FindBy<T>(Expression<Func<T, bool>> where, Expression<Func<T, object>> order, int top, OrderType type = OrderType.Asc)
        {
            string inner = this.FindBy(where, order, type);
            StringBuilder str = new StringBuilder();
            str.Append(_tr.Select).Append("*").Append(_tr.From).AppendFormat("({0})", inner)
                .Append(_tr.Where).AppendFormat("rownum <= {0}", top);
            return str.ToString();
        }

        public override string FindIdentity<T>()
        {
            var map = _cache.GetMapInfo(typeof(T), true);

            try
            {
                StringBuilder b = new StringBuilder();
                string sequenceName = map.PKGenerateString.RemoveBegin(ConstantString.SEQ_Prefix);
                b.Append(_tr.Select).Append(sequenceName).Append(_tr.MemberAccess).Append(ConstantString.SEQ_CurrVal);
                b.Append(_tr.From).Append(ConstantString.Dual);
                return b.ToString();
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }

        public override IPropertyMapInfo GetPrimaryKeyNeedBinding<T>()
        {
            IObjectMapInfo map = _cache.GetMapInfo(typeof(T), true);
            if (map.PrimaryKeyGenerate == PrimaryKeyGenerate.Customized && map.PKGenerateString.StartsWith(ConstantString.SEQ_Prefix))
            {
                if (map.PKStatus == PrimaryKeyStatus.Single)
                    return map.PrimaryKeyMaps.First();
                else
                {
                    GenerateExceptionHelper.ThrowPrimaryKeyDefineNotSupport(typeof(T));
                }
            }

            return null;
        }
    }
}
