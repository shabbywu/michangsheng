using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200107C RID: 4220
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the position of the Rigidbody2D. Returns Success.")]
	public class GetPosition : Action
	{
		// Token: 0x060072DC RID: 29404 RVA: 0x002AEB70 File Offset: 0x002ACD70
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072DD RID: 29405 RVA: 0x002AEBB0 File Offset: 0x002ACDB0
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody2D.position;
			return 2;
		}

		// Token: 0x060072DE RID: 29406 RVA: 0x002AEBE3 File Offset: 0x002ACDE3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector2.zero;
		}

		// Token: 0x04005E8C RID: 24204
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E8D RID: 24205
		[Tooltip("The velocity of the Rigidbody2D")]
		[RequiredField]
		public SharedVector2 storeValue;

		// Token: 0x04005E8E RID: 24206
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005E8F RID: 24207
		private GameObject prevGameObject;
	}
}
