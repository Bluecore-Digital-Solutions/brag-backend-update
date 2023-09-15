using System;
using System.Collections.Generic;
using System.Text;

namespace Brag.SharedServices.Helpers
{
    public class GetEnumValue
    {
        public static string GetEnumStringValue(Enum e, int value)
        {
            var val = "";
            var enumValues = Enum.GetValues(e.GetType());
            foreach (var enumValue in enumValues)
            {
                if (Convert.ToInt32(enumValue).Equals(value))
                {
                    val = enumValue.ToString();
                    break;
                }

            }
            return val;
        }
        public static T GetEnumValue_<T>(int intValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val = ((T[])Enum.GetValues(typeof(T)))[0];

            foreach (T enumValue in (T[])Enum.GetValues(typeof(T)))
            {
                if (Convert.ToInt32(enumValue).Equals(intValue))
                {
                    val = enumValue;
                    break;
                }
            }
            return val;
        }
    }
}
