using System;

namespace KBEngine
{
	// Token: 0x02000B54 RID: 2900
	public class DATATYPE_INT32 : DATATYPE_BASE
	{
		// Token: 0x06005114 RID: 20756 RVA: 0x0022156A File Offset: 0x0021F76A
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readInt32();
		}

		// Token: 0x06005115 RID: 20757 RVA: 0x00221577 File Offset: 0x0021F777
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeInt32(Convert.ToInt32(v));
		}

		// Token: 0x06005116 RID: 20758 RVA: 0x00221588 File Offset: 0x0021F788
		public override object parseDefaultValStr(string v)
		{
			int num = 0;
			int.TryParse(v, out num);
			return num;
		}

		// Token: 0x06005117 RID: 20759 RVA: 0x002215A8 File Offset: 0x0021F7A8
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
