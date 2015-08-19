using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RexToy.Template.Text
{
    public class StringTemplate
    {
        public static implicit operator StringTemplate(string template)
        {
            ExceptionHelper.ThrowArgumentNullException_IfNull(template, "template");
            return new StringTemplate(template);
        }

        private string _template;
        private Dictionary<string, string> _args;
        private StringTemplate(string s)
        {
            this._template = s;
            this._args = new Dictionary<string, string>();
            Scan();
        }

        private void Scan()
        {
            string pattern = @"([^#]|^)#\{(?<param>[\w\W]*?)\}";
            MatchCollection matchColl = Regex.Matches(_template, pattern);
            foreach (Match match in matchColl)
            {
                string name = match.Groups["param"].Value;

                if (Regex.Match(name, @"\s").Success)
                    ExceptionHelper.ThrowInvalidName(name);

                _args[name] = null;
            }
        }

        public void Assign(string name, string val, bool throwOnNotDefine = true)
        {
            if (!_args.ContainsKey(name) && throwOnNotDefine)
                ExceptionHelper.ThrowNameNotDefined(name);
            _args[name] = val;
        }

        public void Clear()
        {
            string[] keys = _args.Keys.ToArray();
            foreach (string key in keys)
            {
                _args[key] = null;
            }
        }

        private string Replace(string template, string name, string val)
        {
            string pattern = @"(?<pre>[^#]|^)#\{" + name + @"\}";
            Regex re = new Regex(pattern);
            return re.Replace(template, "${pre}" + val);
        }

        public string Render(bool allowMissing = false)
        {
            string result = _template;
            foreach (var kvp in _args)
            {
                if (kvp.Value == null)
                {
                    if (allowMissing)
                        continue;
                    else
                        ExceptionHelper.ThrowMissingArgument(kvp.Key);
                }
                result = Replace(result, kvp.Key, kvp.Value);
            }

            //Note:Final process: escape ##{} to #{}
            string pattern = @"##\{([\w\W]*?)\}";
            Regex re = new Regex(pattern);
            return re.Replace(result, @"#{$1}");
        }
    }
}
