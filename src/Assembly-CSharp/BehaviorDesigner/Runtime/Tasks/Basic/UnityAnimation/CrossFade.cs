using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x02001669 RID: 5737
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Fades the animation over a period of time and fades other animations out. Returns Success.")]
	public class CrossFade : Action
	{
		// Token: 0x06008540 RID: 34112 RVA: 0x002D0A40 File Offset: 0x002CEC40
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008541 RID: 34113 RVA: 0x0005C6AE File Offset: 0x0005A8AE
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return 1;
			}
			this.animation.CrossFade(this.animationName.Value, this.fadeLength, this.playMode);
			return 2;
		}

		// Token: 0x06008542 RID: 34114 RVA: 0x0005C6ED File Offset: 0x0005A8ED
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
			this.fadeLength = 0.3f;
			this.playMode = 0;
		}

		// Token: 0x040071FB RID: 29179
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040071FC RID: 29180
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x040071FD RID: 29181
		[Tooltip("The amount of time it takes to blend")]
		public float fadeLength = 0.3f;

		// Token: 0x040071FE RID: 29182
		[Tooltip("The play mode of the animation")]
		public PlayMode playMode;

		// Token: 0x040071FF RID: 29183
		private Animation animation;

		// Token: 0x04007200 RID: 29184
		private GameObject prevGameObject;
	}
}
