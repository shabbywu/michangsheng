using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x0200166F RID: 5743
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Rewinds an animation. Rewinds all animations if animationName is blank. Returns Success.")]
	public class Rewind : Action
	{
		// Token: 0x06008558 RID: 34136 RVA: 0x002D0CE4 File Offset: 0x002CEEE4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008559 RID: 34137 RVA: 0x002D0D24 File Offset: 0x002CEF24
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return 1;
			}
			if (string.IsNullOrEmpty(this.animationName.Value))
			{
				this.animation.Rewind();
			}
			else
			{
				this.animation.Rewind(this.animationName.Value);
			}
			return 2;
		}

		// Token: 0x0600855A RID: 34138 RVA: 0x0005C858 File Offset: 0x0005AA58
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
		}

		// Token: 0x0400721B RID: 29211
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400721C RID: 29212
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x0400721D RID: 29213
		private Animation animation;

		// Token: 0x0400721E RID: 29214
		private GameObject prevGameObject;
	}
}
