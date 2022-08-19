using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001086 RID: 4230
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Sets the gravity scale of the Rigidbody2D. Returns Success.")]
	public class SetGravityScale : Action
	{
		// Token: 0x06007304 RID: 29444 RVA: 0x002AF0C0 File Offset: 0x002AD2C0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007305 RID: 29445 RVA: 0x002AF100 File Offset: 0x002AD300
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

		// Token: 0x06007306 RID: 29446 RVA: 0x002AF133 File Offset: 0x002AD333
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.gravityScale = 0f;
		}

		// Token: 0x04005EB2 RID: 24242
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EB3 RID: 24243
		[Tooltip("The gravity scale of the Rigidbody2D")]
		public SharedFloat gravityScale;

		// Token: 0x04005EB4 RID: 24244
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005EB5 RID: 24245
		private GameObject prevGameObject;
	}
}
