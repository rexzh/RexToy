using System;
using System.Collections.Generic;

using RexToy.Xml;
using RexToy.Configuration;

namespace RexToy.AOP
{
    public class WeaveManager : IWeaveManager
    {
        private const string CLASS = "class";
        private const string POSITION = "position";
        private const string EXECUTE = "execute";
        private const string PATTERN = "pattern";
        private const string JOIN_POINT = "join-point";
        private const string ADVISOR = "advisor";
        private const string NAME_ATTR = "@name";

        private Dictionary<string, Type> _advisors;
        private List<JoinPointDefination> _jointPointDefinations;
        public WeaveManager()
        {
            _advisors = new Dictionary<string, Type>();
            _jointPointDefinations = new List<JoinPointDefination>();
        }

        public void ReadConfig()
        {
            IAOPConfig cfg = AOPConfig.AOPConfiguration;
            XDoc doc = XDoc.LoadFromFile(cfg.LoadAOPInfoPath());

            var xAdvisors = doc.NavigateToList(ADVISOR);
            foreach (var xAdvisor in xAdvisors)
            {
                string name = xAdvisor.GetStringValue(NAME_ATTR);
                Type type = Reflector.LoadType(xAdvisor.GetStringValue(CLASS));
                _advisors.Add(name, type);
            }

            var xJoinPoints = doc.NavigateToList(JOIN_POINT);
            foreach (var xJoinPoint in xJoinPoints)
            {
                Position position = EnumEx.Parse<Position>(xJoinPoint.GetStringValue(POSITION), true);
                string pattern = xJoinPoint.GetStringValue(PATTERN);
                string advisorName = xJoinPoint.GetStringValue(EXECUTE);
                _jointPointDefinations.Add(new JoinPointDefination(position, advisorName, pattern));
            }
        }

        #region IWeaveManager Members

        public T GetAdvisor<T>(string name) where T : IAdvisor
        {
            Type type = _advisors[name];
            object advisor = Activator.CreateInstance(type);
            return (T)advisor;
        }

        public string[] GetAdvisorNames(IMethodCallContext ctx)
        {
            List<string> names = new List<string>();
            foreach (var defination in _jointPointDefinations)
            {
                if (defination.Match(ctx))
                    names.Add(defination.AdvisorName);
            }
            return names.ToArray();
        }

        #endregion
    }
}
