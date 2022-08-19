using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000C87 RID: 3207
	public class SCObject
	{
		// Token: 0x06005978 RID: 22904 RVA: 0x00024C5F File Offset: 0x00022E5F
		public virtual bool valid(Entity caster)
		{
			return true;
		}

		// Token: 0x06005979 RID: 22905 RVA: 0x00255F94 File Offset: 0x00254194
		public virtual Vector3 getPosition()
		{
			return Vector3.zero;
		}
	}
}
