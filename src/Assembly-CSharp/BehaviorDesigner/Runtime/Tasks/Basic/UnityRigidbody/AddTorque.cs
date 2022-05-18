using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200154B RID: 5451
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Applies a torque to the rigidbody. Returns Success.")]
	public class AddTorque : Action
	{
		// Token: 0x0600812A RID: 33066 RVA: 0x002CBEAC File Offset: 0x002CA0AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600812B RID: 33067 RVA: 0x00058213 File Offset: 0x00056413
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.AddTorque(this.torque.Value, this.forceMode);
			return 2;
		}

		// Token: 0x0600812C RID: 33068 RVA: 0x0005824C File Offset: 0x0005644C
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.torque = Vector3.zero;
			this.forceMode = 0;
		}

		// Token: 0x04006DE5 RID: 28133
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DE6 RID: 28134
		[Tooltip("The amount of torque to apply")]
		public SharedVector3 torque;

		// Token: 0x04006DE7 RID: 28135
		[Tooltip("The type of torque")]
		public ForceMode forceMode;

		// Token: 0x04006DE8 RID: 28136
		private Rigidbody rigidbody;

		// Token: 0x04006DE9 RID: 28137
		private GameObject prevGameObject;
	}
}
