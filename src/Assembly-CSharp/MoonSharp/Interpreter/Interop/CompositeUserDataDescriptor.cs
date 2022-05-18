using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x020010FC RID: 4348
	public class CompositeUserDataDescriptor : IUserDataDescriptor
	{
		// Token: 0x060068E3 RID: 26851 RVA: 0x00047E52 File Offset: 0x00046052
		public CompositeUserDataDescriptor(List<IUserDataDescriptor> descriptors, Type type)
		{
			this.m_Descriptors = descriptors;
			this.m_Type = type;
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x060068E4 RID: 26852 RVA: 0x00047E68 File Offset: 0x00046068
		public IList<IUserDataDescriptor> Descriptors
		{
			get
			{
				return this.m_Descriptors;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x060068E5 RID: 26853 RVA: 0x00047E70 File Offset: 0x00046070
		public string Name
		{
			get
			{
				return "^" + this.m_Type.FullName;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x060068E6 RID: 26854 RVA: 0x00047E87 File Offset: 0x00046087
		public Type Type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x060068E7 RID: 26855 RVA: 0x0028C788 File Offset: 0x0028A988
		public DynValue Index(Script script, object obj, DynValue index, bool isNameIndex)
		{
			foreach (IUserDataDescriptor userDataDescriptor in this.m_Descriptors)
			{
				DynValue dynValue = userDataDescriptor.Index(script, obj, index, isNameIndex);
				if (dynValue != null)
				{
					return dynValue;
				}
			}
			return null;
		}

		// Token: 0x060068E8 RID: 26856 RVA: 0x0028C7E8 File Offset: 0x0028A9E8
		public bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isNameIndex)
		{
			using (List<IUserDataDescriptor>.Enumerator enumerator = this.m_Descriptors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.SetIndex(script, obj, index, value, isNameIndex))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060068E9 RID: 26857 RVA: 0x00047E8F File Offset: 0x0004608F
		public string AsString(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			return obj.ToString();
		}

		// Token: 0x060068EA RID: 26858 RVA: 0x0028C848 File Offset: 0x0028AA48
		public DynValue MetaIndex(Script script, object obj, string metaname)
		{
			foreach (IUserDataDescriptor userDataDescriptor in this.m_Descriptors)
			{
				DynValue dynValue = userDataDescriptor.MetaIndex(script, obj, metaname);
				if (dynValue != null)
				{
					return dynValue;
				}
			}
			return null;
		}

		// Token: 0x060068EB RID: 26859 RVA: 0x00046989 File Offset: 0x00044B89
		public bool IsTypeCompatible(Type type, object obj)
		{
			return Framework.Do.IsInstanceOfType(type, obj);
		}

		// Token: 0x0400601F RID: 24607
		private List<IUserDataDescriptor> m_Descriptors;

		// Token: 0x04006020 RID: 24608
		private Type m_Type;
	}
}
