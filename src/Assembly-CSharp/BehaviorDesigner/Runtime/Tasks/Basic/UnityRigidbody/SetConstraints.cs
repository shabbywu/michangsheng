using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x020010A4 RID: 4260
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the constraints of the Rigidbody. Returns Success.")]
	public class SetConstraints : Action
	{
		// Token: 0x0600737C RID: 29564 RVA: 0x002B018C File Offset: 0x002AE38C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600737D RID: 29565 RVA: 0x002B01CC File Offset: 0x002AE3CC
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.constraints = this.constraints;
			return 2;
		}

		// Token: 0x0600737E RID: 29566 RVA: 0x002B01FA File Offset: 0x002AE3FA
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.constraints = 0;
		}

		// Token: 0x04005F30 RID: 24368
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F31 RID: 24369
		[Tooltip("The constraints of the Rigidbody")]
		public RigidbodyConstraints constraints;

		// Token: 0x04005F32 RID: 24370
		private Rigidbody rigidbody;

		// Token: 0x04005F33 RID: 24371
		private GameObject prevGameObject;
	}
}
