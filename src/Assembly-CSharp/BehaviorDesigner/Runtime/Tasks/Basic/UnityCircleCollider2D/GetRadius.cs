using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCircleCollider2D
{
	// Token: 0x02001143 RID: 4419
	[TaskCategory("Basic/CircleCollider2D")]
	[TaskDescription("Stores the radius of the CircleCollider2D. Returns Success.")]
	public class GetRadius : Action
	{
		// Token: 0x060075A2 RID: 30114 RVA: 0x002B4C84 File Offset: 0x002B2E84
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.circleCollider2D = defaultGameObject.GetComponent<CircleCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075A3 RID: 30115 RVA: 0x002B4CC4 File Offset: 0x002B2EC4
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

		// Token: 0x060075A4 RID: 30116 RVA: 0x002B4CF7 File Offset: 0x002B2EF7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400612A RID: 24874
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400612B RID: 24875
		[Tooltip("The radius of the CircleCollider2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400612C RID: 24876
		private CircleCollider2D circleCollider2D;

		// Token: 0x0400612D RID: 24877
		private GameObject prevGameObject;
	}
}
