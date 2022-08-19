using System;

namespace KBEngine
{
	// Token: 0x02000B53 RID: 2899
	public class DATATYPE_INT16 : DATATYPE_BASE
	{
		// Token: 0x0600510F RID: 20751 RVA: 0x002214EC File Offset: 0x0021F6EC
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readInt16();
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x002214F9 File Offset: 0x0021F6F9
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeInt16(Convert.ToInt16(v));
		}

		// Token: 0x06005111 RID: 20753 RVA: 0x00221508 File Offset: 0x0021F708
		public override object parseDefaultValStr(string v)
		{
			short num = 0;
			short.TryParse(v, out num);
			return num;
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x00221528 File Offset: 0x0021F728
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
