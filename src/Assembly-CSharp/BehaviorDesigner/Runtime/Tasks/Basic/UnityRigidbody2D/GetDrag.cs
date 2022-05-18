using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001532 RID: 5426
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the drag of the Rigidbody2D. Returns Success.")]
	public class GetDrag : Action
	{
		// Token: 0x060080C6 RID: 32966 RVA: 0x002CB764 File Offset: 0x002C9964
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080C7 RID: 32967 RVA: 0x00057B66 File Offset: 0x00055D66
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody2D.drag;
			return 2;
		}

		// Token: 0x060080C8 RID: 32968 RVA: 0x00057B99 File Offset: 0x00055D99
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006D7C RID: 28028
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D7D RID: 28029
		[Tooltip("The drag of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006D7E RID: 28030
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006D7F RID: 28031
		private GameObject prevGameObject;
	}
}
