using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x020010A2 RID: 4258
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the angular velocity of the Rigidbody. Returns Success.")]
	public class SetAngularVelocity : Action
	{
		// Token: 0x06007374 RID: 29556 RVA: 0x002B0074 File Offset: 0x002AE274
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007375 RID: 29557 RVA: 0x002B00B4 File Offset: 0x002AE2B4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.angularVelocity = this.angularVelocity.Value;
			return 2;
		}

		// Token: 0x06007376 RID: 29558 RVA: 0x002B00E7 File Offset: 0x002AE2E7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularVelocity = Vector3.zero;
		}

		// Token: 0x04005F28 RID: 24360
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F29 RID: 24361
		[Tooltip("The angular velocity of the Rigidbody")]
		public SharedVector3 angularVelocity;

		// Token: 0x04005F2A RID: 24362
		private Rigidbody rigidbody;

		// Token: 0x04005F2B RID: 24363
		private GameObject prevGameObject;
	}
}
