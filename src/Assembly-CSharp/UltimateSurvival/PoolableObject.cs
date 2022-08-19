using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005E2 RID: 1506
	public class PoolableObject : MonoBehaviour
	{
		// Token: 0x06003080 RID: 12416 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnSpawn()
		{
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnDespawn()
		{
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnPoolableDestroy()
		{
		}
	}
}
