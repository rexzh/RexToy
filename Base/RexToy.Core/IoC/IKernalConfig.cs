using System;
using System.Collections.Generic;

using RexToy.Collections;
using RexToy.Configuration;

namespace RexToy.Configuration
{
    public interface IKernalConfig
    {
        string LoadComponentInfoPath(string kernalId);
        IReadOnlyDictionary<string, Type> LoadCustomizedPolicies();
    }
}
