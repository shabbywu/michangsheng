using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010E9 RID: 4329
	public static class DescriptorHelpers
	{
		// Token: 0x06006886 RID: 26758 RVA: 0x0028B654 File Offset: 0x00289854
		public static bool? GetVisibilityFromAttributes(this MemberInfo mi)
		{
			if (mi == null)
			{
				return new bool?(false);
			}
			MoonSharpVisibleAttribute moonSharpVisibleAttribute = mi.GetCustomAttributes(true).OfType<MoonSharpVisibleAttribute>().SingleOrDefault<MoonSharpVisibleAttribute>();
			MoonSharpHiddenAttribute moonSharpHiddenAttribute = mi.GetCustomAttributes(true).OfType<MoonSharpHiddenAttribute>().SingleOrDefault<MoonSharpHiddenAttribute>();
			if (moonSharpVisibleAttribute != null && moonSharpHiddenAttribute != null && moonSharpVisibleAttribute.Visible)
			{
				throw new InvalidOperationException(string.Format("A member ('{0}') can't have discording MoonSharpHiddenAttribute and MoonSharpVisibleAttribute.", mi.Name));
			}
			if (moonSharpHiddenAttribute != null)
			{
				return new bool?(false);
			}
			if (moonSharpVisibleAttribute != null)
			{
				return new bool?(moonSharpVisibleAttribute.Visible);
			}
			return null;
		}

		// Token: 0x06006887 RID: 26759 RVA: 0x00047BEE File Offset: 0x00045DEE
		public static bool IsDelegateType(this Type t)
		{
			return Framework.Do.IsAssignableFrom(typeof(Delegate), t);
		}

		// Token: 0x06006888 RID: 26760 RVA: 0x0028B6DC File Offset: 0x002898DC
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

		// Token: 0x06006889 RID: 26761 RVA: 0x0028B758 File Offset: 0x00289958
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

		// Token: 0x0600688A RID: 26762 RVA: 0x0028B7B8 File Offset: 0x002899B8
		public static string GetClrVisibility(this PropertyInfo info)
		{
			MethodInfo getMethod = Framework.Do.GetGetMethod(info);
			MethodInfo setMethod = Framework.Do.GetSetMethod(info);
			string text = (getMethod != null) ? getMethod.GetClrVisibility() : "private";
			string a = (setMethod != null) ? setMethod.GetClrVisibility() : "private";
			if (text == "public" || a == "public")
			{
				return "public";
			}
			if (text == "internal" || a == "internal")
			{
				return "internal";
			}
			return text;
		}

		// Token: 0x0600688B RID: 26763 RVA: 0x0028B84C File Offset: 0x00289A4C
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

		// Token: 0x0600688C RID: 26764 RVA: 0x0028B8AC File Offset: 0x00289AAC
		public static bool IsPropertyInfoPublic(this PropertyInfo pi)
		{
			MethodInfo getMethod = Framework.Do.GetGetMethod(pi);
			MethodInfo setMethod = Framework.Do.GetSetMethod(pi);
			return (getMethod != null && getMethod.IsPublic) || (setMethod != null && setMethod.IsPublic);
		}

		// Token: 0x0600688D RID: 26765 RVA: 0x0028B8F8 File Offset: 0x00289AF8
		public static List<string> GetMetaNamesFromAttributes(this MethodInfo mi)
		{
			return (from a in mi.GetCustomAttributes(typeof(MoonSharpUserDataMetamethodAttribute), true).OfType<MoonSharpUserDataMetamethodAttribute>()
			select a.Name).ToList<string>();
		}

		// Token: 0x0600688E RID: 26766 RVA: 0x0028B944 File Offset: 0x00289B44
		public static Type[] SafeGetTypes(this Assembly asm)
		{
			Type[] result;
			try
			{
				result = Framework.Do.GetAssemblyTypes(asm);
			}
			catch (ReflectionTypeLoadException)
			{
				result = new Type[0];
			}
			return result;
		}

		// Token: 0x0600688F RID: 26767 RVA: 0x0028B97C File Offset: 0x00289B7C
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

		// Token: 0x06006890 RID: 26768 RVA: 0x00047C05 File Offset: 0x00045E05
		public static IEnumerable<Type> GetAllImplementedTypes(this Type t)
		{
			Type ot = t;
			while (ot != null)
			{
				yield return ot;
				ot = Framework.Do.GetBaseType(ot);
			}
			ot = null;
			foreach (Type type in Framework.Do.GetInterfaces(t))
			{
				yield return type;
			}
			Type[] array = null;
			yield break;
		}

		// Token: 0x06006891 RID: 26769 RVA: 0x0028B9D0 File Offset: 0x00289BD0
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

		// Token: 0x06006892 RID: 26770 RVA: 0x0028BA30 File Offset: 0x00289C30
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

		// Token: 0x06006893 RID: 26771 RVA: 0x0028BAB4 File Offset: 0x00289CB4
		public static string Camelify(string name)
		{
			StringBuilder stringBuilder = new StringBuilder(name.Length);
			bool flag = false;
			for (int i = 0; i < name.Length; i++)
			{
				if (name[i] == '_' && i != 0)
				{
					flag = true;
				}
				else
				{
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
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006894 RID: 26772 RVA: 0x0028BB20 File Offset: 0x00289D20
		public static string UpperFirstLetter(string name)
		{
			if (!string.IsNullOrEmpty(name))
			{
				return char.ToUpperInvariant(name[0]).ToString() + name.Substring(1);
			}
			return name;
		}
	}
}
