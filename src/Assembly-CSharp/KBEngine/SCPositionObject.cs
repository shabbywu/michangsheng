using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x0200104C RID: 4172
	public class SCPositionObject : SCObject
	{
		// Token: 0x0600643A RID: 25658 RVA: 0x00044EFE File Offset: 0x000430FE
		public SCPositionObject(Vector3 position)
		{
			this.targetPos = position;
		}

		// Token: 0x0600643B RID: 25659 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool valid(Entity caster)
		{
			return true;
		}

		// Token: 0x0600643C RID: 25660 RVA: 0x00044F0D File Offset: 0x0004310D
		public override Vector3 getPosition()
		{
			return this.targetPos;
		}

		// Token: 0x04005DD2 RID: 24018
		public Vector3 targetPos;
	}
}
