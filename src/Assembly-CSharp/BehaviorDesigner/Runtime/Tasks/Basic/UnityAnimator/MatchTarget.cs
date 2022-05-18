using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001656 RID: 5718
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Automatically adjust the gameobject position and rotation so that the AvatarTarget reaches the matchPosition when the current state is at the specified progress. Returns Success.")]
	public class MatchTarget : Action
	{
		// Token: 0x060084E9 RID: 34025 RVA: 0x002D0218 File Offset: 0x002CE418
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060084EA RID: 34026 RVA: 0x002D0258 File Offset: 0x002CE458
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return 1;
			}
			this.animator.MatchTarget(this.matchPosition.Value, this.matchRotation.Value, this.targetBodyPart, new MatchTargetWeightMask(this.weightMaskPosition, this.weightMaskRotation), this.startNormalizedTime, this.targetNormalizedTime);
			return 2;
		}

		// Token: 0x060084EB RID: 34027 RVA: 0x002D02C4 File Offset: 0x002CE4C4
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.matchPosition = Vector3.zero;
			this.matchRotation = Quaternion.identity;
			this.targetBodyPart = 0;
			this.weightMaskPosition = Vector3.zero;
			this.weightMaskRotation = 0f;
			this.startNormalizedTime = 0f;
			this.targetNormalizedTime = 1f;
		}

		// Token: 0x04007198 RID: 29080
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04007199 RID: 29081
		[Tooltip("The position we want the body part to reach")]
		public SharedVector3 matchPosition;

		// Token: 0x0400719A RID: 29082
		[Tooltip("The rotation in which we want the body part to be")]
		public SharedQuaternion matchRotation;

		// Token: 0x0400719B RID: 29083
		[Tooltip("The body part that is involved in the match")]
		public AvatarTarget targetBodyPart;

		// Token: 0x0400719C RID: 29084
		[Tooltip("Weights for matching position")]
		public Vector3 weightMaskPosition;

		// Token: 0x0400719D RID: 29085
		[Tooltip("Weights for matching rotation")]
		public float weightMaskRotation;

		// Token: 0x0400719E RID: 29086
		[Tooltip("Start time within the animation clip")]
		public float startNormalizedTime;

		// Token: 0x0400719F RID: 29087
		[Tooltip("End time within the animation clip")]
		public float targetNormalizedTime = 1f;

		// Token: 0x040071A0 RID: 29088
		private Animator animator;

		// Token: 0x040071A1 RID: 29089
		private GameObject prevGameObject;
	}
}
