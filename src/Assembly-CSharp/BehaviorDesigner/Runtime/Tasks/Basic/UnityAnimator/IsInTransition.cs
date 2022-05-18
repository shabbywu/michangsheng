using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001653 RID: 5715
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Returns success if the specified AnimatorController layer in a transition.")]
	public class IsInTransition : Conditional
	{
		// Token: 0x060084DD RID: 34013 RVA: 0x002D0100 File Offset: 0x002CE300
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084DE RID: 34014 RVA: 0x0005C109 File Offset: 0x0005A309
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			if (!this.animator.IsInTransition(this.index.Value))
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060084DF RID: 34015 RVA: 0x0005C140 File Offset: 0x0005A340
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
		}

		// Token: 0x0400718B RID: 29067
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400718C RID: 29068
		[Tooltip("The layer's index")]
		public SharedInt index;

		// Token: 0x0400718D RID: 29069
		private Animator animator;

		// Token: 0x0400718E RID: 29070
		private GameObject prevGameObject;
	}
}
