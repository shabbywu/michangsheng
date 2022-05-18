using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200152E RID: 5422
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Applies a force at the specified position to the Rigidbody2D. Returns Success.")]
	public class AddForceAtPosition : Action
	{
		// Token: 0x060080B6 RID: 32950 RVA: 0x002CB664 File Offset: 0x002C9864
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080B7 RID: 32951 RVA: 0x00057A1B File Offset: 0x00055C1B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.AddForceAtPosition(this.force.Value, this.position.Value);
			return 2;
		}

		// Token: 0x060080B8 RID: 32952 RVA: 0x00057A59 File Offset: 0x00055C59
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.force = Vector2.zero;
			this.position = Vector2.zero;
		}

		// Token: 0x04006D6B RID: 28011
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D6C RID: 28012
		[Tooltip("The amount of force to apply")]
		public SharedVector2 force;

		// Token: 0x04006D6D RID: 28013
		[Tooltip("The position of the force")]
		public SharedVector2 position;

		// Token: 0x04006D6E RID: 28014
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006D6F RID: 28015
		private GameObject prevGameObject;
	}
}
