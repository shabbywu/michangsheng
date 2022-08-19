using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x020010A8 RID: 4264
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the mass of the Rigidbody. Returns Success.")]
	public class SetMass : Action
	{
		// Token: 0x0600738C RID: 29580 RVA: 0x002B03A8 File Offset: 0x002AE5A8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600738D RID: 29581 RVA: 0x002B03E8 File Offset: 0x002AE5E8
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.mass = this.mass.Value;
			return 2;
		}

		// Token: 0x0600738E RID: 29582 RVA: 0x002B041B File Offset: 0x002AE61B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.mass = 0f;
		}

		// Token: 0x04005F40 RID: 24384
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F41 RID: 24385
		[Tooltip("The mass of the Rigidbody")]
		public SharedFloat mass;

		// Token: 0x04005F42 RID: 24386
		private Rigidbody rigidbody;

		// Token: 0x04005F43 RID: 24387
		private GameObject prevGameObject;
	}
}
