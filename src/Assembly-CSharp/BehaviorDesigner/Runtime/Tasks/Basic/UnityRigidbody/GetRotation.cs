using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200109A RID: 4250
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the rotation of the Rigidbody. Returns Success.")]
	public class GetRotation : Action
	{
		// Token: 0x06007354 RID: 29524 RVA: 0x002AFC40 File Offset: 0x002ADE40
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007355 RID: 29525 RVA: 0x002AFC80 File Offset: 0x002ADE80
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.rotation;
			return 2;
		}

		// Token: 0x06007356 RID: 29526 RVA: 0x002AFCB3 File Offset: 0x002ADEB3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Quaternion.identity;
		}

		// Token: 0x04005F0A RID: 24330
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F0B RID: 24331
		[Tooltip("The rotation of the Rigidbody")]
		[RequiredField]
		public SharedQuaternion storeValue;

		// Token: 0x04005F0C RID: 24332
		private Rigidbody rigidbody;

		// Token: 0x04005F0D RID: 24333
		private GameObject prevGameObject;
	}
}
