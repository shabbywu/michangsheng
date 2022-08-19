using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000B5D RID: 2909
	public class DATATYPE_VECTOR2 : DATATYPE_BASE
	{
		// Token: 0x06005141 RID: 20801 RVA: 0x002219DE File Offset: 0x0021FBDE
		public override object createFromStream(MemoryStream stream)
		{
			return new Vector2(stream.readFloat(), stream.readFloat());
		}

		// Token: 0x06005142 RID: 20802 RVA: 0x002219F6 File Offset: 0x0021FBF6
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeFloat(((Vector2)v).x);
			stream.writeFloat(((Vector2)v).y);
		}

		// Token: 0x06005143 RID: 20803 RVA: 0x00221A1A File Offset: 0x0021FC1A
		public override object parseDefaultValStr(string v)
		{
			return new Vector2(0f, 0f);
		}

		// Token: 0x06005144 RID: 20804 RVA: 0x00221A30 File Offset: 0x0021FC30
		public override bool isSameType(object v)
		{
			return v != null && v.GetType() == typeof(Vector2);
		}
	}
}
