using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001079 RID: 4217
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the gravity scale of the Rigidbody2D. Returns Success.")]
	public class GetGravityScale : Action
	{
		// Token: 0x060072D0 RID: 29392 RVA: 0x002AE9D0 File Offset: 0x002ACBD0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072D1 RID: 29393 RVA: 0x002AEA10 File Offset: 0x002ACC10
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody2D.gravityScale;
			return 2;
		}

		// Token: 0x060072D2 RID: 29394 RVA: 0x002AEA43 File Offset: 0x002ACC43
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04005E80 RID: 24192
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E81 RID: 24193
		[Tooltip("The gravity scale of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04005E82 RID: 24194
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005E83 RID: 24195
		private GameObject prevGameObject;
	}
}
