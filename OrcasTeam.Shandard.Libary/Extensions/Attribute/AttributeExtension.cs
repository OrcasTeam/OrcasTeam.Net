using System;
using System.Reflection;

namespace OrcasTeam.Shandard.Libary.Extensions
{
    public static class AttributeExtension
    {
        /// <summary>
        ///     判断当前成员中是否具有指定特性类型
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="type"></param>
        /// <param name="inherit">
        ///     TRUE:去当前类型的基类查询
        ///     FALSE:不去当前类型基类查询
        ///     默认为FALSE
        /// </param>
        /// <returns>TRUE 存在/FALSE 不存在</returns>
        public static bool IsDefined<T>(this MemberInfo type, bool inherit = false)
            where T : Attribute
            => IsDefined(type, typeof(T), inherit);

        /// <summary>
        ///     判断当前当前中是否具有指定特性类型
        ///     Exception:
        ///         ArgumentException:传入的target类型并不是<see cref="Attribute"/>派生类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="target">
        /// </param>
        /// <param name="inherit"></param>
        /// <returns>TRUE 是/FALSE 不是</returns>
        public static bool IsDefined(this MemberInfo type, System.Type target, bool inherit = false)
        {
            if (!typeof(Attribute).IsAssignableFrom(target))
                throw new ArgumentException($"{nameof(target)}类型不是一个特性");
            return type.GetCustomAttribute(target, inherit) != null;
        }

        /// <summary>
        ///     判断当前对象的类型是否具有指定特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="obj"></param>
        /// <param name="inherit">
        ///     TRUE:去当前类型的基类查询
        ///     FALSE:不去当前类型基类查询
        ///     默认为FALSE
        /// </param>
        /// <returns></returns>
        public static bool IsDefined<T>(this object obj, bool inherit = false)
            where T : Attribute
            => IsDefined<T>(obj.GetType(), inherit);

        /// <summary>
        ///     判断当前对象的类型是否具有指定特性
        ///     Exception:
        ///         ArgumentException:传入的target类型并不是一个特性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="target"></param>
        /// <param name="inherit">
        ///     TRUE:去当前类型的基类查询
        ///     FALSE:不去当前类型基类查询
        ///     默认为FALSE
        /// </param>
        /// <returns></returns>
        public static bool IsDefined(this object obj, Type target, bool inherit = false)
            => IsDefined(obj.GetType(), target, inherit);
    }
}