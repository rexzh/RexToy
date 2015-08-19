using System;
using System.Collections.Generic;

using RexToy.Collections;

namespace RexToy.ExpressionLanguage
{
    using RexToy.ExpressionLanguage.AST;

    public class ExpressionLanguageEngineConfig
    {
        public static readonly ExpressionLanguageEngineConfig Default = new ExpressionLanguageEngineConfig();

        public ExpressionLanguageEngineConfig(IEvalExceptionHandlingPolicy policy = null, object fallbackValue = null, ICache<string, ExpressionLanguageAST> cache = null)
        {
            _policy = policy ?? EvalExceptionHandlingPolicy.ThrowPolicy;
            _fallbackValue = fallbackValue;
            _cache = cache ?? NoCache.GetInstance<string, ExpressionLanguageAST>();
        }

        private IEvalExceptionHandlingPolicy _policy;
        public IEvalExceptionHandlingPolicy Policy
        {
            get { return _policy; }
        }

        private object _fallbackValue;
        public object FallbackValue
        {
            get { return _fallbackValue; }
        }

        private ICache<string, ExpressionLanguageAST> _cache;
        public ICache<string, ExpressionLanguageAST> Cache
        {
            get { return _cache; }
        }
    }
}
