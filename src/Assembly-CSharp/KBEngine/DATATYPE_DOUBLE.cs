using System;

namespace KBEngine
{
	// Token: 0x02000ED8 RID: 3800
	public class DATATYPE_DOUBLE : DATATYPE_BASE
	{
		// Token: 0x06005B73 RID: 23411 RVA: 0x00040672 File Offset: 0x0003E872
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readDouble();
		}

		// Token: 0x06005B74 RID: 23412 RVA: 0x0004067F File Offset: 0x0003E87F
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeDouble(Convert.ToDouble(v));
		}

		// Token: 0x06005B75 RID: 23413 RVA: 0x00250DD8 File Offset: 0x0024EFD8
		public override object parseDefaultValStr(string v)
		{
			double num = 0.0;
			double.TryParse(v, out num);
			return num;
		}

		// Token: 0x06005B76 RID: 23414 RVA: 0x00250E00 File Offset: 0x0024F000
		public override bool isSameType(object v)
		{
			if (v is float)
			{
				return (double)((float)v) >= double.MinValue && (double)((float)v) <= double.MaxValue;
			}
			return v is double && (double)v >= double.MinValue && (double)v <= double.MaxValue;
		}
	}
}
