using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005FB RID: 1531
	public class InteractableObject : MonoBehaviour
	{
		// Token: 0x06003134 RID: 12596 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnRaycastStart(PlayerEventHandler player)
		{
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnRaycastUpdate(PlayerEventHandler player)
		{
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnRaycastEnd(PlayerEventHandler player)
		{
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnInteract(PlayerEventHandler player)
		{
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void OnInteractHold(PlayerEventHandler player)
		{
		}
	}
}
