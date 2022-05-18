using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001554 RID: 5460
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the rotation of the Rigidbody. Returns Success.")]
	public class GetRotation : Action
	{
		// Token: 0x0600814E RID: 33102 RVA: 0x002CC0EC File Offset: 0x002CA2EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600814F RID: 33103 RVA: 0x000584C4 File Offset: 0x000566C4
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

		// Token: 0x06008150 RID: 33104 RVA: 0x000584F7 File Offset: 0x000566F7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Quaternion.identity;
		}

		// Token: 0x04006E0A RID: 28170
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E0B RID: 28171
		[Tooltip("The rotation of the Rigidbody")]
		[RequiredField]
		public SharedQuaternion storeValue;

		// Token: 0x04006E0C RID: 28172
		private Rigidbody rigidbody;

		// Token: 0x04006E0D RID: 28173
		private GameObject prevGameObject;
	}
}
