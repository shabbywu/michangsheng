using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x02001670 RID: 5744
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Samples animations at the current state. Returns Success.")]
	public class Sample : Action
	{
		// Token: 0x0600855C RID: 34140 RVA: 0x002D0D84 File Offset: 0x002CEF84
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600855D RID: 34141 RVA: 0x0005C871 File Offset: 0x0005AA71
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return 1;
			}
			this.animation.Sample();
			return 2;
		}

		// Token: 0x0600855E RID: 34142 RVA: 0x0005C899 File Offset: 0x0005AA99
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400721F RID: 29215
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007220 RID: 29216
		private Animation animation;

		// Token: 0x04007221 RID: 29217
		private GameObject prevGameObject;
	}
}
