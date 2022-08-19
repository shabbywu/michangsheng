using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001093 RID: 4243
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the angular velocity of the Rigidbody. Returns Success.")]
	public class GetAngularVelocity : Action
	{
		// Token: 0x06007338 RID: 29496 RVA: 0x002AF874 File Offset: 0x002ADA74
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007339 RID: 29497 RVA: 0x002AF8B4 File Offset: 0x002ADAB4
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

		// Token: 0x0600733A RID: 29498 RVA: 0x002AF8E7 File Offset: 0x002ADAE7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04005EEE RID: 24302
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EEF RID: 24303
		[Tooltip("The angular velocity of the Rigidbody")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04005EF0 RID: 24304
		private Rigidbody rigidbody;

		// Token: 0x04005EF1 RID: 24305
		private GameObject prevGameObject;
	}
}
