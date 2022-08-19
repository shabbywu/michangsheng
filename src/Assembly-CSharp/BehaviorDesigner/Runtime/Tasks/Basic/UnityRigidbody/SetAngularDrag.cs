using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x020010A1 RID: 4257
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the angular drag of the Rigidbody. Returns Success.")]
	public class SetAngularDrag : Action
	{
		// Token: 0x06007370 RID: 29552 RVA: 0x002AFFE8 File Offset: 0x002AE1E8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007371 RID: 29553 RVA: 0x002B0028 File Offset: 0x002AE228
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.angularDrag = this.angularDrag.Value;
			return 2;
		}

		// Token: 0x06007372 RID: 29554 RVA: 0x002B005B File Offset: 0x002AE25B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularDrag = 0f;
		}

		// Token: 0x04005F24 RID: 24356
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F25 RID: 24357
		[Tooltip("The angular drag of the Rigidbody")]
		public SharedFloat angularDrag;

		// Token: 0x04005F26 RID: 24358
		private Rigidbody rigidbody;

		// Token: 0x04005F27 RID: 24359
		private GameObject prevGameObject;
	}
}
