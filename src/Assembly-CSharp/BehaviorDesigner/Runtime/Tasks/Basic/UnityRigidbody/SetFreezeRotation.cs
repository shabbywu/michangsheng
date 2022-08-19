using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x020010A6 RID: 4262
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the freeze rotation value of the Rigidbody. Returns Success.")]
	public class SetFreezeRotation : Action
	{
		// Token: 0x06007384 RID: 29572 RVA: 0x002B0298 File Offset: 0x002AE498
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007385 RID: 29573 RVA: 0x002B02D8 File Offset: 0x002AE4D8
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.freezeRotation = this.freezeRotation.Value;
			return 2;
		}

		// Token: 0x06007386 RID: 29574 RVA: 0x002B030B File Offset: 0x002AE50B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.freezeRotation = false;
		}

		// Token: 0x04005F38 RID: 24376
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F39 RID: 24377
		[Tooltip("The freeze rotation value of the Rigidbody")]
		public SharedBool freezeRotation;

		// Token: 0x04005F3A RID: 24378
		private Rigidbody rigidbody;

		// Token: 0x04005F3B RID: 24379
		private GameObject prevGameObject;
	}
}
