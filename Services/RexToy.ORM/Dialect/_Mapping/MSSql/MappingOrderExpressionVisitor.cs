using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexToy.ORM.Dialect.MSSql
{
    class MappingOrderExpressionVisitor : AbstractMappingOrderExpressionVisitor
    {
        internal MappingOrderExpressionVisitor(ISQLTranslator tr) : base(tr)
        {

        }
    }
}
