using System;
using System.Reflection;

public static class ReflectionUtils
{
    public static Type GetMemberType(this MemberInfo member)
    {
        switch (member)
        {
            case FieldInfo field:
                return field.FieldType;

            case PropertyInfo property:
                return property.PropertyType;

            case EventInfo evt:
                return evt.EventHandlerType;

            case MethodInfo method:
                return method.ReturnType;

            default:
                return null;
        }
    }

    public static void SetValue(this MemberInfo member, object destObject, object value) 
    {
        switch (member)
        {
            case FieldInfo field:
                field.SetValue(destObject, value);
                break;

            case PropertyInfo property:
                property.SetValue(destObject, value);
                break;

            case MethodInfo method:
                method.Invoke(destObject, new object[] { value });
                break;

            default:
                throw new ArgumentException("MemberInfo must be of type FieldInfo, PropertyInfo or MethodInfo. Current type is <color=orange>{member.MemberType}</color>", nameof(member));
        }
    }

    public static object GetValue(this MemberInfo member, object srcObject) 
    {
        switch (member) 
        {
            case FieldInfo field:
                return field.GetValue(srcObject);
            
            case PropertyInfo property:
                return property.GetValue(srcObject);

            case MethodInfo method:
                return method.Invoke(srcObject, null);
            
            default:
                throw new ArgumentException($"MemberInfo must be of type FieldInfo, PropertyInfo or MethodInfo. Current type is <color=orange>{member.MemberType}</color>", nameof(member));
        }
    }

    public static bool GetCanWrite(this MemberInfo member) {
    switch (member) {
        case FieldInfo field:
            return true;

        case PropertyInfo property:
            return property.CanWrite;

        default:
            throw new ArgumentException("MemberInfo must be if type FieldInfo or PropertyInfo. Current type is <color=orange>{member.MemberType}</color>", nameof(member));
    }
}
}