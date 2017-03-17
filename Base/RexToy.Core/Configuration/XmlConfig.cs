using System;
using System.Collections.Generic;

using RexToy.DesignPattern;
using RexToy.Xml;

namespace RexToy.Configuration
{
    class XmlConfig : IConfig
    {
        private const string GET_KEY = "@key";
        private const string QUERY_BY_SECTION = "section[@name='{0}']";
        private const string QUERY_BY_SECTION_KEY = "section[@name='{0}']/set[@key='{1}']";
        private const string GET_VALUE = "section[@name='{0}']/set[@key='{1}']/@value";

        private XDoc _x;
        internal XmlConfig(string path)
        {
            _x = XDoc.LoadFromFile(path);
        }

        #region IConfig Members

        public string ReadValue(string section, string key)
        {
            section.ThrowIfNullArgument(nameof(section));
            key.ThrowIfNullArgument(nameof(key));

            try
            {
                string xpath = string.Format(GET_VALUE, section, key);
                return _x.NavigateToSingle(xpath).GetStringValue();
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.CreateWrapException(ex, section, key);
            }
        }

        public string TryReadValue(string section, string key)
        {
            section.ThrowIfNullArgument(nameof(section));
            key.ThrowIfNullArgument(nameof(key));

            try
            {
                string xpath = string.Format(GET_VALUE, section, key);
                return _x.NavigateToSingleOrNull(xpath)?.GetStringValue();
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.CreateWrapException(ex, section, key);
            }
        }

        public T ReadValue<T>(string section, string key) where T : struct
        {
            section.ThrowIfNullArgument(nameof(section));
            key.ThrowIfNullArgument(nameof(key));

            try
            {
                string xpath = string.Format(GET_VALUE, section, key);
                return _x.NavigateToSingle(xpath).GetValue<T>().Value;
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.CreateWrapException(ex, section, key);
            }
        }

        public T? TryReadValue<T>(string section, string key) where T : struct
        {
            section.ThrowIfNullArgument(nameof(section));
            key.ThrowIfNullArgument(nameof(key));

            try
            {
                string xpath = string.Format(GET_VALUE, section, key);
                return _x.NavigateToSingleOrNull(xpath)?.GetValue<T>();
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.CreateWrapException(ex, section, key);
            }
        }

        public T ReadEnumValue<T>(string section, string key) where T : struct
        {
            section.ThrowIfNullArgument(nameof(section));
            key.ThrowIfNullArgument(nameof(key));

            try
            {
                string xpath = string.Format(GET_VALUE, section, key);
                return _x.NavigateToSingle(xpath).GetEnumValue<T>().Value;
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.CreateWrapException(ex, section, key);
            }
        }

        public T? TryReadEnumValue<T>(string section, string key) where T : struct
        {
            section.ThrowIfNullArgument(nameof(section));
            key.ThrowIfNullArgument(nameof(key));

            try
            {
                string xpath = string.Format(GET_VALUE, section, key);
                return _x.NavigateToSingleOrNull(xpath)?.GetEnumValue<T>();
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
                string xpath = string.Format(GET_VALUE, section, key);
                string str = _x.GetStringValue(xpath);
                if (string.IsNullOrEmpty(str))
                    return null;
                else
                    return Reflector.LoadType(str);
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.CreateWrapException(ex, section, key);
            }
        }

        public bool ExistsKey(string section, string key)
        {
            section.ThrowIfNullArgument(nameof(section));
            key.ThrowIfNullArgument(nameof(key));

            try
            {
                string xpath = string.Format(QUERY_BY_SECTION_KEY, section, key);
                return _x.NavigateToSingleOrNull(xpath) != null;
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.CreateWrapException(ex, section, key);
            }
        }

        public bool ExistsSection(string section)
        {
            section.ThrowIfNullArgument(nameof(section));

            try
            {
                string xpath = string.Format(QUERY_BY_SECTION, section);
                return _x.NavigateToSingleOrNull(xpath) != null;
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.CreateWrapException(ex, section);
            }
        }

        public IEnumerable<string> GetAllKeysInSection(string section)
        {
            section.ThrowIfNullArgument(nameof(section));

            try
            {
                string xpath = string.Format(QUERY_BY_SECTION, section);
                var xSect = _x.NavigateToSingle(xpath);

                List<string> keys = new List<string>();
                foreach (var k in xSect.Children)
                {
                    if (k.IsComment)
                        continue;
                    string key = k.GetStringValue(GET_KEY);
                    if (key != null)
                        keys.Add(key);
                }

                return keys;
            }
            catch (Exception ex)
            {
                throw ExceptionHelper.CreateWrapException(ex, section);
            }
        }

        #endregion
    }
}
