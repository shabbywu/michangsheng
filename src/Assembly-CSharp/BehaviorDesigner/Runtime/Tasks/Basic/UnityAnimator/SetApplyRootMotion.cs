using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001199 RID: 4505
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets if root motion is applied. Returns Success.")]
	public class SetApplyRootMotion : Action
	{
		// Token: 0x060076F7 RID: 30455 RVA: 0x002B7CC4 File Offset: 0x002B5EC4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076F8 RID: 30456 RVA: 0x002B7D04 File Offset: 0x002B5F04
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

		// Token: 0x060076F9 RID: 30457 RVA: 0x002B7D37 File Offset: 0x002B5F37
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rootMotion = false;
		}

		// Token: 0x04006285 RID: 25221
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006286 RID: 25222
		[Tooltip("Is root motion applied?")]
		public SharedBool rootMotion;

		// Token: 0x04006287 RID: 25223
		private Animator animator;

		// Token: 0x04006288 RID: 25224
		private GameObject prevGameObject;
	}
}
