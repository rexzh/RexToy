using System;
using System.Collections.Generic;

namespace RexToy.ORM.Dialect
{
    public interface IDatabaseMeta
    {
        IEnumerable<ITable> Tables { get; }
    }
}
