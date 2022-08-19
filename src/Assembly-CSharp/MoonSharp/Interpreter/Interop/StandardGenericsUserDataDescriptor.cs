using System;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D25 RID: 3365
	public class StandardGenericsUserDataDescriptor : IUserDataDescriptor, IGeneratorUserDataDescriptor
	{
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06005E85 RID: 24197 RVA: 0x002676A3 File Offset: 0x002658A3
		// (set) Token: 0x06005E86 RID: 24198 RVA: 0x002676AB File Offset: 0x002658AB
		public InteropAccessMode AccessMode { get; private set; }

		// Token: 0x06005E87 RID: 24199 RVA: 0x002676B4 File Offset: 0x002658B4
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

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06005E88 RID: 24200 RVA: 0x002676EF File Offset: 0x002658EF
		// (set) Token: 0x06005E89 RID: 24201 RVA: 0x002676F7 File Offset: 0x002658F7
		public string Name { get; private set; }

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06005E8A RID: 24202 RVA: 0x00267700 File Offset: 0x00265900
		// (set) Token: 0x06005E8B RID: 24203 RVA: 0x00267708 File Offset: 0x00265908
		public Type Type { get; private set; }

		// Token: 0x06005E8C RID: 24204 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing)
		{
			return null;
		}

		// Token: 0x06005E8D RID: 24205 RVA: 0x0000280F File Offset: 0x00000A0F
		public bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing)
		{
			return false;
		}

		// Token: 0x06005E8E RID: 24206 RVA: 0x00267711 File Offset: 0x00265911
		public string AsString(object obj)
		{
			return obj.ToString();
		}

		// Token: 0x06005E8F RID: 24207 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public DynValue MetaIndex(Script script, object obj, string metaname)
		{
			return null;
		}

		// Token: 0x06005E90 RID: 24208 RVA: 0x00259E25 File Offset: 0x00258025
		public bool IsTypeCompatible(Type type, object obj)
		{
			return Framework.Do.IsInstanceOfType(type, obj);
		}

		// Token: 0x06005E91 RID: 24209 RVA: 0x00267719 File Offset: 0x00265919
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
