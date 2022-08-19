using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001077 RID: 4215
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the angular velocity of the Rigidbody2D. Returns Success.")]
	public class GetAngularVelocity : Action
	{
		// Token: 0x060072C8 RID: 29384 RVA: 0x002AE8B8 File Offset: 0x002ACAB8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072C9 RID: 29385 RVA: 0x002AE8F8 File Offset: 0x002ACAF8
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody2D.angularVelocity;
			return 2;
		}

		// Token: 0x060072CA RID: 29386 RVA: 0x002AE92B File Offset: 0x002ACB2B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04005E78 RID: 24184
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E79 RID: 24185
		[Tooltip("The angular velocity of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04005E7A RID: 24186
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005E7B RID: 24187
		private GameObject prevGameObject;
	}
}
