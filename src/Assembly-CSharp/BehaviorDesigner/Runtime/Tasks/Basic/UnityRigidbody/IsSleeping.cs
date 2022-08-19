using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200109E RID: 4254
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Returns Success if the Rigidbody is sleeping, otherwise Failure.")]
	public class IsSleeping : Conditional
	{
		// Token: 0x06007364 RID: 29540 RVA: 0x002AFE58 File Offset: 0x002AE058
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007365 RID: 29541 RVA: 0x002AFE98 File Offset: 0x002AE098
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			if (!this.rigidbody.IsSleeping())
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06007366 RID: 29542 RVA: 0x002AFEC4 File Offset: 0x002AE0C4
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04005F19 RID: 24345
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F1A RID: 24346
		private Rigidbody rigidbody;

		// Token: 0x04005F1B RID: 24347
		private GameObject prevGameObject;
	}
}
