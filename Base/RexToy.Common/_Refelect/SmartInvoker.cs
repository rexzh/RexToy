using System;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

namespace RexToy
{
    public abstract class SmartInvoker : ISmartInvoker
    {
        private IReflector _r;
        protected SmartInvoker(object instance, IReflectPolicy policy)
        {
            instance.ThrowIfNullArgument(nameof(instance));
            _r = Reflector.Bind(instance, policy);
        }

        private MethodBase _candidate;
        private object[] _convertedArgs;

        protected bool FindAvailableMethod(string method, object[] args, bool ctor)
        {
            IEnumerable<MethodBase> methods;
            if (ctor)
                methods = _r.FindAllConstructors();
            else
                methods = _r.GetMethods(method);

            foreach (MethodBase mi in methods)
            {
                ParameterInfo[] paramInfos = mi.GetParameters();
                if (!(mi.IsVarArgs() || mi.HasOptionalArgs()) && (paramInfos.Length != args.Length))
                    continue;

                object[] converted_args;
                bool converted = Convert(mi, args, out converted_args);
                if (converted)
                {
                    if (_candidate != null)//Note: if already has candidate, we need mimic the C# compiler's strategy.
                        FindBetterCandidate(args, mi, converted_args);
                    else//Note: No candidate yet, record it.
                        SaveCurrentFound(mi, converted_args);
                }
            }
            return _candidate != null;
        }

        private void FindBetterCandidate(object[] args, MethodBase mi, object[] converted_args)
        {
            if (_candidate.IsVarArgs() && mi.IsVarArgs())
            {
                //Note: if both candidate and current method isVarArgs, choose the one has more parameter
                if (_candidate.DeclareParamCount() < mi.DeclareParamCount())
                    SaveCurrentFound(mi, converted_args);
            }
            else if (!_candidate.IsVarArgs() && !mi.IsVarArgs())
            {
                //Note:if both not VarArgs, choose exact match one, otherwise is ambiguous call
                //     (in case both can fit with opt param, it's ambiguous call).
                if (_candidate.DeclareParamCount() == args.Length)
                {
                    //Note:Saved candidate is better, nothing to do.
                }
                else if (mi.DeclareParamCount() == args.Length)
                    SaveCurrentFound(mi, converted_args);
                else
                    ThrowHelper.ThrowAmbiguousMethod(_r.BoundType, mi.Name);
            }
            else if (_candidate.IsVarArgs() && !mi.IsVarArgs())//Note:Prefer NonVarArgs method
                SaveCurrentFound(mi, converted_args);
            //Note:else if (!_candidate.IsVarArgs() && mi.IsVarArgs()) Prefer NonVarArgs method, candidate is better, do nothing
        }

        private void SaveCurrentFound(MethodBase mi, object[] converted_args)
        {
            _candidate = mi;
            _convertedArgs = converted_args;
        }

        public object Invoke(string method, object[] args)
        {
            if (FindAvailableMethod(method, args, false))
                return _candidate.Invoke(_r.BoundInstance, _r.Policy.BindingFlags, null, _convertedArgs, null);
            else
            {
                ThrowHelper.ThrowMemberNotExist(_r.BoundType, method, MemberTypes.Method);
                return null;
            }
        }

        public object InvokeConstructor(object[] args)
        {
            if (FindAvailableMethod(null, args, true))
            {
                ConstructorInfo ctorInfo = _candidate as ConstructorInfo;
                return ctorInfo.Invoke(_convertedArgs);
            }
            else
            {
                ThrowHelper.ThrowMemberNotExist(_r.BoundType, "Constructor", MemberTypes.Constructor);
                return null;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1021")]
        protected abstract bool Convert(MethodBase method, object[] args, out object[] convertedArgs);
    }
}
