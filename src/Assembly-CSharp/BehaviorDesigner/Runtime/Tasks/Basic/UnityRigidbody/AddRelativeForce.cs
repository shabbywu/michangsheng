using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200108F RID: 4239
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Applies a force to the rigidbody relative to its coordinate system. Returns Success.")]
	public class AddRelativeForce : Action
	{
		// Token: 0x06007328 RID: 29480 RVA: 0x002AF630 File Offset: 0x002AD830
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007329 RID: 29481 RVA: 0x002AF670 File Offset: 0x002AD870
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.AddRelativeForce(this.force.Value, this.forceMode);
			return 2;
		}

		// Token: 0x0600732A RID: 29482 RVA: 0x002AF6A9 File Offset: 0x002AD8A9
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.force = Vector3.zero;
			this.forceMode = 0;
		}

		// Token: 0x04005EDB RID: 24283
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EDC RID: 24284
		[Tooltip("The amount of force to apply")]
		public SharedVector3 force;

		// Token: 0x04005EDD RID: 24285
		[Tooltip("The type of force")]
		public ForceMode forceMode;

		// Token: 0x04005EDE RID: 24286
		private Rigidbody rigidbody;

		// Token: 0x04005EDF RID: 24287
		private GameObject prevGameObject;
	}
}
