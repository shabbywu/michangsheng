using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MarkerMetro.Unity.WinLegacy.Reflection;

public static class ReflectionExtensions
{
	private static readonly Dictionary<Type, TypeCode> _typeCodeTable = new Dictionary<Type, TypeCode>
	{
		{
			typeof(bool),
			TypeCode.Boolean
		},
		{
			typeof(char),
			TypeCode.Char
		},
		{
			typeof(byte),
			TypeCode.Byte
		},
		{
			typeof(short),
			TypeCode.Int16
		},
		{
			typeof(int),
			TypeCode.Int32
		},
		{
			typeof(long),
			TypeCode.Int64
		},
		{
			typeof(sbyte),
			TypeCode.SByte
		},
		{
			typeof(ushort),
			TypeCode.UInt16
		},
		{
			typeof(uint),
			TypeCode.UInt32
		},
		{
			typeof(ulong),
			TypeCode.UInt64
		},
		{
			typeof(float),
			TypeCode.Single
		},
		{
			typeof(double),
			TypeCode.Double
		},
		{
			typeof(DateTime),
			TypeCode.DateTime
		},
		{
			typeof(decimal),
			TypeCode.Decimal
		},
		{
			typeof(string),
			TypeCode.String
		}
	};

	public static bool IsValueType(this Type type)
	{
		return type.IsValueType;
	}

	public static bool IsGenericType(this Type type)
	{
		return type.IsGenericType;
	}

	public static object[] GetCustomAttributes(this Type type, bool inherit)
	{
		throw new NotImplementedException();
	}

	public static object[] GetCustomAttributes(this Type type, Type attrType)
	{
		throw new NotImplementedException();
	}

	public static object[] GetCustomAttributes(this Type type, Type attrType, bool inherit)
	{
		throw new NotImplementedException();
	}

	public static Assembly GetAssembly(this Type type)
	{
		throw new NotImplementedException();
	}

	public static bool IsAbstract(this Type type)
	{
		return type.IsAbstract;
	}

	public static bool IsDefined(this Type type, Type attributeType)
	{
		throw new NotImplementedException();
	}

	public static bool IsDefined(this Type type, Type attributeType, bool inherit)
	{
		throw new NotImplementedException();
	}

	public static bool IsClass(this Type type)
	{
		return type.IsClass;
	}

	public static bool IsEnum(this Type type)
	{
		return type.IsEnum;
	}

	public static bool IsPublic(this Type type)
	{
		return type.IsPublic;
	}

	public static bool IsVisible(this Type type)
	{
		return type.IsVisible;
	}

	public static ConstructorInfo GetParameterlessConstructor(this Type type)
	{
		throw new NotImplementedException();
	}

	public static ConstructorInfo[] GetConstructors(this Type type)
	{
		throw new NotImplementedException();
	}

	public static Type GetInterface(this Type type, string name)
	{
		return GetInterface(type, name, ignoreCase: false);
	}

	public static Type GetInterface(this Type type, string name, bool ignoreCase)
	{
		throw new NotImplementedException();
	}

	public static PropertyInfo[] GetProperties(this Type type)
	{
		if (type == null)
		{
			throw new NullReferenceException();
		}
		throw new NotImplementedException();
	}

	public static PropertyInfo[] GetProperties(this Type type, BindingFlags flags)
	{
		throw new NotImplementedException();
	}

	public static PropertyInfo GetProperty(this Type type, string name, BindingFlags bindingAttr)
	{
		throw new NotImplementedException();
	}

	public static PropertyInfo GetProperty(this Type type, string name)
	{
		PropertyInfo[] properties = type.GetProperties();
		if (!properties.Any())
		{
			return null;
		}
		return properties.FirstOrDefault((PropertyInfo f) => f.Name == name);
	}

	public static MethodInfo[] GetMethods(this Type type)
	{
		if (type == null)
		{
			throw new NullReferenceException();
		}
		throw new NotImplementedException();
	}

	public static MethodInfo[] GetMethods(this Type t, BindingFlags flags = BindingFlags.InvokeMethod)
	{
		throw new NotImplementedException();
	}

	private static bool TestBindingFlags(Type t, MethodInfo method, BindingFlags flags)
	{
		throw new NotImplementedException();
	}

	public static MemberInfo[] GetMembers(this Type type)
	{
		if (type == null)
		{
			throw new NullReferenceException();
		}
		throw new NotImplementedException();
	}

	public static MemberInfo[] GetMembers(this Type t, BindingFlags flags)
	{
		throw new NotImplementedException();
	}

	public static object InvokeMember(this Type t, string name, BindingFlags flags, object binder, object target, object[] args)
	{
		throw new NotImplementedException();
	}

	public static FieldInfo[] GetFields(this Type type)
	{
		throw new NotImplementedException();
	}

	public static FieldInfo[] GetFields(this Type t, BindingFlags flags)
	{
		throw new NotImplementedException();
	}

	public static FieldInfo GetField(this Type type, string name)
	{
		throw new NotImplementedException();
	}

	public static FieldInfo GetField(this Type type, string name, BindingFlags flags)
	{
		throw new NotImplementedException();
	}

	public static MethodInfo GetMethod(this Type type, string name)
	{
		return GetMethod(type, name, BindingFlags.Default, null);
	}

	public static MethodInfo GetMethod(this Type type, string name, Type[] types)
	{
		return GetMethod(type, name, BindingFlags.Default, types);
	}

	public static MethodInfo GetMethod(this Type t, string name, BindingFlags flags)
	{
		return GetMethod(t, name, flags, null);
	}

	public static Type GetBaseType(this Type type)
	{
		throw new NotImplementedException();
	}

	public static MethodInfo GetMethod(Type t, string name, BindingFlags flags, Type[] parameters)
	{
		throw new NotImplementedException();
	}

	public static Type[] GetGenericArguments(this Type t)
	{
		throw new NotImplementedException();
	}

	public static bool IsAssignableFrom(this Type current, Type toCompare)
	{
		throw new NotImplementedException();
	}

	public static bool IsInterface(this Type type)
	{
		return type.IsInterface;
	}

	public static bool IsPrimitive(this Type type)
	{
		return type.IsPrimitive;
	}

	public static bool IsSubclassOf(this Type type, Type parent)
	{
		throw new NotImplementedException();
	}

	public static Type[] GetTypes(this Assembly assembly)
	{
		throw new NotImplementedException();
	}

	public static Type GetType(this Assembly assembly)
	{
		throw new NotImplementedException();
	}

	public static TypeCode GetTypeCode(this Type type)
	{
		if (type == null)
		{
			return TypeCode.Empty;
		}
		if (!_typeCodeTable.TryGetValue(type, out var value))
		{
			return TypeCode.Object;
		}
		return value;
	}
}
