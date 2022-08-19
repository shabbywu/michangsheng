using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011B1 RID: 4529
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Stops an animation. Stops all animations if animationName is blank. Returns Success.")]
	public class Stop : Action
	{
		// Token: 0x0600775C RID: 30556 RVA: 0x002B8D24 File Offset: 0x002B6F24
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600775D RID: 30557 RVA: 0x002B8D64 File Offset: 0x002B6F64
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

		// Token: 0x0600775E RID: 30558 RVA: 0x002B8DC1 File Offset: 0x002B6FC1
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName = "";
		}

		// Token: 0x040062FB RID: 25339
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062FC RID: 25340
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x040062FD RID: 25341
		private Animation animation;

		// Token: 0x040062FE RID: 25342
		private GameObject prevGameObject;
	}
}
