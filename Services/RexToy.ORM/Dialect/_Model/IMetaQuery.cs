using System;
using System.Collections.Generic;

namespace RexToy.ORM.Dialect
{
    public interface IMetaQuery
    {
        IDatabaseMeta ReadMetaInfo();
    }
}
