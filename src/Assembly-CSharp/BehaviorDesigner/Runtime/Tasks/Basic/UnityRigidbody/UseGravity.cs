using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001568 RID: 5480
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Returns Success if the Rigidbody is using gravity, otherwise Failure.")]
	public class UseGravity : Conditional
	{
		// Token: 0x0600819E RID: 33182 RVA: 0x002CC5EC File Offset: 0x002CA7EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600819F RID: 33183 RVA: 0x00058A4D File Offset: 0x00056C4D
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			if (!this.rigidbody.useGravity)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060081A0 RID: 33184 RVA: 0x00058A79 File Offset: 0x00056C79
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04006E57 RID: 28247
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E58 RID: 28248
		private Rigidbody rigidbody;

		// Token: 0x04006E59 RID: 28249
		private GameObject prevGameObject;
	}
}
