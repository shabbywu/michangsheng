using System;
using System.Text;

namespace KBEngine
{
	// Token: 0x02000B61 RID: 2913
	public class DATATYPE_UNICODE : DATATYPE_BASE
	{
		// Token: 0x06005155 RID: 20821 RVA: 0x00221BC3 File Offset: 0x0021FDC3
		public override object createFromStream(MemoryStream stream)
		{
			return Encoding.UTF8.GetString(stream.readBlob());
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x00221BD5 File Offset: 0x0021FDD5
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeBlob(Encoding.UTF8.GetBytes((string)v));
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x001086F1 File Offset: 0x001068F1
		public override object parseDefaultValStr(string v)
		{
			return v;
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x002219C2 File Offset: 0x0021FBC2
		public override bool isSameType(object v)
		{
			return v != null && v.GetType() == typeof(string);
		}
	}
}
