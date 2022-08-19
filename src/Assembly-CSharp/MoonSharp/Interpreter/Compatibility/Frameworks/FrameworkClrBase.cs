using System;
using System.Reflection;

namespace MoonSharp.Interpreter.Compatibility.Frameworks
{
	// Token: 0x02000D8F RID: 3471
	internal abstract class FrameworkClrBase : FrameworkReflectionBase
	{
		// Token: 0x060062C7 RID: 25287 RVA: 0x001086F1 File Offset: 0x001068F1
		public override Type GetTypeInfoFromType(Type t)
		{
			return t;
		}

		// Token: 0x060062C8 RID: 25288 RVA: 0x00279B61 File Offset: 0x00277D61
		public override MethodInfo GetAddMethod(EventInfo ei)
		{
			return ei.GetAddMethod(true);
		}

		// Token: 0x060062C9 RID: 25289 RVA: 0x00279B6A File Offset: 0x00277D6A
		public override ConstructorInfo[] GetConstructors(Type type)
		{
			return type.GetConstructors(this.BINDINGFLAGS_MEMBER);
		}

		// Token: 0x060062CA RID: 25290 RVA: 0x00279B78 File Offset: 0x00277D78
		public override EventInfo[] GetEvents(Type type)
		{
			return type.GetEvents(this.BINDINGFLAGS_MEMBER);
		}

		// Token: 0x060062CB RID: 25291 RVA: 0x00279B86 File Offset: 0x00277D86
		public override FieldInfo[] GetFields(Type type)
		{
			return type.GetFields(this.BINDINGFLAGS_MEMBER);
		}

		// Token: 0x060062CC RID: 25292 RVA: 0x00279B94 File Offset: 0x00277D94
		public override Type[] GetGenericArguments(Type type)
		{
			return type.GetGenericArguments();
		}

		// Token: 0x060062CD RID: 25293 RVA: 0x00279B9C File Offset: 0x00277D9C
		public override MethodInfo GetGetMethod(PropertyInfo pi)
		{
			return pi.GetGetMethod(true);
		}

		// Token: 0x060062CE RID: 25294 RVA: 0x00279BA5 File Offset: 0x00277DA5
		public override Type[] GetInterfaces(Type t)
		{
			return t.GetInterfaces();
		}

		// Token: 0x060062CF RID: 25295 RVA: 0x00279BAD File Offset: 0x00277DAD
		public override MethodInfo GetMethod(Type type, string name)
		{
			return type.GetMethod(name);
		}

		// Token: 0x060062D0 RID: 25296 RVA: 0x00279BB6 File Offset: 0x00277DB6
		public override MethodInfo[] GetMethods(Type type)
		{
			return type.GetMethods(this.BINDINGFLAGS_MEMBER);
		}

		// Token: 0x060062D1 RID: 25297 RVA: 0x00279BC4 File Offset: 0x00277DC4
		public override Type[] GetNestedTypes(Type type)
		{
			return type.GetNestedTypes(this.BINDINGFLAGS_INNERCLASS);
		}

		// Token: 0x060062D2 RID: 25298 RVA: 0x00279BD2 File Offset: 0x00277DD2
		public override PropertyInfo[] GetProperties(Type type)
		{
			return type.GetProperties(this.BINDINGFLAGS_MEMBER);
		}

		// Token: 0x060062D3 RID: 25299 RVA: 0x00279BE0 File Offset: 0x00277DE0
		public override PropertyInfo GetProperty(Type type, string name)
		{
			return type.GetProperty(name);
		}

		// Token: 0x060062D4 RID: 25300 RVA: 0x00279BE9 File Offset: 0x00277DE9
		public override MethodInfo GetRemoveMethod(EventInfo ei)
		{
			return ei.GetRemoveMethod(true);
		}

		// Token: 0x060062D5 RID: 25301 RVA: 0x00279BF2 File Offset: 0x00277DF2
		public override MethodInfo GetSetMethod(PropertyInfo pi)
		{
			return pi.GetSetMethod(true);
		}

		// Token: 0x060062D6 RID: 25302 RVA: 0x00279BFB File Offset: 0x00277DFB
		public override bool IsAssignableFrom(Type current, Type toCompare)
		{
			return current.IsAssignableFrom(toCompare);
		}

		// Token: 0x060062D7 RID: 25303 RVA: 0x00279C04 File Offset: 0x00277E04
		public override bool IsInstanceOfType(Type t, object o)
		{
			return t.IsInstanceOfType(o);
		}

		// Token: 0x060062D8 RID: 25304 RVA: 0x00279C0D File Offset: 0x00277E0D
		public override MethodInfo GetMethod(Type resourcesType, string name, Type[] types)
		{
			return resourcesType.GetMethod(name, types);
		}

		// Token: 0x060062D9 RID: 25305 RVA: 0x00279C17 File Offset: 0x00277E17
		public override Type[] GetAssemblyTypes(Assembly asm)
		{
			return asm.GetTypes();
		}

		// Token: 0x040055A4 RID: 21924
		private BindingFlags BINDINGFLAGS_MEMBER = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x040055A5 RID: 21925
		private BindingFlags BINDINGFLAGS_INNERCLASS = BindingFlags.Public | BindingFlags.NonPublic;
	}
}
