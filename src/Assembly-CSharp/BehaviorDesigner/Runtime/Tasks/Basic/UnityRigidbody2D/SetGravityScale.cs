using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001540 RID: 5440
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Sets the gravity scale of the Rigidbody2D. Returns Success.")]
	public class SetGravityScale : Action
	{
		// Token: 0x060080FE RID: 33022 RVA: 0x002CBAE4 File Offset: 0x002C9CE4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080FF RID: 33023 RVA: 0x00057F5C File Offset: 0x0005615C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.gravityScale = this.gravityScale.Value;
			return 2;
		}

		// Token: 0x06008100 RID: 33024 RVA: 0x00057F8F File Offset: 0x0005618F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.gravityScale = 0f;
		}

		// Token: 0x04006DB2 RID: 28082
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DB3 RID: 28083
		[Tooltip("The gravity scale of the Rigidbody2D")]
		public SharedFloat gravityScale;

		// Token: 0x04006DB4 RID: 28084
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006DB5 RID: 28085
		private GameObject prevGameObject;
	}
}
