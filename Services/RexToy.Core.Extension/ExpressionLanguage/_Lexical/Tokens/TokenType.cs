using System;

namespace RexToy.ExpressionLanguage.Tokens
{
    public enum TokenType
    {
        None,

        ID,
        String,//Note:Derive from ID
        Long,//Note:Derive from ID
        Decimal,//Note:Derive from ID
        Array,//Note:Derive from ID, keyword
        Hash,//Note:Derive from ID, keyword
        Regex,//Note:Derive from ID, keyword
        New,//Note:Derive from ID, keyword

        Boolean,//Note:Derive from ID
        Left_Parenthesis,
        Right_Parenthesis,
        Left_Square_Bracket,
        Right_Square_Bracket,
        Dot,
        Colon,
        QuestionMark,
        Comma,
        Operator,
        LogicalOperator,//Note:Derive from Operator
        ArithmeticOperator,//Note:Derive from Operator
        CompareOperator,//Note:Derive from Operator
        ClassQualifier//Note: double colon,'::'
    }
}
