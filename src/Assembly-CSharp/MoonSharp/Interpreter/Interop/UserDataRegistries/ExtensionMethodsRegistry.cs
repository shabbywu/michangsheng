using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.DataStructs;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop.UserDataRegistries
{
	// Token: 0x0200112C RID: 4396
	internal class ExtensionMethodsRegistry
	{
		// Token: 0x06006A59 RID: 27225 RVA: 0x002904BC File Offset: 0x0028E6BC
		public static void RegisterExtensionType(Type type, InteropAccessMode mode = InteropAccessMode.Default)
		{
			object obj = ExtensionMethodsRegistry.s_Lock;
			lock (obj)
			{
				bool flag2 = false;
				foreach (MethodInfo methodInfo in from _mi in Framework.Do.GetMethods(type)
				where _mi.IsStatic
				select _mi)
				{
					if (methodInfo.GetCustomAttributes(typeof(ExtensionAttribute), false).Count<object>() != 0)
					{
						if (methodInfo.ContainsGenericParameters)
						{
							ExtensionMethodsRegistry.s_UnresolvedGenericsRegistry.Add(methodInfo.Name, new ExtensionMethodsRegistry.UnresolvedGenericMethod(methodInfo, mode));
							flag2 = true;
						}
						else if (MethodMemberDescriptor.CheckMethodIsCompatible(methodInfo, false))
						{
							MethodMemberDescriptor value = new MethodMemberDescriptor(methodInfo, mode);
							ExtensionMethodsRegistry.s_Registry.Add(methodInfo.Name, value);
							flag2 = true;
						}
					}
				}
				if (flag2)
				{
					ExtensionMethodsRegistry.s_ExtensionMethodChangeVersion++;
				}
			}
		}

		// Token: 0x06006A5A RID: 27226 RVA: 0x0001C722 File Offset: 0x0001A922
		private static object FrameworkGetMethods()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006A5B RID: 27227 RVA: 0x002905D0 File Offset: 0x0028E7D0
		public static IEnumerable<IOverloadableMemberDescriptor> GetExtensionMethodsByName(string name)
		{
			object obj = ExtensionMethodsRegistry.s_Lock;
			IEnumerable<IOverloadableMemberDescriptor> result;
			lock (obj)
			{
				result = new List<IOverloadableMemberDescriptor>(ExtensionMethodsRegistry.s_Registry.Find(name));
			}
			return result;
		}

		// Token: 0x06006A5C RID: 27228 RVA: 0x0004886A File Offset: 0x00046A6A
		public static int GetExtensionMethodsChangeVersion()
		{
			return ExtensionMethodsRegistry.s_ExtensionMethodChangeVersion;
		}

		// Token: 0x06006A5D RID: 27229 RVA: 0x0029061C File Offset: 0x0028E81C
		public static List<IOverloadableMemberDescriptor> GetExtensionMethodsByNameAndType(string name, Type extendedType)
		{
			List<ExtensionMethodsRegistry.UnresolvedGenericMethod> list = null;
			object obj = ExtensionMethodsRegistry.s_Lock;
			lock (obj)
			{
				list = ExtensionMethodsRegistry.s_UnresolvedGenericsRegistry.Find(name).ToList<ExtensionMethodsRegistry.UnresolvedGenericMethod>();
			}
			foreach (ExtensionMethodsRegistry.UnresolvedGenericMethod unresolvedGenericMethod in list)
			{
				ParameterInfo[] parameters = unresolvedGenericMethod.Method.GetParameters();
				if (parameters.Length != 0)
				{
					Type parameterType = parameters[0].ParameterType;
					Type genericMatch = ExtensionMethodsRegistry.GetGenericMatch(parameterType, extendedType);
					if (unresolvedGenericMethod.AlreadyAddedTypes.Add(genericMatch) && genericMatch != null)
					{
						MethodInfo methodInfo = ExtensionMethodsRegistry.InstantiateMethodInfo(unresolvedGenericMethod.Method, parameterType, genericMatch, extendedType);
						if (methodInfo != null && MethodMemberDescriptor.CheckMethodIsCompatible(methodInfo, false))
						{
							MethodMemberDescriptor value = new MethodMemberDescriptor(methodInfo, unresolvedGenericMethod.AccessMode);
							ExtensionMethodsRegistry.s_Registry.Add(unresolvedGenericMethod.Method.Name, value);
							ExtensionMethodsRegistry.s_ExtensionMethodChangeVersion++;
						}
					}
				}
			}
			return (from d in ExtensionMethodsRegistry.s_Registry.Find(name)
			where d.ExtensionMethodType != null && Framework.Do.IsAssignableFrom(d.ExtensionMethodType, extendedType)
			select d).ToList<IOverloadableMemberDescriptor>();
		}

		// Token: 0x06006A5E RID: 27230 RVA: 0x00290780 File Offset: 0x0028E980
		private static MethodInfo InstantiateMethodInfo(MethodInfo mi, Type extensionType, Type genericType, Type extendedType)
		{
			Type[] genericArguments = mi.GetGenericArguments();
			Type[] genericArguments2 = Framework.Do.GetGenericArguments(genericType);
			if (genericArguments2.Length == genericArguments.Length)
			{
				return mi.MakeGenericMethod(genericArguments2);
			}
			return null;
		}

		// Token: 0x06006A5F RID: 27231 RVA: 0x002907B4 File Offset: 0x0028E9B4
		private static Type GetGenericMatch(Type extensionType, Type extendedType)
		{
			if (!extensionType.IsGenericParameter)
			{
				extensionType = extensionType.GetGenericTypeDefinition();
				foreach (Type type in extendedType.GetAllImplementedTypes())
				{
					if (Framework.Do.IsGenericType(type) && type.GetGenericTypeDefinition() == extensionType)
					{
						return type;
					}
				}
			}
			return null;
		}

		// Token: 0x040060A9 RID: 24745
		private static object s_Lock = new object();

		// Token: 0x040060AA RID: 24746
		private static MultiDictionary<string, IOverloadableMemberDescriptor> s_Registry = new MultiDictionary<string, IOverloadableMemberDescriptor>();

		// Token: 0x040060AB RID: 24747
		private static MultiDictionary<string, ExtensionMethodsRegistry.UnresolvedGenericMethod> s_UnresolvedGenericsRegistry = new MultiDictionary<string, ExtensionMethodsRegistry.UnresolvedGenericMethod>();

		// Token: 0x040060AC RID: 24748
		private static int s_ExtensionMethodChangeVersion = 0;

		// Token: 0x0200112D RID: 4397
		private class UnresolvedGenericMethod
		{
			// Token: 0x06006A62 RID: 27234 RVA: 0x00048897 File Offset: 0x00046A97
			public UnresolvedGenericMethod(MethodInfo mi, InteropAccessMode mode)
			{
				this.AccessMode = mode;
				this.Method = mi;
			}

			// Token: 0x040060AD RID: 24749
			public readonly MethodInfo Method;

			// Token: 0x040060AE RID: 24750
			public readonly InteropAccessMode AccessMode;

			// Token: 0x040060AF RID: 24751
			public readonly HashSet<Type> AlreadyAddedTypes = new HashSet<Type>();
		}
	}
}
