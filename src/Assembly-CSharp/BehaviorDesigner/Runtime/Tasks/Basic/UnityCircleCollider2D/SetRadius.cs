using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCircleCollider2D
{
	// Token: 0x02001144 RID: 4420
	[TaskCategory("Basic/CircleCollider2D")]
	[TaskDescription("Sets the radius of the CircleCollider2D. Returns Success.")]
	public class SetRadius : Action
	{
		// Token: 0x060075A6 RID: 30118 RVA: 0x002B4D10 File Offset: 0x002B2F10
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.circleCollider2D = defaultGameObject.GetComponent<CircleCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060075A7 RID: 30119 RVA: 0x002B4D50 File Offset: 0x002B2F50
		public override TaskStatus OnUpdate()
		{
			if (this.circleCollider2D == null)
			{
				Debug.LogWarning("CircleCollider2D is null");
				return 1;
			}
			this.circleCollider2D.radius = this.radius.Value;
			return 2;
		}

		// Token: 0x060075A8 RID: 30120 RVA: 0x002B4D83 File Offset: 0x002B2F83
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.radius = 0f;
		}

		// Token: 0x0400612E RID: 24878
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400612F RID: 24879
		[Tooltip("The radius of the CircleCollider2D")]
		public SharedFloat radius;

		// Token: 0x04006130 RID: 24880
		private CircleCollider2D circleCollider2D;

		// Token: 0x04006131 RID: 24881
		private GameObject prevGameObject;
	}
}
