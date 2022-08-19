using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityAnimator
{
	// Token: 0x02001197 RID: 4503
	[TaskCategory("Basic/Animator")]
	[TaskDescription("Automatically adjust the gameobject position and rotation so that the AvatarTarget reaches the matchPosition when the current state is at the specified progress. Returns Success.")]
	public class MatchTarget : Action
	{
		// Token: 0x060076EF RID: 30447 RVA: 0x002B7AD8 File Offset: 0x002B5CD8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060076F0 RID: 30448 RVA: 0x002B7B18 File Offset: 0x002B5D18
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

		// Token: 0x060076F1 RID: 30449 RVA: 0x002B7B84 File Offset: 0x002B5D84
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

		// Token: 0x04006275 RID: 25205
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04006276 RID: 25206
		[Tooltip("The position we want the body part to reach")]
		public SharedVector3 matchPosition;

		// Token: 0x04006277 RID: 25207
		[Tooltip("The rotation in which we want the body part to be")]
		public SharedQuaternion matchRotation;

		// Token: 0x04006278 RID: 25208
		[Tooltip("The body part that is involved in the match")]
		public AvatarTarget targetBodyPart;

		// Token: 0x04006279 RID: 25209
		[Tooltip("Weights for matching position")]
		public Vector3 weightMaskPosition;

		// Token: 0x0400627A RID: 25210
		[Tooltip("Weights for matching rotation")]
		public float weightMaskRotation;

		// Token: 0x0400627B RID: 25211
		[Tooltip("Start time within the animation clip")]
		public float startNormalizedTime;

		// Token: 0x0400627C RID: 25212
		[Tooltip("End time within the animation clip")]
		public float targetNormalizedTime = 1f;

		// Token: 0x0400627D RID: 25213
		private Animator animator;

		// Token: 0x0400627E RID: 25214
		private GameObject prevGameObject;
	}
}
