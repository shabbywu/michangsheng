using System;

namespace KBEngine
{
	// Token: 0x02000B52 RID: 2898
	public class DATATYPE_INT8 : DATATYPE_BASE
	{
		// Token: 0x0600510A RID: 20746 RVA: 0x00221475 File Offset: 0x0021F675
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readInt8();
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x00221482 File Offset: 0x0021F682
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeInt8(Convert.ToSByte(v));
		}

		// Token: 0x0600510C RID: 20748 RVA: 0x00221490 File Offset: 0x0021F690
		public override object parseDefaultValStr(string v)
		{
			sbyte b = 0;
			sbyte.TryParse(v, out b);
			return b;
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x002214B0 File Offset: 0x0021F6B0
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
