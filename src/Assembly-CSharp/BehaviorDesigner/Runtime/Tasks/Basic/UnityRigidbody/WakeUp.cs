using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x020010AF RID: 4271
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Forces the Rigidbody to wake up. Returns Success.")]
	public class WakeUp : Conditional
	{
		// Token: 0x060073A8 RID: 29608 RVA: 0x002B074C File Offset: 0x002AE94C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060073A9 RID: 29609 RVA: 0x002B078C File Offset: 0x002AE98C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.WakeUp();
			return 2;
		}

		// Token: 0x060073AA RID: 29610 RVA: 0x002B07B4 File Offset: 0x002AE9B4
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005F5A RID: 24410
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F5B RID: 24411
		private Rigidbody rigidbody;

		// Token: 0x04005F5C RID: 24412
		private GameObject prevGameObject;
	}
}
