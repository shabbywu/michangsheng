using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200109F RID: 4255
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Moves the Rigidbody to the specified position. Returns Success.")]
	public class MovePosition : Action
	{
		// Token: 0x06007368 RID: 29544 RVA: 0x002AFED0 File Offset: 0x002AE0D0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007369 RID: 29545 RVA: 0x002AFF10 File Offset: 0x002AE110
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.MovePosition(this.position.Value);
			return 2;
		}

		// Token: 0x0600736A RID: 29546 RVA: 0x002AFF43 File Offset: 0x002AE143
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04005F1C RID: 24348
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F1D RID: 24349
		[Tooltip("The new position of the Rigidbody")]
		public SharedVector3 position;

		// Token: 0x04005F1E RID: 24350
		private Rigidbody rigidbody;

		// Token: 0x04005F1F RID: 24351
		private GameObject prevGameObject;
	}
}
