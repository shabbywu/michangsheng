using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001084 RID: 4228
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Sets the angular velocity of the Rigidbody2D. Returns Success.")]
	public class SetAngularVelocity : Action
	{
		// Token: 0x060072FC RID: 29436 RVA: 0x002AEFA8 File Offset: 0x002AD1A8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072FD RID: 29437 RVA: 0x002AEFE8 File Offset: 0x002AD1E8
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.angularVelocity = this.angularVelocity.Value;
			return 2;
		}

		// Token: 0x060072FE RID: 29438 RVA: 0x002AF01B File Offset: 0x002AD21B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularVelocity = 0f;
		}

		// Token: 0x04005EAA RID: 24234
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EAB RID: 24235
		[Tooltip("The angular velocity of the Rigidbody2D")]
		public SharedFloat angularVelocity;

		// Token: 0x04005EAC RID: 24236
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005EAD RID: 24237
		private GameObject prevGameObject;
	}
}
