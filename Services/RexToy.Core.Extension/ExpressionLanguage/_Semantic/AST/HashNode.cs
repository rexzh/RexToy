using System;
using System.Collections.Generic;

using RexToy.Collections;
using RexToy.Compiler.Semantic;

namespace RexToy.ExpressionLanguage.AST
{
    public class HashNode : Node
    {
        private Dictionary<string, Node> _dict;
        public IReadOnlyDictionary<string, Node> KVPairs
        {
            get { return _dict; }
        }

        public HashNode(Dictionary<string, Node> dict)
        {
            dict.ThrowIfNullArgument(nameof(dict));
            _dict = dict;
        }
    }
}
