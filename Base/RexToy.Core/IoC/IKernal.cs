using System;
using System.Collections.Generic;

namespace RexToy.IoC
{
    public interface IKernal : IDisposable
    {
        string Id { get; }

        bool ReadyToBuild(string id);
        bool ReadyToBuild(Type serviceType);

        T Lookup<T>();
        T Lookup<T>(string id);
        object Lookup(Type serviceType);
        object Lookup(string id);

        void TearDown(object instance);

        void Register<TService, TComponent>(string id, IList<IPolicy> policies = null, bool lifecycleManagement = false);
        void Register<TComponent>(string id, IList<IPolicy> policies = null, bool lifecycleManagement = false);
        void Register(string id, IList<IPolicy> policies = null, bool lifecycleManagement = false);
    }
}