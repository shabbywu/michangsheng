using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200119E RID: 4510
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the look at position. Returns Success.")]
	public class SetLookAtPosition : Action
	{
		// Token: 0x0600770E RID: 30478 RVA: 0x002B80F4 File Offset: 0x002B62F4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
			this.positionSet = false;
		}

		// Token: 0x0600770F RID: 30479 RVA: 0x002B813B File Offset: 0x002B633B
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			if (!this.positionSet)
			{
				return 3;
			}
			return 2;
		}

		// Token: 0x06007710 RID: 30480 RVA: 0x002B8162 File Offset: 0x002B6362
		public override void OnAnimatorIK()
		{
			if (this.animator == null)
			{
				return;
			}
			this.animator.SetLookAtPosition(this.position.Value);
			this.positionSet = true;
		}

		// Token: 0x06007711 RID: 30481 RVA: 0x002B8190 File Offset: 0x002B6390
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x040062A3 RID: 25251
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062A4 RID: 25252
		[Tooltip("The position to lookAt")]
		public SharedVector3 position;

		// Token: 0x040062A5 RID: 25253
		private Animator animator;

		// Token: 0x040062A6 RID: 25254
		private GameObject prevGameObject;

		// Token: 0x040062A7 RID: 25255
		private bool positionSet;
	}
}
