using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001198 RID: 4504
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Plays an animator state. Returns Success.")]
	public class Play : Action
	{
		// Token: 0x060076F3 RID: 30451 RVA: 0x002B7C00 File Offset: 0x002B5E00
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076F4 RID: 30452 RVA: 0x002B7C40 File Offset: 0x002B5E40
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.animator.Play(this.stateName.Value, this.layer, this.normalizedTime);
			return 2;
		}

		// Token: 0x060076F5 RID: 30453 RVA: 0x002B7C7F File Offset: 0x002B5E7F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.stateName = "";
			this.layer = -1;
			this.normalizedTime = float.NegativeInfinity;
		}

		// Token: 0x0400627F RID: 25215
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006280 RID: 25216
		[Tooltip("The name of the state")]
		public SharedString stateName;

		// Token: 0x04006281 RID: 25217
		[Tooltip("The layer where the state is")]
		public int layer = -1;

		// Token: 0x04006282 RID: 25218
		[Tooltip("The normalized time at which the state will play")]
		public float normalizedTime = float.NegativeInfinity;

		// Token: 0x04006283 RID: 25219
		private Animator animator;

		// Token: 0x04006284 RID: 25220
		private GameObject prevGameObject;
	}
}
