using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001328 RID: 4904
	[EventHandlerInfo("MonoBehaviour", "Collision2D", "The block will execute when a 2d physics collision matching some basic conditions is met.")]
	[AddComponentMenu("")]
	public class Collision2D : BasePhysicsEventHandler
	{
		// Token: 0x06007755 RID: 30549 RVA: 0x0005156B File Offset: 0x0004F76B
		private void OnCollisionEnter2D(Collision2D collision)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Enter, collision.collider.tag);
		}

		// Token: 0x06007756 RID: 30550 RVA: 0x0005157F File Offset: 0x0004F77F
		private void OnCollisionStay2D(Collision2D collision)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Stay, collision.collider.tag);
		}

		// Token: 0x06007757 RID: 30551 RVA: 0x00051593 File Offset: 0x0004F793
		private void OnCollisionExit2D(Collision2D collision)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Exit, collision.collider.tag);
		}
	}
}
