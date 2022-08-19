using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x020010AA RID: 4266
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the rotation of the Rigidbody. Returns Success.")]
	public class SetRotation : Action
	{
		// Token: 0x06007394 RID: 29588 RVA: 0x002B04C0 File Offset: 0x002AE6C0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007395 RID: 29589 RVA: 0x002B0500 File Offset: 0x002AE700
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.rotation = this.rotation.Value;
			return 2;
		}

		// Token: 0x06007396 RID: 29590 RVA: 0x002B0533 File Offset: 0x002AE733
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x04005F48 RID: 24392
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F49 RID: 24393
		[Tooltip("The rotation of the Rigidbody")]
		public SharedQuaternion rotation;

		// Token: 0x04005F4A RID: 24394
		private Rigidbody rigidbody;

		// Token: 0x04005F4B RID: 24395
		private GameObject prevGameObject;
	}
}
