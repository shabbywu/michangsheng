using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011AD RID: 4525
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Rewinds an animation. Rewinds all animations if animationName is blank. Returns Success.")]
	public class Rewind : Action
	{
		// Token: 0x0600774C RID: 30540 RVA: 0x002B8AF0 File Offset: 0x002B6CF0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600774D RID: 30541 RVA: 0x002B8B30 File Offset: 0x002B6D30
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

		// Token: 0x0600774E RID: 30542 RVA: 0x002B8B8D File Offset: 0x002B6D8D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
		}

		// Token: 0x040062EC RID: 25324
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062ED RID: 25325
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x040062EE RID: 25326
		private Animation animation;

		// Token: 0x040062EF RID: 25327
		private GameObject prevGameObject;
	}
}
