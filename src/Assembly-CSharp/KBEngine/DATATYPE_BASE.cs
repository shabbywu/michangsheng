using System;

namespace KBEngine
{
	// Token: 0x02000B51 RID: 2897
	public class DATATYPE_BASE
	{
		// Token: 0x06005104 RID: 20740 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void bind()
		{
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public virtual object createFromStream(MemoryStream stream)
		{
			return null;
		}

		// Token: 0x06005106 RID: 20742 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void addToStream(Bundle stream, object v)
		{
		}

		// Token: 0x06005107 RID: 20743 RVA: 0x000306E7 File Offset: 0x0002E8E7
		public virtual object parseDefaultValStr(string v)
		{
			return null;
		}

		// Token: 0x06005108 RID: 20744 RVA: 0x0022146F File Offset: 0x0021F66F
		public virtual bool isSameType(object v)
		{
			return v == null;
		}
	}
}
