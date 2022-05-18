using System;

namespace KBEngine
{
	// Token: 0x02000EDD RID: 3805
	public class DATATYPE_PYTHON : DATATYPE_BASE
	{
		// Token: 0x06005B8C RID: 23436 RVA: 0x00040817 File Offset: 0x0003EA17
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readBlob();
		}

		// Token: 0x06005B8D RID: 23437 RVA: 0x0004081F File Offset: 0x0003EA1F
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeBlob((byte[])v);
		}

		// Token: 0x06005B8E RID: 23438 RVA: 0x0004082D File Offset: 0x0003EA2D
		public override object parseDefaultValStr(string v)
		{
			return new byte[0];
		}

		// Token: 0x06005B8F RID: 23439 RVA: 0x00040835 File Offset: 0x0003EA35
		public override bool isSameType(object v)
		{
			return v != null && v.GetType() == typeof(byte[]);
		}
	}
}
