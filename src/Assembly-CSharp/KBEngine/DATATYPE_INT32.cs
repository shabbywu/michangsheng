using System;

namespace KBEngine
{
	// Token: 0x02000ED1 RID: 3793
	public class DATATYPE_INT32 : DATATYPE_BASE
	{
		// Token: 0x06005B50 RID: 23376 RVA: 0x000405B4 File Offset: 0x0003E7B4
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readInt32();
		}

		// Token: 0x06005B51 RID: 23377 RVA: 0x000405C1 File Offset: 0x0003E7C1
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeInt32(Convert.ToInt32(v));
		}

		// Token: 0x06005B52 RID: 23378 RVA: 0x00250B04 File Offset: 0x0024ED04
		public override object parseDefaultValStr(string v)
		{
			int num = 0;
			int.TryParse(v, out num);
			return num;
		}

		// Token: 0x06005B53 RID: 23379 RVA: 0x00250B24 File Offset: 0x0024ED24
		public override bool isSameType(object v)
		{
			if (!KBEMath.isNumeric(v))
			{
				return false;
			}
			decimal d = Convert.ToDecimal(v);
			return d >= -2147483648m && d <= 2147483647m;
		}
	}
}
