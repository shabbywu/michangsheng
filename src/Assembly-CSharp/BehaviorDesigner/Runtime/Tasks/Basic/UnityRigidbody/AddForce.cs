using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x0200108D RID: 4237
	[RequiredComponent(typeof(Rigidbody))]
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Applies a force to the rigidbody. Returns Success.")]
	public class AddForce : Action
	{
		// Token: 0x06007320 RID: 29472 RVA: 0x002AF4CC File Offset: 0x002AD6CC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007321 RID: 29473 RVA: 0x002AF50C File Offset: 0x002AD70C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.AddForce(this.force.Value, this.forceMode);
			return 2;
		}

		// Token: 0x06007322 RID: 29474 RVA: 0x002AF545 File Offset: 0x002AD745
		public override void OnReset()
		{
			this.targetGameObject = null;
			if (this.force != null)
			{
				this.force.Value = Vector3.zero;
			}
			this.forceMode = 0;
		}

		// Token: 0x04005ED0 RID: 24272
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005ED1 RID: 24273
		[Tooltip("The amount of force to apply")]
		public SharedVector3 force;

		// Token: 0x04005ED2 RID: 24274
		[Tooltip("The type of force")]
		public ForceMode forceMode;

		// Token: 0x04005ED3 RID: 24275
		private Rigidbody rigidbody;

		// Token: 0x04005ED4 RID: 24276
		private GameObject prevGameObject;
	}
}
