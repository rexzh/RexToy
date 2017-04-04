using System;
namespace RexToy.ORM.Dialect
{
    public interface IColumn
    {
        string DbType { get; }
        Type CLRType { get; }
        int Length { get; }
        string DbName { get; }
        string CLRName { get; }
        string InstanceName { get; }
        bool IsPrimaryKey { get; }
        bool IsNullable { get; }
    }
}
