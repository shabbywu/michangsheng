using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x0200104B RID: 4171
	public class SCEntityObject : SCObject
	{
		// Token: 0x06006437 RID: 25655 RVA: 0x00044EEF File Offset: 0x000430EF
		public SCEntityObject(int id)
		{
			this.targetID = id;
		}

		// Token: 0x06006438 RID: 25656 RVA: 0x0000A093 File Offset: 0x00008293
		public override bool valid(Entity caster)
		{
			return true;
		}

		// Token: 0x06006439 RID: 25657 RVA: 0x00281248 File Offset: 0x0027F448
		public override Vector3 getPosition()
		{
			Entity entity = KBEngineApp.app.findEntity(this.targetID);
			if (entity == null)
			{
				return base.getPosition();
			}
			return entity.position;
		}

		// Token: 0x04005DD1 RID: 24017
		public int targetID;
	}
}
