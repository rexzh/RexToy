using System;
using System.Text;

using RexToy.ORM.QueryModel;

namespace RexToy.ORM.Dialect
{
    public interface IJoinExpressionVisitor
    {
        StringBuilder GetJoinEquation(JoinView view);
    }
}
