using System;
using System.Linq;
using System.Reflection;

namespace MoonSharp.Interpreter.Compatibility.Frameworks
{
	// Token: 0x02000D90 RID: 3472
	internal abstract class FrameworkReflectionBase : FrameworkBase
	{
		// Token: 0x060062DB RID: 25307
		public abstract Type GetTypeInfoFromType(Type t);

		// Token: 0x060062DC RID: 25308 RVA: 0x00279C37 File Offset: 0x00277E37
		public override Assembly GetAssembly(Type t)
		{
			return this.GetTypeInfoFromType(t).Assembly;
		}

		// Token: 0x060062DD RID: 25309 RVA: 0x00279C45 File Offset: 0x00277E45
		public override Type GetBaseType(Type t)
		{
			return this.GetTypeInfoFromType(t).BaseType;
		}

		// Token: 0x060062DE RID: 25310 RVA: 0x00279C53 File Offset: 0x00277E53
		public override bool IsValueType(Type t)
		{
			return this.GetTypeInfoFromType(t).IsValueType;
		}

		// Token: 0x060062DF RID: 25311 RVA: 0x00279C61 File Offset: 0x00277E61
		public override bool IsInterface(Type t)
		{
			return this.GetTypeInfoFromType(t).IsInterface;
		}

		// Token: 0x060062E0 RID: 25312 RVA: 0x00279C6F File Offset: 0x00277E6F
		public override bool IsNestedPublic(Type t)
		{
			return this.GetTypeInfoFromType(t).IsNestedPublic;
		}

		// Token: 0x060062E1 RID: 25313 RVA: 0x00279C7D File Offset: 0x00277E7D
		public override bool IsAbstract(Type t)
		{
			return this.GetTypeInfoFromType(t).IsAbstract;
		}

		// Token: 0x060062E2 RID: 25314 RVA: 0x00279C8B File Offset: 0x00277E8B
		public override bool IsEnum(Type t)
		{
			return this.GetTypeInfoFromType(t).IsEnum;
		}

		// Token: 0x060062E3 RID: 25315 RVA: 0x00279C99 File Offset: 0x00277E99
		public override bool IsGenericTypeDefinition(Type t)
		{
			return this.GetTypeInfoFromType(t).IsGenericTypeDefinition;
		}

		// Token: 0x060062E4 RID: 25316 RVA: 0x00279CA7 File Offset: 0x00277EA7
		public override bool IsGenericType(Type t)
		{
			return this.GetTypeInfoFromType(t).IsGenericType;
		}

		// Token: 0x060062E5 RID: 25317 RVA: 0x00279CB5 File Offset: 0x00277EB5
		public override Attribute[] GetCustomAttributes(Type t, bool inherit)
		{
			return this.GetTypeInfoFromType(t).GetCustomAttributes(inherit).OfType<Attribute>().ToArray<Attribute>();
		}

		// Token: 0x060062E6 RID: 25318 RVA: 0x00279CCE File Offset: 0x00277ECE
		public override Attribute[] GetCustomAttributes(Type t, Type at, bool inherit)
		{
			return this.GetTypeInfoFromType(t).GetCustomAttributes(at, inherit).OfType<Attribute>().ToArray<Attribute>();
		}
	}
}
