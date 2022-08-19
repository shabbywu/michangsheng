using System;

namespace KBEngine
{
	// Token: 0x02000B60 RID: 2912
	public class DATATYPE_PYTHON : DATATYPE_BASE
	{
		// Token: 0x06005150 RID: 20816 RVA: 0x00221B89 File Offset: 0x0021FD89
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readBlob();
		}

		// Token: 0x06005151 RID: 20817 RVA: 0x00221B91 File Offset: 0x0021FD91
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeBlob((byte[])v);
		}

		// Token: 0x06005152 RID: 20818 RVA: 0x00221B9F File Offset: 0x0021FD9F
		public override object parseDefaultValStr(string v)
		{
			return new byte[0];
		}

		// Token: 0x06005153 RID: 20819 RVA: 0x00221BA7 File Offset: 0x0021FDA7
		public override bool isSameType(object v)
		{
			return v != null && v.GetType() == typeof(byte[]);
		}
	}
}
