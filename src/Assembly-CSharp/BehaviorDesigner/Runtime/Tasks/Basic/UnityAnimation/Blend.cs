using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011A6 RID: 4518
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Blends the animation. Returns Success.")]
	public class Blend : Action
	{
		// Token: 0x06007730 RID: 30512 RVA: 0x002B85D4 File Offset: 0x002B67D4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007731 RID: 30513 RVA: 0x002B8614 File Offset: 0x002B6814
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

		// Token: 0x06007732 RID: 30514 RVA: 0x002B8653 File Offset: 0x002B6853
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName = "";
			this.targetWeight = 1f;
			this.fadeLength = 0.3f;
		}

		// Token: 0x040062C6 RID: 25286
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062C7 RID: 25287
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x040062C8 RID: 25288
		[Tooltip("The weight the animation should blend to")]
		public float targetWeight = 1f;

		// Token: 0x040062C9 RID: 25289
		[Tooltip("The amount of time it takes to blend")]
		public float fadeLength = 0.3f;

		// Token: 0x040062CA RID: 25290
		private Animation animation;

		// Token: 0x040062CB RID: 25291
		private GameObject prevGameObject;
	}
}
