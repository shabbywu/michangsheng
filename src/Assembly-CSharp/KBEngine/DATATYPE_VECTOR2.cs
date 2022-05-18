using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000EDA RID: 3802
	public class DATATYPE_VECTOR2 : DATATYPE_BASE
	{
		// Token: 0x06005B7D RID: 23421 RVA: 0x000406BF File Offset: 0x0003E8BF
		public override object createFromStream(MemoryStream stream)
		{
			return new Vector2(stream.readFloat(), stream.readFloat());
		}

		// Token: 0x06005B7E RID: 23422 RVA: 0x000406D7 File Offset: 0x0003E8D7
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeFloat(((Vector2)v).x);
			stream.writeFloat(((Vector2)v).y);
		}

		// Token: 0x06005B7F RID: 23423 RVA: 0x000406FB File Offset: 0x0003E8FB
		public override object parseDefaultValStr(string v)
		{
			return new Vector2(0f, 0f);
		}

		// Token: 0x06005B80 RID: 23424 RVA: 0x00040711 File Offset: 0x0003E911
		public override bool isSameType(object v)
		{
			return v != null && v.GetType() == typeof(Vector2);
		}
	}
}
