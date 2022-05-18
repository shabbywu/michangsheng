using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200154A RID: 5450
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Applies a torque to the rigidbody relative to its coordinate system. Returns Success.")]
	public class AddRelativeTorque : Action
	{
		// Token: 0x06008126 RID: 33062 RVA: 0x002CBE6C File Offset: 0x002CA06C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008127 RID: 33063 RVA: 0x000581D4 File Offset: 0x000563D4
		public override TaskStatus OnUpdate()
		{
			this.rigidbody.AddRelativeTorque(this.torque.Value, this.forceMode);
			return 2;
		}

		// Token: 0x06008128 RID: 33064 RVA: 0x000581F3 File Offset: 0x000563F3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.torque = Vector3.zero;
			this.forceMode = 0;
		}

		// Token: 0x04006DE0 RID: 28128
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DE1 RID: 28129
		[Tooltip("The amount of torque to apply")]
		public SharedVector3 torque;

		// Token: 0x04006DE2 RID: 28130
		[Tooltip("The type of torque")]
		public ForceMode forceMode;

		// Token: 0x04006DE3 RID: 28131
		private Rigidbody rigidbody;

		// Token: 0x04006DE4 RID: 28132
		private GameObject prevGameObject;
	}
}
