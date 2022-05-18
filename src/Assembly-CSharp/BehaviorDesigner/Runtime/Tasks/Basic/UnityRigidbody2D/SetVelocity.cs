using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001543 RID: 5443
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Sets the velocity of the Rigidbody2D. Returns Success.")]
	public class SetVelocity : Action
	{
		// Token: 0x0600810A RID: 33034 RVA: 0x002CBBA4 File Offset: 0x002C9DA4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600810B RID: 33035 RVA: 0x0005803C File Offset: 0x0005623C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.velocity = this.velocity.Value;
			return 2;
		}

		// Token: 0x0600810C RID: 33036 RVA: 0x0005806F File Offset: 0x0005626F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.velocity = Vector2.zero;
		}

		// Token: 0x04006DBE RID: 28094
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DBF RID: 28095
		[Tooltip("The velocity of the Rigidbody2D")]
		public SharedVector2 velocity;

		// Token: 0x04006DC0 RID: 28096
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006DC1 RID: 28097
		private GameObject prevGameObject;
	}
}
