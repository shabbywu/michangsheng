using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000B5F RID: 2911
	public class DATATYPE_VECTOR4 : DATATYPE_BASE
	{
		// Token: 0x0600514B RID: 20811 RVA: 0x00221AD6 File Offset: 0x0021FCD6
		public override object createFromStream(MemoryStream stream)
		{
			return new Vector4(stream.readFloat(), stream.readFloat(), stream.readFloat(), stream.readFloat());
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x00221AFC File Offset: 0x0021FCFC
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeFloat(((Vector4)v).x);
			stream.writeFloat(((Vector4)v).y);
			stream.writeFloat(((Vector4)v).z);
			stream.writeFloat(((Vector4)v).w);
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x00221B4D File Offset: 0x0021FD4D
		public override object parseDefaultValStr(string v)
		{
			return new Vector4(0f, 0f, 0f, 0f);
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x00221B6D File Offset: 0x0021FD6D
		public override bool isSameType(object v)
		{
			return v != null && v.GetType() == typeof(Vector4);
		}
	}
}
