using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics.CodeAnalysis;

using RexToy.Logging;

namespace RexToy.AOP
{
    class InterceptSink : IMessageSink
    {
        private static ILog _log = LogContext.GetLogger<InterceptSink>();

        protected IMessageSink m_NextSink;
        protected MarshalByRefObject m_InterceptedObject;
        protected IWeaveManager m_WeaveManager;
        public InterceptSink(MarshalByRefObject interceptedObject, IMessageSink nextSink)
        {
            m_NextSink = nextSink;
            m_InterceptedObject = interceptedObject;
            m_WeaveManager = WeaveManagerFactory.Create();
        }

        #region IMessageSink Members

        public IMessageSink NextSink
        {
            get { return m_NextSink; }
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            IMethodCallMessage mcm = msg as IMethodCallMessage;
            if (mcm != null)
                _log.Warning("Async call on {0}.{1}, no intercept apply.", mcm.MethodName, mcm.TypeName);
            else
                _log.Warning("Async call detected, no intercept apply, message is {0}.", msg);

            return m_NextSink.AsyncProcessMessage(msg, replySink);
        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            IMethodCallMessage mcm = msg as IMethodCallMessage;
            MethodCallContext ctx = new MethodCallContext(mcm, m_NextSink);

            ctx.Position = Position.Before;
            PreProcess(ctx);

            ctx.Position = Position.Around;
            AroundProcess(ctx);

            if (ctx.ReturnMessage.Exception != null)
            {
                ctx.Position = Position.Throw;
                ExceptionProcess(ctx);
            }

            ctx.Position = Position.After;
            PostProcess(ctx);

            return ctx.ReturnMessage;
        }

        #endregion

        [SuppressMessage("Microsoft.Design", "CA1031")]
        protected virtual void PreProcess(MethodCallContext ctx)
        {
            List<BeforeAdvisor> advisors = new List<BeforeAdvisor>();
            try
            {
                string[] advisorNames = m_WeaveManager.GetAdvisorNames(ctx);
                for (int i = 0; i < advisorNames.Length; i++)
                {
                    BeforeAdvisor advisor = m_WeaveManager.GetAdvisor<BeforeAdvisor>(advisorNames[i]);
                    advisor.SetContext(ctx);
                    advisors.Add(advisor);
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.ThrowWeaveException(ctx, ex);
            }

            try
            {
                for (int i = 0; i < advisors.Count; i++)
                {
                    advisors[i].Execute();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.ThrowAspectExecuteError(ctx, ex);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031")]
        protected virtual void AroundProcess(MethodCallContext ctx)
        {
            List<AroundAdvisor> advisors = new List<AroundAdvisor>();
            try
            {
                string[] advisorNames = m_WeaveManager.GetAdvisorNames(ctx);
                for (int i = 0; i < advisorNames.Length; i++)
                {
                    AroundAdvisor advisor = m_WeaveManager.GetAdvisor<AroundAdvisor>(advisorNames[i]);
                    advisor.SetContext(ctx);
                    advisors.Add(advisor);
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.ThrowWeaveException(ctx, ex);
            }

            for (int idx = 0; idx < advisors.Count - 1; idx++)
                advisors[idx].Next = advisors[idx + 1];

            try
            {
                if (advisors.Count == 0)
                {
                    ctx.ReturnMessage = m_NextSink.SyncProcessMessage(ctx.CallMessage) as IMethodReturnMessage;
                    return;
                }
                else
                {
                    for (int i = 0; i < advisors.Count; i++)
                    {
                        advisors[i].Execute();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.ThrowAspectExecuteError(ctx, ex);
            }

            if (ctx.ReturnMessage == null)//Note: build fake return message
            {
                Assertion.IsFalse(ctx.CallMessage.MethodBase.IsConstructor, "Should not be ctor.");
                MethodInfo method = ctx.CallMessage.MethodBase as MethodInfo;
                List<object> outArgs = new List<object>();
                ParameterInfo[] paramInfos = method.GetParameters();
                for (int idx = 0; idx < paramInfos.Length; idx++)
                {
                    if (paramInfos[idx].IsOut)
                    {
                        if (paramInfos[idx].ParameterType.IsValueType)
                        {
                            object val = Activator.CreateInstance(paramInfos[idx].ParameterType);
                            outArgs.Add(val);
                        }
                        else
                        {
                            outArgs.Add(null);
                        }
                    }
                }
                object ret = null;
                if (method.ReturnType.IsValueType && method.ReturnType != typeof(void))
                {
                    ret = Activator.CreateInstance(method.ReturnType);
                }
                else
                    ret = null;
                ReturnMessage rm = new ReturnMessage(ret, outArgs.ToArray(), outArgs.Count, ctx.CallMessage.LogicalCallContext, ctx.CallMessage);
                ctx.ReturnMessage = rm;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031")]
        protected virtual void PostProcess(MethodCallContext ctx)
        {
            List<AfterAdvisor> advisors = new List<AfterAdvisor>();
            try
            {
                string[] advisorNames = m_WeaveManager.GetAdvisorNames(ctx);
                for (int i = 0; i < advisorNames.Length; i++)
                {
                    AfterAdvisor advisor = m_WeaveManager.GetAdvisor<AfterAdvisor>(advisorNames[i]);
                    advisor.SetContext(ctx);
                    advisors.Add(advisor);
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.ThrowWeaveException(ctx, ex);
            }

            try
            {
                for (int i = 0; i < advisors.Count; i++)
                {
                    advisors[i].Execute();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.ThrowAspectExecuteError(ctx, ex);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031")]
        protected virtual void ExceptionProcess(MethodCallContext ctx)
        {
            List<ExceptionAdvisor> advisors = new List<ExceptionAdvisor>();
            try
            {
                string[] advisorNames = m_WeaveManager.GetAdvisorNames(ctx);
                for (int i = 0; i < advisorNames.Length; i++)
                {
                    ExceptionAdvisor advisor = m_WeaveManager.GetAdvisor<ExceptionAdvisor>(advisorNames[i]);
                    advisor.SetContext(ctx);
                    advisors.Add(advisor);
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.ThrowWeaveException(ctx, ex);
            }

            try
            {
                for (int i = 0; i < advisors.Count; i++)
                {
                    advisors[i].Execute();
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.ThrowAspectExecuteError(ctx, ex);
            }
        }
    }
}
