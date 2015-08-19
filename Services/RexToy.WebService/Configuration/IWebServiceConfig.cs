using System;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy.WebService.Configuration
{
    public interface IWebServiceConfig
    {
        string BaseUrl { get; }
        Assembly[] Assemblies { get; }
    }
}
