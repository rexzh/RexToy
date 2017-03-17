using System;
using System.Collections.Generic;

namespace RexToy.Configuration
{
    public interface IConfig
    {
        string ReadValue(string section, string key);
        string TryReadValue(string section, string key);
        T ReadValue<T>(string section, string key) where T : struct;
        T ReadEnumValue<T>(string section, string key) where T : struct;
        T? TryReadValue<T>(string section, string key) where T : struct;
        T? TryReadEnumValue<T>(string section, string key) where T : struct;
        Type ReadType(string section, string key);

        bool ExistsKey(string section, string key);
        bool ExistsSection(string section);
        IEnumerable<string> GetAllKeysInSection(string section);
    }
}
