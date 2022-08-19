using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x020010AB RID: 4267
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the use gravity value of the Rigidbody. Returns Success.")]
	public class SetUseGravity : Action
	{
		// Token: 0x06007398 RID: 29592 RVA: 0x002B054C File Offset: 0x002AE74C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007399 RID: 29593 RVA: 0x002B058C File Offset: 0x002AE78C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.useGravity = this.isKinematic.Value;
			return 2;
		}

		// Token: 0x0600739A RID: 29594 RVA: 0x002B05BF File Offset: 0x002AE7BF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.isKinematic = false;
		}

		// Token: 0x04005F4C RID: 24396
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F4D RID: 24397
		[Tooltip("The use gravity value of the Rigidbody")]
		public SharedBool isKinematic;

		// Token: 0x04005F4E RID: 24398
		private Rigidbody rigidbody;

		// Token: 0x04005F4F RID: 24399
		private GameObject prevGameObject;
	}
}
