using System;
using System.Linq.Expressions;

namespace RexToy.ORM.QueryModel
{
    public interface IQuery
    {
        Criteria Criteria { get; }
        View View { get; }
        OrderCollection Order { get; }
    }
}
