using System;

namespace KBEngine
{
	// Token: 0x02000B56 RID: 2902
	public class DATATYPE_UINT8 : DATATYPE_BASE
	{
		// Token: 0x0600511E RID: 20766 RVA: 0x00221672 File Offset: 0x0021F872
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readUint8();
		}

		// Token: 0x0600511F RID: 20767 RVA: 0x0022167F File Offset: 0x0021F87F
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeUint8(Convert.ToByte(v));
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x00221690 File Offset: 0x0021F890
		public override object parseDefaultValStr(string v)
		{
			byte b = 0;
			byte.TryParse(v, out b);
			return b;
		}

		// Token: 0x06005121 RID: 20769 RVA: 0x002216B0 File Offset: 0x0021F8B0
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
