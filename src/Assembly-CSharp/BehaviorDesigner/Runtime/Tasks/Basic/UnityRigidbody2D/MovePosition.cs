using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200153B RID: 5435
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Moves the Rigidbody2D to the specified position. Returns Success.")]
	public class MovePosition : Action
	{
		// Token: 0x060080EA RID: 33002 RVA: 0x002CB9A4 File Offset: 0x002C9BA4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080EB RID: 33003 RVA: 0x00057DE0 File Offset: 0x00055FE0
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

		// Token: 0x060080EC RID: 33004 RVA: 0x00057E13 File Offset: 0x00056013
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector2.zero;
		}

		// Token: 0x04006D9E RID: 28062
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D9F RID: 28063
		[Tooltip("The new position of the Rigidbody")]
		public SharedVector2 position;

		// Token: 0x04006DA0 RID: 28064
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006DA1 RID: 28065
		private GameObject prevGameObject;
	}
}
