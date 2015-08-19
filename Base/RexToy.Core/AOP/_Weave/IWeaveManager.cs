using System;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy.AOP
{
    public interface IWeaveManager
    {
        void ReadConfig();
        T GetAdvisor<T>(string name) where T : IAdvisor;
        string[] GetAdvisorNames(IMethodCallContext ctx);
    }
}
