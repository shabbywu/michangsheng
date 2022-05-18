using System;

namespace KBEngine
{
	// Token: 0x02000ED4 RID: 3796
	public class DATATYPE_UINT16 : DATATYPE_BASE
	{
		// Token: 0x06005B5F RID: 23391 RVA: 0x00040605 File Offset: 0x0003E805
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readUint16();
		}

		// Token: 0x06005B60 RID: 23392 RVA: 0x00040612 File Offset: 0x0003E812
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeUint16(Convert.ToUInt16(v));
		}

		// Token: 0x06005B61 RID: 23393 RVA: 0x00250C34 File Offset: 0x0024EE34
		public override object parseDefaultValStr(string v)
		{
			ushort num = 0;
			ushort.TryParse(v, out num);
			return num;
		}

		// Token: 0x06005B62 RID: 23394 RVA: 0x00250C54 File Offset: 0x0024EE54
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
