using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200152D RID: 5421
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Applies a force to the Rigidbody2D. Returns Success.")]
	public class AddForce : Action
	{
		// Token: 0x060080B2 RID: 32946 RVA: 0x002CB624 File Offset: 0x002C9824
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080B3 RID: 32947 RVA: 0x000579CF File Offset: 0x00055BCF
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.AddForce(this.force.Value);
			return 2;
		}

		// Token: 0x060080B4 RID: 32948 RVA: 0x00057A02 File Offset: 0x00055C02
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.force = Vector2.zero;
		}

		// Token: 0x04006D67 RID: 28007
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D68 RID: 28008
		[Tooltip("The amount of force to apply")]
		public SharedVector2 force;

		// Token: 0x04006D69 RID: 28009
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006D6A RID: 28010
		private GameObject prevGameObject;
	}
}
