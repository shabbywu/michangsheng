using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001664 RID: 5732
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the animator in playback mode.")]
	public class StartPlayback : Action
	{
		// Token: 0x0600852C RID: 34092 RVA: 0x002D0900 File Offset: 0x002CEB00
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600852D RID: 34093 RVA: 0x0005C551 File Offset: 0x0005A751
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.animator.StartPlayback();
			return 2;
		}

		// Token: 0x0600852E RID: 34094 RVA: 0x0005C579 File Offset: 0x0005A779
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040071E8 RID: 29160
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040071E9 RID: 29161
		private Animator animator;

		// Token: 0x040071EA RID: 29162
		private GameObject prevGameObject;
	}
}
