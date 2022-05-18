using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x0200166A RID: 5738
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Cross fades an animation after previous animations has finished playing. Returns Success.")]
	public class CrossFadeQueued : Action
	{
		// Token: 0x06008544 RID: 34116 RVA: 0x002D0A80 File Offset: 0x002CEC80
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008545 RID: 34117 RVA: 0x002D0AC0 File Offset: 0x002CECC0
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

		// Token: 0x06008546 RID: 34118 RVA: 0x0005C72B File Offset: 0x0005A92B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
			this.fadeLength = 0.3f;
			this.queue = 0;
			this.playMode = 0;
		}

		// Token: 0x04007201 RID: 29185
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007202 RID: 29186
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x04007203 RID: 29187
		[Tooltip("The amount of time it takes to blend")]
		public float fadeLength = 0.3f;

		// Token: 0x04007204 RID: 29188
		[Tooltip("Specifies when the animation should start playing")]
		public QueueMode queue;

		// Token: 0x04007205 RID: 29189
		[Tooltip("The play mode of the animation")]
		public PlayMode playMode;

		// Token: 0x04007206 RID: 29190
		private Animation animation;

		// Token: 0x04007207 RID: 29191
		private GameObject prevGameObject;
	}
}
