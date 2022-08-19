using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x020011A5 RID: 4517
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stops animator record mode. Returns Success.")]
	public class StopRecording : Action
	{
		// Token: 0x0600772C RID: 30508 RVA: 0x002B8560 File Offset: 0x002B6760
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600772D RID: 30509 RVA: 0x002B85A0 File Offset: 0x002B67A0
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.animator.StopRecording();
			return 2;
		}

		// Token: 0x0600772E RID: 30510 RVA: 0x002B85C8 File Offset: 0x002B67C8
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040062C3 RID: 25283
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062C4 RID: 25284
		private Animator animator;

		// Token: 0x040062C5 RID: 25285
		private GameObject prevGameObject;
	}
}
