using System;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop;

namespace MoonSharp.Interpreter
{
	// Token: 0x02001088 RID: 4232
	internal class AutoDescribingUserDataDescriptor : IUserDataDescriptor
	{
		// Token: 0x06006649 RID: 26185 RVA: 0x00046956 File Offset: 0x00044B56
		public AutoDescribingUserDataDescriptor(Type type, string friendlyName)
		{
			this.m_FriendlyName = friendlyName;
			this.m_Type = type;
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x0600664A RID: 26186 RVA: 0x0004696C File Offset: 0x00044B6C
		public string Name
		{
			get
			{
				return this.m_FriendlyName;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x0600664B RID: 26187 RVA: 0x00046974 File Offset: 0x00044B74
		public Type Type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x0600664C RID: 26188 RVA: 0x00283B2C File Offset: 0x00281D2C
		public DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing)
		{
			IUserDataType userDataType = obj as IUserDataType;
			if (userDataType != null)
			{
				return userDataType.Index(script, index, isDirectIndexing);
			}
			return null;
		}

		// Token: 0x0600664D RID: 26189 RVA: 0x00283B50 File Offset: 0x00281D50
		public bool SetIndex(Script script, object obj, DynValue index, DynValue value, bool isDirectIndexing)
		{
			IUserDataType userDataType = obj as IUserDataType;
			return userDataType != null && userDataType.SetIndex(script, index, value, isDirectIndexing);
		}

		// Token: 0x0600664E RID: 26190 RVA: 0x0004697C File Offset: 0x00044B7C
		public string AsString(object obj)
		{
			if (obj != null)
			{
				return obj.ToString();
			}
			return null;
		}

		// Token: 0x0600664F RID: 26191 RVA: 0x00283B78 File Offset: 0x00281D78
		public DynValue MetaIndex(Script script, object obj, string metaname)
		{
			IUserDataType userDataType = obj as IUserDataType;
			if (userDataType != null)
			{
				return userDataType.MetaIndex(script, metaname);
			}
			return null;
		}

		// Token: 0x06006650 RID: 26192 RVA: 0x00046989 File Offset: 0x00044B89
		public bool IsTypeCompatible(Type type, object obj)
		{
			return Framework.Do.IsInstanceOfType(type, obj);
		}

		// Token: 0x04005E97 RID: 24215
		private string m_FriendlyName;

		// Token: 0x04005E98 RID: 24216
		private Type m_Type;
	}
}
