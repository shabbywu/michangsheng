using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001090 RID: 4240
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Applies a torque to the rigidbody relative to its coordinate system. Returns Success.")]
	public class AddRelativeTorque : Action
	{
		// Token: 0x0600732C RID: 29484 RVA: 0x002AF6CC File Offset: 0x002AD8CC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600732D RID: 29485 RVA: 0x002AF70C File Offset: 0x002AD90C
		public override TaskStatus OnUpdate()
		{
			this.rigidbody.AddRelativeTorque(this.torque.Value, this.forceMode);
			return 2;
		}

		// Token: 0x0600732E RID: 29486 RVA: 0x002AF72B File Offset: 0x002AD92B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.torque = Vector3.zero;
			this.forceMode = 0;
		}

		// Token: 0x04005EE0 RID: 24288
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EE1 RID: 24289
		[Tooltip("The amount of torque to apply")]
		public SharedVector3 torque;

		// Token: 0x04005EE2 RID: 24290
		[Tooltip("The type of torque")]
		public ForceMode forceMode;

		// Token: 0x04005EE3 RID: 24291
		private Rigidbody rigidbody;

		// Token: 0x04005EE4 RID: 24292
		private GameObject prevGameObject;
	}
}
