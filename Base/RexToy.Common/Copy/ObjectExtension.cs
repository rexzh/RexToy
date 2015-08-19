using System;
using System.Collections.Generic;

using RexToy.Logging;

namespace RexToy.Copy
{
    public static class ObjectExtension
    {
        //private static ILog _log = LogContext.GetLogger("RexToy.Copy");

        public static void ShallowCopy(this object src, object dest, CopyOptions option = CopyOptions.BaseOnBoth, bool throwOnNotExist = true)
        {
            src.ThrowIfNullArgument(nameof(src));
            dest.ThrowIfNullArgument(nameof(dest));

            option.ThrowIfEnumOutOfRange();
            switch (option)
            {
                case CopyOptions.BaseOnBoth:
                    CopyByBoth(src, dest);
                    break;

                case CopyOptions.BaseOnSource:
                    CopyBySource(src, dest, throwOnNotExist);
                    break;

                case CopyOptions.BaseOnDest:
                    CopyByDest(src, dest, throwOnNotExist);
                    break;
            }
        }

        private static void CopyBySource(object src, object dest, bool throwOnNotExist)
        {
            IReflector rSrc = Reflector.Bind(src);
            IReflector rDest = Reflector.Bind(dest);

            foreach (var pName in rSrc.FindAllPropertyNames())
            {
                if (!rDest.ExistProperty(pName))
                {
                    if (throwOnNotExist)
                        ExceptionHelper.ThrowDestPropertyNotExist(dest.GetType(), pName);
                }

                rDest.SetPropertyValue(pName, rSrc.GetPropertyValue(pName));
            }
        }

        private static void CopyByDest(object src, object dest, bool throwOnNotExist)
        {
            IReflector rSrc = Reflector.Bind(src);
            IReflector rDest = Reflector.Bind(dest);
            foreach (var pName in rDest.FindAllPropertyNames())
            {
                if (!rSrc.ExistProperty(pName))
                {
                    if (throwOnNotExist)
                        ExceptionHelper.ThrowSourceKeyOrPropertyNotExist(pName);
                }

                rDest.SetPropertyValue(pName, rSrc.GetPropertyValue(pName));
            }
        }

        private static void CopyByBoth(object src, object dest)
        {
            IReflector rSrc = Reflector.Bind(src);
            IReflector rDest = Reflector.Bind(dest);

            foreach (var pName in rSrc.FindAllPropertyNames())
            {
                if (rDest.ExistProperty(pName))
                {
                    rDest.SetPropertyValue(pName, rSrc.GetPropertyValue(pName));
                }
            }
        }
    }
}
