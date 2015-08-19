using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect.OleDb
{
    class MappingSQLEmit : AbstractMappingSQLEmit
    {
        public MappingSQLEmit(IObjectMapInfoCache cache, IMappingColumnsBuilder cb, ISQLTranslator tr, IMappingConditionExpressionVisitor v)
            : base(cache, cb, tr, v)
        {
        }

        public override string FindIdentity<T>()
        {
            return "SELECT @@Identity";
        }
    }
}
