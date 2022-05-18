using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001661 RID: 5729
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Sets the look at weight. Returns success immediately after.")]
	public class SetLookAtWeight : Action
	{
		// Token: 0x0600851F RID: 34079 RVA: 0x002D0794 File Offset: 0x002CE994
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

		// Token: 0x06008520 RID: 34080 RVA: 0x0005C474 File Offset: 0x0005A674
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

		// Token: 0x06008521 RID: 34081 RVA: 0x002D07DC File Offset: 0x002CE9DC
		public override void OnAnimatorIK()
		{
			if (this.animator == null)
			{
				return;
			}
			this.animator.SetLookAtWeight(this.weight.Value, this.bodyWeight, this.headWeight, this.eyesWeight, this.clampWeight);
			this.weightSet = true;
		}

		// Token: 0x06008522 RID: 34082 RVA: 0x002D0830 File Offset: 0x002CEA30
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.weight = 0f;
			this.bodyWeight = 0f;
			this.headWeight = 1f;
			this.eyesWeight = 0f;
			this.clampWeight = 0.5f;
		}

		// Token: 0x040071D7 RID: 29143
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040071D8 RID: 29144
		[Tooltip("(0-1) the global weight of the LookAt, multiplier for other parameters.")]
		public SharedFloat weight;

		// Token: 0x040071D9 RID: 29145
		[Tooltip("(0-1) determines how much the body is involved in the LookAt.")]
		public float bodyWeight;

		// Token: 0x040071DA RID: 29146
		[Tooltip("(0-1) determines how much the head is involved in the LookAt.")]
		public float headWeight = 1f;

		// Token: 0x040071DB RID: 29147
		[Tooltip("(0-1) determines how much the eyes are involved in the LookAt.")]
		public float eyesWeight;

		// Token: 0x040071DC RID: 29148
		[Tooltip("(0-1) 0.0 means the character is completely unrestrained in motion, 1.0 means he's completely clamped (look at becomes impossible), and 0.5 means he'll be able to move on half of the possible range (180 degrees).")]
		public float clampWeight = 0.5f;

		// Token: 0x040071DD RID: 29149
		private Animator animator;

		// Token: 0x040071DE RID: 29150
		private GameObject prevGameObject;

		// Token: 0x040071DF RID: 29151
		private bool weightSet;
	}
}
