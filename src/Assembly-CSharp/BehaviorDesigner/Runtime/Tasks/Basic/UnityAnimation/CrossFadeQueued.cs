using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011A8 RID: 4520
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Cross fades an animation after previous animations has finished playing. Returns Success.")]
	public class CrossFadeQueued : Action
	{
		// Token: 0x06007738 RID: 30520 RVA: 0x002B8760 File Offset: 0x002B6960
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007739 RID: 30521 RVA: 0x002B87A0 File Offset: 0x002B69A0
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return 1;
			}
			this.animation.CrossFadeQueued(this.animationName.Value, this.fadeLength, this.queue, this.playMode);
			return 2;
		}

		// Token: 0x0600773A RID: 30522 RVA: 0x002B87F1 File Offset: 0x002B69F1
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
			this.fadeLength = 0.3f;
			this.queue = 0;
			this.playMode = 0;
		}

		// Token: 0x040062D2 RID: 25298
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062D3 RID: 25299
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x040062D4 RID: 25300
		[Tooltip("The amount of time it takes to blend")]
		public float fadeLength = 0.3f;

		// Token: 0x040062D5 RID: 25301
		[Tooltip("Specifies when the animation should start playing")]
		public QueueMode queue;

		// Token: 0x040062D6 RID: 25302
		[Tooltip("The play mode of the animation")]
		public PlayMode playMode;

		// Token: 0x040062D7 RID: 25303
		private Animation animation;

		// Token: 0x040062D8 RID: 25304
		private GameObject prevGameObject;
	}
}
