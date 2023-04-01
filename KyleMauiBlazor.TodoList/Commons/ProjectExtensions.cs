using System;
using System.ComponentModel;

namespace KyleMauiBlazor.TodoList.Commons
{
    public static class ProjectExtensions
    {

        public static string GetDescription(this Enum enumValue)
        {
            var value = enumValue.ToString();
            var field = enumValue.GetType().GetField(value);

            var objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (objs is null || !objs.Any()) return string.Empty;

            var attribute = (DescriptionAttribute)objs[0];
            return attribute.Description;
        }


    }
}

