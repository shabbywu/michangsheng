using System;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000C88 RID: 3208
	public class SCEntityObject : SCObject
	{
		// Token: 0x0600597A RID: 22906 RVA: 0x00255F9B File Offset: 0x0025419B
		public SCEntityObject(int id)
		{
			this.targetID = id;
		}

		// Token: 0x0600597B RID: 22907 RVA: 0x00024C5F File Offset: 0x00022E5F
		public override bool valid(Entity caster)
		{
			return true;
		}

		// Token: 0x0600597C RID: 22908 RVA: 0x00255FAC File Offset: 0x002541AC
		public override Vector3 getPosition()
		{
			Entity entity = KBEngineApp.app.findEntity(this.targetID);
			if (entity == null)
			{
				return base.getPosition();
			}
			return entity.position;
		}

		// Token: 0x04005225 RID: 21029
		public int targetID;
	}
}
