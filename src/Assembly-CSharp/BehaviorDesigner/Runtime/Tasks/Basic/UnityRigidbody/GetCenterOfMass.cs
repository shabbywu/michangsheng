using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200154E RID: 5454
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the center of mass of the Rigidbody. Returns Success.")]
	public class GetCenterOfMass : Action
	{
		// Token: 0x06008136 RID: 33078 RVA: 0x002CBF6C File Offset: 0x002CA16C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008137 RID: 33079 RVA: 0x00058304 File Offset: 0x00056504
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.centerOfMass;
			return 2;
		}

		// Token: 0x06008138 RID: 33080 RVA: 0x00058337 File Offset: 0x00056537
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006DF2 RID: 28146
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006DF3 RID: 28147
		[Tooltip("The center of mass of the Rigidbody")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006DF4 RID: 28148
		private Rigidbody rigidbody;

		// Token: 0x04006DF5 RID: 28149
		private GameObject prevGameObject;
	}
}
