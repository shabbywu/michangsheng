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
	// Token: 0x02000D29 RID: 3369
	internal class ExtensionMethodsRegistry
	{
		// Token: 0x06005E98 RID: 24216 RVA: 0x00267D9C File Offset: 0x00265F9C
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

		// Token: 0x06005E99 RID: 24217 RVA: 0x000DBFA9 File Offset: 0x000DA1A9
		private static object FrameworkGetMethods()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005E9A RID: 24218 RVA: 0x00267EB0 File Offset: 0x002660B0
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

		// Token: 0x06005E9B RID: 24219 RVA: 0x00267EFC File Offset: 0x002660FC
		public static int GetExtensionMethodsChangeVersion()
		{
			return ExtensionMethodsRegistry.s_ExtensionMethodChangeVersion;
		}

		// Token: 0x06005E9C RID: 24220 RVA: 0x00267F04 File Offset: 0x00266104
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

		// Token: 0x06005E9D RID: 24221 RVA: 0x00268068 File Offset: 0x00266268
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

		// Token: 0x06005E9E RID: 24222 RVA: 0x0026809C File Offset: 0x0026629C
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

		// Token: 0x04005456 RID: 21590
		private static object s_Lock = new object();

		// Token: 0x04005457 RID: 21591
		private static MultiDictionary<string, IOverloadableMemberDescriptor> s_Registry = new MultiDictionary<string, IOverloadableMemberDescriptor>();

		// Token: 0x04005458 RID: 21592
		private static MultiDictionary<string, ExtensionMethodsRegistry.UnresolvedGenericMethod> s_UnresolvedGenericsRegistry = new MultiDictionary<string, ExtensionMethodsRegistry.UnresolvedGenericMethod>();

		// Token: 0x04005459 RID: 21593
		private static int s_ExtensionMethodChangeVersion = 0;

		// Token: 0x02001678 RID: 5752
		private class UnresolvedGenericMethod
		{
			// Token: 0x0600872C RID: 34604 RVA: 0x002E6C8D File Offset: 0x002E4E8D
			public UnresolvedGenericMethod(MethodInfo mi, InteropAccessMode mode)
			{
				this.AccessMode = mode;
				this.Method = mi;
			}

			// Token: 0x040072B0 RID: 29360
			public readonly MethodInfo Method;

			// Token: 0x040072B1 RID: 29361
			public readonly InteropAccessMode AccessMode;

			// Token: 0x040072B2 RID: 29362
			public readonly HashSet<Type> AlreadyAddedTypes = new HashSet<Type>();
		}
	}
}
