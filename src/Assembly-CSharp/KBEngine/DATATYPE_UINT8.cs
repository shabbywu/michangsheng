using System;

namespace KBEngine
{
	// Token: 0x02000ED3 RID: 3795
	public class DATATYPE_UINT8 : DATATYPE_BASE
	{
		// Token: 0x06005B5A RID: 23386 RVA: 0x000405EA File Offset: 0x0003E7EA
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readUint8();
		}

		// Token: 0x06005B5B RID: 23387 RVA: 0x000405F7 File Offset: 0x0003E7F7
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeUint8(Convert.ToByte(v));
		}

		// Token: 0x06005B5C RID: 23388 RVA: 0x00250BD4 File Offset: 0x0024EDD4
		public override object parseDefaultValStr(string v)
		{
			byte b = 0;
			byte.TryParse(v, out b);
			return b;
		}

		// Token: 0x06005B5D RID: 23389 RVA: 0x00250BF4 File Offset: 0x0024EDF4
		public override bool isSameType(object v)
		{
			if (!KBEMath.isNumeric(v))
			{
				return false;
			}
			decimal d = Convert.ToDecimal(v);
			return d >= 0m && d <= 255m;
		}
	}
}
