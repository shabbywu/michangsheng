using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001189 RID: 4489
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stores if root motion is applied. Returns Success.")]
	public class GetApplyRootMotion : Action
	{
		// Token: 0x060076B8 RID: 30392 RVA: 0x002B72E4 File Offset: 0x002B54E4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076B9 RID: 30393 RVA: 0x002B7324 File Offset: 0x002B5524
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

		// Token: 0x060076BA RID: 30394 RVA: 0x002B7357 File Offset: 0x002B5557
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x0400623A RID: 25146
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400623B RID: 25147
		[Tooltip("Is root motion applied?")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x0400623C RID: 25148
		private Animator animator;

		// Token: 0x0400623D RID: 25149
		private GameObject prevGameObject;
	}
}
