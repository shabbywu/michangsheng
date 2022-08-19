using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200118E RID: 4494
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stores the current gravity weight based on current animations that are played. Returns Success.")]
	public class GetGravityWeight : Action
	{
		// Token: 0x060076CC RID: 30412 RVA: 0x002B75D4 File Offset: 0x002B57D4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076CD RID: 30413 RVA: 0x002B7614 File Offset: 0x002B5814
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.storeValue.Value = this.animator.gravityWeight;
			return 2;
		}

		// Token: 0x060076CE RID: 30414 RVA: 0x002B7647 File Offset: 0x002B5847
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04006250 RID: 25168
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006251 RID: 25169
		[Tooltip("The value of the gravity weight")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04006252 RID: 25170
		private Animator animator;

		// Token: 0x04006253 RID: 25171
		private GameObject prevGameObject;
	}
}
