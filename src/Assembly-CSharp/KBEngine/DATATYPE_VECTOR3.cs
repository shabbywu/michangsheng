using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000B5E RID: 2910
	public class DATATYPE_VECTOR3 : DATATYPE_BASE
	{
		// Token: 0x06005146 RID: 20806 RVA: 0x00221A4C File Offset: 0x0021FC4C
		public override object createFromStream(MemoryStream stream)
		{
			return new Vector3(stream.readFloat(), stream.readFloat(), stream.readFloat());
		}

		// Token: 0x06005147 RID: 20807 RVA: 0x00221A6A File Offset: 0x0021FC6A
		public override void addToStream(Bundle stream, object v)
		{
			stream.writeFloat(((Vector3)v).x);
			stream.writeFloat(((Vector3)v).y);
			stream.writeFloat(((Vector3)v).z);
		}

		// Token: 0x06005148 RID: 20808 RVA: 0x00221A9F File Offset: 0x0021FC9F
		public override object parseDefaultValStr(string v)
		{
			return new Vector3(0f, 0f, 0f);
		}

		// Token: 0x06005149 RID: 20809 RVA: 0x00221ABA File Offset: 0x0021FCBA
		public override bool isSameType(object v)
		{
			return v != null && v.GetType() == typeof(Vector3);
		}
	}
}
