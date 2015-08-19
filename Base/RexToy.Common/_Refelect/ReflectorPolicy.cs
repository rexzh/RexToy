using System;
using System.Diagnostics;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

namespace RexToy
{
    public class ReflectorPolicy : IReflectPolicy
    {
        [DebuggerStepThrough]
        public static IReflectPolicy CreateInstance(bool includeNonPublic = false, bool includeStatic = false, bool ignoreCase = false, bool bindInstance = true)
        {
            return new ReflectorPolicy(includeNonPublic, includeStatic, ignoreCase, bindInstance);
        }

        [DebuggerStepThrough]
        private ReflectorPolicy(bool includeNonPublic, bool includeStatic, bool ignoreCase, bool bindInstance)
        {
            _includeStatic = includeStatic;
            _bindInstance = bindInstance;
            _includeNonPublic = includeNonPublic;
            _ignoreCase = ignoreCase;
        }

        #region IReflectPolicy Members
        private bool _includeNonPublic;
        public bool IncludeNonPublic
        {
            get { return _includeNonPublic; }
        }

        private bool _includeStatic;
        public bool IncludeStatic
        {
            get { return _includeStatic; }
        }

        private bool _ignoreCase;
        public bool IgnoreCase
        {
            get { return _ignoreCase; }
        }

        private bool _bindInstance;
        public bool BindInstance
        {
            get { return _bindInstance; }
        }

        public BindingFlags BindingFlags
        {
            [DebuggerStepThrough]
            get
            {
                BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.OptionalParamBinding;
                if (_includeNonPublic)
                    flag |= BindingFlags.NonPublic;
                if (_ignoreCase)
                    flag |= BindingFlags.IgnoreCase;
                if (_includeStatic)
                    flag |= BindingFlags.Static;
                return flag;
            }
        }

        #endregion

        [SuppressMessage("Microsoft.Security", "CA2104")]
        public static readonly IReflectPolicy InstancePublic = new ReflectorPolicy(false, false, false, true);

        [SuppressMessage("Microsoft.Security", "CA2104")]
        public static readonly IReflectPolicy InstancePublicIgnoreCase = new ReflectorPolicy(false, false, true, true);

        [SuppressMessage("Microsoft.Security", "CA2104")]
        public static readonly IReflectPolicy InstanceAll = new ReflectorPolicy(true, false, false, true);

        [SuppressMessage("Microsoft.Security", "CA2104")]
        public static readonly IReflectPolicy InstanceAllIgnoreCase = new ReflectorPolicy(true, false, true, true);

        [SuppressMessage("Microsoft.Security", "CA2104")]
        public static readonly IReflectPolicy TypePublic = new ReflectorPolicy(false, true, false, false);

        [SuppressMessage("Microsoft.Security", "CA2104")]
        public static readonly IReflectPolicy TypePublicIgnoreCase = new ReflectorPolicy(false, true, true, false);

        [SuppressMessage("Microsoft.Security", "CA2104")]
        public static readonly IReflectPolicy TypeAll = new ReflectorPolicy(true, true, false, false);

        [SuppressMessage("Microsoft.Security", "CA2104")]
        public static readonly IReflectPolicy TypeAllIgnoreCase = new ReflectorPolicy(true, true, true, false);
    }
}
