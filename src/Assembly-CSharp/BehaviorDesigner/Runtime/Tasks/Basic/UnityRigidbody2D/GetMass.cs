using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200107B RID: 4219
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the mass of the Rigidbody2D. Returns Success.")]
	public class GetMass : Action
	{
		// Token: 0x060072D8 RID: 29400 RVA: 0x002AEAE4 File Offset: 0x002ACCE4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072D9 RID: 29401 RVA: 0x002AEB24 File Offset: 0x002ACD24
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody2D.mass;
			return 2;
		}

		// Token: 0x060072DA RID: 29402 RVA: 0x002AEB57 File Offset: 0x002ACD57
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04005E88 RID: 24200
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E89 RID: 24201
		[Tooltip("The mass of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04005E8A RID: 24202
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005E8B RID: 24203
		private GameObject prevGameObject;
	}
}
