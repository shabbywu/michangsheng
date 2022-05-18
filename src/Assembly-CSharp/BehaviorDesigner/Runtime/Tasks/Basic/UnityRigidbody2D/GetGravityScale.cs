using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001533 RID: 5427
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the gravity scale of the Rigidbody2D. Returns Success.")]
	public class GetGravityScale : Action
	{
		// Token: 0x060080CA RID: 32970 RVA: 0x002CB7A4 File Offset: 0x002C99A4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080CB RID: 32971 RVA: 0x00057BB2 File Offset: 0x00055DB2
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

		// Token: 0x060080CC RID: 32972 RVA: 0x00057BE5 File Offset: 0x00055DE5
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006D80 RID: 28032
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D81 RID: 28033
		[Tooltip("The gravity scale of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006D82 RID: 28034
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006D83 RID: 28035
		private GameObject prevGameObject;
	}
}
