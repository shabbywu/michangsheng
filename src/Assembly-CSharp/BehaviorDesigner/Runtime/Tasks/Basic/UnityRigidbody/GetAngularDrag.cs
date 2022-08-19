using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001092 RID: 4242
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the angular drag of the Rigidbody. Returns Success.")]
	public class GetAngularDrag : Action
	{
		// Token: 0x06007334 RID: 29492 RVA: 0x002AF7E8 File Offset: 0x002AD9E8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007335 RID: 29493 RVA: 0x002AF828 File Offset: 0x002ADA28
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.angularDrag;
			return 2;
		}

		// Token: 0x06007336 RID: 29494 RVA: 0x002AF85B File Offset: 0x002ADA5B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04005EEA RID: 24298
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EEB RID: 24299
		[Tooltip("The angular drag of the Rigidbody")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04005EEC RID: 24300
		private Rigidbody rigidbody;

		// Token: 0x04005EED RID: 24301
		private GameObject prevGameObject;
	}
}
