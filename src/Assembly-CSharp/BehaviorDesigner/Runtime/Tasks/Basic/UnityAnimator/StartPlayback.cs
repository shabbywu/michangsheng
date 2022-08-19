using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x020011A2 RID: 4514
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the animator in playback mode.")]
	public class StartPlayback : Action
	{
		// Token: 0x06007720 RID: 30496 RVA: 0x002B83F8 File Offset: 0x002B65F8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007721 RID: 30497 RVA: 0x002B8438 File Offset: 0x002B6638
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

		// Token: 0x06007722 RID: 30498 RVA: 0x002B8460 File Offset: 0x002B6660
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040062B9 RID: 25273
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062BA RID: 25274
		private Animator animator;

		// Token: 0x040062BB RID: 25275
		private GameObject prevGameObject;
	}
}
