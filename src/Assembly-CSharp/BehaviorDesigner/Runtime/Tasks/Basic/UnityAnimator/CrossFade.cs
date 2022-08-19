using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001188 RID: 4488
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Creates a dynamic transition between the current state and the destination state. Returns Success.")]
	public class CrossFade : Action
	{
		// Token: 0x060076B4 RID: 30388 RVA: 0x002B71F8 File Offset: 0x002B53F8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076B5 RID: 30389 RVA: 0x002B7238 File Offset: 0x002B5438
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.animator.CrossFade(this.stateName.Value, this.transitionDuration.Value, this.layer, this.normalizedTime);
			return 2;
		}

		// Token: 0x060076B6 RID: 30390 RVA: 0x002B728D File Offset: 0x002B548D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.stateName = "";
			this.transitionDuration = 0f;
			this.layer = -1;
			this.normalizedTime = float.NegativeInfinity;
		}

		// Token: 0x04006233 RID: 25139
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006234 RID: 25140
		[Tooltip("The name of the state")]
		public SharedString stateName;

		// Token: 0x04006235 RID: 25141
		[Tooltip("The duration of the transition. Value is in source state normalized time")]
		public SharedFloat transitionDuration;

		// Token: 0x04006236 RID: 25142
		[Tooltip("The layer where the state is")]
		public int layer = -1;

		// Token: 0x04006237 RID: 25143
		[Tooltip("The normalized time at which the state will play")]
		public float normalizedTime = float.NegativeInfinity;

		// Token: 0x04006238 RID: 25144
		private Animator animator;

		// Token: 0x04006239 RID: 25145
		private GameObject prevGameObject;
	}
}
