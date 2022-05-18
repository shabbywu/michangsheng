using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x0200166E RID: 5742
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Plays an animation after previous animations has finished playing. Returns Success.")]
	public class PlayQueued : Action
	{
		// Token: 0x06008554 RID: 34132 RVA: 0x002D0CA4 File Offset: 0x002CEEA4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008555 RID: 34133 RVA: 0x0005C7F1 File Offset: 0x0005A9F1
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

		// Token: 0x06008556 RID: 34134 RVA: 0x0005C831 File Offset: 0x0005AA31
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
			this.queue = 0;
			this.playMode = 0;
		}

		// Token: 0x04007215 RID: 29205
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007216 RID: 29206
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x04007217 RID: 29207
		[Tooltip("Specifies when the animation should start playing")]
		public QueueMode queue;

		// Token: 0x04007218 RID: 29208
		[Tooltip("The play mode of the animation")]
		public PlayMode playMode;

		// Token: 0x04007219 RID: 29209
		private Animation animation;

		// Token: 0x0400721A RID: 29210
		private GameObject prevGameObject;
	}
}
