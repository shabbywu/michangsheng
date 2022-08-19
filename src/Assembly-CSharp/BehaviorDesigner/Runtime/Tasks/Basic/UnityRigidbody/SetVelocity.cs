using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x020010AC RID: 4268
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the velocity of the Rigidbody. Returns Success.")]
	public class SetVelocity : Action
	{
		// Token: 0x0600739C RID: 29596 RVA: 0x002B05D4 File Offset: 0x002AE7D4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600739D RID: 29597 RVA: 0x002B0614 File Offset: 0x002AE814
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.velocity = this.velocity.Value;
			return 2;
		}

		// Token: 0x0600739E RID: 29598 RVA: 0x002B0647 File Offset: 0x002AE847
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.velocity = Vector3.zero;
		}

		// Token: 0x04005F50 RID: 24400
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F51 RID: 24401
		[Tooltip("The velocity of the Rigidbody")]
		public SharedVector3 velocity;

		// Token: 0x04005F52 RID: 24402
		private Rigidbody rigidbody;

		// Token: 0x04005F53 RID: 24403
		private GameObject prevGameObject;
	}
}
