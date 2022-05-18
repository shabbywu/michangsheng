using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001662 RID: 5730
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the playback speed of the Animator. 1 is normal playback speed. Returns Success.")]
	public class SetSpeed : Action
	{
		// Token: 0x06008524 RID: 34084 RVA: 0x002D0880 File Offset: 0x002CEA80
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06008525 RID: 34085 RVA: 0x0005C4B9 File Offset: 0x0005A6B9
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.animator.speed = this.speed.Value;
			return 2;
		}

		// Token: 0x06008526 RID: 34086 RVA: 0x0005C4EC File Offset: 0x0005A6EC
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.speed = 0f;
		}

		// Token: 0x040071E0 RID: 29152
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040071E1 RID: 29153
		[Tooltip("The playback speed of the Animator")]
		public SharedFloat speed;

		// Token: 0x040071E2 RID: 29154
		private Animator animator;

		// Token: 0x040071E3 RID: 29155
		private GameObject prevGameObject;
	}
}
