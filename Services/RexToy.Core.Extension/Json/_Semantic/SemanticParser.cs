using System;
using System.Collections.Generic;
using System.Diagnostics;

using RexToy.Logging;
using RexToy.Compiler.Lexical;
using RexToy.Compiler.Semantic;

namespace RexToy.Json
{
    internal class SemanticParser : SemanticParser<TokenType, object>
    {
        private const char SPACE = ' ';
        private const int IDENT = 6;
        private int callLevel;

        private static ILog _log = LogContext.GetLogger<SemanticParser>();

        [Conditional("DEBUG")]
        private void CalllevelIncrement()
        {
            callLevel++;
        }

        [Conditional("DEBUG")]
        private void CalllevelDecrement()
        {
            callLevel--;
        }

        public override object Parse()
        {
            callLevel = 0;
            switch (this.Tokens[0].TokenType)
            {
                case TokenType.LEFT_BRACKET:
                    return Object();

                case TokenType.LEFT_SQUARE_BRACKET:
                    return Array();

                case TokenType.NULL:
                    if (this.Tokens.Count == 1)
                        return null;
                    break;

                case TokenType.ID:
                    if (this.Tokens.Count == 1)
                        return this.Tokens[0].TokenValue;
                    break;
            }
            ExceptionHelper.ThrowParseException(this.Tokens[0].Position);
            return null;
        }

        //Note:
        //EBNF of Json:
        //object -> { pairList }
        //pairList -> kvpair | kvpair , pairList
        //kvpair -> ID : Value
        //Value -> ID | object | array
        //array -> [valList]
        //valList-> Value | Value , valList
        private JsonObject Object()
        {
            Token<TokenType> t = this.GetNextToken();
            Assertion.AreEqual(TokenType.LEFT_BRACKET, t.TokenType, "JSON object should begin with '{'");

            JsonObject jsonObject = new JsonObject();

            CalllevelIncrement();
            _log.Debug(new string(SPACE, callLevel * IDENT) + "Object-> {");

            this.PairList(jsonObject);

            t = this.GetNextToken();
            if (t.TokenType != TokenType.RIGHT_BRACKET)
                ExceptionHelper.ThrowTokenExpected(TokenType.RIGHT_BRACKET, t.Position);

            _log.Debug(new string(SPACE, callLevel * IDENT) + "Object<- }");
            CalllevelDecrement();

            return jsonObject;
        }

        private void PairList(JsonObject jsonObject)
        {
            if (PeekNextToken().TokenType == TokenType.RIGHT_BRACKET)
                return;

            Pair(jsonObject);
            while (PeekNextToken().TokenType == TokenType.COMMA)
            {
                Token<TokenType> t = GetNextToken();//Note:Parse comma
                Pair(jsonObject);
            }
        }

        private void Pair(JsonObject jsonObject)
        {
            Token<TokenType> t = GetNextToken();
            string key = t.TokenValue;

            if ((!key.StartsWith(JsonConstant.Quot)) || (!key.EndsWith(JsonConstant.Quot)))
                ExceptionHelper.ThrowSyntaxNoQuotError();

            key = key.UnBracketing(StringPair.DoubleQuote);

            CalllevelIncrement();
            _log.Debug("{0}Pair-> {1}:", new string(SPACE, callLevel * IDENT), key);

            t = GetNextToken();//Note:Colon
            if (t.TokenType != TokenType.COLON)
                ExceptionHelper.ThrowTokenExpected(TokenType.COLON, t.Position);

            object val = Value();

            jsonObject.AddKVPair(key, val);

            _log.Debug("{0}Pair<-", new string(SPACE, callLevel * IDENT));
            CalllevelDecrement();
        }

        private object Value()
        {
            CalllevelIncrement();

            Token<TokenType> t = PeekNextToken();
            switch (t.TokenType)
            {
                case TokenType.ID:
                    t = GetNextToken();
                    _log.Debug("{0}Value-> {1}", new string(SPACE, callLevel * IDENT), t.TokenValue);
                    CalllevelDecrement();
                    return t.TokenValue;

                case TokenType.LEFT_BRACKET:
                    CalllevelDecrement();
                    return Object();

                case TokenType.LEFT_SQUARE_BRACKET:
                    CalllevelDecrement();
                    return Array();

                case TokenType.NULL:
                    t = GetNextToken();
                    CalllevelDecrement();
                    return null;
            }
            Assertion.Fail("Error when get value.");
            return null;
        }

        private JsonArray Array()
        {
            CalllevelIncrement();
            _log.Debug("{0}Array-> [", new string(SPACE, callLevel * IDENT));
            JsonArray array = new JsonArray();

            Token<TokenType> t = this.GetNextToken();
            Assertion.AreEqual(TokenType.LEFT_SQUARE_BRACKET, t.TokenType, "JSON array should begin with '['");

            ValueList(array);

            t = this.GetNextToken();
            if (t.TokenType != TokenType.RIGHT_SQUARE_BRACKET)
                ExceptionHelper.ThrowTokenExpected(TokenType.RIGHT_SQUARE_BRACKET, t.Position);

            _log.Debug("{0}Array<- ]", new string(SPACE, callLevel * IDENT));
            CalllevelDecrement();
            return array;
        }

        private void ValueList(JsonArray array)
        {
            if (PeekNextToken().TokenType == TokenType.RIGHT_SQUARE_BRACKET)
                return;

            array.AddValue(Value());

            while (PeekNextToken().TokenType == TokenType.COMMA)
            {
                GetNextToken();//Note:Get comma
                array.AddValue(Value());
            }
        }
    }
}