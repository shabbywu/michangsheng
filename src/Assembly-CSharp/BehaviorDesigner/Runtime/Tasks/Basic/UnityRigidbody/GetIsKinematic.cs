using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001097 RID: 4247
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the is kinematic value of the Rigidbody. Returns Success.")]
	public class GetIsKinematic : Action
	{
		// Token: 0x06007348 RID: 29512 RVA: 0x002AFAA0 File Offset: 0x002ADCA0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007349 RID: 29513 RVA: 0x002AFAE0 File Offset: 0x002ADCE0
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.isKinematic;
			return 2;
		}

		// Token: 0x0600734A RID: 29514 RVA: 0x002AFB13 File Offset: 0x002ADD13
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x04005EFE RID: 24318
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EFF RID: 24319
		[Tooltip("The is kinematic value of the Rigidbody")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04005F00 RID: 24320
		private Rigidbody rigidbody;

		// Token: 0x04005F01 RID: 24321
		private GameObject prevGameObject;
	}
}
