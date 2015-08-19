using System;
using System.Collections.Generic;
using System.IO;

namespace RexToy.Resources
{
    public interface ITargetLocator
    {
        ITargetLocator Combine(string path);
        Stream GetStream(string path, bool throwOnNotFound = false);
        Stream GetStream(bool throwOnNotFound = false);
        IEnumerable<string> EnumItems();
    }
}
