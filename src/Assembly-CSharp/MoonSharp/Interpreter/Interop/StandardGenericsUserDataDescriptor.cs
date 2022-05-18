using System;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02001127 RID: 4391
	public class StandardGenericsUserDataDescriptor : IUserDataDescriptor, IGeneratorUserDataDescriptor
	{
		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06006A43 RID: 27203 RVA: 0x000487AF File Offset: 0x000469AF
		// (set) Token: 0x06006A44 RID: 27204 RVA: 0x000487B7 File Offset: 0x000469B7
		public InteropAccessMode AccessMode { get; private set; }

		// Token: 0x06006A45 RID: 27205 RVA: 0x000487C0 File Offset: 0x000469C0
		public StandardGenericsUserDataDescriptor(Type type, InteropAccessMode accessMode)
		{
			if (accessMode == InteropAccessMode.NoReflectionAllowed)
			{
				throw new ArgumentException("Can't create a StandardGenericsUserDataDescriptor under a NoReflectionAllowed access mode");
			}
			this.AccessMode = accessMode;
			this.Type = type;
			this.Name = "@@" + type.FullName;
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06006A46 RID: 27206 RVA: 0x000487FB File Offset: 0x000469FB
		// (set) Token: 0x06006A47 RID: 27207 RVA: 0x00048803 File Offset: 0x00046A03
		public string Name { get; private set; }

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06006A48 RID: 27208 RVA: 0x0004880C File Offset: 0x00046A0C
		// (set) Token: 0x06006A49 RID: 27209 RVA: 0x00048814 File Offset: 0x00046A14
		public Type Type { get; private set; }

		// Token: 0x06006A4A RID: 27210 RVA: 0x0000B171 File Offset: 0x00009371
		public DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing)
		{
			return null;
		}

		// Token: 0x06006A4B RID: 27211 RVA: 0x00004050 File Offset: 0x00002250
		public bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing)
		{
			return false;
		}

		// Token: 0x06006A4C RID: 27212 RVA: 0x0003222E File Offset: 0x0003042E
		public string AsString(object obj)
		{
			return obj.ToString();
		}

		// Token: 0x06006A4D RID: 27213 RVA: 0x0000B171 File Offset: 0x00009371
		public DynValue MetaIndex(Script script, object obj, string metaname)
		{
			return null;
		}

		// Token: 0x06006A4E RID: 27214 RVA: 0x00046989 File Offset: 0x00044B89
		public bool IsTypeCompatible(Type type, object obj)
		{
			return Framework.Do.IsInstanceOfType(type, obj);
		}

		// Token: 0x06006A4F RID: 27215 RVA: 0x0004881D File Offset: 0x00046A1D
		public IUserDataDescriptor Generate(Type type)
		{
			if (UserData.IsTypeRegistered(type))
			{
				return null;
			}
			if (Framework.Do.IsGenericTypeDefinition(type))
			{
				return null;
			}
			return UserData.RegisterType(type, this.AccessMode, null);
		}
	}
}
