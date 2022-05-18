using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001559 RID: 5465
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Moves the Rigidbody to the specified position. Returns Success.")]
	public class MovePosition : Action
	{
		// Token: 0x06008162 RID: 33122 RVA: 0x002CC22C File Offset: 0x002CA42C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008163 RID: 33123 RVA: 0x0005860E File Offset: 0x0005680E
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

		// Token: 0x06008164 RID: 33124 RVA: 0x00058641 File Offset: 0x00056841
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04006E1C RID: 28188
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E1D RID: 28189
		[Tooltip("The new position of the Rigidbody")]
		public SharedVector3 position;

		// Token: 0x04006E1E RID: 28190
		private Rigidbody rigidbody;

		// Token: 0x04006E1F RID: 28191
		private GameObject prevGameObject;
	}
}
