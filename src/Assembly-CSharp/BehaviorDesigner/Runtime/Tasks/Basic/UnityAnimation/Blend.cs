using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x02001668 RID: 5736
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Blends the animation. Returns Success.")]
	public class Blend : Action
	{
		// Token: 0x0600853C RID: 34108 RVA: 0x002D0A00 File Offset: 0x002CEC00
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600853D RID: 34109 RVA: 0x0005C622 File Offset: 0x0005A822
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return 1;
			}
			this.animation.Blend(this.animationName.Value, this.targetWeight, this.fadeLength);
			return 2;
		}

		// Token: 0x0600853E RID: 34110 RVA: 0x0005C661 File Offset: 0x0005A861
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName = "";
			this.targetWeight = 1f;
			this.fadeLength = 0.3f;
		}

		// Token: 0x040071F5 RID: 29173
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040071F6 RID: 29174
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x040071F7 RID: 29175
		[Tooltip("The weight the animation should blend to")]
		public float targetWeight = 1f;

		// Token: 0x040071F8 RID: 29176
		[Tooltip("The amount of time it takes to blend")]
		public float fadeLength = 0.3f;

		// Token: 0x040071F9 RID: 29177
		private Animation animation;

		// Token: 0x040071FA RID: 29178
		private GameObject prevGameObject;
	}
}
