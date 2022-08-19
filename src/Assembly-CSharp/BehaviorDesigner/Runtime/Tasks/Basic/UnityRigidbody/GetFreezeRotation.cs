using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001096 RID: 4246
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the freeze rotation value of the Rigidbody. Returns Success.")]
	public class GetFreezeRotation : Action
	{
		// Token: 0x06007344 RID: 29508 RVA: 0x002AFA18 File Offset: 0x002ADC18
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007345 RID: 29509 RVA: 0x002AFA58 File Offset: 0x002ADC58
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.freezeRotation;
			return 2;
		}

		// Token: 0x06007346 RID: 29510 RVA: 0x002AFA8B File Offset: 0x002ADC8B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x04005EFA RID: 24314
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EFB RID: 24315
		[Tooltip("The freeze rotation value of the Rigidbody")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04005EFC RID: 24316
		private Rigidbody rigidbody;

		// Token: 0x04005EFD RID: 24317
		private GameObject prevGameObject;
	}
}
