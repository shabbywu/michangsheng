using System;

namespace KBEngine
{
	// Token: 0x02000B55 RID: 2901
	public class DATATYPE_INT64 : DATATYPE_BASE
	{
		// Token: 0x06005119 RID: 20761 RVA: 0x002215EA File Offset: 0x0021F7EA
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readInt64();
		}

		// Token: 0x0600511A RID: 20762 RVA: 0x002215F7 File Offset: 0x0021F7F7
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeInt64(Convert.ToInt64(v));
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x00221608 File Offset: 0x0021F808
		public override object parseDefaultValStr(string v)
		{
			long num = 0L;
			long.TryParse(v, out num);
			return num;
		}

		// Token: 0x0600511C RID: 20764 RVA: 0x00221628 File Offset: 0x0021F828
		public override bool isSameType(object v)
		{
			if (!KBEMath.isNumeric(v))
			{
				return false;
			}
			decimal d = Convert.ToDecimal(v);
			return d >= -9223372036854775808m && d <= 9223372036854775807m;
		}
	}
}
