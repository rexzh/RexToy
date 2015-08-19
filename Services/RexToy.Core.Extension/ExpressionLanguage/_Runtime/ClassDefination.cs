using System;

namespace RexToy.ExpressionLanguage
{
    class ClassDefination
    {
        internal ClassDefination(Type type)
        {
            _type = type;
        }

        private Type _type;
        public Type ObjType 
        {
            get { return _type; } 
        }
    }
}
