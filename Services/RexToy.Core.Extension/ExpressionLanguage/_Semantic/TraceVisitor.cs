using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using RexToy.Logging;
using RexToy.DesignPattern;
using RexToy.Compiler.Semantic;

namespace RexToy.ExpressionLanguage
{
    using RexToy.ExpressionLanguage.AST;

    internal class TraceVisitor : VisitorBase<int, IndexerNode, MethodNode, OperatorNode, ParamListNode, PropertyNode, SimpleNode, UnaryNode, TernaryNode, ArrayNode, HashNode, AutoIntArrayNode, ClassQualifierNode, RegexNode, NewNode>
    {
        private const int IDENT = 6;
        private const char SPACE = ' ';
        private static ILog _log = LogContext.GetLogger<TraceVisitor>();

        private int callLevel;
        public TraceVisitor()
        {
        }

        public override int Visit(TernaryNode obj)
        {
            callLevel++;
            _log.Debug(new string(SPACE, callLevel * IDENT) + "Ternary->condition");
            obj.Condition.Accept(this);
            _log.Debug(new string(SPACE, callLevel * IDENT) + "Ternary->true");
            obj.TrueValue.Accept(this);
            _log.Debug(new string(SPACE, callLevel * IDENT) + "Ternary->false");
            obj.FalseValue.Accept(this);
            _log.Debug(new string(SPACE, callLevel * IDENT) + "Ternary<-");
            callLevel--;

            return callLevel;
        }

        public override int Visit(UnaryNode obj)
        {
            callLevel++;
            _log.Debug(new string(SPACE, callLevel * IDENT) + "Unary->" + obj.Token.TokenValue);

            obj.OperandNode.Accept(this);

            _log.Debug(new string(SPACE, callLevel * IDENT) + "Unary<-");
            callLevel--;

            return callLevel;
        }

        public override int Visit(SimpleNode obj)
        {
            callLevel++;
            _log.Debug(new string(SPACE, callLevel * IDENT) + "Simple->" + obj.Token.TokenValue);

            callLevel--;

            return callLevel;
        }

        public override int Visit(PropertyNode obj)
        {
            callLevel++;
            _log.Debug(new string(SPACE, callLevel * IDENT) + "Property->");

            obj.Variable.Accept(this);
            obj.Property.Accept(this);

            _log.Debug(new string(SPACE, callLevel * IDENT) + "Property<-");
            callLevel--;

            return callLevel;
        }

        public override int Visit(ParamListNode obj)
        {
            callLevel++;

            foreach (Node node in obj.Values)
                node.Accept(this);

            callLevel--;

            return callLevel;
        }

        public override int Visit(OperatorNode obj)
        {
            callLevel++;
            _log.Debug(new string(SPACE, callLevel * IDENT) + "Operator->[" + obj.Token.TokenValue + "]");

            obj.Lhs.Accept(this);
            obj.Rhs.Accept(this);

            _log.Debug(new string(SPACE, callLevel * IDENT) + "Operator<-");
            callLevel--;

            return callLevel;
        }

        public override int Visit(MethodNode obj)
        {
            callLevel++;
            _log.Debug(new string(SPACE, callLevel * IDENT) + "Method->");

            if (obj.Variable != null)
                obj.Variable.Accept(this);
            obj.Method.Accept(this);
            obj.Args.Accept(this);

            _log.Debug(new string(SPACE, callLevel * IDENT) + "Method<-");
            callLevel--;

            return callLevel;
        }

        public override int Visit(IndexerNode obj)
        {
            callLevel++;
            _log.Debug(new string(SPACE, callLevel * IDENT) + "Indexer->");

            obj.Variable.Accept(this);
            obj.Args.Accept(this);

            _log.Debug(new string(SPACE, callLevel * IDENT) + "Indexer<-");
            callLevel--;

            return callLevel;
        }

        public override int Visit(ArrayNode obj)
        {
            callLevel++;
            _log.Debug(new string(SPACE, callLevel * IDENT) + "Array->");

            foreach (Node node in obj.Elements)
                node.Accept(this);

            _log.Debug(new string(SPACE, callLevel * IDENT) + "Array<-");
            callLevel--;

            return callLevel;
        }

        public override int Visit(HashNode obj)
        {
            callLevel++;
            _log.Debug(new string(SPACE, callLevel * IDENT) + "Tuple->");

            callLevel++;
            foreach (var kvp in obj.KVPairs)
            {
                _log.Debug(new string(SPACE, callLevel * IDENT) + "pair->" + kvp.Key + ":");
                kvp.Value.Accept(this);
            }
            callLevel--;

            _log.Debug(new string(SPACE, callLevel * IDENT) + "Tuple<-");
            callLevel--;

            return callLevel;
        }

        public override int Visit(AutoIntArrayNode obj)
        {
            callLevel++;
            _log.Debug(new string(SPACE, callLevel * IDENT) + "AutoArray->");

            obj.Begin.Accept(this);
            obj.End.Accept(this);

            _log.Debug(new string(SPACE, callLevel * IDENT) + "AutoArray<-");
            callLevel--;

            return callLevel;
        }

        public override int Visit(ClassQualifierNode obj)
        {
            callLevel++;
            _log.Debug(new string(SPACE, callLevel * IDENT) + "ClassQualifier->" + obj.ClassName);
            callLevel--;
            return callLevel;
        }

        public override int Visit(RegexNode obj)
        {
            callLevel++;
            _log.Debug(new string(SPACE, callLevel * IDENT) + "Regex->" + obj.Pattern);
            callLevel--;
            return callLevel;
        }

        public override int Visit(NewNode obj)
        {
            callLevel++;

            _log.Debug(new string(SPACE, callLevel * IDENT) + "new->" + obj.Type.ClassName);
            obj.CtorParams.Accept(this);

            callLevel--;
            return callLevel;
        }
    }
}
