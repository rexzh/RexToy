using System;
using System.Diagnostics;
using System.Threading;
using System.Diagnostics.CodeAnalysis;

namespace RexToy.DesignPattern
{
    [SuppressMessage("Microsoft.Design", "CA1053")]
    public class Singleton<T>
    {
        protected Singleton()
        {
        }

        private static object _lock = new object();
        private static T _instance;

        [DebuggerStepThrough]
        [SuppressMessage("Microsoft.Design", "CA1005")]
        public static T Instance()
        {
            Assertion.IsFalse(typeof(T).HasPublicConstructor(), "Singleton class {0} should not have public constructor", typeof(T));
            Assertion.IsTrue(typeof(T).HasDefaultConstructor(), "Singleton class {0} should have parameterless constructor", typeof(T));
            Assertion.IsFalse(typeof(T).IsAbstract || typeof(T).IsInterface, "Singleton class {0} should not be abstract or interface", typeof(T));

            if (_instance == null)
            {
                Monitor.Enter(_lock);
                try
                {
                    if (_instance == null)
                    {
                        _instance = (T)Activator.CreateInstance(typeof(T), true);
                    }
                }
                finally
                {
                    Monitor.Exit(_lock);
                }
            }
            return _instance;
        }

        [SuppressMessage("Microsoft.Design", "CA1005")]
        public static void Destroy()
        {
            Monitor.Enter(_lock);
            IDisposable d = _instance as IDisposable;
            if (d != null)
                d.Dispose();
            _instance = default(T);
            Monitor.Exit(_lock);
        }
    }
}
