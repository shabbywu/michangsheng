using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001081 RID: 4225
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Moves the Rigidbody2D to the specified position. Returns Success.")]
	public class MovePosition : Action
	{
		// Token: 0x060072F0 RID: 29424 RVA: 0x002AEE04 File Offset: 0x002AD004
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072F1 RID: 29425 RVA: 0x002AEE44 File Offset: 0x002AD044
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody2D.MovePosition(this.position.Value);
			return 2;
		}

		// Token: 0x060072F2 RID: 29426 RVA: 0x002AEE77 File Offset: 0x002AD077
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector2.zero;
		}

		// Token: 0x04005E9E RID: 24222
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E9F RID: 24223
		[Tooltip("The new position of the Rigidbody")]
		public SharedVector2 position;

		// Token: 0x04005EA0 RID: 24224
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005EA1 RID: 24225
		private GameObject prevGameObject;
	}
}
