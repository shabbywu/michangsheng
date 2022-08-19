using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EA5 RID: 3749
	[EventHandlerInfo("MonoBehaviour", "Collision", "The block will execute when a 3d physics collision matching some basic conditions is met.")]
	[AddComponentMenu("")]
	public class Collision : BasePhysicsEventHandler
	{
		// Token: 0x06006A1B RID: 27163 RVA: 0x00292A4F File Offset: 0x00290C4F
		private void OnCollisionEnter(Collision collision)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Enter, collision.collider.tag);
		}

		// Token: 0x06006A1C RID: 27164 RVA: 0x00292A63 File Offset: 0x00290C63
		private void OnCollisionStay(Collision collision)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Stay, collision.collider.tag);
		}

		// Token: 0x06006A1D RID: 27165 RVA: 0x00292A77 File Offset: 0x00290C77
		private void OnCollisionExit(Collision collision)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Exit, collision.collider.tag);
		}
	}
}
