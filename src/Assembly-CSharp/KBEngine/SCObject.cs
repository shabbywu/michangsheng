using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x0200104A RID: 4170
	public class SCObject
	{
		// Token: 0x06006435 RID: 25653 RVA: 0x0000A093 File Offset: 0x00008293
		public virtual bool valid(Entity caster)
		{
			return true;
		}

		// Token: 0x06006436 RID: 25654 RVA: 0x00044EE8 File Offset: 0x000430E8
		public virtual Vector3 getPosition()
		{
			return Vector3.zero;
		}
	}
}
