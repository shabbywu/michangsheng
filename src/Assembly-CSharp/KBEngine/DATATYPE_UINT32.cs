using System;

namespace KBEngine
{
	// Token: 0x02000B58 RID: 2904
	public class DATATYPE_UINT32 : DATATYPE_BASE
	{
		// Token: 0x06005128 RID: 20776 RVA: 0x00221765 File Offset: 0x0021F965
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readUint32();
		}

		// Token: 0x06005129 RID: 20777 RVA: 0x00221772 File Offset: 0x0021F972
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeUint32(Convert.ToUInt32(v));
		}

		// Token: 0x0600512A RID: 20778 RVA: 0x00221780 File Offset: 0x0021F980
		public override object parseDefaultValStr(string v)
		{
			uint num = 0U;
			uint.TryParse(v, out num);
			return num;
		}

		// Token: 0x0600512B RID: 20779 RVA: 0x002217A0 File Offset: 0x0021F9A0
		public override bool isSameType(object v)
		{
			if (!KBEMath.isNumeric(v))
			{
				return false;
			}
			decimal d = Convert.ToDecimal(v);
			return d >= 0m && d <= 4294967295m;
		}
	}
}
