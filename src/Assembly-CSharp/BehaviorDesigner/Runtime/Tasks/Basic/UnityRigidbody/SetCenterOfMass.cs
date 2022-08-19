using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x020010A3 RID: 4259
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the center of mass of the Rigidbody. Returns Success.")]
	public class SetCenterOfMass : Action
	{
		// Token: 0x06007378 RID: 29560 RVA: 0x002B0100 File Offset: 0x002AE300
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007379 RID: 29561 RVA: 0x002B0140 File Offset: 0x002AE340
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.centerOfMass = this.centerOfMass.Value;
			return 2;
		}

		// Token: 0x0600737A RID: 29562 RVA: 0x002B0173 File Offset: 0x002AE373
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.centerOfMass = Vector3.zero;
		}

		// Token: 0x04005F2C RID: 24364
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F2D RID: 24365
		[Tooltip("The center of mass of the Rigidbody")]
		public SharedVector3 centerOfMass;

		// Token: 0x04005F2E RID: 24366
		private Rigidbody rigidbody;

		// Token: 0x04005F2F RID: 24367
		private GameObject prevGameObject;
	}
}
