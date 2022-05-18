using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008B0 RID: 2224
	public class PoolableObject : MonoBehaviour
	{
		// Token: 0x0600394C RID: 14668 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnSpawn()
		{
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnDespawn()
		{
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnPoolableDestroy()
		{
		}
	}
}
