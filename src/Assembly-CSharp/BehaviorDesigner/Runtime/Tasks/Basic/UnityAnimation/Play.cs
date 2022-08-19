using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011AB RID: 4523
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Plays animation without any blending. Returns Success.")]
	public class Play : Action
	{
		// Token: 0x06007744 RID: 30532 RVA: 0x002B8980 File Offset: 0x002B6B80
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007745 RID: 30533 RVA: 0x002B89C0 File Offset: 0x002B6BC0
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return 1;
			}
			if (string.IsNullOrEmpty(this.animationName.Value))
			{
				this.animation.Play();
			}
			else
			{
				this.animation.Play(this.animationName.Value, this.playMode);
			}
			return 2;
		}

		// Token: 0x06007746 RID: 30534 RVA: 0x002B8A25 File Offset: 0x002B6C25
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
			this.playMode = 0;
		}

		// Token: 0x040062E1 RID: 25313
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062E2 RID: 25314
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x040062E3 RID: 25315
		[Tooltip("The play mode of the animation")]
		public PlayMode playMode;

		// Token: 0x040062E4 RID: 25316
		private Animation animation;

		// Token: 0x040062E5 RID: 25317
		private GameObject prevGameObject;
	}
}
