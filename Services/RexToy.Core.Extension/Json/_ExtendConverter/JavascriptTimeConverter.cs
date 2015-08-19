using System;
using System.Collections.Generic;
using System.Text;

namespace RexToy.Json
{
    public class JavascriptTimeConverter : IExtendConverter
    {
        public static readonly long DateTimeMinTimeTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;

        #region IExtendConverter Members

        public bool CanConvert(Type t)
        {
            return t == typeof(DateTime) || t == typeof(DateTime?);
        }

        public string ToJsonString(object instance)
        {
            if (instance == null)
                return null;

            DateTime time = (DateTime)instance;
            return ((time.ToUniversalTime().Ticks - DateTimeMinTimeTicks) / 100).ToString();
        }

        public object FromJson(Type t, object obj, bool ignoreTypeSafe = false)
        {
            if (obj == null)
                return null;

            var str = obj as string;
            if (str != null)
                return new DateTime(long.Parse(str) * 100 + DateTimeMinTimeTicks).ToLocalTime();

            ExceptionHelper.ThrowExtendConverterUnknownFormat(this.GetType(), obj.GetType());
            return null;
        }

        #endregion
    }
}
