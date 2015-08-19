using System;

namespace RexToy.ORM.Dialect
{
    //Note:It depend on QueryFunctionExtension, slightly broken the module dependency
    static class ExtensionFunctionNames
    {
        public static readonly string EXT_CLASS = typeof(QueryFunctionExtension).Name;
        public const string FUNC_IN = "In";
        public const string FUNC_LIKE = "Like";

        public const string FUNC_TRUE = "True";
        public const string FUNC_FALSE = "False";
    }
}
