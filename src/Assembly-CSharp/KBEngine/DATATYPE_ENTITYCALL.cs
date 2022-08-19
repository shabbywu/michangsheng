using System;

namespace KBEngine
{
	// Token: 0x02000B62 RID: 2914
	public class DATATYPE_ENTITYCALL : DATATYPE_BASE
	{
		// Token: 0x0600515A RID: 20826 RVA: 0x00221B89 File Offset: 0x0021FD89
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readBlob();
		}

		// Token: 0x0600515B RID: 20827 RVA: 0x00221B91 File Offset: 0x0021FD91
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeBlob((byte[])v);
		}

		// Token: 0x0600515C RID: 20828 RVA: 0x00221B9F File Offset: 0x0021FD9F
		public override object parseDefaultValStr(string v)
		{
			return new byte[0];
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x00221BA7 File Offset: 0x0021FDA7
		public override bool isSameType(object v)
		{
			return v != null && v.GetType() == typeof(byte[]);
		}
	}
}
