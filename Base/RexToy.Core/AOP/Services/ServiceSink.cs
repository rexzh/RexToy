using System;
using System.Transactions;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

using RexToy.Logging;

namespace RexToy.AOP.Services
{
    sealed class ServiceSink : InterceptSink, IDisposable
    {
        private static ILog _log = LogContext.GetLogger<ServiceSink>();

        private bool _txParticipant;
        private TransactionScope _txScope;
        public ServiceSink(MarshalByRefObject component, IMessageSink nextSink)
            : base(component, nextSink)
        {
        }

        protected override void PreProcess(MethodCallContext ctx)
        {
            base.PreProcess(ctx);

            Type type = m_InterceptedObject.GetType();
            MethodBase method = ctx.CallMessage.MethodBase;
            try
            {
                AutoCompleteAttribute auto = method.GetSingleAttribute<AutoCompleteAttribute>();
                _txParticipant = (auto != null);

                if (_txParticipant)
                {
                    TransactionAttribute tx = type.GetSingleAttribute<TransactionAttribute>();
                    if (tx == null)
                        ContainerExceptionHelper.ThrowComponentTransactionNotDefine(type, method);

                    TransactionOptions txOptions = new TransactionOptions();
                    txOptions.IsolationLevel = tx.TxIsolationLevel;
                    _txScope = new TransactionScope(tx.Option, txOptions);

                    _log.Info("Transaction [{0}] started on [{1}.{2}].", Transaction.Current.TransactionInformation, type.Name, method.Name);
                }
            }
            catch (Exception ex)
            {
                _log.Error("Transaction failed to start on [{1}.{2}]. [{0}]", ex.ToString(), type.Name, method.Name);
                throw;
            }
        }

        protected override void PostProcess(MethodCallContext ctx)
        {
            try
            {
                if (_txParticipant)
                {
                    if (ctx.ReturnMessage.Exception == null)
                    {
                        _log.Info("No Exception, TransactionScope.Complete() before Dispose() with Transaction[{0}].", Transaction.Current.TransactionInformation);
                        _txScope.Complete();
                    }
                    _txScope.Dispose();
                }
            }
            catch (Exception ex)
            {
                _log.Error("Transaction management error with Transaction[{0}]. {1}", Transaction.Current, ex.ToString());
                throw;
            }

            base.PostProcess(ctx);
        }

        public void Dispose()
        {
            _txScope.Dispose();
        }    
    }
}
