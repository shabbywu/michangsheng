using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x020010A7 RID: 4263
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the is kinematic value of the Rigidbody. Returns Success.")]
	public class SetIsKinematic : Action
	{
		// Token: 0x06007388 RID: 29576 RVA: 0x002B0320 File Offset: 0x002AE520
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007389 RID: 29577 RVA: 0x002B0360 File Offset: 0x002AE560
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.isKinematic = this.isKinematic.Value;
			return 2;
		}

		// Token: 0x0600738A RID: 29578 RVA: 0x002B0393 File Offset: 0x002AE593
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.isKinematic = false;
		}

		// Token: 0x04005F3C RID: 24380
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F3D RID: 24381
		[Tooltip("The is kinematic value of the Rigidbody")]
		public SharedBool isKinematic;

		// Token: 0x04005F3E RID: 24382
		private Rigidbody rigidbody;

		// Token: 0x04005F3F RID: 24383
		private GameObject prevGameObject;
	}
}
