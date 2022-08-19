using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody
{
	// Token: 0x020010A9 RID: 4265
	[TaskCategory("Basic/Rigidbody")]
	[TaskDescription("Sets the position of the Rigidbody. Returns Success.")]
	public class SetPosition : Action
	{
		// Token: 0x06007390 RID: 29584 RVA: 0x002B0434 File Offset: 0x002AE634
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007391 RID: 29585 RVA: 0x002B0474 File Offset: 0x002AE674
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return 1;
			}
			this.rigidbody.position = this.position.Value;
			return 2;
		}

		// Token: 0x06007392 RID: 29586 RVA: 0x002B04A7 File Offset: 0x002AE6A7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04005F44 RID: 24388
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005F45 RID: 24389
		[Tooltip("The position of the Rigidbody")]
		public SharedVector3 position;

		// Token: 0x04005F46 RID: 24390
		private Rigidbody rigidbody;

		// Token: 0x04005F47 RID: 24391
		private GameObject prevGameObject;
	}
}
