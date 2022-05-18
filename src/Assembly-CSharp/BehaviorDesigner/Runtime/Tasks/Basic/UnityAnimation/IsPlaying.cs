using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x0200166C RID: 5740
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Returns Success if the animation is currently playing.")]
	public class IsPlaying : Conditional
	{
		// Token: 0x0600854C RID: 34124 RVA: 0x002D0B54 File Offset: 0x002CED54
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600854D RID: 34125 RVA: 0x002D0B94 File Offset: 0x002CED94
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return 1;
			}
			if (string.IsNullOrEmpty(this.animationName.Value))
			{
				if (!this.animation.isPlaying)
				{
					return 1;
				}
				return 2;
			}
			else
			{
				if (!this.animation.IsPlaying(this.animationName.Value))
				{
					return 1;
				}
				return 2;
			}
		}

		// Token: 0x0600854E RID: 34126 RVA: 0x0005C7B8 File Offset: 0x0005A9B8
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
		}

		// Token: 0x0400720C RID: 29196
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400720D RID: 29197
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x0400720E RID: 29198
		private Animation animation;

		// Token: 0x0400720F RID: 29199
		private GameObject prevGameObject;
	}
}
