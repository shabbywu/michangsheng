using System;

namespace KBEngine
{
	// Token: 0x02000ED0 RID: 3792
	public class DATATYPE_INT16 : DATATYPE_BASE
	{
		// Token: 0x06005B4B RID: 23371 RVA: 0x00040599 File Offset: 0x0003E799
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readInt16();
		}

		// Token: 0x06005B4C RID: 23372 RVA: 0x000405A6 File Offset: 0x0003E7A6
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeInt16(Convert.ToInt16(v));
		}

		// Token: 0x06005B4D RID: 23373 RVA: 0x00250AA0 File Offset: 0x0024ECA0
		public override object parseDefaultValStr(string v)
		{
			short num = 0;
			short.TryParse(v, out num);
			return num;
		}

		// Token: 0x06005B4E RID: 23374 RVA: 0x00250AC0 File Offset: 0x0024ECC0
		public override bool isSameType(object v)
		{
			if (!KBEMath.isNumeric(v))
			{
				return false;
			}
			decimal d = Convert.ToDecimal(v);
			return d >= -32768m && d <= 32767m;
		}
	}
}
