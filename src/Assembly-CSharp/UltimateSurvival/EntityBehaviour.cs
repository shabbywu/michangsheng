using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005C7 RID: 1479
	public class EntityBehaviour : MonoBehaviour
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06002FCE RID: 12238 RVA: 0x001590CD File Offset: 0x001572CD
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

		// Token: 0x04002A26 RID: 10790
		private EntityEventHandler m_Entity;
	}
}
