using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001088 RID: 4232
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Sets the mass of the Rigidbody2D. Returns Success.")]
	public class SetMass : Action
	{
		// Token: 0x0600730C RID: 29452 RVA: 0x002AF1D4 File Offset: 0x002AD3D4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600730D RID: 29453 RVA: 0x002AF214 File Offset: 0x002AD414
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.mass = this.mass.Value;
			return 2;
		}

		// Token: 0x0600730E RID: 29454 RVA: 0x002AF247 File Offset: 0x002AD447
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.mass = 0f;
		}

		// Token: 0x04005EBA RID: 24250
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EBB RID: 24251
		[Tooltip("The mass of the Rigidbody2D")]
		public SharedFloat mass;

		// Token: 0x04005EBC RID: 24252
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005EBD RID: 24253
		private GameObject prevGameObject;
	}
}
