using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x020010AD RID: 4269
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Forces the Rigidbody to sleep at least one frame. Returns Success.")]
	public class Sleep : Conditional
	{
		// Token: 0x060073A0 RID: 29600 RVA: 0x002B0660 File Offset: 0x002AE860
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060073A1 RID: 29601 RVA: 0x002B06A0 File Offset: 0x002AE8A0
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.Sleep();
			return 2;
		}

		// Token: 0x060073A2 RID: 29602 RVA: 0x002B06C8 File Offset: 0x002AE8C8
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005F54 RID: 24404
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F55 RID: 24405
		private Rigidbody rigidbody;

		// Token: 0x04005F56 RID: 24406
		private GameObject prevGameObject;
	}
}
