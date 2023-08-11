using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop;

public static class DescriptorHelpers
{
	public static bool? GetVisibilityFromAttributes(this MemberInfo mi)
	{
		if (mi == null)
		{
			return false;
		}
		MoonSharpVisibleAttribute moonSharpVisibleAttribute = mi.GetCustomAttributes(inherit: true).OfType<MoonSharpVisibleAttribute>().SingleOrDefault();
		MoonSharpHiddenAttribute moonSharpHiddenAttribute = mi.GetCustomAttributes(inherit: true).OfType<MoonSharpHiddenAttribute>().SingleOrDefault();
		if (moonSharpVisibleAttribute != null && moonSharpHiddenAttribute != null && moonSharpVisibleAttribute.Visible)
		{
			throw new InvalidOperationException($"A member ('{mi.Name}') can't have discording MoonSharpHiddenAttribute and MoonSharpVisibleAttribute.");
		}
		if (moonSharpHiddenAttribute != null)
		{
			return false;
		}
		return moonSharpVisibleAttribute?.Visible;
	}

	public static bool IsDelegateType(this Type t)
	{
		return Framework.Do.IsAssignableFrom(typeof(Delegate), t);
	}

	public static string GetClrVisibility(this Type type)
	{
		if (type.IsPublic || type.IsNestedPublic)
		{
			return "public";
		}
		if ((type.IsNotPublic && !type.IsNested) || type.IsNestedAssembly)
		{
			return "internal";
		}
		if (type.IsNestedFamORAssem)
		{
			return "protected-internal";
		}
		if (type.IsNestedFamANDAssem || type.IsNestedFamily)
		{
			return "protected";
		}
		if (type.IsNestedPrivate)
		{
			return "private";
		}
		return "unknown";
	}

	public static string GetClrVisibility(this FieldInfo info)
	{
		if (info.IsPublic)
		{
			return "public";
		}
		if (info.IsAssembly)
		{
			return "internal";
		}
		if (info.IsFamilyOrAssembly)
		{
			return "protected-internal";
		}
		if (info.IsFamilyAndAssembly || info.IsFamily)
		{
			return "protected";
		}
		if (info.IsPrivate)
		{
			return "private";
		}
		return "unknown";
	}

	public static string GetClrVisibility(this PropertyInfo info)
	{
		MethodInfo getMethod = Framework.Do.GetGetMethod(info);
		MethodInfo setMethod = Framework.Do.GetSetMethod(info);
		string text = ((getMethod != null) ? getMethod.GetClrVisibility() : "private");
		string text2 = ((setMethod != null) ? setMethod.GetClrVisibility() : "private");
		if (text == "public" || text2 == "public")
		{
			return "public";
		}
		if (text == "internal" || text2 == "internal")
		{
			return "internal";
		}
		return text;
	}

	public static string GetClrVisibility(this MethodBase info)
	{
		if (info.IsPublic)
		{
			return "public";
		}
		if (info.IsAssembly)
		{
			return "internal";
		}
		if (info.IsFamilyOrAssembly)
		{
			return "protected-internal";
		}
		if (info.IsFamilyAndAssembly || info.IsFamily)
		{
			return "protected";
		}
		if (info.IsPrivate)
		{
			return "private";
		}
		return "unknown";
	}

	public static bool IsPropertyInfoPublic(this PropertyInfo pi)
	{
		MethodInfo getMethod = Framework.Do.GetGetMethod(pi);
		MethodInfo setMethod = Framework.Do.GetSetMethod(pi);
		if (!(getMethod != null) || !getMethod.IsPublic)
		{
			if (setMethod != null)
			{
				return setMethod.IsPublic;
			}
			return false;
		}
		return true;
	}

	public static List<string> GetMetaNamesFromAttributes(this MethodInfo mi)
	{
		return (from a in mi.GetCustomAttributes(typeof(MoonSharpUserDataMetamethodAttribute), inherit: true).OfType<MoonSharpUserDataMetamethodAttribute>()
			select a.Name).ToList();
	}

	public static Type[] SafeGetTypes(this Assembly asm)
	{
		try
		{
			return Framework.Do.GetAssemblyTypes(asm);
		}
		catch (ReflectionTypeLoadException)
		{
			return new Type[0];
		}
	}

	public static string GetConversionMethodName(this Type type)
	{
		StringBuilder stringBuilder = new StringBuilder(type.Name);
		for (int i = 0; i < stringBuilder.Length; i++)
		{
			if (!char.IsLetterOrDigit(stringBuilder[i]))
			{
				stringBuilder[i] = '_';
			}
		}
		return "__to" + stringBuilder.ToString();
	}

	public static IEnumerable<Type> GetAllImplementedTypes(this Type t)
	{
		Type ot = t;
		while (ot != null)
		{
			yield return ot;
			ot = Framework.Do.GetBaseType(ot);
		}
		Type[] interfaces = Framework.Do.GetInterfaces(t);
		for (int i = 0; i < interfaces.Length; i++)
		{
			yield return interfaces[i];
		}
	}

	public static bool IsValidSimpleIdentifier(string str)
	{
		if (string.IsNullOrEmpty(str))
		{
			return false;
		}
		if (str[0] != '_' && !char.IsLetter(str[0]))
		{
			return false;
		}
		for (int i = 1; i < str.Length; i++)
		{
			if (str[i] != '_' && !char.IsLetterOrDigit(str[i]))
			{
				return false;
			}
		}
		return true;
	}

	public static string ToValidSimpleIdentifier(string str)
	{
		if (string.IsNullOrEmpty(str))
		{
			return "_";
		}
		if (str[0] != '_' && !char.IsLetter(str[0]))
		{
			str = "_" + str;
		}
		StringBuilder stringBuilder = new StringBuilder(str);
		for (int i = 0; i < stringBuilder.Length; i++)
		{
			if (stringBuilder[i] != '_' && !char.IsLetterOrDigit(stringBuilder[i]))
			{
				stringBuilder[i] = '_';
			}
		}
		return stringBuilder.ToString();
	}

	public static string Camelify(string name)
	{
		StringBuilder stringBuilder = new StringBuilder(name.Length);
		bool flag = false;
		for (int i = 0; i < name.Length; i++)
		{
			if (name[i] == '_' && i != 0)
			{
				flag = true;
				continue;
			}
			if (flag)
			{
				stringBuilder.Append(char.ToUpperInvariant(name[i]));
			}
			else
			{
				stringBuilder.Append(name[i]);
			}
			flag = false;
		}
		return stringBuilder.ToString();
	}

	public static string UpperFirstLetter(string name)
	{
		if (!string.IsNullOrEmpty(name))
		{
			return char.ToUpperInvariant(name[0]) + name.Substring(1);
		}
		return name;
	}
}
