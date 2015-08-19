using System;

namespace RexToy.ORM.Configuration
{
    public interface IDatabaseInfo
    {
        string ConnectString { get; }
        string DialectId { get; }
        string DialectProvider { get; set; }
        string Provider { get; set; }
    }
}
