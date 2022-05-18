using System;

namespace KBEngine
{
	// Token: 0x02000ED9 RID: 3801
	public class DATATYPE_STRING : DATATYPE_BASE
	{
		// Token: 0x06005B78 RID: 23416 RVA: 0x0004068D File Offset: 0x0003E88D
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readString();
		}

		// Token: 0x06005B79 RID: 23417 RVA: 0x00040695 File Offset: 0x0003E895
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeString(Convert.ToString(v));
		}

		// Token: 0x06005B7A RID: 23418 RVA: 0x00010DC9 File Offset: 0x0000EFC9
		public override object parseDefaultValStr(string v)
		{
			return v;
		}

		// Token: 0x06005B7B RID: 23419 RVA: 0x000406A3 File Offset: 0x0003E8A3
		public override bool isSameType(object v)
		{
			return v != null && v.GetType() == typeof(string);
		}
	}
}
