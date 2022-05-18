using System;

namespace KBEngine
{
	// Token: 0x02000ED5 RID: 3797
	public class DATATYPE_UINT32 : DATATYPE_BASE
	{
		// Token: 0x06005B64 RID: 23396 RVA: 0x00040620 File Offset: 0x0003E820
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readUint32();
		}

		// Token: 0x06005B65 RID: 23397 RVA: 0x0004062D File Offset: 0x0003E82D
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeUint32(Convert.ToUInt32(v));
		}

		// Token: 0x06005B66 RID: 23398 RVA: 0x00250C94 File Offset: 0x0024EE94
		public override object parseDefaultValStr(string v)
		{
			uint num = 0U;
			uint.TryParse(v, out num);
			return num;
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x00250CB4 File Offset: 0x0024EEB4
		public override bool isSameType(object v)
		{
			if (!KBEMath.isNumeric(v))
			{
				return false;
			}
			decimal d = Convert.ToDecimal(v);
			return d >= 0m && d <= 4294967295m;
		}
	}
}
