using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001648 RID: 5704
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stores if root motion is applied. Returns Success.")]
	public class GetApplyRootMotion : Action
	{
		// Token: 0x060084B2 RID: 33970 RVA: 0x002CFE80 File Offset: 0x002CE080
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084B3 RID: 33971 RVA: 0x0005BD77 File Offset: 0x00059F77
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.storeValue.Value = this.animator.applyRootMotion;
			return 2;
		}

		// Token: 0x060084B4 RID: 33972 RVA: 0x0005BDAA File Offset: 0x00059FAA
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x0400715D RID: 29021
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400715E RID: 29022
		[Tooltip("Is root motion applied?")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x0400715F RID: 29023
		private Animator animator;

		// Token: 0x04007160 RID: 29024
		private GameObject prevGameObject;
	}
}
