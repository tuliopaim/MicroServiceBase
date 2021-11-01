using System;
using System.ComponentModel;
using System.Globalization;

namespace MSBase.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T e) where T : Enum, IConvertible
        {
            var description = e.ToString();

            var type = e.GetType();
            var values = Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val != e.ToInt32(CultureInfo.InvariantCulture)) continue;

                var memInfo = type.GetMember(type.GetEnumName(val));
                var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (descriptionAttributes.Length > 0)
                    description = ((DescriptionAttribute)descriptionAttributes[0]).Description;

                break;
            }
            
            return description;
        }
    }
}