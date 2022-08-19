using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001194 RID: 4500
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Returns success if the specified AnimatorController layer in a transition.")]
	public class IsInTransition : Conditional
	{
		// Token: 0x060076E3 RID: 30435 RVA: 0x002B7900 File Offset: 0x002B5B00
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076E4 RID: 30436 RVA: 0x002B7940 File Offset: 0x002B5B40
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

		// Token: 0x060076E5 RID: 30437 RVA: 0x002B7977 File Offset: 0x002B5B77
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
		}

		// Token: 0x04006268 RID: 25192
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006269 RID: 25193
		[Tooltip("The layer's index")]
		public SharedInt index;

		// Token: 0x0400626A RID: 25194
		private Animator animator;

		// Token: 0x0400626B RID: 25195
		private GameObject prevGameObject;
	}
}
