using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200108E RID: 4238
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Applies a force at the specified position to the rigidbody. Returns Success.")]
	public class AddForceAtPosition : Action
	{
		// Token: 0x06007324 RID: 29476 RVA: 0x002AF570 File Offset: 0x002AD770
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007325 RID: 29477 RVA: 0x002AF5B0 File Offset: 0x002AD7B0
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.AddForceAtPosition(this.force.Value, this.position.Value, this.forceMode);
			return 2;
		}

		// Token: 0x06007326 RID: 29478 RVA: 0x002AF5FF File Offset: 0x002AD7FF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.force = Vector3.zero;
			this.position = Vector3.zero;
			this.forceMode = 0;
		}

		// Token: 0x04005ED5 RID: 24277
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005ED6 RID: 24278
		[Tooltip("The amount of force to apply")]
		public SharedVector3 force;

		// Token: 0x04005ED7 RID: 24279
		[Tooltip("The position of the force")]
		public SharedVector3 position;

		// Token: 0x04005ED8 RID: 24280
		[Tooltip("The type of force")]
		public ForceMode forceMode;

		// Token: 0x04005ED9 RID: 24281
		private Rigidbody rigidbody;

		// Token: 0x04005EDA RID: 24282
		private GameObject prevGameObject;
	}
}
