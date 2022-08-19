using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x0200119F RID: 4511
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the look at weight. Returns success immediately after.")]
	public class SetLookAtWeight : Action
	{
		// Token: 0x06007713 RID: 30483 RVA: 0x002B81AC File Offset: 0x002B63AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
			this.weightSet = false;
		}

		// Token: 0x06007714 RID: 30484 RVA: 0x002B81F3 File Offset: 0x002B63F3
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			if (!this.weightSet)
			{
				return 3;
			}
			return 2;
		}

		// Token: 0x06007715 RID: 30485 RVA: 0x002B821C File Offset: 0x002B641C
		public override void OnAnimatorIK()
		{
			if (this.animator == null)
			{
				return;
			}
			this.animator.SetLookAtWeight(this.weight.Value, this.bodyWeight, this.headWeight, this.eyesWeight, this.clampWeight);
			this.weightSet = true;
		}

		// Token: 0x06007716 RID: 30486 RVA: 0x002B8270 File Offset: 0x002B6470
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.weight = 0f;
			this.bodyWeight = 0f;
			this.headWeight = 1f;
			this.eyesWeight = 0f;
			this.clampWeight = 0.5f;
		}

		// Token: 0x040062A8 RID: 25256
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040062A9 RID: 25257
		[Tooltip("(0-1) the global weight of the LookAt, multiplier for other parameters.")]
		public SharedFloat weight;

		// Token: 0x040062AA RID: 25258
		[Tooltip("(0-1) determines how much the body is involved in the LookAt.")]
		public float bodyWeight;

		// Token: 0x040062AB RID: 25259
		[Tooltip("(0-1) determines how much the head is involved in the LookAt.")]
		public float headWeight = 1f;

		// Token: 0x040062AC RID: 25260
		[Tooltip("(0-1) determines how much the eyes are involved in the LookAt.")]
		public float eyesWeight;

		// Token: 0x040062AD RID: 25261
		[Tooltip("(0-1) 0.0 means the character is completely unrestrained in motion, 1.0 means he's completely clamped (look at becomes impossible), and 0.5 means he'll be able to move on half of the possible range (180 degrees).")]
		public float clampWeight = 0.5f;

		// Token: 0x040062AE RID: 25262
		private Animator animator;

		// Token: 0x040062AF RID: 25263
		private GameObject prevGameObject;

		// Token: 0x040062B0 RID: 25264
		private bool weightSet;
	}
}
