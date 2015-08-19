using System;
using System.Collections.Generic;

using RexToy.Logging;
using RexToy.Collections;

namespace RexToy.ExpressionLanguage
{
    using RexToy.ExpressionLanguage.AST;

    public class ExpressionLanguageEngine
    {
        private const string NULL = "null";
        private static ILog _log = LogContext.GetLogger<ExpressionLanguageEngine>();

        private IInterpreter _i;
        private IEvalContext _ctx;
        private IEvalExceptionHandlingPolicy _policy;
        private object _fallbackValue;
        private ICache<string, ExpressionLanguageAST> _cache;

        private ExpressionLanguageEngine(IEvalContext ctx, IEvalExceptionHandlingPolicy policy, object fallbackValue, ICache<string, ExpressionLanguageAST> cache)
        {
            _i = new Interpreter();
            if (ctx != null)
                _ctx = ctx;
            else
                _ctx = new EvalContext();

            _ctx.Assign(NULL, null);//Note:null works as keyword

            _policy = policy;
            _fallbackValue = fallbackValue;
            _cache = cache;
        }


        public static ExpressionLanguageEngine CreateEngine(ExpressionLanguageEngineConfig cfg, IEvalContext ctx = null)
        {
            cfg.ThrowIfNullArgument(nameof(cfg));

            return new ExpressionLanguageEngine(ctx, cfg.Policy, cfg.FallbackValue, cfg.Cache);
        }

        public static ExpressionLanguageEngine CreateEngine(IEvalContext ctx = null)
        {
            return CreateEngine(ExpressionLanguageEngineConfig.Default, ctx);
        }

        public void Assign(string param, object val)
        {
            _ctx.Assign(param, val);
        }

        public object Eval(string expr)
        {
            try
            {
                _log.Info("Eval expression: [{0}]", expr);

                ExpressionLanguageAST ast = _cache.Get(expr);
                if (ast == null)
                {
                    ast = _i.Parser(expr);
                    _cache.Put(expr, ast);
                }

                EvalVisitor v = new EvalVisitor(_ctx);
                object result = ast.Accept(v);

                _log.Info("Eval result: [{0}]", result);
                return result;
            }
            catch (ELParseException parseException)
            {
                _policy.Handle(parseException);
                if (_policy.Throw)
                    throw;
                else
                    return _fallbackValue;
            }
            catch (Exception ex)
            {
                _policy.Handle(ex);
                if (_policy.Throw)
                    throw ex.CreateWrapException<EvalException>();
                else
                    return _fallbackValue;
            }
        }

        public T Eval<T>(string expr)
        {
            object val = this.Eval(expr);
            return TypeCast.ChangeToTypeOrNullableType<T>(val);
        }

        public bool EvalToBool(object val)
        {
            return ScriptTypeUtil.EvalToBoolean(val);
        }
    }
}
