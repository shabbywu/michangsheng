using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x02001673 RID: 5747
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Stops an animation. Stops all animations if animationName is blank. Returns Success.")]
	public class Stop : Action
	{
		// Token: 0x06008568 RID: 34152 RVA: 0x002D0E44 File Offset: 0x002CF044
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008569 RID: 34153 RVA: 0x002D0E84 File Offset: 0x002CF084
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return 1;
			}
			if (string.IsNullOrEmpty(this.animationName.Value))
			{
				this.animation.Stop();
			}
			else
			{
				this.animation.Stop(this.animationName.Value);
			}
			return 2;
		}

		// Token: 0x0600856A RID: 34154 RVA: 0x0005C928 File Offset: 0x0005AB28
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName = "";
		}

		// Token: 0x0400722A RID: 29226
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400722B RID: 29227
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x0400722C RID: 29228
		private Animation animation;

		// Token: 0x0400722D RID: 29229
		private GameObject prevGameObject;
	}
}
