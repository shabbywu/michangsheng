using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x020011A4 RID: 4516
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stops the animator playback mode. Returns Success.")]
	public class StopPlayback : Action
	{
		// Token: 0x06007728 RID: 30504 RVA: 0x002B84EC File Offset: 0x002B66EC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007729 RID: 30505 RVA: 0x002B852C File Offset: 0x002B672C
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.animator.StopPlayback();
			return 2;
		}

		// Token: 0x0600772A RID: 30506 RVA: 0x002B8554 File Offset: 0x002B6754
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040062C0 RID: 25280
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062C1 RID: 25281
		private Animator animator;

		// Token: 0x040062C2 RID: 25282
		private GameObject prevGameObject;
	}
}
