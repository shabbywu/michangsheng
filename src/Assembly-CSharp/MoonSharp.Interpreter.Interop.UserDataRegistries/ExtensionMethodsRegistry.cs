using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.DataStructs;
using MoonSharp.Interpreter.Interop.BasicDescriptors;

namespace MoonSharp.Interpreter.Interop.UserDataRegistries;

internal class ExtensionMethodsRegistry
{
	private class UnresolvedGenericMethod
	{
		public readonly MethodInfo Method;

		public readonly InteropAccessMode AccessMode;

		public readonly HashSet<Type> AlreadyAddedTypes = new HashSet<Type>();

		public UnresolvedGenericMethod(MethodInfo mi, InteropAccessMode mode)
		{
			AccessMode = mode;
			Method = mi;
		}
	}

	private static object s_Lock = new object();

	private static MultiDictionary<string, IOverloadableMemberDescriptor> s_Registry = new MultiDictionary<string, IOverloadableMemberDescriptor>();

	private static MultiDictionary<string, UnresolvedGenericMethod> s_UnresolvedGenericsRegistry = new MultiDictionary<string, UnresolvedGenericMethod>();

	private static int s_ExtensionMethodChangeVersion = 0;

	public static void RegisterExtensionType(Type type, InteropAccessMode mode = InteropAccessMode.Default)
	{
		lock (s_Lock)
		{
			bool flag = false;
			foreach (MethodInfo item in from _mi in Framework.Do.GetMethods(type)
				where _mi.IsStatic
				select _mi)
			{
				if (item.GetCustomAttributes(typeof(ExtensionAttribute), inherit: false).Count() != 0)
				{
					if (item.ContainsGenericParameters)
					{
						s_UnresolvedGenericsRegistry.Add(item.Name, new UnresolvedGenericMethod(item, mode));
						flag = true;
					}
					else if (MethodMemberDescriptor.CheckMethodIsCompatible(item, throwException: false))
					{
						MethodMemberDescriptor value = new MethodMemberDescriptor(item, mode);
						s_Registry.Add(item.Name, value);
						flag = true;
					}
				}
			}
			if (flag)
			{
				s_ExtensionMethodChangeVersion++;
			}
		}
	}

	private static object FrameworkGetMethods()
	{
		throw new NotImplementedException();
	}

	public static IEnumerable<IOverloadableMemberDescriptor> GetExtensionMethodsByName(string name)
	{
		lock (s_Lock)
		{
			return new List<IOverloadableMemberDescriptor>(s_Registry.Find(name));
		}
	}

	public static int GetExtensionMethodsChangeVersion()
	{
		return s_ExtensionMethodChangeVersion;
	}

	public static List<IOverloadableMemberDescriptor> GetExtensionMethodsByNameAndType(string name, Type extendedType)
	{
		List<UnresolvedGenericMethod> list = null;
		lock (s_Lock)
		{
			list = s_UnresolvedGenericsRegistry.Find(name).ToList();
		}
		foreach (UnresolvedGenericMethod item in list)
		{
			ParameterInfo[] parameters = item.Method.GetParameters();
			if (parameters.Length == 0)
			{
				continue;
			}
			Type parameterType = parameters[0].ParameterType;
			Type genericMatch = GetGenericMatch(parameterType, extendedType);
			if (item.AlreadyAddedTypes.Add(genericMatch) && genericMatch != null)
			{
				MethodInfo methodInfo = InstantiateMethodInfo(item.Method, parameterType, genericMatch, extendedType);
				if (methodInfo != null && MethodMemberDescriptor.CheckMethodIsCompatible(methodInfo, throwException: false))
				{
					MethodMemberDescriptor value = new MethodMemberDescriptor(methodInfo, item.AccessMode);
					s_Registry.Add(item.Method.Name, value);
					s_ExtensionMethodChangeVersion++;
				}
			}
		}
		return (from d in s_Registry.Find(name)
			where d.ExtensionMethodType != null && Framework.Do.IsAssignableFrom(d.ExtensionMethodType, extendedType)
			select d).ToList();
	}

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

	private static Type GetGenericMatch(Type extensionType, Type extendedType)
	{
		if (!extensionType.IsGenericParameter)
		{
			extensionType = extensionType.GetGenericTypeDefinition();
			foreach (Type allImplementedType in extendedType.GetAllImplementedTypes())
			{
				if (Framework.Do.IsGenericType(allImplementedType) && allImplementedType.GetGenericTypeDefinition() == extensionType)
				{
					return allImplementedType;
				}
			}
		}
		return null;
	}
}
