using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200109C RID: 4252
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the velocity of the Rigidbody. Returns Success.")]
	public class GetVelocity : Action
	{
		// Token: 0x0600735C RID: 29532 RVA: 0x002AFD54 File Offset: 0x002ADF54
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600735D RID: 29533 RVA: 0x002AFD94 File Offset: 0x002ADF94
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.velocity;
			return 2;
		}

		// Token: 0x0600735E RID: 29534 RVA: 0x002AFDC7 File Offset: 0x002ADFC7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04005F12 RID: 24338
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F13 RID: 24339
		[Tooltip("The velocity of the Rigidbody")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04005F14 RID: 24340
		private Rigidbody rigidbody;

		// Token: 0x04005F15 RID: 24341
		private GameObject prevGameObject;
	}
}
