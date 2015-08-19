using System;
using System.Collections.Generic;

namespace RexToy.ORM.Configuration
{
    public interface IORMConfig
    {
        int DatabaseCount { get; }

        IEnumerable<string> GetAllDbIds();        
        IDatabaseInfo GetDatabaseInfo(string dbId);
        bool ExistDatabase(string dbId);
        string[] GetObjectMapPaths();
    }
}
