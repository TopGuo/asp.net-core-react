using System;
using System.Linq;
using System.Reflection;

namespace infrastructure.utils
{
    public static class SetValueUtil
    {
        /// <summary>
        /// 设置/修改字段值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public static void SetFieldValue<T>(this object obj, string fieldName, T value)
        {
            var type = obj.GetType();
            FieldInfo field = null;
            while (type != null)
            {
                field = type.GetTypeInfo().DeclaredFields.SingleOrDefault(t => t.Name.IndexOf($"<{fieldName}>", StringComparison.OrdinalIgnoreCase) > -1);
                if (field != null)
                {
                    break;
                }
                else
                {
                    type = type.GetTypeInfo().BaseType;
                }
            }
            if (field != null)
            {
                field.SetValue(obj, value);
            }
        }
    }
}