using System;
using System.Transactions;

using RexToy.AOP.Services;

namespace UnitTest.Sample
{
    [Transaction]
    public class ComponentB : ServicedComponent
    {
        public TransactionInformation MethodD()
        {
            var tx = Transaction.Current;
            if (tx != null)
                return tx.TransactionInformation;
            else
                return null;
        }

        [AutoComplete]
        public TransactionInformation MethodE()
        {
            var tx = Transaction.Current;
            if (tx != null)
                return tx.TransactionInformation;
            else
                return null;
        }
    }
}
