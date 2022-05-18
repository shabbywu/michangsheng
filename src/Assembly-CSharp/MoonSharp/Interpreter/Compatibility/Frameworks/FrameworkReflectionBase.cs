using System;
using System.Linq;
using System.Reflection;

namespace MoonSharp.Interpreter.Compatibility.Frameworks
{
	// Token: 0x020011B3 RID: 4531
	internal abstract class FrameworkReflectionBase : FrameworkBase
	{
		// Token: 0x06006F0C RID: 28428
		public abstract Type GetTypeInfoFromType(Type t);

		// Token: 0x06006F0D RID: 28429 RVA: 0x0004B771 File Offset: 0x00049971
		public override Assembly GetAssembly(Type t)
		{
			return this.GetTypeInfoFromType(t).Assembly;
		}

		// Token: 0x06006F0E RID: 28430 RVA: 0x0004B77F File Offset: 0x0004997F
		public override Type GetBaseType(Type t)
		{
			return this.GetTypeInfoFromType(t).BaseType;
		}

		// Token: 0x06006F0F RID: 28431 RVA: 0x0004B78D File Offset: 0x0004998D
		public override bool IsValueType(Type t)
		{
			return this.GetTypeInfoFromType(t).IsValueType;
		}

		// Token: 0x06006F10 RID: 28432 RVA: 0x0004B79B File Offset: 0x0004999B
		public override bool IsInterface(Type t)
		{
			return this.GetTypeInfoFromType(t).IsInterface;
		}

		// Token: 0x06006F11 RID: 28433 RVA: 0x0004B7A9 File Offset: 0x000499A9
		public override bool IsNestedPublic(Type t)
		{
			return this.GetTypeInfoFromType(t).IsNestedPublic;
		}

		// Token: 0x06006F12 RID: 28434 RVA: 0x0004B7B7 File Offset: 0x000499B7
		public override bool IsAbstract(Type t)
		{
			return this.GetTypeInfoFromType(t).IsAbstract;
		}

		// Token: 0x06006F13 RID: 28435 RVA: 0x0004B7C5 File Offset: 0x000499C5
		public override bool IsEnum(Type t)
		{
			return this.GetTypeInfoFromType(t).IsEnum;
		}

		// Token: 0x06006F14 RID: 28436 RVA: 0x0004B7D3 File Offset: 0x000499D3
		public override bool IsGenericTypeDefinition(Type t)
		{
			return this.GetTypeInfoFromType(t).IsGenericTypeDefinition;
		}

		// Token: 0x06006F15 RID: 28437 RVA: 0x0004B7E1 File Offset: 0x000499E1
		public override bool IsGenericType(Type t)
		{
			return this.GetTypeInfoFromType(t).IsGenericType;
		}

		// Token: 0x06006F16 RID: 28438 RVA: 0x0004B7EF File Offset: 0x000499EF
		public override Attribute[] GetCustomAttributes(Type t, bool inherit)
		{
			return this.GetTypeInfoFromType(t).GetCustomAttributes(inherit).OfType<Attribute>().ToArray<Attribute>();
		}

		// Token: 0x06006F17 RID: 28439 RVA: 0x0004B808 File Offset: 0x00049A08
		public override Attribute[] GetCustomAttributes(Type t, Type at, bool inherit)
		{
			return this.GetTypeInfoFromType(t).GetCustomAttributes(at, inherit).OfType<Attribute>().ToArray<Attribute>();
		}
	}
}
