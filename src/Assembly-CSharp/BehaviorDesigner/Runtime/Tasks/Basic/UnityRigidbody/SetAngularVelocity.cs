using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200155C RID: 5468
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the angular velocity of the Rigidbody. Returns Success.")]
	public class SetAngularVelocity : Action
	{
		// Token: 0x0600816E RID: 33134 RVA: 0x002CC2EC File Offset: 0x002CA4EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600816F RID: 33135 RVA: 0x000586F2 File Offset: 0x000568F2
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.angularVelocity = this.angularVelocity.Value;
			return 2;
		}

		// Token: 0x06008170 RID: 33136 RVA: 0x00058725 File Offset: 0x00056925
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularVelocity = Vector3.zero;
		}

		// Token: 0x04006E28 RID: 28200
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E29 RID: 28201
		[Tooltip("The angular velocity of the Rigidbody")]
		public SharedVector3 angularVelocity;

		// Token: 0x04006E2A RID: 28202
		private Rigidbody rigidbody;

		// Token: 0x04006E2B RID: 28203
		private GameObject prevGameObject;
	}
}
