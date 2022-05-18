using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001666 RID: 5734
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Stops the animator playback mode. Returns Success.")]
	public class StopPlayback : Action
	{
		// Token: 0x06008534 RID: 34100 RVA: 0x002D0980 File Offset: 0x002CEB80
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008535 RID: 34101 RVA: 0x0005C5C0 File Offset: 0x0005A7C0
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

		// Token: 0x06008536 RID: 34102 RVA: 0x0005C5E8 File Offset: 0x0005A7E8
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040071EF RID: 29167
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040071F0 RID: 29168
		private Animator animator;

		// Token: 0x040071F1 RID: 29169
		private GameObject prevGameObject;
	}
}
