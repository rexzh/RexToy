using System;
using System.Collections.Generic;

namespace RexToy.Resources
{
    public static class LocatorFactory
    {
        public static ITargetLocator Create(string uri)
        {
            uri.ThrowIfNullArgument(nameof(uri));

            if (uri.StartsWith(LocalFilePath.LOCAL_FILE_SCHEME))
                return new LocalFileSystemLocator(uri.RemoveBegin(LocalFilePath.LOCAL_FILE_SCHEME));
            if (uri.StartsWith(ClrClassPath.CLR_SCHEME))
                return new CLRLocator(uri.RemoveBegin(ClrClassPath.CLR_SCHEME));
            ExceptionHelper.ThrowUnknowSchema(uri);
            return null;
        }
    }
}
