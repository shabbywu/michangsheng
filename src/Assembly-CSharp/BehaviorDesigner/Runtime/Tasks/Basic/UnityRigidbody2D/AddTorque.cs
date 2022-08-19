using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001075 RID: 4213
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Applies a torque to the Rigidbody2D. Returns Success.")]
	public class AddTorque : Action
	{
		// Token: 0x060072C0 RID: 29376 RVA: 0x002AE7A0 File Offset: 0x002AC9A0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072C1 RID: 29377 RVA: 0x002AE7E0 File Offset: 0x002AC9E0
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.AddTorque(this.torque.Value);
			return 2;
		}

		// Token: 0x060072C2 RID: 29378 RVA: 0x002AE813 File Offset: 0x002ACA13
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.torque = 0f;
		}

		// Token: 0x04005E70 RID: 24176
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E71 RID: 24177
		[Tooltip("The amount of torque to apply")]
		public SharedFloat torque;

		// Token: 0x04005E72 RID: 24178
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005E73 RID: 24179
		private GameObject prevGameObject;
	}
}
