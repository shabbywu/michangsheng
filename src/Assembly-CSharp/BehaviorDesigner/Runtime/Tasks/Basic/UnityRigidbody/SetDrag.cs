using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200155F RID: 5471
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the drag of the Rigidbody. Returns Success.")]
	public class SetDrag : Action
	{
		// Token: 0x0600817A RID: 33146 RVA: 0x002CC3AC File Offset: 0x002CA5AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600817B RID: 33147 RVA: 0x000587C8 File Offset: 0x000569C8
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.drag = this.drag.Value;
			return 2;
		}

		// Token: 0x0600817C RID: 33148 RVA: 0x000587FB File Offset: 0x000569FB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.drag = 0f;
		}

		// Token: 0x04006E34 RID: 28212
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E35 RID: 28213
		[Tooltip("The drag of the Rigidbody")]
		public SharedFloat drag;

		// Token: 0x04006E36 RID: 28214
		private Rigidbody rigidbody;

		// Token: 0x04006E37 RID: 28215
		private GameObject prevGameObject;
	}
}
