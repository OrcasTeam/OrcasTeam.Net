using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OrcasTeam.Shandard.Libary.Extensions.Attribute
{
   public static partial class AttributeExtension
    {
        public static bool HasAttribute<T>(this MemberInfo type, bool inherit = false)
            => type.GetCustomAttribute(typeof(T), inherit) != null;


        public static bool HasAttribute<T>(this object obj, bool inherit = false)
            => HasAttribute<T>(obj.GetType(), inherit);
    }
}
