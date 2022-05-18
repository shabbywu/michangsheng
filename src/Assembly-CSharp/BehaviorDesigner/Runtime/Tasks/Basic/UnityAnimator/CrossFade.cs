using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001647 RID: 5703
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Creates a dynamic transition between the current state and the destination state. Returns Success.")]
	public class CrossFade : Action
	{
		// Token: 0x060084AE RID: 33966 RVA: 0x002CFDE8 File Offset: 0x002CDFE8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084AF RID: 33967 RVA: 0x002CFE28 File Offset: 0x002CE028
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

		// Token: 0x060084B0 RID: 33968 RVA: 0x0005BD22 File Offset: 0x00059F22
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.stateName = "";
			this.transitionDuration = 0f;
			this.layer = -1;
			this.normalizedTime = float.NegativeInfinity;
		}

		// Token: 0x04007156 RID: 29014
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007157 RID: 29015
		[Tooltip("The name of the state")]
		public SharedString stateName;

		// Token: 0x04007158 RID: 29016
		[Tooltip("The duration of the transition. Value is in source state normalized time")]
		public SharedFloat transitionDuration;

		// Token: 0x04007159 RID: 29017
		[Tooltip("The layer where the state is")]
		public int layer = -1;

		// Token: 0x0400715A RID: 29018
		[Tooltip("The normalized time at which the state will play")]
		public float normalizedTime = float.NegativeInfinity;

		// Token: 0x0400715B RID: 29019
		private Animator animator;

		// Token: 0x0400715C RID: 29020
		private GameObject prevGameObject;
	}
}
