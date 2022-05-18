using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001556 RID: 5462
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the velocity of the Rigidbody. Returns Success.")]
	public class GetVelocity : Action
	{
		// Token: 0x06008156 RID: 33110 RVA: 0x002CC16C File Offset: 0x002CA36C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008157 RID: 33111 RVA: 0x00058558 File Offset: 0x00056758
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.velocity;
			return 2;
		}

		// Token: 0x06008158 RID: 33112 RVA: 0x0005858B File Offset: 0x0005678B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006E12 RID: 28178
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006E13 RID: 28179
		[Tooltip("The velocity of the Rigidbody")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006E14 RID: 28180
		private Rigidbody rigidbody;

		// Token: 0x04006E15 RID: 28181
		private GameObject prevGameObject;
	}
}
