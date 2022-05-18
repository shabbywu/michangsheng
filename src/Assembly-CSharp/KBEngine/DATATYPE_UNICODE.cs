using System;
using System.Text;

namespace KBEngine
{
	// Token: 0x02000EDE RID: 3806
	public class DATATYPE_UNICODE : DATATYPE_BASE
	{
		// Token: 0x06005B91 RID: 23441 RVA: 0x00040851 File Offset: 0x0003EA51
		public override object createFromStream(MemoryStream stream)
		{
			return Encoding.UTF8.GetString(stream.readBlob());
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x00040863 File Offset: 0x0003EA63
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeBlob(Encoding.UTF8.GetBytes((string)v));
		}

		// Token: 0x06005B93 RID: 23443 RVA: 0x00010DC9 File Offset: 0x0000EFC9
		public override object parseDefaultValStr(string v)
		{
			return v;
		}

		// Token: 0x06005B94 RID: 23444 RVA: 0x000406A3 File Offset: 0x0003E8A3
		public override bool isSameType(object v)
		{
			return v != null && v.GetType() == typeof(string);
		}
	}
}
