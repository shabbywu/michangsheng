using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MarkerMetro.Unity.WinLegacy.Reflection
{
	// Token: 0x02001058 RID: 4184
	public static class ReflectionExtensions
	{
		// Token: 0x06006465 RID: 25701 RVA: 0x00045161 File Offset: 0x00043361
		public static bool IsValueType(this Type type)
		{
			return type.IsValueType;
		}

		// Token: 0x06006466 RID: 25702 RVA: 0x00045169 File Offset: 0x00043369
		public static bool IsGenericType(this Type type)
		{
			return type.IsGenericType;
		}

		// Token: 0x06006467 RID: 25703 RVA: 0x0001C722 File Offset: 0x0001A922
		public static object[] GetCustomAttributes(this Type type, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006468 RID: 25704 RVA: 0x0001C722 File Offset: 0x0001A922
		public static object[] GetCustomAttributes(this Type type, Type attrType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006469 RID: 25705 RVA: 0x0001C722 File Offset: 0x0001A922
		public static object[] GetCustomAttributes(this Type type, Type attrType, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600646A RID: 25706 RVA: 0x0001C722 File Offset: 0x0001A922
		public static Assembly GetAssembly(this Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600646B RID: 25707 RVA: 0x00045171 File Offset: 0x00043371
		public static bool IsAbstract(this Type type)
		{
			return type.IsAbstract;
		}

		// Token: 0x0600646C RID: 25708 RVA: 0x0001C722 File Offset: 0x0001A922
		public static bool IsDefined(this Type type, Type attributeType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600646D RID: 25709 RVA: 0x0001C722 File Offset: 0x0001A922
		public static bool IsDefined(this Type type, Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600646E RID: 25710 RVA: 0x00045179 File Offset: 0x00043379
		public static bool IsClass(this Type type)
		{
			return type.IsClass;
		}

		// Token: 0x0600646F RID: 25711 RVA: 0x00045181 File Offset: 0x00043381
		public static bool IsEnum(this Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06006470 RID: 25712 RVA: 0x00045189 File Offset: 0x00043389
		public static bool IsPublic(this Type type)
		{
			return type.IsPublic;
		}

		// Token: 0x06006471 RID: 25713 RVA: 0x00045191 File Offset: 0x00043391
		public static bool IsVisible(this Type type)
		{
			return type.IsVisible;
		}

		// Token: 0x06006472 RID: 25714 RVA: 0x0001C722 File Offset: 0x0001A922
		public static ConstructorInfo GetParameterlessConstructor(this Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006473 RID: 25715 RVA: 0x0001C722 File Offset: 0x0001A922
		public static ConstructorInfo[] GetConstructors(this Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006474 RID: 25716 RVA: 0x00045199 File Offset: 0x00043399
		public static Type GetInterface(this Type type, string name)
		{
			return type.GetInterface(name, false);
		}

		// Token: 0x06006475 RID: 25717 RVA: 0x0001C722 File Offset: 0x0001A922
		public static Type GetInterface(this Type type, string name, bool ignoreCase)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006476 RID: 25718 RVA: 0x000451A3 File Offset: 0x000433A3
		public static PropertyInfo[] GetProperties(this Type type)
		{
			if (type == null)
			{
				throw new NullReferenceException();
			}
			throw new NotImplementedException();
		}

		// Token: 0x06006477 RID: 25719 RVA: 0x0001C722 File Offset: 0x0001A922
		public static PropertyInfo[] GetProperties(this Type type, BindingFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006478 RID: 25720 RVA: 0x0001C722 File Offset: 0x0001A922
		public static PropertyInfo GetProperty(this Type type, string name, BindingFlags bindingAttr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006479 RID: 25721 RVA: 0x00281954 File Offset: 0x0027FB54
		public static PropertyInfo GetProperty(this Type type, string name)
		{
			PropertyInfo[] properties = type.GetProperties();
			if (!properties.Any<PropertyInfo>())
			{
				return null;
			}
			return properties.FirstOrDefault((PropertyInfo f) => f.Name == name);
		}

		// Token: 0x0600647A RID: 25722 RVA: 0x000451A3 File Offset: 0x000433A3
		public static MethodInfo[] GetMethods(this Type type)
		{
			if (type == null)
			{
				throw new NullReferenceException();
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600647B RID: 25723 RVA: 0x0001C722 File Offset: 0x0001A922
		public static MethodInfo[] GetMethods(this Type t, BindingFlags flags = BindingFlags.InvokeMethod)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600647C RID: 25724 RVA: 0x0001C722 File Offset: 0x0001A922
		private static bool TestBindingFlags(Type t, MethodInfo method, BindingFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600647D RID: 25725 RVA: 0x000451A3 File Offset: 0x000433A3
		public static MemberInfo[] GetMembers(this Type type)
		{
			if (type == null)
			{
				throw new NullReferenceException();
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600647E RID: 25726 RVA: 0x0001C722 File Offset: 0x0001A922
		public static MemberInfo[] GetMembers(this Type t, BindingFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600647F RID: 25727 RVA: 0x0001C722 File Offset: 0x0001A922
		public static object InvokeMember(this Type t, string name, BindingFlags flags, object binder, object target, object[] args)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006480 RID: 25728 RVA: 0x0001C722 File Offset: 0x0001A922
		public static FieldInfo[] GetFields(this Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006481 RID: 25729 RVA: 0x0001C722 File Offset: 0x0001A922
		public static FieldInfo[] GetFields(this Type t, BindingFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006482 RID: 25730 RVA: 0x0001C722 File Offset: 0x0001A922
		public static FieldInfo GetField(this Type type, string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006483 RID: 25731 RVA: 0x0001C722 File Offset: 0x0001A922
		public static FieldInfo GetField(this Type type, string name, BindingFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006484 RID: 25732 RVA: 0x000451B9 File Offset: 0x000433B9
		public static MethodInfo GetMethod(this Type type, string name)
		{
			return ReflectionExtensions.GetMethod(type, name, BindingFlags.Default, null);
		}

		// Token: 0x06006485 RID: 25733 RVA: 0x000451C4 File Offset: 0x000433C4
		public static MethodInfo GetMethod(this Type type, string name, Type[] types)
		{
			return ReflectionExtensions.GetMethod(type, name, BindingFlags.Default, types);
		}

		// Token: 0x06006486 RID: 25734 RVA: 0x000451CF File Offset: 0x000433CF
		public static MethodInfo GetMethod(this Type t, string name, BindingFlags flags)
		{
			return ReflectionExtensions.GetMethod(t, name, flags, null);
		}

		// Token: 0x06006487 RID: 25735 RVA: 0x0001C722 File Offset: 0x0001A922
		public static Type GetBaseType(this Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006488 RID: 25736 RVA: 0x0001C722 File Offset: 0x0001A922
		public static MethodInfo GetMethod(Type t, string name, BindingFlags flags, Type[] parameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006489 RID: 25737 RVA: 0x0001C722 File Offset: 0x0001A922
		public static Type[] GetGenericArguments(this Type t)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600648A RID: 25738 RVA: 0x0001C722 File Offset: 0x0001A922
		public static bool IsAssignableFrom(this Type current, Type toCompare)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600648B RID: 25739 RVA: 0x000451DA File Offset: 0x000433DA
		public static bool IsInterface(this Type type)
		{
			return type.IsInterface;
		}

		// Token: 0x0600648C RID: 25740 RVA: 0x000451E2 File Offset: 0x000433E2
		public static bool IsPrimitive(this Type type)
		{
			return type.IsPrimitive;
		}

		// Token: 0x0600648D RID: 25741 RVA: 0x0001C722 File Offset: 0x0001A922
		public static bool IsSubclassOf(this Type type, Type parent)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600648E RID: 25742 RVA: 0x0001C722 File Offset: 0x0001A922
		public static Type[] GetTypes(this Assembly assembly)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600648F RID: 25743 RVA: 0x0001C722 File Offset: 0x0001A922
		public static Type GetType(this Assembly assembly)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006490 RID: 25744 RVA: 0x00281994 File Offset: 0x0027FB94
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

		// Token: 0x04005DF9 RID: 24057
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
