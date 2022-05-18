using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001565 RID: 5477
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the use gravity value of the Rigidbody. Returns Success.")]
	public class SetUseGravity : Action
	{
		// Token: 0x06008192 RID: 33170 RVA: 0x002CC52C File Offset: 0x002CA72C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008193 RID: 33171 RVA: 0x00058988 File Offset: 0x00056B88
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.useGravity = this.isKinematic.Value;
			return 2;
		}

		// Token: 0x06008194 RID: 33172 RVA: 0x000589BB File Offset: 0x00056BBB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.isKinematic = false;
		}

		// Token: 0x04006E4C RID: 28236
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E4D RID: 28237
		[Tooltip("The use gravity value of the Rigidbody")]
		public SharedBool isKinematic;

		// Token: 0x04006E4E RID: 28238
		private Rigidbody rigidbody;

		// Token: 0x04006E4F RID: 28239
		private GameObject prevGameObject;
	}
}
