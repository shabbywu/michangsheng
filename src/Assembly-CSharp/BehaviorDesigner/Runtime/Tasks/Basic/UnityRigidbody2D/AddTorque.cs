using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200152F RID: 5423
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Applies a torque to the Rigidbody2D. Returns Success.")]
	public class AddTorque : Action
	{
		// Token: 0x060080BA RID: 32954 RVA: 0x002CB6A4 File Offset: 0x002C98A4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060080BB RID: 32955 RVA: 0x00057A82 File Offset: 0x00055C82
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

		// Token: 0x060080BC RID: 32956 RVA: 0x00057AB5 File Offset: 0x00055CB5
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.torque = 0f;
		}

		// Token: 0x04006D70 RID: 28016
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006D71 RID: 28017
		[Tooltip("The amount of torque to apply")]
		public SharedFloat torque;

		// Token: 0x04006D72 RID: 28018
		private Rigidbody2D rigidbody2D;

		// Token: 0x04006D73 RID: 28019
		private GameObject prevGameObject;
	}
}
