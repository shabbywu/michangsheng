using System;

namespace KBEngine
{
	// Token: 0x02000EDF RID: 3807
	public class DATATYPE_ENTITYCALL : DATATYPE_BASE
	{
		// Token: 0x06005B96 RID: 23446 RVA: 0x00040817 File Offset: 0x0003EA17
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readBlob();
		}

		// Token: 0x06005B97 RID: 23447 RVA: 0x0004081F File Offset: 0x0003EA1F
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeBlob((byte[])v);
		}

		// Token: 0x06005B98 RID: 23448 RVA: 0x0004082D File Offset: 0x0003EA2D
		public override object parseDefaultValStr(string v)
		{
			return new byte[0];
		}

		// Token: 0x06005B99 RID: 23449 RVA: 0x00040835 File Offset: 0x0003EA35
		public override bool isSameType(object v)
		{
			return v != null && v.GetType() == typeof(byte[]);
		}
	}
}
