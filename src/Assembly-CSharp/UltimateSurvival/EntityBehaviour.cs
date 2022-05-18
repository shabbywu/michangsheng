using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000889 RID: 2185
	public class EntityBehaviour : MonoBehaviour
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600385E RID: 14430 RVA: 0x00028FD5 File Offset: 0x000271D5
		public EntityEventHandler Entity
		{
			get
			{
				if (!this.m_Entity)
				{
					this.m_Entity = base.GetComponent<EntityEventHandler>();
				}
				if (!this.m_Entity)
				{
					this.m_Entity = base.GetComponentInParent<EntityEventHandler>();
				}
				return this.m_Entity;
			}
		}

		// Token: 0x040032BC RID: 12988
		private EntityEventHandler m_Entity;
	}
}
