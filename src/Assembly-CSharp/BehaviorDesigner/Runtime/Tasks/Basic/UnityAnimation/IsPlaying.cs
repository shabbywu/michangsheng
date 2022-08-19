using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011AA RID: 4522
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Returns Success if the animation is currently playing.")]
	public class IsPlaying : Conditional
	{
		// Token: 0x06007740 RID: 30528 RVA: 0x002B88C0 File Offset: 0x002B6AC0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007741 RID: 30529 RVA: 0x002B8900 File Offset: 0x002B6B00
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

		// Token: 0x06007742 RID: 30530 RVA: 0x002B8965 File Offset: 0x002B6B65
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
		}

		// Token: 0x040062DD RID: 25309
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062DE RID: 25310
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x040062DF RID: 25311
		private Animation animation;

		// Token: 0x040062E0 RID: 25312
		private GameObject prevGameObject;
	}
}
