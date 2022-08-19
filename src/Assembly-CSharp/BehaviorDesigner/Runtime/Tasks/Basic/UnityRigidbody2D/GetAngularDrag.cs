using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001076 RID: 4214
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the angular drag of the Rigidbody2D. Returns Success.")]
	public class GetAngularDrag : Action
	{
		// Token: 0x060072C4 RID: 29380 RVA: 0x002AE82C File Offset: 0x002ACA2C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072C5 RID: 29381 RVA: 0x002AE86C File Offset: 0x002ACA6C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody2D.angularDrag;
			return 2;
		}

		// Token: 0x060072C6 RID: 29382 RVA: 0x002AE89F File Offset: 0x002ACA9F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04005E74 RID: 24180
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E75 RID: 24181
		[Tooltip("The angular drag of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04005E76 RID: 24182
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005E77 RID: 24183
		private GameObject prevGameObject;
	}
}
