using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200118B RID: 4491
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Gets the avatar delta position for the last evaluated frame. Returns Success.")]
	public class GetDeltaPosition : Action
	{
		// Token: 0x060076C0 RID: 30400 RVA: 0x002B7410 File Offset: 0x002B5610
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076C1 RID: 30401 RVA: 0x002B7450 File Offset: 0x002B5650
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.storeValue.Value = this.animator.deltaPosition;
			return 2;
		}

		// Token: 0x060076C2 RID: 30402 RVA: 0x002B7483 File Offset: 0x002B5683
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04006243 RID: 25155
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006244 RID: 25156
		[Tooltip("The avatar delta position")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04006245 RID: 25157
		private Animator animator;

		// Token: 0x04006246 RID: 25158
		private GameObject prevGameObject;
	}
}
