using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EAD RID: 3757
	[EventHandlerInfo("MonoBehaviour", "Trigger2D", "The block will execute when a 2d physics trigger matching some basic conditions is met.")]
	[AddComponentMenu("")]
	public class Trigger2D : BasePhysicsEventHandler
	{
		// Token: 0x06006A41 RID: 27201 RVA: 0x00292C68 File Offset: 0x00290E68
		private void OnTriggerEnter2D(Collider2D col)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Enter, col.tag);
		}

		// Token: 0x06006A42 RID: 27202 RVA: 0x00292C77 File Offset: 0x00290E77
		private void OnTriggerStay2D(Collider2D col)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Stay, col.tag);
		}

		// Token: 0x06006A43 RID: 27203 RVA: 0x00292C86 File Offset: 0x00290E86
		private void OnTriggerExit2D(Collider2D col)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Exit, col.tag);
		}
	}
}
