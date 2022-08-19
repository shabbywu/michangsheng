using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x02001098 RID: 4248
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Stores the mass of the Rigidbody. Returns Success.")]
	public class GetMass : Action
	{
		// Token: 0x0600734C RID: 29516 RVA: 0x002AFB28 File Offset: 0x002ADD28
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600734D RID: 29517 RVA: 0x002AFB68 File Offset: 0x002ADD68
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.storeValue.Value = this.rigidbody.mass;
			return 2;
		}

		// Token: 0x0600734E RID: 29518 RVA: 0x002AFB9B File Offset: 0x002ADD9B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04005F02 RID: 24322
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F03 RID: 24323
		[Tooltip("The mass of the Rigidbody")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04005F04 RID: 24324
		private Rigidbody rigidbody;

		// Token: 0x04005F05 RID: 24325
		private GameObject prevGameObject;
	}
}
