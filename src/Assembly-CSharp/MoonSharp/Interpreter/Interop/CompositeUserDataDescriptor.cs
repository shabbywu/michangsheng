using System;
using System.Collections.Generic;
using MoonSharp.Interpreter.Compatibility;

namespace MoonSharp.Interpreter.Interop
{
	// Token: 0x02000D18 RID: 3352
	public class CompositeUserDataDescriptor : IUserDataDescriptor
	{
		// Token: 0x06005DB4 RID: 23988 RVA: 0x00263BC3 File Offset: 0x00261DC3
		public CompositeUserDataDescriptor(List<IUserDataDescriptor> descriptors, Type type)
		{
			this.m_Descriptors = descriptors;
			this.m_Type = type;
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06005DB5 RID: 23989 RVA: 0x00263BD9 File Offset: 0x00261DD9
		public IList<IUserDataDescriptor> Descriptors
		{
			get
			{
				return this.m_Descriptors;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06005DB6 RID: 23990 RVA: 0x00263BE1 File Offset: 0x00261DE1
		public string Name
		{
			get
			{
				return "^" + this.m_Type.FullName;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06005DB7 RID: 23991 RVA: 0x00263BF8 File Offset: 0x00261DF8
		public Type Type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x06005DB8 RID: 23992 RVA: 0x00263C00 File Offset: 0x00261E00
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

		// Token: 0x06005DB9 RID: 23993 RVA: 0x00263C60 File Offset: 0x00261E60
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

		// Token: 0x06005DBA RID: 23994 RVA: 0x00263CC0 File Offset: 0x00261EC0
		public string AsString(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			return obj.ToString();
		}

		// Token: 0x06005DBB RID: 23995 RVA: 0x00263CD0 File Offset: 0x00261ED0
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

		// Token: 0x06005DBC RID: 23996 RVA: 0x00259E25 File Offset: 0x00258025
		public bool IsTypeCompatible(Type type, object obj)
		{
			return Framework.Do.IsInstanceOfType(type, obj);
		}

		// Token: 0x04005408 RID: 21512
		private List<IUserDataDescriptor> m_Descriptors;

		// Token: 0x04005409 RID: 21513
		private Type m_Type;
	}
}
