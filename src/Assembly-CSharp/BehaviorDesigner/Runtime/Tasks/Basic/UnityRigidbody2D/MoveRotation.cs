using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001082 RID: 4226
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Rotates the Rigidbody2D to the specified rotation. Returns Success.")]
	public class MoveRotation : Action
	{
		// Token: 0x060072F4 RID: 29428 RVA: 0x002AEE90 File Offset: 0x002AD090
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072F5 RID: 29429 RVA: 0x002AEED0 File Offset: 0x002AD0D0
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody2D.MoveRotation(this.rotation.Value);
			return 2;
		}

		// Token: 0x060072F6 RID: 29430 RVA: 0x002AEF03 File Offset: 0x002AD103
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rotation = 0f;
		}

		// Token: 0x04005EA2 RID: 24226
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EA3 RID: 24227
		[Tooltip("The new rotation of the Rigidbody")]
		public SharedFloat rotation;

		// Token: 0x04005EA4 RID: 24228
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005EA5 RID: 24229
		private GameObject prevGameObject;
	}
}
