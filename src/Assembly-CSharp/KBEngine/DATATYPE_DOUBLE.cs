using System;

namespace KBEngine
{
	// Token: 0x02000B5B RID: 2907
	public class DATATYPE_DOUBLE : DATATYPE_BASE
	{
		// Token: 0x06005137 RID: 20791 RVA: 0x002218F6 File Offset: 0x0021FAF6
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readDouble();
		}

		// Token: 0x06005138 RID: 20792 RVA: 0x00221903 File Offset: 0x0021FB03
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeDouble(Convert.ToDouble(v));
		}

		// Token: 0x06005139 RID: 20793 RVA: 0x00221914 File Offset: 0x0021FB14
		public override object parseDefaultValStr(string v)
		{
			double num = 0.0;
			double.TryParse(v, out num);
			return num;
		}

		// Token: 0x0600513A RID: 20794 RVA: 0x0022193C File Offset: 0x0021FB3C
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
