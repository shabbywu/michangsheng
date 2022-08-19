using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityRigidbody2D
{
	// Token: 0x02001083 RID: 4227
	[TaskCategory("Basic/Rigidbody2D")]
	[TaskDescription("Sets the angular drag of the Rigidbody2D. Returns Success.")]
	public class SetAngularDrag : Action
	{
		// Token: 0x060072F8 RID: 29432 RVA: 0x002AEF1C File Offset: 0x002AD11C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060072F9 RID: 29433 RVA: 0x002AEF5C File Offset: 0x002AD15C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return 1;
			}
			this.rigidbody2D.angularDrag = this.angularDrag.Value;
			return 2;
		}

		// Token: 0x060072FA RID: 29434 RVA: 0x002AEF8F File Offset: 0x002AD18F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularDrag = 0f;
		}

		// Token: 0x04005EA6 RID: 24230
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04005EA7 RID: 24231
		[Tooltip("The angular drag of the Rigidbody2D")]
		public SharedFloat angularDrag;

		// Token: 0x04005EA8 RID: 24232
		private Rigidbody2D rigidbody2D;

		// Token: 0x04005EA9 RID: 24233
		private GameObject prevGameObject;
	}
}
