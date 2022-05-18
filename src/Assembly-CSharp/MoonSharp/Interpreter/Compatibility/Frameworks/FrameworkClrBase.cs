using System;
using System.Reflection;

namespace MoonSharp.Interpreter.Compatibility.Frameworks
{
	// Token: 0x020011B2 RID: 4530
	internal abstract class FrameworkClrBase : FrameworkReflectionBase
	{
		// Token: 0x06006EF8 RID: 28408 RVA: 0x00010DC9 File Offset: 0x0000EFC9
		public override Type GetTypeInfoFromType(Type t)
		{
			return t;
		}

		// Token: 0x06006EF9 RID: 28409 RVA: 0x0004B69B File Offset: 0x0004989B
		public override MethodInfo GetAddMethod(EventInfo ei)
		{
			return ei.GetAddMethod(true);
		}

		// Token: 0x06006EFA RID: 28410 RVA: 0x0004B6A4 File Offset: 0x000498A4
		public override ConstructorInfo[] GetConstructors(Type type)
		{
			return type.GetConstructors(this.BINDINGFLAGS_MEMBER);
		}

		// Token: 0x06006EFB RID: 28411 RVA: 0x0004B6B2 File Offset: 0x000498B2
		public override EventInfo[] GetEvents(Type type)
		{
			return type.GetEvents(this.BINDINGFLAGS_MEMBER);
		}

		// Token: 0x06006EFC RID: 28412 RVA: 0x0004B6C0 File Offset: 0x000498C0
		public override FieldInfo[] GetFields(Type type)
		{
			return type.GetFields(this.BINDINGFLAGS_MEMBER);
		}

		// Token: 0x06006EFD RID: 28413 RVA: 0x0004B6CE File Offset: 0x000498CE
		public override Type[] GetGenericArguments(Type type)
		{
			return type.GetGenericArguments();
		}

		// Token: 0x06006EFE RID: 28414 RVA: 0x0004B6D6 File Offset: 0x000498D6
		public override MethodInfo GetGetMethod(PropertyInfo pi)
		{
			return pi.GetGetMethod(true);
		}

		// Token: 0x06006EFF RID: 28415 RVA: 0x0004B6DF File Offset: 0x000498DF
		public override Type[] GetInterfaces(Type t)
		{
			return t.GetInterfaces();
		}

		// Token: 0x06006F00 RID: 28416 RVA: 0x0004B6E7 File Offset: 0x000498E7
		public override MethodInfo GetMethod(Type type, string name)
		{
			return type.GetMethod(name);
		}

		// Token: 0x06006F01 RID: 28417 RVA: 0x0004B6F0 File Offset: 0x000498F0
		public override MethodInfo[] GetMethods(Type type)
		{
			return type.GetMethods(this.BINDINGFLAGS_MEMBER);
		}

		// Token: 0x06006F02 RID: 28418 RVA: 0x0004B6FE File Offset: 0x000498FE
		public override Type[] GetNestedTypes(Type type)
		{
			return type.GetNestedTypes(this.BINDINGFLAGS_INNERCLASS);
		}

		// Token: 0x06006F03 RID: 28419 RVA: 0x0004B70C File Offset: 0x0004990C
		public override PropertyInfo[] GetProperties(Type type)
		{
			return type.GetProperties(this.BINDINGFLAGS_MEMBER);
		}

		// Token: 0x06006F04 RID: 28420 RVA: 0x0004B71A File Offset: 0x0004991A
		public override PropertyInfo GetProperty(Type type, string name)
		{
			return type.GetProperty(name);
		}

		// Token: 0x06006F05 RID: 28421 RVA: 0x0004B723 File Offset: 0x00049923
		public override MethodInfo GetRemoveMethod(EventInfo ei)
		{
			return ei.GetRemoveMethod(true);
		}

		// Token: 0x06006F06 RID: 28422 RVA: 0x0004B72C File Offset: 0x0004992C
		public override MethodInfo GetSetMethod(PropertyInfo pi)
		{
			return pi.GetSetMethod(true);
		}

		// Token: 0x06006F07 RID: 28423 RVA: 0x0004B735 File Offset: 0x00049935
		public override bool IsAssignableFrom(Type current, Type toCompare)
		{
			return current.IsAssignableFrom(toCompare);
		}

		// Token: 0x06006F08 RID: 28424 RVA: 0x0004B73E File Offset: 0x0004993E
		public override bool IsInstanceOfType(Type t, object o)
		{
			return t.IsInstanceOfType(o);
		}

		// Token: 0x06006F09 RID: 28425 RVA: 0x0004B747 File Offset: 0x00049947
		public override MethodInfo GetMethod(Type resourcesType, string name, Type[] types)
		{
			return resourcesType.GetMethod(name, types);
		}

		// Token: 0x06006F0A RID: 28426 RVA: 0x0004B751 File Offset: 0x00049951
		public override Type[] GetAssemblyTypes(Assembly asm)
		{
			return asm.GetTypes();
		}

		// Token: 0x0400627B RID: 25211
		private BindingFlags BINDINGFLAGS_MEMBER = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x0400627C RID: 25212
		private BindingFlags BINDINGFLAGS_INNERCLASS = BindingFlags.Public | BindingFlags.NonPublic;
	}
}
