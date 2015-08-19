using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect.OleDb
{
    class MappingColumnsBuilder : AbstractMappingColumnsBuilder
    {
        public MappingColumnsBuilder(ISQLTranslator tr)
            : base(tr)
        {
        }
    }
}
