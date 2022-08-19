using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001091 RID: 4241
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Applies a torque to the rigidbody. Returns Success.")]
	public class AddTorque : Action
	{
		// Token: 0x06007330 RID: 29488 RVA: 0x002AF74C File Offset: 0x002AD94C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007331 RID: 29489 RVA: 0x002AF78C File Offset: 0x002AD98C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.AddTorque(this.torque.Value, this.forceMode);
			return 2;
		}

		// Token: 0x06007332 RID: 29490 RVA: 0x002AF7C5 File Offset: 0x002AD9C5
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.torque = Vector3.zero;
			this.forceMode = 0;
		}

		// Token: 0x04005EE5 RID: 24293
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EE6 RID: 24294
		[Tooltip("The amount of torque to apply")]
		public SharedVector3 torque;

		// Token: 0x04005EE7 RID: 24295
		[Tooltip("The type of torque")]
		public ForceMode forceMode;

		// Token: 0x04005EE8 RID: 24296
		private Rigidbody rigidbody;

		// Token: 0x04005EE9 RID: 24297
		private GameObject prevGameObject;
	}
}
