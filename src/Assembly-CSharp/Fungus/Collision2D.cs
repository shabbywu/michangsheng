using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EA6 RID: 3750
	[EventHandlerInfo("MonoBehaviour", "Collision2D", "The block will execute when a 2d physics collision matching some basic conditions is met.")]
	[AddComponentMenu("")]
	public class Collision2D : BasePhysicsEventHandler
	{
		// Token: 0x06006A1F RID: 27167 RVA: 0x00292A93 File Offset: 0x00290C93
		private void OnCollisionEnter2D(Collision2D collision)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Enter, collision.collider.tag);
		}

		// Token: 0x06006A20 RID: 27168 RVA: 0x00292AA7 File Offset: 0x00290CA7
		private void OnCollisionStay2D(Collision2D collision)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Stay, collision.collider.tag);
		}

		// Token: 0x06006A21 RID: 27169 RVA: 0x00292ABB File Offset: 0x00290CBB
		private void OnCollisionExit2D(Collision2D collision)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Exit, collision.collider.tag);
		}
	}
}
