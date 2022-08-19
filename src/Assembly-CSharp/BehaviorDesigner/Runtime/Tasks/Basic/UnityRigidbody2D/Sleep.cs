using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200108A RID: 4234
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Forces the Rigidbody2D to sleep at least one frame. Returns Success.")]
	public class Sleep : Conditional
	{
		// Token: 0x06007314 RID: 29460 RVA: 0x002AF2EC File Offset: 0x002AD4EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007315 RID: 29461 RVA: 0x002AF32C File Offset: 0x002AD52C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.Sleep();
			return 2;
		}

		// Token: 0x06007316 RID: 29462 RVA: 0x002AF354 File Offset: 0x002AD554
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005EC2 RID: 24258
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EC3 RID: 24259
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005EC4 RID: 24260
		private GameObject prevGameObject;
	}
}
