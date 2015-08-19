using System;
using System.Collections.Generic;
using System.Globalization;

using RexToy.Resources;

namespace RexToy.L10N
{
    public interface ILocalization
    {
        CultureInfo CultureInfo { get; }
        ITargetLocator Locator { get; }
        void Initialize();
        string Localize(string str);
    }
}
