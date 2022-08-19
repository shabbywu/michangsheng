using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x020010A0 RID: 4256
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Rotates the Rigidbody to the specified rotation. Returns Success.")]
	public class MoveRotation : Action
	{
		// Token: 0x0600736C RID: 29548 RVA: 0x002AFF5C File Offset: 0x002AE15C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600736D RID: 29549 RVA: 0x002AFF9C File Offset: 0x002AE19C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.MoveRotation(this.rotation.Value);
			return 2;
		}

		// Token: 0x0600736E RID: 29550 RVA: 0x002AFFCF File Offset: 0x002AE1CF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x04005F20 RID: 24352
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F21 RID: 24353
		[Tooltip("The new rotation of the Rigidbody")]
		public SharedQuaternion rotation;

		// Token: 0x04005F22 RID: 24354
		private Rigidbody rigidbody;

		// Token: 0x04005F23 RID: 24355
		private GameObject prevGameObject;
	}
}
