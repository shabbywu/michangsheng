using System;

namespace KBEngine
{
	// Token: 0x02000ED2 RID: 3794
	public class DATATYPE_INT64 : DATATYPE_BASE
	{
		// Token: 0x06005B55 RID: 23381 RVA: 0x000405CF File Offset: 0x0003E7CF
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readInt64();
		}

		// Token: 0x06005B56 RID: 23382 RVA: 0x000405DC File Offset: 0x0003E7DC
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeInt64(Convert.ToInt64(v));
		}

		// Token: 0x06005B57 RID: 23383 RVA: 0x00250B68 File Offset: 0x0024ED68
		public override object parseDefaultValStr(string v)
		{
			long num = 0L;
			long.TryParse(v, out num);
			return num;
		}

		// Token: 0x06005B58 RID: 23384 RVA: 0x00250B88 File Offset: 0x0024ED88
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
