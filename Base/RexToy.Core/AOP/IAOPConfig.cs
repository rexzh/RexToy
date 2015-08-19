using System;
using System.Collections.Generic;

using RexToy.Configuration;

namespace RexToy.Configuration
{
    public interface IAOPConfig
    {
        string LoadAOPInfoPath();
        Type LoadSinkFactory();
    }
}
