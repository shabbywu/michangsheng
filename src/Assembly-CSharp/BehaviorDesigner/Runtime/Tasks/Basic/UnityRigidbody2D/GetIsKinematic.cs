using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200107A RID: 4218
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the is kinematic value of the Rigidbody2D. Returns Success.")]
	public class GetIsKinematic : Action
	{
		// Token: 0x060072D4 RID: 29396 RVA: 0x002AEA5C File Offset: 0x002ACC5C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072D5 RID: 29397 RVA: 0x002AEA9C File Offset: 0x002ACC9C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody2D.isKinematic;
			return 2;
		}

		// Token: 0x060072D6 RID: 29398 RVA: 0x002AEACF File Offset: 0x002ACCCF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x04005E84 RID: 24196
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E85 RID: 24197
		[Tooltip("The is kinematic value of the Rigidbody2D")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04005E86 RID: 24198
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005E87 RID: 24199
		private GameObject prevGameObject;
	}
}
