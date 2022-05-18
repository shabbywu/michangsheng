using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000EDC RID: 3804
	public class DATATYPE_VECTOR4 : DATATYPE_BASE
	{
		// Token: 0x06005B87 RID: 23431 RVA: 0x000407B7 File Offset: 0x0003E9B7
		public override object createFromStream(MemoryStream stream)
		{
			return new Vector4(stream.readFloat(), stream.readFloat(), stream.readFloat(), stream.readFloat());
		}

		// Token: 0x06005B88 RID: 23432 RVA: 0x00250E70 File Offset: 0x0024F070
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeFloat(((Vector4)v).x);
			stream.writeFloat(((Vector4)v).y);
			stream.writeFloat(((Vector4)v).z);
			stream.writeFloat(((Vector4)v).w);
		}

		// Token: 0x06005B89 RID: 23433 RVA: 0x000407DB File Offset: 0x0003E9DB
		public override object parseDefaultValStr(string v)
		{
			return new Vector4(0f, 0f, 0f, 0f);
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x000407FB File Offset: 0x0003E9FB
		public override bool isSameType(object v)
		{
			return v != null && v.GetType() == typeof(Vector4);
		}
	}
}
