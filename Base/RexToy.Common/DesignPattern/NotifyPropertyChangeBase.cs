using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace RexToy.DesignPattern
{
    public abstract class NotifyPropertyChangeBase : INotifyPropertyChanged
    {
        private static MemberInfo CaptureMemberInfo<T>(Expression<Func<T, object>> expr)
        {
            MemberExpression me = null;
            switch (expr.Body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    me = expr.Body as MemberExpression;
                    break;

                case ExpressionType.Convert:
                    UnaryExpression ue = expr.Body as UnaryExpression;
                    me = ue.Operand as MemberExpression;
                    break;

                default:
                    Assertion.Fail("Only simple expression supported.");
                    break;
            }

            return me.Member;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChange(string name)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(name);
                handler(this, e);
            }
        }

        protected virtual void OnPropertyChange<T>(Expression<Func<T, object>> expr)
        {
            string propertyName = CaptureMemberInfo(expr).Name;
            OnPropertyChange(propertyName);
        }
    }
}
