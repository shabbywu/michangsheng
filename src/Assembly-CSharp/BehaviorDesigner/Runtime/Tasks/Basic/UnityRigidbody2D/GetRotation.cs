using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001537 RID: 5431
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the rotation of the Rigidbody2D. Returns Success.")]
	public class GetRotation : Action
	{
		// Token: 0x060080DA RID: 32986 RVA: 0x002CB8A4 File Offset: 0x002C9AA4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080DB RID: 32987 RVA: 0x00057CDE File Offset: 0x00055EDE
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody2D.rotation;
			return 2;
		}

		// Token: 0x060080DC RID: 32988 RVA: 0x00057D11 File Offset: 0x00055F11
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006D90 RID: 28048
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D91 RID: 28049
		[Tooltip("The rotation of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006D92 RID: 28050
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006D93 RID: 28051
		private GameObject prevGameObject;
	}
}
