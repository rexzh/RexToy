using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

using RexToy.Collections;
using RexToy.Compiler.Lexical;
using RexToy.Compiler.Semantic;

namespace RexToy.ExpressionLanguage
{
    using RexToy.ExpressionLanguage.AST;
    using RexToy.ExpressionLanguage.Tokens;

    internal class SemanticParser : SemanticParser<TokenType, ExpressionLanguageAST>
    {
        //BNF:
        //Value ::= Expr ? Expr : Expr | Expr
        //Expr ::=  Logic LogicOp(||,&&) Logic | Logic
        //Logic ::= Compare CompareOp(>,>=,<,<=,==,!=) Compare | Compare
        //Compare ::= Term (+-) Term | Term
        //Term ::= Exponent (*/%) Exponent | Exponent
        //Exponent ::= Unary (^) Unary
        //Unary ::= Factor | -Factor | !Factor
        //Factor ::= Obj.ID | Obj.ID(ValueList) | Obj[ValueList] | ID(ValueList) | Obj
        //Obj ::= (Value) | ID | obj.ID,obj.ID(),obj[] | array[ValueList] | tuple[ID-Value pair] | regex[pattern] | new[ClassQualifier ValueList] | ClassQualifier
        //ValueList ::= | Value | Value,ValueList

        public override ExpressionLanguageAST Parse()
        {
            ExpressionLanguageAST ast = new ExpressionLanguageAST(Value());

            if (this.Index < this.Tokens.Count)
                ExceptionHelper.ThrowParseError(this.Tokens[this.Index].Position);

            Trace(ast);
            return ast;
        }

        [Conditional("DEBUG")]
        private void Trace(ExpressionLanguageAST ast)
        {
            TraceVisitor trace = new TraceVisitor();
            ast.Accept(trace);
        }

        private Node Value()
        {
            Node result = Expr();
            Token<TokenType> t = PeekNextToken();
            bool loop = true;
            while (!t.End)
            {
                switch (t.TokenType)
                {
                    case TokenType.QuestionMark:
                        GetNextToken();//Note:question token
                        Node trueNode = Expr();
                        Token<TokenType> colonToken = GetNextToken();
                        if (colonToken.TokenType != TokenType.Colon)
                            ExceptionHelper.ThrowExpectToken(TokenType.Colon, colonToken.Position);

                        Node falseNode = Expr();

                        result = new TernaryNode(result, trueNode, falseNode);
                        break;

                    default:
                        loop = false;
                        break;
                }

                if (!loop)
                    break;
                t = PeekNextToken();
            }

            return result;
        }

        private Node Expr()
        {
            Node result = Logic();
            Token<TokenType> t = PeekNextToken();
            while (!t.End)
            {
                if (t.TokenType == TokenType.LogicalOperator)
                {
                    Token<TokenType> opToken = GetNextToken();
                    Node rhsNode = Logic();
                    Node lhsNode = result;
                    result = new OperatorNode(opToken, lhsNode, rhsNode);
                }
                else
                    break;

                t = PeekNextToken();
            }

            return result;
        }

        private Node Logic()
        {
            Node result = Compare();
            Token<TokenType> t = PeekNextToken();
            while (!t.End)
            {
                if (t.TokenType == TokenType.CompareOperator)
                {
                    Token<TokenType> opToken = GetNextToken();
                    Node rhsNode = Compare();
                    Node lhsNode = result;

                    result = new OperatorNode(opToken, lhsNode, rhsNode);
                }
                else
                    break;

                t = PeekNextToken();
            }

            return result;
        }

        private Node Compare()
        {
            Node result = Term();
            Token<TokenType> t = PeekNextToken();
            while (!t.End)
            {
                if (t.TokenType == TokenType.ArithmeticOperator && (t.TokenValue == "+" || t.TokenValue == "-"))
                {
                    Token<TokenType> opToken = GetNextToken();
                    Node rhsNode = Term();
                    Node lhsNode = result;
                    result = new OperatorNode(opToken, lhsNode, rhsNode);
                }
                else
                    break;

                t = PeekNextToken();
            }

            return result;
        }

        private Node Term()
        {
            Node result = Exponent();
            Token<TokenType> t = PeekNextToken();
            while (!t.End)
            {
                if (t.TokenType == TokenType.ArithmeticOperator && (t.TokenValue == "*" || t.TokenValue == "/" || t.TokenValue == "%"))
                {
                    Token<TokenType> opToken = GetNextToken();
                    Node rhsNode = Exponent();
                    Node lhsNode = result;
                    result = new OperatorNode(opToken, result, rhsNode);
                }
                else
                    break;

                t = PeekNextToken();
            }
            return result;
        }

        private Node Exponent()
        {
            Node result = Unary();
            Token<TokenType> t = PeekNextToken();
            while (!t.End)
            {
                if (t.TokenType == TokenType.ArithmeticOperator && t.TokenValue == "^")
                {
                    Token<TokenType> opToken = GetNextToken();
                    Node rhsNode = Unary();
                    Node lhsNode = result;
                    result = new OperatorNode(opToken, lhsNode, rhsNode);
                }
                else
                    break;

                t = PeekNextToken();
            }
            return result;
        }

        private Node Unary()
        {
            Token<TokenType> t = PeekNextToken();
            switch (t.TokenType)
            {
                case TokenType.ArithmeticOperator:
                    if (t.TokenValue == "-" || t.TokenValue == "+")
                    {
                        GetNextToken();
                        return new UnaryNode(t, Factor());
                    }
                    else
                    {
                        ExceptionHelper.ThrowExpectToken(TokenType.ArithmeticOperator, t.Position);
                        return null;
                    }

                case TokenType.LogicalOperator:
                    if (t.TokenValue == "!")
                    {
                        GetNextToken();
                        return new UnaryNode(t, Factor());
                    }
                    else
                    {
                        ExceptionHelper.ThrowExpectToken(TokenType.LogicalOperator, t.Position);
                        return null;
                    }

                default:
                    return Factor();
            }
        }

        private Node Factor()
        {
            Node node = Obj();
            Token<TokenType> t = PeekNextToken();
            while (!t.End)
            {
                if (t.TokenType == TokenType.Dot)
                {
                    GetNextToken();
                    Token<TokenType> idToken = GetNextToken();
                    if (idToken.TokenType != TokenType.ID)
                        ExceptionHelper.ThrowExpectToken(TokenType.ID, idToken.Position);
                    if (PeekNextToken().TokenType == TokenType.Left_Parenthesis)//Note:Method Call
                    {
                        GetNextToken();
                        ParamListNode method_param_node = ValueList();
                        Token<TokenType> rightToken = GetNextToken();
                        if (rightToken.TokenType != TokenType.Right_Parenthesis)
                            ExceptionHelper.ThrowExpectToken(TokenType.Right_Parenthesis, rightToken.Position);

                        MethodNode methodNode = new MethodNode(node, new SimpleNode(idToken), method_param_node);
                        node = methodNode;

                        t = PeekNextToken();
                        continue;
                    }
                    else//Note:Property
                    {
                        PropertyNode propNode = new PropertyNode(node, new SimpleNode(idToken));
                        node = propNode;
                    }
                }
                else if (t.TokenType == TokenType.Left_Square_Bracket)
                {
                    GetNextToken();
                    ParamListNode index_param_node = ValueList();
                    Token<TokenType> rightToken = GetNextToken();
                    if (rightToken.TokenType != TokenType.Right_Square_Bracket)
                        ExceptionHelper.ThrowExpectToken(TokenType.Right_Square_Bracket, rightToken.Position);

                    IndexerNode indexerNode = new IndexerNode(node, index_param_node);
                    node = indexerNode;
                }
                else if (t.TokenType == TokenType.Left_Parenthesis)
                {
                    GetNextToken();
                    ParamListNode static_method_param_node = ValueList();
                    Token<TokenType> rightToken = GetNextToken();
                    if (rightToken.TokenType != TokenType.Right_Parenthesis)
                        ExceptionHelper.ThrowExpectToken(TokenType.Right_Parenthesis, t.Position);

                    MethodNode staticMethodNode = new MethodNode(null, node, static_method_param_node);
                    node = staticMethodNode;
                }
                else
                    break;

                t = PeekNextToken();
            }
            return node;
        }

        private Node Obj()
        {
            Token<TokenType> t = PeekNextToken();
            switch (t.TokenType)
            {
                case TokenType.Array:
                    return Array();

                case TokenType.Hash:
                    return Hash();

                case TokenType.Regex:
                    return Regex();

                case TokenType.New:
                    return New();

                case TokenType.Left_Parenthesis:
                    GetNextToken();
                    Node node = Value();
                    Token<TokenType> rightToken = GetNextToken();
                    if (rightToken.TokenType != TokenType.Right_Parenthesis)
                        ExceptionHelper.ThrowExpectToken(TokenType.Right_Parenthesis, t.Position);
                    return node;

                case TokenType.ID:
                    Token<TokenType> idToken = GetNextToken();
                    Token<TokenType> next = PeekNextToken();
                    if (next.TokenType == TokenType.ClassQualifier)
                    {
                        List<string> strList = new List<string>();
                        strList.Add(idToken.TokenValue);
                        while (next.TokenType == TokenType.ClassQualifier)
                        {
                            GetNextToken();//Note:Get the :: operator
                            idToken = GetNextToken();
                            if (idToken.TokenType != TokenType.ID)
                                ExceptionHelper.ThrowExpectToken(TokenType.ID, idToken.Position);
                            strList.Add(idToken.TokenValue);
                            next = PeekNextToken();
                        }
                        return new ClassQualifierNode(strList);
                    }
                    else
                        return new SimpleNode(idToken);

                case TokenType.String:
                case TokenType.Long:
                case TokenType.Decimal:
                case TokenType.Boolean:
                    Token<TokenType> valToken = GetNextToken();
                    return new SimpleNode(valToken);

                default:
                    ExceptionHelper.ThrowParseError(t.Position);
                    return null;
            }
        }

        private ParamListNode ValueList()
        {
            ParamListNode param_list_node = new ParamListNode(new List<Node>());
            Token<TokenType> t = PeekNextToken();
            if (t.TokenType == TokenType.Right_Parenthesis || t.TokenType == TokenType.Right_Square_Bracket)
            {
                return param_list_node;
            }
            else
            {
                Node node = Value();
                param_list_node.AppendNode(node);
                while (PeekNextToken().TokenType == TokenType.Comma)
                {
                    Token<TokenType> commaToken = GetNextToken();
                    if (commaToken.TokenType != TokenType.Comma)
                        ExceptionHelper.ThrowExpectToken(TokenType.Comma, commaToken.Position);

                    node = Value();
                    param_list_node.AppendNode(node);
                }
                return param_list_node;
            }
        }

        private Node Array()
        {
            Token<TokenType> t = GetNextToken();
            Assertion.AreEqual(TokenType.Array, t.TokenType, "Should be array keyword.");
            t = GetNextToken();
            if (t.TokenType != TokenType.Left_Square_Bracket)
                ExceptionHelper.ThrowExpectToken(TokenType.Left_Square_Bracket, t.Position);
            Node result = null;
            Node node = Value();
            Token<TokenType> separator = PeekNextToken();
            switch (separator.TokenType)
            {
                case TokenType.Comma:
                    GetNextToken();//Get the comma
                    var list = ValueList();
                    list.InsertNodeAtHead(node);
                    result = new ArrayNode((List<Node>)list.Values);
                    break;

                case TokenType.Colon:
                    GetNextToken();//Get Colon
                    var end = Value();
                    result = new AutoIntArrayNode(node, end);
                    break;

                default:
                    ExceptionHelper.ThrowUnexpectedToken(separator.TokenType, separator.Position);
                    break;
            }

            t = GetNextToken();
            if (t.TokenType != TokenType.Right_Square_Bracket)
                ExceptionHelper.ThrowExpectToken(TokenType.Right_Square_Bracket, t.Position);
            return result;
        }

        private HashNode Hash()
        {
            Dictionary<string, Node> kvpairs = new Dictionary<string, Node>();

            Token<TokenType> t = GetNextToken();
            Assertion.AreEqual(TokenType.Hash, t.TokenType, "Should be hash keyword.");
            t = GetNextToken();
            if (t.TokenType != TokenType.Left_Square_Bracket)
                ExceptionHelper.ThrowExpectToken(TokenType.Left_Square_Bracket, t.Position);

            KVPairList(kvpairs);
            HashNode tuple = new HashNode(kvpairs);

            t = GetNextToken();
            if (t.TokenType != TokenType.Right_Square_Bracket)
                ExceptionHelper.ThrowExpectToken(TokenType.Right_Square_Bracket, t.Position);
            return tuple;
        }

        private RegexNode Regex()
        {
            Token<TokenType> t = GetNextToken();
            Assertion.AreEqual(TokenType.Regex, t.TokenType, "Should be regex keyword.");

            t = GetNextToken();
            if (t.TokenType != TokenType.Left_Square_Bracket)
                ExceptionHelper.ThrowExpectToken(TokenType.Left_Square_Bracket, t.Position);

            t = GetNextToken();
            if (t.TokenType != TokenType.String)
                ExceptionHelper.ThrowExpectToken(TokenType.String, t.Position);
            RegexNode regex = new RegexNode(t);

            t = GetNextToken();
            if (t.TokenType != TokenType.Right_Square_Bracket)
                ExceptionHelper.ThrowExpectToken(TokenType.Right_Square_Bracket, t.Position);
            return regex;
        }

        private NewNode New()
        {
            Token<TokenType> t = GetNextToken();
            Assertion.AreEqual(TokenType.New, t.TokenType, "Should be new keyword.");

            t = GetNextToken();
            if (t.TokenType != TokenType.Left_Square_Bracket)
                ExceptionHelper.ThrowExpectToken(TokenType.Left_Square_Bracket, t.Position);

            Node typeNode = Obj();
            Assertion.IsTrue(typeNode is ClassQualifierNode, "First object should be class qualifi node.");

            ParamListNode paramList = ValueList();

            t = GetNextToken();
            if (t.TokenType != TokenType.Right_Square_Bracket)
                ExceptionHelper.ThrowExpectToken(TokenType.Right_Square_Bracket, t.Position);

            return new NewNode(typeNode, paramList);
        }


        private void KVPairList(Dictionary<string, Node> kvpairs)
        {
            KVPair(kvpairs);

            while (PeekNextToken().TokenType == TokenType.Comma)
            {
                Token<TokenType> comma = GetNextToken();
                KVPair(kvpairs);
            }
        }

        private void KVPair(Dictionary<string, Node> kvpairs)
        {
            Token<TokenType> key = GetNextToken();
            if (key.TokenType != TokenType.ID)
                ExceptionHelper.ThrowExpectToken(TokenType.ID, key.Position);
            Token<TokenType> colon = GetNextToken();
            if (colon.TokenType != TokenType.Colon)
                ExceptionHelper.ThrowExpectToken(TokenType.Colon, colon.Position);
            Node val = Value();
            kvpairs.Add(key.TokenValue, val);
        }
    }
}
