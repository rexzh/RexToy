using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

using RexToy.Collections;
using RexToy.Logging;
using RexToy.Configuration;
using RexToy.Xml;

namespace RexToy.IoC
{
    public sealed class Kernal : IKernal
    {
        private const string COMPONENT = "component";

        private static ILog _log = LogContext.GetLogger<Kernal>();

        private IComponentInfoStore _store;
        private IObjectBuilder _b;
        private ComponentInfoFactory _factory;
        private ConcurrentDictionary<object, IObjectBuildContext> _lifecycleContainer;

        public Kernal(string id)
        {
            _log.Info("Kernal [{0}] start up.", id);
            _id = id;
            _b = new ObjectBuilder(this);
            _store = new ComponentInfoStore();
            _lifecycleContainer = new ConcurrentDictionary<object, IObjectBuildContext>();            

            Initialize();
            _log.Info("Kernal [{0}] successfully initialized.", id);
        }

        private void Initialize()
        {
            var cfg = KernalConfig.KernalConfiguration;
            _factory = new ComponentInfoFactory(cfg);
            string path = cfg.LoadComponentInfoPath(_id);

            XDoc doc = XDoc.LoadFromFile(path);
            var xComponents = doc.NavigateToList(COMPONENT);
            foreach (var x in xComponents)
            {
                IComponentInfo info = _factory.CreateInfo(x);
                _store.AddComponentInfo(info);
            }
        }

        #region IKernal Members

        private string _id;
        public string Id
        {
            get { return _id; }
        }

        public bool ReadyToBuild(string id)
        {
            _log.Debug("Check [{0}] is ready.", id);
            var info = _store.FindById(id);
            return _b.ReadyToBuild(info);
        }

        public bool ReadyToBuild(Type serviceType)
        {
            _log.Debug("Check [{0}] is ready.", serviceType.Name);
            var info = _store.FindByContract(serviceType);
            return _b.ReadyToBuild(info);
        }

        public T Lookup<T>()
        {
            object result = Lookup(typeof(T));
            return (T)result;
        }

        public T Lookup<T>(string id)
        {
            object result = Lookup(id);
            return (T)result;
        }

        public object Lookup(Type serviceType)
        {
            var info = _store.FindByContract(serviceType);
            return Lookup(info);
        }

        public object Lookup(string id)
        {
            var info = _store.FindById(id);
            return Lookup(info);
        }

        [SuppressMessage("Microsoft.Design", "CA1031")]
        private object Lookup(IComponentInfo info)
        {
            _log.Info("Start build object [{0}].", info.Id);

            try
            {
                IObjectBuildContext ctx = _b.Build(info);
                if (ctx.LifeCycleManagement)
                {
                    _lifecycleContainer[ctx.Instance] = ctx;
                }
                _log.Info("Build object [{0}] success.", info.Id);
                return ctx.Instance;
            }
            catch (ObjectBuilderException)
            {
                _log.Warning("Build object [{0}] fail.", info.Id);
                throw;
            }
            catch (ManifestException)
            {
                _log.Warning("Build object [{0}] fail.", info.Id);
                throw;
            }
            catch (Exception ex)
            {
                _log.Warning("Build object [{0}] fail.", info.Id);
                ex.CreateWrapException<ObjectBuilderException>();
                return null;
            }
        }

        public void TearDown(object instance)
        {
            _log.Debug("Teardown instance of type [{0}]", instance.GetType().FullName);
            IObjectBuildContext ctx;
            if (_lifecycleContainer.TryRemove(instance, out ctx))
            {
                _log.Debug("[{0}] is going to teardown.", instance);
                _b.TearDown(instance, ctx);
            }
            else
            {
                _log.Debug("[{0}] is going to dispose.", instance);
                IDisposable d = instance as IDisposable;
                if (d != null)
                    d.Dispose();
            }
        }

        public void Register<TService, TComponent>(string id, IList<IPolicy> policies = null, bool lifecycleManagement = false)
        {
            ComponentInfo info = new ComponentInfo(id, typeof(TService), typeof(TComponent), policies, lifecycleManagement);
            _store.AddComponentInfo(info);
        }

        public void Register<TComponent>(string id, IList<IPolicy> policies = null, bool lifecycleManagement = false)
        {
            ComponentInfo info = new ComponentInfo(id, null, typeof(TComponent), policies, lifecycleManagement);
            _store.AddComponentInfo(info);
        }

        public void Register(string id, IList<IPolicy> policies = null, bool lifecycleManagement = false)
        {
            ComponentInfo info = new ComponentInfo(id, null, null, policies, lifecycleManagement);
            _store.AddComponentInfo(info);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private bool _disposed;
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                foreach (var kv in _lifecycleContainer)
                {
                    IDisposable d = kv.Key as IDisposable;
                    if (d != null)
                        d.Dispose();
                }
                _lifecycleContainer.Clear();
            }

            _disposed = true;
        }

        ~Kernal()
        {
            Dispose(false);
        }
    }
}
