using System;
using System.Collections.Generic;

using RexToy.ORM.MappingInfo;

namespace RexToy.ORM.Dialect.MySQL
{
    class MappingColumnsBuilder : AbstractMappingColumnsBuilder
    {
        public MappingColumnsBuilder(ISQLTranslator tr)
            : base(tr)
        {
        }
    }
}
