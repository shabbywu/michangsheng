using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001548 RID: 5448
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Applies a force at the specified position to the rigidbody. Returns Success.")]
	public class AddForceAtPosition : Action
	{
		// Token: 0x0600811E RID: 33054 RVA: 0x002CBD9C File Offset: 0x002C9F9C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600811F RID: 33055 RVA: 0x002CBDDC File Offset: 0x002C9FDC
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

		// Token: 0x06008120 RID: 33056 RVA: 0x0005814B File Offset: 0x0005634B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.force = Vector3.zero;
			this.position = Vector3.zero;
			this.forceMode = 0;
		}

		// Token: 0x04006DD5 RID: 28117
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DD6 RID: 28118
		[Tooltip("The amount of force to apply")]
		public SharedVector3 force;

		// Token: 0x04006DD7 RID: 28119
		[Tooltip("The position of the force")]
		public SharedVector3 position;

		// Token: 0x04006DD8 RID: 28120
		[Tooltip("The type of force")]
		public ForceMode forceMode;

		// Token: 0x04006DD9 RID: 28121
		private Rigidbody rigidbody;

		// Token: 0x04006DDA RID: 28122
		private GameObject prevGameObject;
	}
}
