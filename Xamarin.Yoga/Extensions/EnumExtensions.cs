using System;
using System.ComponentModel;

namespace Xamarin.Yoga
{
    public static class EnumExtensions
    {
        public static string ToDescription<T>(this T value) where T : struct
        {
            var type = value.GetType();
            if (!type.IsEnum)
                throw new ArgumentException("EnumerationValue must be of Enum type", nameof(value));

            var memberInfo = type.GetMember(value.ToString());
            if (memberInfo.Length <= 0)
                return value.ToString();

            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attrs.Length > 0 
                ? ((DescriptionAttribute)attrs[0]).Description 
                : value.ToString();
        }

    }
}
