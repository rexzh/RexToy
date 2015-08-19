using System;
using System.Transactions;

using RexToy.AOP.Services;

namespace UnitTest.Sample
{
    [Transaction(TransactionOption.Required)]
    public class ComponentA : ServicedComponent
    {
        [AutoComplete]
        public TransactionInformation MethodA()
        {
            Transaction tx = Transaction.Current;
            if (tx != null)
                return tx.TransactionInformation;
            else
                return null;
        }

        public TransactionInformation MethodB()
        {
            Transaction tx = Transaction.Current;
            if (tx != null)
                return tx.TransactionInformation;
            else
                return null;
        }

        [AutoComplete]
        public TransactionInformation MethodC()
        {
            Transaction tx = Transaction.Current;
            TransactionInformation ti = (tx != null) ? tx.TransactionInformation : null;

            ComponentB b = new ComponentB();
            TransactionInformation tiInner = b.MethodE();

            if (ti.CreationTime == tiInner.CreationTime)
                return ti;
            else
                return null;
        }
    }
}
