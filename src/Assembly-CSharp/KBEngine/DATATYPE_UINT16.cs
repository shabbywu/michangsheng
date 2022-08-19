using System;

namespace KBEngine
{
	// Token: 0x02000B57 RID: 2903
	public class DATATYPE_UINT16 : DATATYPE_BASE
	{
		// Token: 0x06005123 RID: 20771 RVA: 0x002216ED File Offset: 0x0021F8ED
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readUint16();
		}

		// Token: 0x06005124 RID: 20772 RVA: 0x002216FA File Offset: 0x0021F8FA
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeUint16(Convert.ToUInt16(v));
		}

		// Token: 0x06005125 RID: 20773 RVA: 0x00221708 File Offset: 0x0021F908
		public override object parseDefaultValStr(string v)
		{
			ushort num = 0;
			ushort.TryParse(v, out num);
			return num;
		}

		// Token: 0x06005126 RID: 20774 RVA: 0x00221728 File Offset: 0x0021F928
		public override bool isSameType(object v)
		{
			if (!KBEMath.isNumeric(v))
			{
				return false;
			}
			decimal d = Convert.ToDecimal(v);
			return d >= 0m && d <= 65535m;
		}
	}
}
