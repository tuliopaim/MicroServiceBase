using System.ComponentModel;
using System.Globalization;

namespace MSBase.Core.Extensions;

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

    public static List<T> ToList<T>(this T _) where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToList();
    }

    public static List<string> ToStringList<T>(this T e) where T : Enum
    {
        return e.ToList().Select(x => x.ToString()).ToList();
    }
}
