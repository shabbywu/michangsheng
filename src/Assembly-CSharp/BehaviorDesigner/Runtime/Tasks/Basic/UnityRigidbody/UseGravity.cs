using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x020010AE RID: 4270
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Returns Success if the Rigidbody is using gravity, otherwise Failure.")]
	public class UseGravity : Conditional
	{
		// Token: 0x060073A4 RID: 29604 RVA: 0x002B06D4 File Offset: 0x002AE8D4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060073A5 RID: 29605 RVA: 0x002B0714 File Offset: 0x002AE914
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

		// Token: 0x060073A6 RID: 29606 RVA: 0x002B0740 File Offset: 0x002AE940
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005F57 RID: 24407
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F58 RID: 24408
		private Rigidbody rigidbody;

		// Token: 0x04005F59 RID: 24409
		private GameObject prevGameObject;
	}
}
