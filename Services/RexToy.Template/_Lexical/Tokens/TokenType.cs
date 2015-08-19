using System;

namespace RexToy.Template.Tokens
{
    public enum TokenType
    {
        None,
        Script,

        If,//Note:Derive from Script
        Else,//Note:Derive from Script
        
        For,//Note:Derive from Script

        Break,//Note:Derive from Script
        Continue,//Note:Derive from Script
        
        End,//Note:End if/End For, derive from script
        Include,
        Expression,//Note:Derive from Script

        Let,//Note:Derive From Script
        Remark,//Note:Derive From Script

        Text,
    }
}
