using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200109D RID: 4253
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Returns Success if the Rigidbody is kinematic, otherwise Failure.")]
	public class IsKinematic : Conditional
	{
		// Token: 0x06007360 RID: 29536 RVA: 0x002AFDE0 File Offset: 0x002ADFE0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007361 RID: 29537 RVA: 0x002AFE20 File Offset: 0x002AE020
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			if (!this.rigidbody.isKinematic)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007362 RID: 29538 RVA: 0x002AFE4C File Offset: 0x002AE04C
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005F16 RID: 24342
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F17 RID: 24343
		private Rigidbody rigidbody;

		// Token: 0x04005F18 RID: 24344
		private GameObject prevGameObject;
	}
}
