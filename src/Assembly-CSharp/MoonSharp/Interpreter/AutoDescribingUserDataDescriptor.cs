using System;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop;

namespace MoonSharp.Interpreter
{
	// Token: 0x02000CB9 RID: 3257
	internal class AutoDescribingUserDataDescriptor : IUserDataDescriptor
	{
		// Token: 0x06005B57 RID: 23383 RVA: 0x00259D85 File Offset: 0x00257F85
		public AutoDescribingUserDataDescriptor(Type type, string friendlyName)
		{
			this.m_FriendlyName = friendlyName;
			this.m_Type = type;
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06005B58 RID: 23384 RVA: 0x00259D9B File Offset: 0x00257F9B
		public string Name
		{
			get
			{
				return this.m_FriendlyName;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06005B59 RID: 23385 RVA: 0x00259DA3 File Offset: 0x00257FA3
		public Type Type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x06005B5A RID: 23386 RVA: 0x00259DAC File Offset: 0x00257FAC
		public DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing)
		{
			IUserDataType userDataType = obj as IUserDataType;
			if (userDataType != null)
			{
				return userDataType.Index(script, index, isDirectIndexing);
			}
			return null;
		}

		// Token: 0x06005B5B RID: 23387 RVA: 0x00259DD0 File Offset: 0x00257FD0
		public bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing)
		{
			IUserDataType userDataType = obj as IUserDataType;
			return userDataType != null && userDataType.SetIndex(script, index, value, isDirectIndexing);
		}

		// Token: 0x06005B5C RID: 23388 RVA: 0x00259DF5 File Offset: 0x00257FF5
		public string AsString(object obj)
		{
			if (obj != null)
			{
				return obj.ToString();
			}
			return null;
		}

		// Token: 0x06005B5D RID: 23389 RVA: 0x00259E04 File Offset: 0x00258004
		public DynValue MetaIndex(Script script, object obj, string metaname)
		{
			IUserDataType userDataType = obj as IUserDataType;
			if (userDataType != null)
			{
				return userDataType.MetaIndex(script, metaname);
			}
			return null;
		}

		// Token: 0x06005B5E RID: 23390 RVA: 0x00259E25 File Offset: 0x00258025
		public bool IsTypeCompatible(Type type, object obj)
		{
			return Framework.Do.IsInstanceOfType(type, obj);
		}

		// Token: 0x040052C2 RID: 21186
		private string m_FriendlyName;

		// Token: 0x040052C3 RID: 21187
		private Type m_Type;
	}
}
