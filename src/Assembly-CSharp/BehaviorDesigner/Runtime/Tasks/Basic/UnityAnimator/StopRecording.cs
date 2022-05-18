using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001667 RID: 5735
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stops animator record mode. Returns Success.")]
	public class StopRecording : Action
	{
		// Token: 0x06008538 RID: 34104 RVA: 0x002D09C0 File Offset: 0x002CEBC0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008539 RID: 34105 RVA: 0x0005C5F1 File Offset: 0x0005A7F1
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

		// Token: 0x0600853A RID: 34106 RVA: 0x0005C619 File Offset: 0x0005A819
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040071F2 RID: 29170
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040071F3 RID: 29171
		private Animator animator;

		// Token: 0x040071F4 RID: 29172
		private GameObject prevGameObject;
	}
}
