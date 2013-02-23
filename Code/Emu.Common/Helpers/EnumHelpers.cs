﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emu.Common
{
    public static class EnumHelpers
    {


        public static T ToEnum<T>(this int value)
        {
            return ToEnum(value, default(T));
        }

        public static T ToEnum<T>(this int value, T defaultValue)
        {
            if (Enum.IsDefined(typeof(T), value))
            {
                return (T)Enum.ToObject(typeof(T), value);
            }
            else
            {
                return defaultValue;
            }
        }

        public static Dictionary<int, string> ToKeyValuePair(this Enum enumValue)
        {
            Type enumType = enumValue.GetType();
            return Enum.GetValues(enumType)
                .Cast<int>()
                .ToDictionary(i => i, i => Enum.GetName(enumType, i));
                               

        }
    }
}
