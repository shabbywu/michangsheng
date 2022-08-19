using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011AC RID: 4524
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Plays an animation after previous animations has finished playing. Returns Success.")]
	public class PlayQueued : Action
	{
		// Token: 0x06007748 RID: 30536 RVA: 0x002B8A48 File Offset: 0x002B6C48
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007749 RID: 30537 RVA: 0x002B8A88 File Offset: 0x002B6C88
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return 1;
			}
			this.animation.PlayQueued(this.animationName.Value, this.queue, this.playMode);
			return 2;
		}

		// Token: 0x0600774A RID: 30538 RVA: 0x002B8AC8 File Offset: 0x002B6CC8
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
			this.queue = 0;
			this.playMode = 0;
		}

		// Token: 0x040062E6 RID: 25318
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062E7 RID: 25319
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x040062E8 RID: 25320
		[Tooltip("Specifies when the animation should start playing")]
		public QueueMode queue;

		// Token: 0x040062E9 RID: 25321
		[Tooltip("The play mode of the animation")]
		public PlayMode playMode;

		// Token: 0x040062EA RID: 25322
		private Animation animation;

		// Token: 0x040062EB RID: 25323
		private GameObject prevGameObject;
	}
}
