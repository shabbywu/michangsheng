using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityCircleCollider2D
{
	// Token: 0x02001603 RID: 5635
	[TaskCategory("Basic/CircleCollider2D")]
	[TaskDescription("Sets the radius of the CircleCollider2D. Returns Success.")]
	public class SetRadius : Action
	{
		// Token: 0x060083A0 RID: 33696 RVA: 0x002CEC4C File Offset: 0x002CCE4C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.circleCollider2D = defaultGameObject.GetComponent<CircleCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060083A1 RID: 33697 RVA: 0x0005AA01 File Offset: 0x00058C01
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

		// Token: 0x060083A2 RID: 33698 RVA: 0x0005AA34 File Offset: 0x00058C34
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.radius = 0f;
		}

		// Token: 0x04007051 RID: 28753
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007052 RID: 28754
		[Tooltip("The radius of the CircleCollider2D")]
		public SharedFloat radius;

		// Token: 0x04007053 RID: 28755
		private CircleCollider2D circleCollider2D;

		// Token: 0x04007054 RID: 28756
		private GameObject prevGameObject;
	}
}
