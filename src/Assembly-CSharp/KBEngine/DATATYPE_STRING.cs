using System;

namespace KBEngine
{
	// Token: 0x02000B5C RID: 2908
	public class DATATYPE_STRING : DATATYPE_BASE
	{
		// Token: 0x0600513C RID: 20796 RVA: 0x002219AC File Offset: 0x0021FBAC
		public override object createFromStream(MemoryStream stream)
		{
			return stream.readString();
		}

		// Token: 0x0600513D RID: 20797 RVA: 0x002219B4 File Offset: 0x0021FBB4
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeString(Convert.ToString(v));
		}

		// Token: 0x0600513E RID: 20798 RVA: 0x001086F1 File Offset: 0x001068F1
		public override object parseDefaultValStr(string v)
		{
			return v;
		}

		// Token: 0x0600513F RID: 20799 RVA: 0x002219C2 File Offset: 0x0021FBC2
		public override bool isSameType(object v)
		{
			return v != null && v.GetType() == typeof(string);
		}
	}
}
