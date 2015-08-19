using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Linq;

using RexToy.Xml;

namespace RexToy.IoC
{
    abstract class CreationPolicy : IPolicy
    {
        protected const string PARAM = "params/param";
        protected const string NAME_ATTR = "@name";
        protected const string VALUE_ATTR = "@value";
        protected const string TEARDOWN_ATTR = "@teardown";

        protected Dictionary<string, string> _injectionHints;
        protected CreationPolicy()
        {
            _injectionHints = new Dictionary<string, string>();
        }

        protected bool _teardown;

        #region IPolicy Members

        public Stages Stage
        {
            get { return Stages.Creation; }
        }

        public abstract void Initialize(XAccessor x);
        public abstract void OnBuildComplete(IObjectBuildContext ctx);
        public abstract bool ReadyToBuild(IObjectBuildContext ctx);
        public abstract void BuildUp(IObjectBuildContext ctx);

        public virtual void TearDown(object instance, IObjectBuildContext ctx)
        {
            if (!ctx.LifeCycleManagement || !_teardown)
                return;

            if (ctx.SkipCreationPolicy)
                return;

            foreach (object injection in ctx.InjectedParameters)
            {
                ctx.Kernal.TearDown(injection);
            }
        }

        #endregion

        protected object[] PrepareArgs(IObjectBuildContext ctx, MethodBase method)
        {
            ParameterInfo[] pInfos = method.GetParameters();
            object[] args = new object[pInfos.Length];
            string val;
            for (int idx = 0; idx < args.Length; idx++)
            {
                ParameterInfo pInfo = pInfos[idx];

                bool hint = _injectionHints.TryGetValue(pInfo.Name, out val);
                if (!hint && pInfo.IsOptional)
                    args[idx] = pInfo.DefaultValue;
                else
                    args[idx] = PolicyUtility.Build(val, pInfo.ParameterType, ctx);
            }
            return args;
        }

        protected bool IsReady(MethodBase method, IObjectBuildContext ctx)
        {
            foreach (ParameterInfo pInfo in method.GetParameters())
            {
                string key = pInfo.Name;

                if ((!_injectionHints.ContainsKey(key)) && pInfo.IsOptional)
                    continue;
                else
                {
                    string val = null;
                    _injectionHints.TryGetValue(key, out val);
                    if (!PolicyUtility.IsReady(val, pInfo.ParameterType, ctx))
                        return false;
                }
            }

            return true;
        }

        private bool IsValidMethod(MethodBase method)
        {
            var q = (from p in method.GetParameters()
                     select p.Name).ToList();

            foreach (string key in _injectionHints.Keys)
            {
                if (!q.Contains(key))
                    return false;
            }
            return true;
        }

        protected MethodBase[] FilterByHintsAndSort(IEnumerable<MethodBase> methods)
        {
            List<MethodBase> result = new List<MethodBase>();
            foreach (MethodBase method in methods)
            {
                if (IsValidMethod(method))
                    result.Add(method);
            }
            result.Sort((x, y) => x.GetParameters().Length - y.GetParameters().Length);
            return result.ToArray();
        }
    }
}
