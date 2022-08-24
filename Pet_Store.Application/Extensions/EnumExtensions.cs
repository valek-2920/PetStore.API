using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Application.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum obj)
        {
            var type = obj.GetType();
            var memberInfo = type.GetMember(obj.ToString());
            var attributes = memberInfo.FirstOrDefault().GetCustomAttributes
                (typeof(DescriptionAttribute), false);

            return
                attributes.Length > 0
                    ? ((DescriptionAttribute)attributes.FirstOrDefault()).Description
                    : obj.ToString();
        }
    }
}
