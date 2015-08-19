using System;

namespace RexToy.Template.Text
{
    static class ExceptionHelper
    {
        public static void ThrowArgumentNullException_IfNull(object arg, string argName)
        {
            if (arg == null)
                throw new ArgumentNullException(argName);
        }

        public static void ThrowInvalidName(string name)
        {
            throw new StringTemplateException(string.Format("[{0}] is not a valid name.", name));
        }

        public static void ThrowNameNotDefined(string name)
        {
            throw new StringTemplateException(string.Format("Name [{0}] is not defined in template.", name));
        }

        public static void ThrowMissingArgument(string name)
        {
            throw new StringTemplateException(string.Format("Value for [{0}] is not assigned.", name));
        }
    }
}
