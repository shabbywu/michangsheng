using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D07 RID: 3335
	public static class DescriptorHelpers
	{
		// Token: 0x06005D62 RID: 23906 RVA: 0x0026299C File Offset: 0x00260B9C
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

		// Token: 0x06005D63 RID: 23907 RVA: 0x00262A23 File Offset: 0x00260C23
		public static bool IsDelegateType(this Type t)
		{
			return Framework.Do.IsAssignableFrom(typeof(Delegate), t);
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x00262A3C File Offset: 0x00260C3C
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

		// Token: 0x06005D65 RID: 23909 RVA: 0x00262AB8 File Offset: 0x00260CB8
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

		// Token: 0x06005D66 RID: 23910 RVA: 0x00262B18 File Offset: 0x00260D18
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

		// Token: 0x06005D67 RID: 23911 RVA: 0x00262BAC File Offset: 0x00260DAC
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

		// Token: 0x06005D68 RID: 23912 RVA: 0x00262C0C File Offset: 0x00260E0C
		public static bool IsPropertyInfoPublic(this PropertyInfo pi)
		{
			MethodInfo getMethod = Framework.Do.GetGetMethod(pi);
			MethodInfo setMethod = Framework.Do.GetSetMethod(pi);
			return (getMethod != null && getMethod.IsPublic) || (setMethod != null && setMethod.IsPublic);
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x00262C58 File Offset: 0x00260E58
		public static List<string> GetMetaNamesFromAttributes(this MethodInfo mi)
		{
			return (from a in mi.GetCustomAttributes(typeof(MoonSharpUserDataMetamethodAttribute), true).OfType<MoonSharpUserDataMetamethodAttribute>()
			select a.Name).ToList<string>();
		}

		// Token: 0x06005D6A RID: 23914 RVA: 0x00262CA4 File Offset: 0x00260EA4
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

		// Token: 0x06005D6B RID: 23915 RVA: 0x00262CDC File Offset: 0x00260EDC
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

		// Token: 0x06005D6C RID: 23916 RVA: 0x00262D2D File Offset: 0x00260F2D
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

		// Token: 0x06005D6D RID: 23917 RVA: 0x00262D40 File Offset: 0x00260F40
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

		// Token: 0x06005D6E RID: 23918 RVA: 0x00262DA0 File Offset: 0x00260FA0
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

		// Token: 0x06005D6F RID: 23919 RVA: 0x00262E24 File Offset: 0x00261024
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

		// Token: 0x06005D70 RID: 23920 RVA: 0x00262E90 File Offset: 0x00261090
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
