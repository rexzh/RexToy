using System;
using System.Collections.Generic;
using System.Data;

namespace RexToy.ORM
{
    public interface ITransactional
    {
        void BeginTransaction();
        void BeginTransaction(IsolationLevel il);
        void CommitTransaction();
        void RollbackTransaction();
    }
}
