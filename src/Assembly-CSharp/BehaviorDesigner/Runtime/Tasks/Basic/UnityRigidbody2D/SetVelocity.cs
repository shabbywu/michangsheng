using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001089 RID: 4233
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Sets the velocity of the Rigidbody2D. Returns Success.")]
	public class SetVelocity : Action
	{
		// Token: 0x06007310 RID: 29456 RVA: 0x002AF260 File Offset: 0x002AD460
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007311 RID: 29457 RVA: 0x002AF2A0 File Offset: 0x002AD4A0
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

		// Token: 0x06007312 RID: 29458 RVA: 0x002AF2D3 File Offset: 0x002AD4D3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.velocity = Vector2.zero;
		}

		// Token: 0x04005EBE RID: 24254
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EBF RID: 24255
		[Tooltip("The velocity of the Rigidbody2D")]
		public SharedVector2 velocity;

		// Token: 0x04005EC0 RID: 24256
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005EC1 RID: 24257
		private GameObject prevGameObject;
	}
}
