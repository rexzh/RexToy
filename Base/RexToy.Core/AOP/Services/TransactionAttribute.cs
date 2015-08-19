using System;

using SysIsolationLevel = System.Transactions.IsolationLevel;

namespace RexToy.AOP.Services
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class TransactionAttribute : Attribute
    {
        private IsolationLevel _isolationLevel;
        public SysIsolationLevel TxIsolationLevel
        {
            get
            {
                switch (_isolationLevel)
                {
                    case IsolationLevel.ReadUncommitted:
                        return System.Transactions.IsolationLevel.ReadUncommitted;

                    case IsolationLevel.ReadCommitted:
                        return System.Transactions.IsolationLevel.ReadCommitted;

                    case IsolationLevel.RepeatableRead:
                        return System.Transactions.IsolationLevel.RepeatableRead;

                    case IsolationLevel.Serializable:
                        return System.Transactions.IsolationLevel.Serializable;

                    default:
                        return System.Transactions.IsolationLevel.Unspecified;
                }
            }
        }

        private TransactionOption _option;
        public System.Transactions.TransactionScopeOption Option
        {
            get
            {
                switch (_option)
                {
                    case TransactionOption.Required:
                        return System.Transactions.TransactionScopeOption.Required;

                    case TransactionOption.RequiresNew:
                        return System.Transactions.TransactionScopeOption.RequiresNew;

                    default:
                        return System.Transactions.TransactionScopeOption.Suppress;
                }
            }
        }

        public TransactionAttribute(TransactionOption option = TransactionOption.Required, IsolationLevel il = IsolationLevel.ReadCommitted)
        {
            _option = option;
            _isolationLevel = il;
        }
    }
}
