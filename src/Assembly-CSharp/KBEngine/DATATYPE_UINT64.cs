using System;

namespace KBEngine
{
	// Token: 0x02000ED6 RID: 3798
	public class DATATYPE_UINT64 : DATATYPE_BASE
	{
		// Token: 0x06005B69 RID: 23401 RVA: 0x0004063B File Offset: 0x0003E83B
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readUint64();
		}

		// Token: 0x06005B6A RID: 23402 RVA: 0x00040648 File Offset: 0x0003E848
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeUint64(Convert.ToUInt64(v));
		}

		// Token: 0x06005B6B RID: 23403 RVA: 0x00250CF0 File Offset: 0x0024EEF0
		public override object parseDefaultValStr(string v)
		{
			ulong num = 0UL;
			ulong.TryParse(v, out num);
			return num;
		}

		// Token: 0x06005B6C RID: 23404 RVA: 0x00250D10 File Offset: 0x0024EF10
		public override bool isSameType(object v)
		{
			if (!KBEMath.isNumeric(v))
			{
				return false;
			}
			decimal d = Convert.ToDecimal(v);
			return d >= 0m && d <= 18446744073709551615m;
		}
	}
}
