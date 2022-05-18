using System;

namespace KBEngine
{
	// Token: 0x02000ECE RID: 3790
	public class DATATYPE_BASE
	{
		// Token: 0x06005B40 RID: 23360 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void bind()
		{
		}

		// Token: 0x06005B41 RID: 23361 RVA: 0x0000B171 File Offset: 0x00009371
		public virtual object createFromStream(MemoryStream stream)
		{
			return null;
		}

		// Token: 0x06005B42 RID: 23362 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void addToStream(Bundle stream, object v)
		{
		}

		// Token: 0x06005B43 RID: 23363 RVA: 0x0000B171 File Offset: 0x00009371
		public virtual object parseDefaultValStr(string v)
		{
			return null;
		}

		// Token: 0x06005B44 RID: 23364 RVA: 0x00040578 File Offset: 0x0003E778
		public virtual bool isSameType(object v)
		{
			return v == null;
		}
	}
}
