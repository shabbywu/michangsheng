using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001531 RID: 5425
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the angular velocity of the Rigidbody2D. Returns Success.")]
	public class GetAngularVelocity : Action
	{
		// Token: 0x060080C2 RID: 32962 RVA: 0x002CB724 File Offset: 0x002C9924
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080C3 RID: 32963 RVA: 0x00057B1A File Offset: 0x00055D1A
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

		// Token: 0x060080C4 RID: 32964 RVA: 0x00057B4D File Offset: 0x00055D4D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006D78 RID: 28024
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D79 RID: 28025
		[Tooltip("The angular velocity of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006D7A RID: 28026
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006D7B RID: 28027
		private GameObject prevGameObject;
	}
}
