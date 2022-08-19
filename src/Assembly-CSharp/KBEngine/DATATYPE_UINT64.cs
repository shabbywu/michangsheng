using System;

namespace KBEngine
{
	// Token: 0x02000B59 RID: 2905
	public class DATATYPE_UINT64 : DATATYPE_BASE
	{
		// Token: 0x0600512D RID: 20781 RVA: 0x002217D9 File Offset: 0x0021F9D9
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readUint64();
		}

		// Token: 0x0600512E RID: 20782 RVA: 0x002217E6 File Offset: 0x0021F9E6
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeUint64(Convert.ToUInt64(v));
		}

		// Token: 0x0600512F RID: 20783 RVA: 0x002217F4 File Offset: 0x0021F9F4
		public override object parseDefaultValStr(string v)
		{
			ulong num = 0UL;
			ulong.TryParse(v, out num);
			return num;
		}

		// Token: 0x06005130 RID: 20784 RVA: 0x00221814 File Offset: 0x0021FA14
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
