using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200108B RID: 4235
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Forces the Rigidbody2D to wake up. Returns Success.")]
	public class WakeUp : Conditional
	{
		// Token: 0x06007318 RID: 29464 RVA: 0x002AF360 File Offset: 0x002AD560
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007319 RID: 29465 RVA: 0x002AF3A0 File Offset: 0x002AD5A0
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.WakeUp();
			return 2;
		}

		// Token: 0x0600731A RID: 29466 RVA: 0x002AF3C8 File Offset: 0x002AD5C8
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005EC5 RID: 24261
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EC6 RID: 24262
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005EC7 RID: 24263
		private GameObject prevGameObject;
	}
}
