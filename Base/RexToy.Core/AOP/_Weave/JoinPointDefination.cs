using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using RexToy.Logging;
using RexToy.Compiler.Lexical;

namespace RexToy.AOP
{
    //Extend:To recogonize C# syntax (int, double..)
    public class JoinPointDefination
    {
        private static ILog _log = LogContext.GetLogger<JoinPointDefination>();

        [SuppressMessage("Microsoft.Design", "CA1031")]
        public JoinPointDefination(Position position, string advisorName, string pattern)
        {
            _position = position;
            _advisorName = advisorName;
            _pattern = pattern;

            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(_pattern);
            var tokens = lp.Parse();
            try
            {
                Init(tokens);
            }
            catch (Exception ex)
            {
                ExceptionHelper.ThrowWeaveSyntaxInitialize(_pattern, ex);
            }
        }

        private static int GetLastDotTokenIndex(List<Token<TokenType>> tokens)
        {
            for (int i = tokens.Count - 1; i >= 0; i--)
            {
                if (tokens[i].TokenType == TokenType.DOT)
                    return i;
            }
            return -1;
        }

        private static string MakePattern(List<Token<TokenType>> tokens)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < tokens.Count; i++)
            {
                switch (tokens[i].TokenType)
                {
                    case TokenType.ANY:
                        str.Append(@"[\w\W_.]*");
                        break;

                    case TokenType.ASTERISK:
                        str.Append(@"[\w\W_]*");
                        break;

                    default:
                        str.Append(tokens[i].TokenValue);
                        break;
                }

            }
            return str.ToString();
        }

        private static string MakeParamListPattern(List<Token<TokenType>> tokens)
        {
            StringBuilder pattern = new StringBuilder();
            foreach (var token in tokens)
            {
                if (token.TokenType == TokenType.ASTERISK)
                {
                    pattern.Append(@"[\w\W_]*");
                }
                else if (token.TokenType == TokenType.ANY)
                {
                    pattern.Append(@"[\w\W_,]*");
                }
                else
                    pattern.Append(token.TokenValue);
            }
            return pattern.ToString();
        }

        private static string JoinTokens(List<Token<TokenType>> tokens, int start, int end)
        {
            StringBuilder str = new StringBuilder();
            for (int i = start; i < end; i++)
            {
                str.Append(tokens[i].TokenValue);
            }
            return str.ToString();
        }

        private string _accessModifier;
        private string _returnType;
        private Regex _methodRegex;
        private string _typeDesc;
        private bool _checkBaseType;
        private string _paramPattern;
        private void Init(IList<Token<TokenType>> tokens)
        {
            Assertion.IsTrue(tokens.Count >= 2, string.Format("Pattern [{0}] format not correct.", _pattern));

            _accessModifier = tokens[0].TokenValue;
            _returnType = tokens[1].TokenValue;

            int m = -1, n = -1;
            //Note: skip access modifier and return type, so we can start from 2.
            for (int i = 2; i < tokens.Count; i++)
            {
                if (tokens[i].TokenType == TokenType.LEFT_P)
                    m = i;
                if (tokens[i].TokenType == TokenType.RIGHT_P)
                    n = i;
            }
            Assertion.IsTrue(m != -1, "( not found in signature.");
            Assertion.IsTrue(n != -1, ") not found in signature.");

            var tokenList = tokens as List<Token<TokenType>>;//TODO:
            var callTokens = tokenList.GetRange(2, m - 2);
            var paramListTokens = tokenList.GetRange(m + 1, n - m - 1);//Note:n-1-(m+1)+1

            int lastDotIdx = GetLastDotTokenIndex(callTokens);
            Assertion.IsTrue(lastDotIdx != -1, "Did not found method call, missing . before method pattern?");

            var typeTokens = callTokens.GetRange(0, lastDotIdx);
            var methodTokens = callTokens.GetRange(lastDotIdx + 1, callTokens.Count - lastDotIdx - 1);
            _methodRegex = new Regex(MakePattern(methodTokens));

            if (typeTokens[typeTokens.Count - 1].TokenType == TokenType.PLUS)
            {
                _checkBaseType = true;
                _typeDesc = JoinTokens(typeTokens, 0, typeTokens.Count - 1);//Note:skip last + operator
                Assertion.IsFalse(_typeDesc.IndexOf('*') > 0 || _typeDesc.IndexOf("..") > 0, "When use + operator in match pattern, can not use * or .. operator");
            }
            else
            {
                _checkBaseType = false;
                _typeDesc = MakePattern(typeTokens);
            }

            _paramPattern = MakeParamListPattern(paramListTokens);
        }

        private Position _position;
        public Position Position
        {
            get { return _position; }
        }

        private string _pattern;
        public string Pattern
        {
            get { return _pattern; }
        }

        private string _advisorName;
        public string AdvisorName
        {
            get { return _advisorName; }
        }

        internal bool Match(IMethodCallContext ctx)
        {
            if (_position != ctx.Position)
                return false;

            MethodInfo method = ctx.CallMessage.MethodBase as MethodInfo;
            if (method == null)
                ExceptionHelper.ThrowNotMethodInfo(method);
            return Match(method);
        }

        public bool Match(MethodInfo method)
        {
            _log.Debug("Method [{0}.{1}({2})] match pattern {3}.", method.ReflectedType.FullName, method.Name, method.GetParamsTypeArray().Join(','), _pattern);
            if (AccessModifierMatch(method) && ReturnTypeMatch(method) && TypeMatch(method)
                && MethodNameMatch(method) && ParamListMatch(method))
            {
                _log.Debug("Match success.");
                return true;
            }
            else
            {
                _log.Debug("Match fail.");
                return false;
            }
        }

        private bool AccessModifierMatch(MethodInfo method)
        {
            switch (_accessModifier)
            {
                case "*":
                    return true;

                case "public":
                    return method.IsPublic;

                case "private":
                    return method.IsPrivate;

                default:
                    ExceptionHelper.ThrowInvalidAccessModifier(_accessModifier);
                    return false;
            }
        }

        private bool ReturnTypeMatch(MethodInfo method)
        {
            if (_returnType == "*")
                return true;
            else
            {
                string name = method.ReturnType.Name.StripGenericAndRef();
                if (name == "Void" && _returnType == "void")
                    return true;

                return name == _returnType;
            }
        }

        private bool TypeMatch(MethodInfo method)
        {
            if (_checkBaseType)
            {
                Type type = method.ReflectedType;
                Type interfaceType = type.GetInterface(_typeDesc);
                if (interfaceType != null)
                    return true;
                do
                {
                    if (type.FullName.StripGenericAndRef() == _typeDesc)
                        return true;
                    type = type.BaseType;
                } while (type != typeof(object));//Note:Match on object is not allowed.
                return false;
            }
            else
            {
                string longName = method.ReflectedType.Namespace + Type.Delimiter + method.ReflectedType.Name.StripGenericAndRef();
                Regex regex = new Regex(_typeDesc);
                return regex.IsMatch(longName);
            }
        }

        private bool MethodNameMatch(MethodInfo method)
        {
            return _methodRegex.IsMatch(method.Name);
        }

        private bool ParamListMatch(MethodInfo method)
        {
            Type[] paramTypeArray = method.GetParamsTypeArray();
            if (_paramPattern.Length == 0)
                return paramTypeArray.Length == 0;

            string paramList = paramTypeArray.Join<Type>(
                t => t.Name.StripGenericAndRef(), ',');

            Regex regex = new Regex(_paramPattern);
            return regex.IsMatch(paramList);
        }
    }
}
