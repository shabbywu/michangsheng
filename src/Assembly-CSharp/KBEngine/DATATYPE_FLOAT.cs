using System;

namespace KBEngine
{
	// Token: 0x02000ED7 RID: 3799
	public class DATATYPE_FLOAT : DATATYPE_BASE
	{
		// Token: 0x06005B6E RID: 23406 RVA: 0x00040656 File Offset: 0x0003E856
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readFloat();
		}

		// Token: 0x06005B6F RID: 23407 RVA: 0x00040663 File Offset: 0x0003E863
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeFloat((float)Convert.ToDouble(v));
		}

		// Token: 0x06005B70 RID: 23408 RVA: 0x00250D4C File Offset: 0x0024EF4C
		public override object parseDefaultValStr(string v)
		{
			float num = 0f;
			float.TryParse(v, out num);
			return num;
		}

		// Token: 0x06005B71 RID: 23409 RVA: 0x00250D70 File Offset: 0x0024EF70
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
