using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCircleCollider2D
{
	// Token: 0x02001602 RID: 5634
	[TaskCategory("Basic/CircleCollider2D")]
	[TaskDescription("Stores the radius of the CircleCollider2D. Returns Success.")]
	public class GetRadius : Action
	{
		// Token: 0x0600839C RID: 33692 RVA: 0x002CEC0C File Offset: 0x002CCE0C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.circleCollider2D = defaultGameObject.GetComponent<CircleCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600839D RID: 33693 RVA: 0x0005A9B5 File Offset: 0x00058BB5
		public override TaskStatus OnUpdate()
		{
			if (this.circleCollider2D == null)
			{
				Debug.LogWarning("CircleCollider2D is null");
				return 1;
			}
			this.storeValue.Value = this.circleCollider2D.radius;
			return 2;
		}

		// Token: 0x0600839E RID: 33694 RVA: 0x0005A9E8 File Offset: 0x00058BE8
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400704D RID: 28749
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400704E RID: 28750
		[Tooltip("The radius of the CircleCollider2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400704F RID: 28751
		private CircleCollider2D circleCollider2D;

		// Token: 0x04007050 RID: 28752
		private GameObject prevGameObject;
	}
}
