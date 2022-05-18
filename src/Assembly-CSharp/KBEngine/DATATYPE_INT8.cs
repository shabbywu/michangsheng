using System;

namespace KBEngine
{
	// Token: 0x02000ECF RID: 3791
	public class DATATYPE_INT8 : DATATYPE_BASE
	{
		// Token: 0x06005B46 RID: 23366 RVA: 0x0004057E File Offset: 0x0003E77E
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readInt8();
		}

		// Token: 0x06005B47 RID: 23367 RVA: 0x0004058B File Offset: 0x0003E78B
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeInt8(Convert.ToSByte(v));
		}

		// Token: 0x06005B48 RID: 23368 RVA: 0x00250A44 File Offset: 0x0024EC44
		public override object parseDefaultValStr(string v)
		{
			sbyte b = 0;
			sbyte.TryParse(v, out b);
			return b;
		}

		// Token: 0x06005B49 RID: 23369 RVA: 0x00250A64 File Offset: 0x0024EC64
		public override bool isSameType(object v)
		{
			if (!KBEMath.isNumeric(v))
			{
				return false;
			}
			decimal d = Convert.ToDecimal(v);
			return d >= -128m && d <= 127m;
		}
	}
}
