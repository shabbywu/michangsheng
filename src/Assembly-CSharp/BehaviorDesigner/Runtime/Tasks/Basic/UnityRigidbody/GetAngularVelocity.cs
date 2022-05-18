using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200154D RID: 5453
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the angular velocity of the Rigidbody. Returns Success.")]
	public class GetAngularVelocity : Action
	{
		// Token: 0x06008132 RID: 33074 RVA: 0x002CBF2C File Offset: 0x002CA12C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008133 RID: 33075 RVA: 0x000582B8 File Offset: 0x000564B8
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.angularVelocity;
			return 2;
		}

		// Token: 0x06008134 RID: 33076 RVA: 0x000582EB File Offset: 0x000564EB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006DEE RID: 28142
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DEF RID: 28143
		[Tooltip("The angular velocity of the Rigidbody")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006DF0 RID: 28144
		private Rigidbody rigidbody;

		// Token: 0x04006DF1 RID: 28145
		private GameObject prevGameObject;
	}
}
