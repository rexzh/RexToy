using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect.MSSql
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
                return string.Format("{0}Ident_Current('{1}')", _tr.Select, _tr.GetEscapedTableName(map.Table.LocalName));
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<SQLGenerateException>();
            }
        }
    }
}
