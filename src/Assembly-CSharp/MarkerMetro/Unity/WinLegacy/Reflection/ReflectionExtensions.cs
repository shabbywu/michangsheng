using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MarkerMetro.Unity.WinLegacy.Reflection
{
	// Token: 0x02000C93 RID: 3219
	public static class ReflectionExtensions
	{
		// Token: 0x060059A5 RID: 22949 RVA: 0x00256921 File Offset: 0x00254B21
		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x060059A6 RID: 22950 RVA: 0x00256929 File Offset: 0x00254B29
		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x060059A7 RID: 22951 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static object[] GetCustomAttributes(this Type type, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059A8 RID: 22952 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static object[] GetCustomAttributes(this Type type, Type attrType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059A9 RID: 22953 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static object[] GetCustomAttributes(this Type type, Type attrType, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059AA RID: 22954 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static Assembly GetAssembly(this Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059AB RID: 22955 RVA: 0x00256931 File Offset: 0x00254B31
		public static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x060059AC RID: 22956 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static bool IsDefined(this Type type, Type attributeType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059AD RID: 22957 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static bool IsDefined(this Type type, Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059AE RID: 22958 RVA: 0x00256939 File Offset: 0x00254B39
		public static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x060059AF RID: 22959 RVA: 0x00256941 File Offset: 0x00254B41
		public static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x060059B0 RID: 22960 RVA: 0x00256949 File Offset: 0x00254B49
		public static bool IsPublic(this Type type)
		{
			return type.IsPublic;
		}

		// Token: 0x060059B1 RID: 22961 RVA: 0x00256951 File Offset: 0x00254B51
		public static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x060059B2 RID: 22962 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static ConstructorInfo GetParameterlessConstructor(this Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059B3 RID: 22963 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static ConstructorInfo[] GetConstructors(this Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059B4 RID: 22964 RVA: 0x00256959 File Offset: 0x00254B59
		public static Type GetInterface(this Type type, string name)
		{
			return type.GetInterface(name, false);
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static Type GetInterface(this Type type, string name, bool ignoreCase)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059B6 RID: 22966 RVA: 0x00256963 File Offset: 0x00254B63
		public static PropertyInfo[] GetProperties(this Type type)
		{
			if (type == null)
			{
				throw new NullReferenceException();
			}
			throw new NotImplementedException();
		}

		// Token: 0x060059B7 RID: 22967 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static PropertyInfo[] GetProperties(this Type type, BindingFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059B8 RID: 22968 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static PropertyInfo GetProperty(this Type type, string name, BindingFlags bindingAttr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059B9 RID: 22969 RVA: 0x0025697C File Offset: 0x00254B7C
		public static PropertyInfo GetProperty(this Type type, string name)
		{
			PropertyInfo[] properties = type.GetProperties();
			if (!properties.Any<PropertyInfo>())
			{
				return null;
			}
			return properties.FirstOrDefault((PropertyInfo f) => f.Name == name);
		}

		// Token: 0x060059BA RID: 22970 RVA: 0x00256963 File Offset: 0x00254B63
		public static MethodInfo[] GetMethods(this Type type)
		{
			if (type == null)
			{
				throw new NullReferenceException();
			}
			throw new NotImplementedException();
		}

		// Token: 0x060059BB RID: 22971 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static MethodInfo[] GetMethods(this Type t, BindingFlags flags = BindingFlags.InvokeMethod)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059BC RID: 22972 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		private static bool TestBindingFlags(Type t, MethodInfo method, BindingFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059BD RID: 22973 RVA: 0x00256963 File Offset: 0x00254B63
		public static MemberInfo[] GetMembers(this Type type)
		{
			if (type == null)
			{
				throw new NullReferenceException();
			}
			throw new NotImplementedException();
		}

		// Token: 0x060059BE RID: 22974 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static MemberInfo[] GetMembers(this Type t, BindingFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059BF RID: 22975 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static object InvokeMember(this Type t, string name, BindingFlags flags, object binder, object target, object[] args)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059C0 RID: 22976 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static FieldInfo[] GetFields(this Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059C1 RID: 22977 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static FieldInfo[] GetFields(this Type t, BindingFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059C2 RID: 22978 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static FieldInfo GetField(this Type type, string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059C3 RID: 22979 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static FieldInfo GetField(this Type type, string name, BindingFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059C4 RID: 22980 RVA: 0x002569B9 File Offset: 0x00254BB9
		public static MethodInfo GetMethod(this Type type, string name)
		{
			return ReflectionExtensions.GetMethod(type, name, BindingFlags.Default, null);
		}

		// Token: 0x060059C5 RID: 22981 RVA: 0x002569C4 File Offset: 0x00254BC4
		public static MethodInfo GetMethod(this Type type, string name, Type[] types)
		{
			return ReflectionExtensions.GetMethod(type, name, BindingFlags.Default, types);
		}

		// Token: 0x060059C6 RID: 22982 RVA: 0x002569CF File Offset: 0x00254BCF
		public static MethodInfo GetMethod(this Type t, string name, BindingFlags flags)
		{
			return ReflectionExtensions.GetMethod(t, name, flags, null);
		}

		// Token: 0x060059C7 RID: 22983 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static Type GetBaseType(this Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059C8 RID: 22984 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static MethodInfo GetMethod(Type t, string name, BindingFlags flags, Type[] parameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059C9 RID: 22985 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static Type[] GetGenericArguments(this Type t)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059CA RID: 22986 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static bool IsAssignableFrom(this Type current, Type toCompare)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059CB RID: 22987 RVA: 0x002569DA File Offset: 0x00254BDA
		public static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x060059CC RID: 22988 RVA: 0x002569E2 File Offset: 0x00254BE2
		public static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x060059CD RID: 22989 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static bool IsSubclassOf(this Type type, Type parent)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059CE RID: 22990 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static Type[] GetTypes(this Assembly assembly)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059CF RID: 22991 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		public static Type GetType(this Assembly assembly)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059D0 RID: 22992 RVA: 0x002569EC File Offset: 0x00254BEC
		public static TypeCode GetTypeCode(this Type type)
		{
			if (type == null)
			{
				return TypeCode.Empty;
			}
			TypeCode result;
			if (!ReflectionExtensions._typeCodeTable.TryGetValue(type, out result))
			{
				result = TypeCode.Object;
			}
			return result;
		}

		// Token: 0x0400524B RID: 21067
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
	}
}
