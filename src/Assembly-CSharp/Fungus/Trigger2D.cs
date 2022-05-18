using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001333 RID: 4915
	[EventHandlerInfo("MonoBehaviour", "Trigger2D", "The block will execute when a 2d physics trigger matching some basic conditions is met.")]
	[AddComponentMenu("")]
	public class Trigger2D : BasePhysicsEventHandler
	{
		// Token: 0x06007777 RID: 30583 RVA: 0x00051740 File Offset: 0x0004F940
		private void OnTriggerEnter2D(Collider2D col)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Enter, col.tag);
		}

		// Token: 0x06007778 RID: 30584 RVA: 0x0005174F File Offset: 0x0004F94F
		private void OnTriggerStay2D(Collider2D col)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Stay, col.tag);
		}

		// Token: 0x06007779 RID: 30585 RVA: 0x0005175E File Offset: 0x0004F95E
		private void OnTriggerExit2D(Collider2D col)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Exit, col.tag);
		}
	}
}
