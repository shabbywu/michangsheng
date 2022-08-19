using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000C89 RID: 3209
	public class SCPositionObject : SCObject
	{
		// Token: 0x0600597D RID: 22909 RVA: 0x00255FDA File Offset: 0x002541DA
		public SCPositionObject(Vector3 position)
		{
			this.targetPos = position;
		}

		// Token: 0x0600597E RID: 22910 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool valid(Entity caster)
		{
			return true;
		}

		// Token: 0x0600597F RID: 22911 RVA: 0x00255FE9 File Offset: 0x002541E9
		public override Vector3 getPosition()
		{
			return this.targetPos;
		}

		// Token: 0x04005226 RID: 21030
		public Vector3 targetPos;
	}
}
