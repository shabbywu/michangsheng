using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001074 RID: 4212
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Applies a force at the specified position to the Rigidbody2D. Returns Success.")]
	public class AddForceAtPosition : Action
	{
		// Token: 0x060072BC RID: 29372 RVA: 0x002AE6F8 File Offset: 0x002AC8F8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072BD RID: 29373 RVA: 0x002AE738 File Offset: 0x002AC938
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

		// Token: 0x060072BE RID: 29374 RVA: 0x002AE776 File Offset: 0x002AC976
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.force = Vector2.zero;
			this.position = Vector2.zero;
		}

		// Token: 0x04005E6B RID: 24171
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E6C RID: 24172
		[Tooltip("The amount of force to apply")]
		public SharedVector2 force;

		// Token: 0x04005E6D RID: 24173
		[Tooltip("The position of the force")]
		public SharedVector2 position;

		// Token: 0x04005E6E RID: 24174
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005E6F RID: 24175
		private GameObject prevGameObject;
	}
}
