using System;
using System.Collections.Generic;
using System.IO;

namespace RexToy.Configuration
{
    class TextConfig : IConfig
    {
        private Dictionary<string, Dictionary<string, string>> _cfg;
        internal TextConfig(string path)
        {
            _cfg = new Dictionary<string, Dictionary<string, string>>();
            using (StreamReader r = new StreamReader(path))
            {
                string section = null;
                Dictionary<string, string> sect = null;
                do
                {
                    string line = r.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (line.BracketedBy(StringPair.SquareBracket))
                        {
                            section = line.UnBracketing(StringPair.SquareBracket);
                            sect = new Dictionary<string, string>();
                            _cfg.Add(section, sect);
                        }
                        else
                        {
                            string[] kvp = line.Split('=');
                            sect.Add(kvp[0].Trim(), kvp[1].Trim());
                        }
                    }
                } while (!r.EndOfStream);
            }
        }

        #region IConfig Members
        public string ReadValue(string section, string key)
        {
            try
            {
                return _cfg[section][key];
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.CreateWrapException(ex, section, key);
            }
        }

        public T ReadValue<T>(string section, string key) where T : struct
        {
            try
            {
                string val = _cfg[section][key];
                return TypeCast.ChangeType<T>(val);
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.CreateWrapException(ex, section, key);
            }
        }

        public T ReadEnumValue<T>(string section, string key) where T : struct
        {
            try
            {
                string val = _cfg[section][key];
                return EnumEx.Parse<T>(val);
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.CreateWrapException(ex, section, key);
            }
        }

        public Type ReadType(string section, string key)
        {
            try
            {
                string val = _cfg[section][key];
                if (val != null)
                    return Reflector.LoadType(val);
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.CreateWrapException(ex, section, key);
            }
        }

        public bool ExistsKey(string section, string key)
        {
            try
            {
                return _cfg.ContainsKey(section) && _cfg[section].ContainsKey(key);
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.CreateWrapException(ex, section, key);
            }
        }

        public bool ExistsSection(string section)
        {
            try
            {
                return _cfg.ContainsKey(section);
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.CreateWrapException(ex, section);
            }
        }

        public IEnumerable<string> GetAllKeysInSection(string section)
        {
            try
            {
                var sect = _cfg[section];
                return sect.Keys;
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.CreateWrapException(ex, section);
            }
        }

        public string TryReadValue(string section, string key)
        {
            if (ExistsKey(section, key))
                return _cfg[section][key];
            else
                return null;
        }

        public T? TryReadValue<T>(string section, string key) where T : struct
        {
            if (ExistsKey(section, key))
            {
                string val = _cfg[section][key];
                return TypeCast.ChangeType<T>(val);
            }                
            else
                return default(T);
        }

        public T? TryReadEnumValue<T>(string section, string key) where T : struct
        {
            if (ExistsKey(section, key))
            {
                string val = _cfg[section][key];
                return EnumEx.Parse<T>(val);
            }
            else
                return default(T);
        }
        #endregion
    }
}
