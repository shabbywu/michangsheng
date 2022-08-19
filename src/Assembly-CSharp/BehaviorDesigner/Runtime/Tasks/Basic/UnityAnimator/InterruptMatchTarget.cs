using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001193 RID: 4499
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Interrupts the automatic target matching. Returns Success.")]
	public class InterruptMatchTarget : Action
	{
		// Token: 0x060076DF RID: 30431 RVA: 0x002B7870 File Offset: 0x002B5A70
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076E0 RID: 30432 RVA: 0x002B78B0 File Offset: 0x002B5AB0
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.animator.InterruptMatchTarget(this.completeMatch);
			return 2;
		}

		// Token: 0x060076E1 RID: 30433 RVA: 0x002B78DE File Offset: 0x002B5ADE
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.completeMatch = true;
		}

		// Token: 0x04006264 RID: 25188
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006265 RID: 25189
		[Tooltip("CompleteMatch will make the gameobject match the target completely at the next frame")]
		public bool completeMatch = true;

		// Token: 0x04006266 RID: 25190
		private Animator animator;

		// Token: 0x04006267 RID: 25191
		private GameObject prevGameObject;
	}
}
