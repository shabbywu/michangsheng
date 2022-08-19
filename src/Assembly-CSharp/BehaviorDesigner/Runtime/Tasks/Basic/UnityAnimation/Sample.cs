using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimation
{
	// Token: 0x020011AE RID: 4526
	[TaskCategory("Basic/Animation")]
	[TaskDescription("Samples animations at the current state. Returns Success.")]
	public class Sample : Action
	{
		// Token: 0x06007750 RID: 30544 RVA: 0x002B8BA8 File Offset: 0x002B6DA8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06007751 RID: 30545 RVA: 0x002B8BE8 File Offset: 0x002B6DE8
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

		// Token: 0x06007752 RID: 30546 RVA: 0x002B8C10 File Offset: 0x002B6E10
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040062F0 RID: 25328
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062F1 RID: 25329
		private Animation animation;

		// Token: 0x040062F2 RID: 25330
		private GameObject prevGameObject;
	}
}
