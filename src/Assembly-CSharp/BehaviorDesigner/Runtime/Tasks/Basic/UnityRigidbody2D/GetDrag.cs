using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001078 RID: 4216
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the drag of the Rigidbody2D. Returns Success.")]
	public class GetDrag : Action
	{
		// Token: 0x060072CC RID: 29388 RVA: 0x002AE944 File Offset: 0x002ACB44
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072CD RID: 29389 RVA: 0x002AE984 File Offset: 0x002ACB84
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody2D.drag;
			return 2;
		}

		// Token: 0x060072CE RID: 29390 RVA: 0x002AE9B7 File Offset: 0x002ACBB7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04005E7C RID: 24188
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E7D RID: 24189
		[Tooltip("The drag of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04005E7E RID: 24190
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005E7F RID: 24191
		private GameObject prevGameObject;
	}
}
