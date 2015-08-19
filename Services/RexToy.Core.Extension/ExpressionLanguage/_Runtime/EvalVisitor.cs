using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

using RexToy.Collections;
using RexToy.DesignPattern;

namespace RexToy.ExpressionLanguage
{
    using RexToy.ExpressionLanguage.AST;
    using RexToy.ExpressionLanguage.Tokens;

    internal class EvalVisitor : VisitorBase<object, IndexerNode, MethodNode, OperatorNode, ParamListNode, PropertyNode, SimpleNode, UnaryNode, TernaryNode, ArrayNode, HashNode, AutoIntArrayNode, ClassQualifierNode, RegexNode, NewNode>
    {
        private IEvalContext _ctx;
        public EvalVisitor(IEvalContext ctx)
        {
            _ctx = ctx;
        }

        public override object Visit(TernaryNode obj)
        {
            object condition = obj.Condition.Accept(this);
            bool b = ScriptTypeUtil.EvalToBoolean(condition);
            return b ? obj.TrueValue.Accept(this) : obj.FalseValue.Accept(this);
        }

        public override object Visit(UnaryNode obj)
        {
            object val = obj.OperandNode.Accept(this);

            if (obj.Token.TokenValue == "!")
            {
                bool b = ScriptTypeUtil.EvalToBoolean(val);
                return !b;
            }

            if (val == null)
                ExceptionHelper.ThrowEvalNull();

            if (obj.Token.TokenValue == "+")
                return val;

            if (obj.Token.TokenValue == "-")
            {
                if (ScriptTypeUtil.IsDecimal(val))
                {
                    decimal result = ScriptTypeUtil.ConvertToDecimal(val);
                    return -result;
                }

                if (ScriptTypeUtil.IsLong(val))
                {
                    long result = ScriptTypeUtil.ConvertToLong(val);
                    return -result;
                }
            }

            ExceptionHelper.ThrowUnaryOperatorInvalid(obj.Token.TokenValue, val);
            return null;
        }

        public override object Visit(SimpleNode obj)
        {
            switch (obj.Token.TokenType)
            {
                case TokenType.String:
                    return obj.Token.TokenValue;

                case TokenType.Boolean:
                    return bool.Parse(obj.Token.TokenValue);

                case TokenType.Long:
                    return long.Parse(obj.Token.TokenValue);

                case TokenType.Decimal:
                    return decimal.Parse(obj.Token.TokenValue);

                case TokenType.ID:
                    return _ctx.Resolve(obj.Token.TokenValue);
            }

            ExceptionHelper.ThrowInvalidToken(obj.Token.TokenType);
            return null;
        }

        public override object Visit(PropertyNode obj)
        {
            object instance = obj.Variable.Accept(this);
            if (instance == null)
                ExceptionHelper.ThrowEvalNull();

            string propertyName = (obj.Property as SimpleNode).Token.TokenValue;
            IReflector r;
            ClassDefination cdef = instance as ClassDefination;
            if (cdef != null)
                r = Reflector.Bind(cdef.ObjType, ReflectorPolicy.CreateInstance(false, true, false, false));
            else
                r = Reflector.Bind(instance, ReflectorPolicy.CreateInstance(false, true, false, true));

            if (r.ExistProperty(propertyName))//Note:Property first
                return r.GetPropertyValue(propertyName);
            else if (r.ExistField(propertyName))//Note:Field 2nd
                return r.GetFieldValue(propertyName);
            else//Note:Then try indexer
            {
                try
                {
                    return r.Invoke("get_Item", new object[] { propertyName });
                }
                catch
                {
                    //Note:Try indexer failed.
                    ExceptionHelper.ThrowIndexOrPropertyNotExist(instance.GetType(), propertyName);
                    return null;
                }
            }
        }

        public override object Visit(ParamListNode obj)
        {
            object[] array = new object[obj.Values.Count];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = obj.Values[i].Accept(this);
            }
            return array;
        }

        private object Arithmetic(OperatorNode obj)
        {
            object lhs = obj.Lhs.Accept(this);
            object rhs = obj.Rhs.Accept(this);

            //Note:string concat
            if (obj.Token.TokenValue == "+" && (lhs is string || rhs is string))
            {
                string result = string.Empty;
                if (lhs != null)
                    result += lhs.ToString();
                if (rhs != null)
                    result += rhs.ToString();
                return result;
            }

            //Note:string multiple
            if (obj.Token.TokenValue == "*" && lhs is string && ScriptTypeUtil.IsLong(rhs))
            {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < (long)rhs; i++)
                {
                    result.Append(lhs);
                }
                return result.ToString();
            }

            //Note:Only number is valid currently, everything will convert to decimal
            decimal lNumber = ScriptTypeUtil.ConvertToDecimal(lhs);
            decimal rNumber = ScriptTypeUtil.ConvertToDecimal(rhs);
            switch (obj.Token.TokenValue)
            {
                case "+":
                    return lNumber + rNumber;

                case "-":
                    return lNumber - rNumber;

                case "*":
                    return lNumber * rNumber;

                case "/":
                    return lNumber / rNumber;

                case "%":
                    return lNumber % rNumber;

                case "^":
                    double b = double.Parse(lNumber.ToString());
                    double x = double.Parse(rNumber.ToString());
                    return (decimal)Math.Pow(b, x);
            }

            ExceptionHelper.ThrowBinaryOperatorInvalid(obj.Token.TokenValue, lhs, rhs);
            return null;
        }

        private bool Compare(OperatorNode obj)
        {
            object lhs = obj.Lhs.Accept(this);
            object rhs = obj.Rhs.Accept(this);

            if (ScriptTypeUtil.IsNumber(lhs) && ScriptTypeUtil.IsNumber(rhs))
            {
                decimal lNumber = ScriptTypeUtil.ConvertToDecimal(lhs);
                decimal rNumber = ScriptTypeUtil.ConvertToDecimal(rhs);
                switch (obj.Token.TokenValue)
                {
                    case ">":
                        return lNumber > rNumber;

                    case "<":
                        return lNumber < rNumber;

                    case ">=":
                        return lNumber >= rNumber;

                    case "<=":
                        return lNumber <= rNumber;

                    case "==":
                        return lNumber == rNumber;

                    case "<>":
                    case "!=":
                        return lNumber != rNumber;
                }
            }

            //Note: If compare equality, null is OK.

            switch (obj.Token.TokenValue)
            {
                case "==":
                    if (lhs == null)
                        return rhs == null;
                    else
                        return lhs.Equals(rhs);

                case "<>":
                case "!=":
                    if (lhs == null)
                        return rhs != null;
                    else
                        return !(lhs.Equals(rhs));
            }

            ExceptionHelper.ThrowBinaryOperatorInvalid(obj.Token.TokenValue, lhs, rhs);
            return false;
        }

        private bool Logic(OperatorNode obj)
        {
            bool lhs = ScriptTypeUtil.EvalToBoolean(obj.Lhs.Accept(this));
            //Note: Short cut Rhs
            if (lhs && obj.Token.TokenValue == "||")
                return true;
            if (!lhs && obj.Token.TokenValue == "&&")
                return false;
            //Note: Eval Rhs
            bool rhs = ScriptTypeUtil.EvalToBoolean(obj.Rhs.Accept(this));
            if (obj.Token.TokenValue == "||")
                return lhs || rhs;
            if (obj.Token.TokenValue == "&&")
                return lhs && rhs;

            ExceptionHelper.ThrowBinaryOperatorInvalid(obj.Token.TokenValue, lhs, rhs);
            return false;
        }

        public override object Visit(OperatorNode obj)
        {
            switch (obj.Token.TokenType)
            {
                case TokenType.LogicalOperator:
                    return Logic(obj);

                case TokenType.CompareOperator:
                    return Compare(obj);

                case TokenType.ArithmeticOperator:
                    return Arithmetic(obj);
            }

            ExceptionHelper.ThrowBinaryOperatorInvalid(obj.Token.TokenValue, obj.Lhs.GetType(), obj.Rhs.GetType());
            return null;
        }

        public override object Visit(MethodNode obj)
        {
            object instance;
            if (obj.Variable != null)
                instance = obj.Variable.Accept(this);
            else
                instance = _ctx.Resolve("this");//Note:treat it as part of host class

            string methodName = (obj.Method as SimpleNode).Token.TokenValue;
            object[] paramArray = (object[])obj.Args.Accept(this);

            if (instance == null)
                ExceptionHelper.ThrowEvalNull();

            ISmartInvoker invoker;
            ClassDefination cdef = instance as ClassDefination;
            if (cdef != null)
                invoker = EvalSmartInvoker.CreateInstance(cdef.ObjType, false);
            else
                invoker = EvalSmartInvoker.CreateInstance(instance, true);
            return invoker.Invoke(methodName, paramArray);
        }

        public override object Visit(IndexerNode obj)
        {
            object instance = obj.Variable.Accept(this);
            object[] paramArray = (object[])obj.Args.Accept(this);

            if (instance == null)
                ExceptionHelper.ThrowEvalNull();

            ISmartInvoker invoker;
            ClassDefination cdef = instance as ClassDefination;
            if (cdef != null)
                invoker = EvalSmartInvoker.CreateInstance(cdef.ObjType, false);
            else
                invoker = EvalSmartInvoker.CreateInstance(instance, true);

            if (instance.GetType().IsArray)//Note:invoke "Get" on array
            {
                return invoker.Invoke("Get", paramArray);
            }
            else//Note:invoke indexer on instance
            {
                try//Note:Try indexer first
                {
                    return invoker.Invoke("get_Item", paramArray);
                }
                catch//Note:then try property
                {
                    if (paramArray.Length == 1 && paramArray[0] != null)
                    {
                        string propertyName = paramArray[0].ToString();
                        IReflector r = Reflector.Bind(instance);
                        if (r.ExistProperty(propertyName))
                            return r.GetPropertyValue(propertyName);
                        else if (r.ExistField(propertyName))
                            return r.GetFieldValue(propertyName);
                        else
                        {
                            ExceptionHelper.ThrowIndexOrPropertyNotExist(instance.GetType(), propertyName);
                        }
                    }
                    //Note:Can not convert to property, throw
                    throw;
                }
            }
        }

        public override object Visit(ArrayNode obj)
        {
            object[] array = new object[obj.Elements.Count];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = obj.Elements[i].Accept(this);
            }
            return array;
        }

        public override object Visit(HashNode obj)
        {
            StringKeyDictionary<object> tuple = new StringKeyDictionary<object>();
            foreach (var kvp in obj.KVPairs)
            {
                tuple.Add(kvp.Key, kvp.Value.Accept(this));
            }
            return tuple;
        }

        public override object Visit(AutoIntArrayNode obj)
        {
            object begin = obj.Begin.Accept(this);
            object end = obj.End.Accept(this);
            long a = ScriptTypeUtil.ConvertToLong(begin);
            long b = ScriptTypeUtil.ConvertToLong(end);
            int step = (a <= b ? 1 : -1);
            long len = Math.Abs(a - b) + 1;
            long[] array = new long[len];
            for (long idx = 0; idx < len; idx++)
                array[idx] = a + step * idx;
            return array;
        }

        public override object Visit(ClassQualifierNode obj)
        {
            string name = obj.ClassName;
            Type type = Type.GetType(name);
            if (type != null)
                return new ClassDefination(type);
            else
            {
                foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    type = a.GetType(name);
                    if (type != null)
                        return new ClassDefination(type);
                }
            }

            ExceptionHelper.ThrowEvalToTypeError(name);
            return null;
        }

        public override object Visit(RegexNode obj)
        {
            Regex regex = new Regex(obj.Pattern);
            return regex;
        }

        public override object Visit(NewNode obj)
        {
            ClassDefination cd = (ClassDefination)obj.Type.Accept(this);
            object[] paramList = (object[])obj.CtorParams.Accept(this);

            ISmartInvoker s = EvalSmartInvoker.CreateInstance(cd.ObjType, false);
            return s.InvokeConstructor(paramList);
        }
    }
}