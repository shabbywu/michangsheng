using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x0200107D RID: 4221
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Stores the rotation of the Rigidbody2D. Returns Success.")]
	public class GetRotation : Action
	{
		// Token: 0x060072E0 RID: 29408 RVA: 0x002AEBFC File Offset: 0x002ACDFC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072E1 RID: 29409 RVA: 0x002AEC3C File Offset: 0x002ACE3C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody2D.rotation;
			return 2;
		}

		// Token: 0x060072E2 RID: 29410 RVA: 0x002AEC6F File Offset: 0x002ACE6F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04005E90 RID: 24208
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005E91 RID: 24209
		[Tooltip("The rotation of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04005E92 RID: 24210
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005E93 RID: 24211
		private GameObject prevGameObject;
	}
}
