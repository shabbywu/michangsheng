using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200109B RID: 4251
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the use gravity value of the Rigidbody. Returns Success.")]
	public class GetUseGravity : Action
	{
		// Token: 0x06007358 RID: 29528 RVA: 0x002AFCCC File Offset: 0x002ADECC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007359 RID: 29529 RVA: 0x002AFD0C File Offset: 0x002ADF0C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.useGravity;
			return 2;
		}

		// Token: 0x0600735A RID: 29530 RVA: 0x002AFD3F File Offset: 0x002ADF3F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x04005F0E RID: 24334
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F0F RID: 24335
		[Tooltip("The use gravity value of the Rigidbody")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04005F10 RID: 24336
		private Rigidbody rigidbody;

		// Token: 0x04005F11 RID: 24337
		private GameObject prevGameObject;
	}
}
