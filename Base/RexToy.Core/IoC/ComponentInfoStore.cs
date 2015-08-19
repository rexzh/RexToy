using System;
using System.Collections.Concurrent;
using System.IO;

using RexToy.Xml;

namespace RexToy.IoC
{
    class ComponentInfoStore : IComponentInfoStore
    {
        private ConcurrentDictionary<string, IComponentInfo> _idInfoDict;
        private ConcurrentDictionary<Type, string> _serviceIdDict;
        public ComponentInfoStore()
        {
            _idInfoDict = new ConcurrentDictionary<string, IComponentInfo>();
            _serviceIdDict = new ConcurrentDictionary<Type, string>();
        }

        #region IComponentInfoStore Members

        public IComponentInfo FindById(string id)
        {
            if (!_idInfoDict.ContainsKey(id))
                ExceptionHelper.ThrowIdNotFound(id);
            return _idInfoDict[id];
        }

        public IComponentInfo FindByContract(Type type)
        {
            if (!_serviceIdDict.ContainsKey(type))
            {
                ExceptionHelper.ThrowServiceNotFound(type);
            }
            string id = _serviceIdDict[type];
            if (id.Length != 0)
                return _idInfoDict[id];
            else
            {
                ExceptionHelper.ThrowServiceMultiFound(type);
            }
            return null;
        }

        public void AddComponentInfo(IComponentInfo info)
        {
            if (_idInfoDict.ContainsKey(info.Id))
            {
                ExceptionHelper.ThrowDuplicateComponentId(info.Id);
            }
            else
            {
                _idInfoDict[info.Id] = info;
            }

            if (info.ServiceType == null)
                return;
            if (_serviceIdDict.ContainsKey(info.ServiceType))
            {
                _serviceIdDict[info.ServiceType] = string.Empty;
            }
            else
            {
                _serviceIdDict[info.ServiceType] = info.Id;
            }
        }

        #endregion
    }
}
