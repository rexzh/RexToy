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
    }
}
