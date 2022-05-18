using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000EDB RID: 3803
	public class DATATYPE_VECTOR3 : DATATYPE_BASE
	{
		// Token: 0x06005B82 RID: 23426 RVA: 0x0004072D File Offset: 0x0003E92D
		public override object createFromStream(MemoryStream stream)
		{
			return new Vector3(stream.readFloat(), stream.readFloat(), stream.readFloat());
		}

		// Token: 0x06005B83 RID: 23427 RVA: 0x0004074B File Offset: 0x0003E94B
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeFloat(((Vector3)v).x);
			stream.writeFloat(((Vector3)v).y);
			stream.writeFloat(((Vector3)v).z);
		}

		// Token: 0x06005B84 RID: 23428 RVA: 0x00040780 File Offset: 0x0003E980
		public override object parseDefaultValStr(string v)
		{
			return new Vector3(0f, 0f, 0f);
		}

		// Token: 0x06005B85 RID: 23429 RVA: 0x0004079B File Offset: 0x0003E99B
		public override bool isSameType(object v)
		{
			return v != null && v.GetType() == typeof(Vector3);
		}
	}
}
