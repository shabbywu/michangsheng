using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001332 RID: 4914
	[EventHandlerInfo("MonoBehaviour", "Trigger", "The block will execute when a 3d physics trigger matching some basic conditions is met.")]
	[AddComponentMenu("")]
	public class Trigger : BasePhysicsEventHandler
	{
		// Token: 0x06007773 RID: 30579 RVA: 0x00051740 File Offset: 0x0004F940
		private void OnTriggerEnter(Collider col)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Enter, col.tag);
		}

		// Token: 0x06007774 RID: 30580 RVA: 0x0005174F File Offset: 0x0004F94F
		private void OnTriggerStay(Collider col)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Stay, col.tag);
		}

		// Token: 0x06007775 RID: 30581 RVA: 0x0005175E File Offset: 0x0004F95E
		private void OnTriggerExit(Collider col)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Exit, col.tag);
		}
	}
}
