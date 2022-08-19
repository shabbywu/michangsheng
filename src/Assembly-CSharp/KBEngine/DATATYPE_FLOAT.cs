using System;

namespace KBEngine
{
	// Token: 0x02000B5A RID: 2906
	public class DATATYPE_FLOAT : DATATYPE_BASE
	{
		// Token: 0x06005132 RID: 20786 RVA: 0x0022184E File Offset: 0x0021FA4E
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readFloat();
		}

		// Token: 0x06005133 RID: 20787 RVA: 0x0022185B File Offset: 0x0021FA5B
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeFloat((float)Convert.ToDouble(v));
		}

		// Token: 0x06005134 RID: 20788 RVA: 0x0022186C File Offset: 0x0021FA6C
		public override object parseDefaultValStr(string v)
		{
			float num = 0f;
			float.TryParse(v, out num);
			return num;
		}

		// Token: 0x06005135 RID: 20789 RVA: 0x00221890 File Offset: 0x0021FA90
		public override bool isSameType(object v)
		{
			if (v is float)
			{
				return (float)v >= float.MinValue && (float)v <= float.MaxValue;
			}
			return v is double && (double)v >= -3.4028234663852886E+38 && (double)v <= 3.4028234663852886E+38;
		}
	}
}
