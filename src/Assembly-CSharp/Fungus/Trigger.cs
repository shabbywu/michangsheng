using System;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000EAC RID: 3756
	[EventHandlerInfo("MonoBehaviour", "Trigger", "The block will execute when a 3d physics trigger matching some basic conditions is met.")]
	[AddComponentMenu("")]
	public class Trigger : BasePhysicsEventHandler
	{
		// Token: 0x06006A3D RID: 27197 RVA: 0x00292C68 File Offset: 0x00290E68
		private void OnTriggerEnter(Collider col)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Enter, col.tag);
		}

		// Token: 0x06006A3E RID: 27198 RVA: 0x00292C77 File Offset: 0x00290E77
		private void OnTriggerStay(Collider col)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Stay, col.tag);
		}

		// Token: 0x06006A3F RID: 27199 RVA: 0x00292C86 File Offset: 0x00290E86
		private void OnTriggerExit(Collider col)
		{
			base.ProcessCollider(BasePhysicsEventHandler.PhysicsMessageType.Exit, col.tag);
		}
	}
}
