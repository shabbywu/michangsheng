using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001658 RID: 5720
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets if root motion is applied. Returns Success.")]
	public class SetApplyRootMotion : Action
	{
		// Token: 0x060084F1 RID: 34033 RVA: 0x002D036C File Offset: 0x002CE56C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084F2 RID: 34034 RVA: 0x0005C261 File Offset: 0x0005A461
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.animator.applyRootMotion = this.rootMotion.Value;
			return 2;
		}

		// Token: 0x060084F3 RID: 34035 RVA: 0x0005C294 File Offset: 0x0005A494
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rootMotion = false;
		}

		// Token: 0x040071A8 RID: 29096
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040071A9 RID: 29097
		[Tooltip("Is root motion applied?")]
		public SharedBool rootMotion;

		// Token: 0x040071AA RID: 29098
		private Animator animator;

		// Token: 0x040071AB RID: 29099
		private GameObject prevGameObject;
	}
}
