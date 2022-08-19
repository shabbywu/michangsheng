using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001087 RID: 4231
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Sets the is kinematic value of the Rigidbody2D. Returns Success.")]
	public class SetIsKinematic : Action
	{
		// Token: 0x06007308 RID: 29448 RVA: 0x002AF14C File Offset: 0x002AD34C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007309 RID: 29449 RVA: 0x002AF18C File Offset: 0x002AD38C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.isKinematic = this.isKinematic.Value;
			return 2;
		}

		// Token: 0x0600730A RID: 29450 RVA: 0x002AF1BF File Offset: 0x002AD3BF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.isKinematic = false;
		}

		// Token: 0x04005EB6 RID: 24246
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EB7 RID: 24247
		[Tooltip("The is kinematic value of the Rigidbody2D")]
		public SharedBool isKinematic;

		// Token: 0x04005EB8 RID: 24248
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005EB9 RID: 24249
		private GameObject prevGameObject;
	}
}
