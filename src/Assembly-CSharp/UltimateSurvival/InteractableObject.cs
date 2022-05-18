using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008D5 RID: 2261
	public class InteractableObject : MonoBehaviour
	{
		// Token: 0x06003A31 RID: 14897 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnRaycastStart(PlayerEventHandler player)
		{
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnRaycastUpdate(PlayerEventHandler player)
		{
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnRaycastEnd(PlayerEventHandler player)
		{
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnInteract(PlayerEventHandler player)
		{
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void OnInteractHold(PlayerEventHandler player)
		{
		}
	}
}
