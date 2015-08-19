using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Collections;
using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public interface IQueryColumnBuilder
    {
        StringBuilder BuildSelectColumns(View view);
    }
}
