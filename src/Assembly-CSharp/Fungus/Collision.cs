using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001327 RID: 4903
	[EventHandlerInfo("MonoBehaviour", "Collision", "The block will execute when a 3d physics collision matching some basic conditions is met.")]
	[AddComponentMenu("")]
	public class Collision : BasePhysicsEventHandler
	{
		// Token: 0x06007751 RID: 30545 RVA: 0x00051527 File Offset: 0x0004F727
		private void OnCollisionEnter(Collision collision)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Enter, collision.collider.tag);
		}

		// Token: 0x06007752 RID: 30546 RVA: 0x0005153B File Offset: 0x0004F73B
		private void OnCollisionStay(Collision collision)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Stay, collision.collider.tag);
		}

		// Token: 0x06007753 RID: 30547 RVA: 0x0005154F File Offset: 0x0004F74F
		private void OnCollisionExit(Collision collision)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Exit, collision.collider.tag);
		}
	}
}
