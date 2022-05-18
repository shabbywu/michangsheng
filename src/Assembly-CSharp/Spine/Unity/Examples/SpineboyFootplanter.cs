using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E5D RID: 3677
	public class SpineboyFootplanter : MonoBehaviour
	{
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06005823 RID: 22563 RVA: 0x0003F091 File Offset: 0x0003D291
		public float Balance
		{
			get
			{
				return this.balance;
			}
		}

		// Token: 0x06005824 RID: 22564 RVA: 0x00246CDC File Offset: 0x00244EDC
		private void Start()
		{
			Time.timeScale = this.timeScale;
			Vector3 position = base.transform.position;
			this.nearFoot.worldPos = position;
			SpineboyFootplanter.Foot foot = this.nearFoot;
			foot.worldPos.x = foot.worldPos.x - this.comfyDistance;
			this.nearFoot.worldPosPrev = (this.nearFoot.worldPosNext = this.nearFoot.worldPos);
			this.farFoot.worldPos = position;
			SpineboyFootplanter.Foot foot2 = this.farFoot;
			foot2.worldPos.x = foot2.worldPos.x + this.comfyDistance;
			this.farFoot.worldPosPrev = (this.farFoot.worldPosNext = this.farFoot.worldPos);
			SkeletonAnimation component = base.GetComponent<SkeletonAnimation>();
			this.skeleton = component.Skeleton;
			component.UpdateLocal += new UpdateBonesDelegate(this.UpdateLocal);
			this.nearFootBone = this.skeleton.FindBone(this.nearBoneName);
			this.farFootBone = this.skeleton.FindBone(this.farBoneName);
			this.nearFoot.lerp = 1f;
			this.farFoot.lerp = 1f;
		}

		// Token: 0x06005825 RID: 22565 RVA: 0x00246E14 File Offset: 0x00245014
		private void UpdateLocal(ISkeletonAnimation animated)
		{
			Transform transform = base.transform;
			Vector2 vector = transform.position;
			float centerOfGravityX = vector.x + this.centerOfGravityXOffset;
			this.nearFoot.UpdateDistance(centerOfGravityX);
			this.farFoot.UpdateDistance(centerOfGravityX);
			this.balance = this.nearFoot.displacementFromCenter + this.farFoot.displacementFromCenter;
			this.distanceBetweenFeet = Mathf.Abs(this.nearFoot.worldPos.x - this.farFoot.worldPos.x);
			bool flag = Mathf.Abs(this.balance) > this.offBalanceThreshold;
			if (this.distanceBetweenFeet > this.feetTooFarApartThreshold || flag)
			{
				SpineboyFootplanter.Foot foot;
				SpineboyFootplanter.Foot foot2;
				if (this.nearFoot.distanceFromCenter > this.farFoot.distanceFromCenter)
				{
					foot = this.nearFoot;
					foot2 = this.farFoot;
				}
				else
				{
					foot = this.farFoot;
					foot2 = this.nearFoot;
				}
				if (!foot.IsStepInProgress && foot2.IsPrettyMuchDoneStepping)
				{
					float newDisplacement = SpineboyFootplanter.Foot.GetNewDisplacement(foot2.displacementFromCenter, this.comfyDistance, this.minimumSpaceBetweenFeet, this.maxNewStepDisplacement, this.forward, this.backward);
					foot.StartNewStep(newDisplacement, centerOfGravityX, vector.y, this.footRayRaise, this.hits, this.footSize);
				}
			}
			float deltaTime = Time.deltaTime;
			float num = this.baseLerpSpeed;
			num += (Mathf.Abs(this.balance) - 0.6f) * 2.5f;
			this.nearFoot.UpdateStepProgress(deltaTime, num, this.shuffleDistance, this.forward, this.backward);
			this.farFoot.UpdateStepProgress(deltaTime, num, this.shuffleDistance, this.forward, this.backward);
			SkeletonExtensions.SetLocalPosition(this.nearFootBone, transform.InverseTransformPoint(this.nearFoot.worldPos));
			SkeletonExtensions.SetLocalPosition(this.farFootBone, transform.InverseTransformPoint(this.farFoot.worldPos));
		}

		// Token: 0x06005826 RID: 22566 RVA: 0x00247018 File Offset: 0x00245218
		private void OnDrawGizmos()
		{
			if (Application.isPlaying)
			{
				Gizmos.color = Color.green;
				Gizmos.DrawSphere(this.nearFoot.worldPos, 0.15f);
				Gizmos.DrawWireSphere(this.nearFoot.worldPosNext, 0.15f);
				Gizmos.color = Color.magenta;
				Gizmos.DrawSphere(this.farFoot.worldPos, 0.15f);
				Gizmos.DrawWireSphere(this.farFoot.worldPosNext, 0.15f);
			}
		}

		// Token: 0x04005834 RID: 22580
		public float timeScale = 0.5f;

		// Token: 0x04005835 RID: 22581
		[SpineBone("", "", true, false)]
		public string nearBoneName;

		// Token: 0x04005836 RID: 22582
		[SpineBone("", "", true, false)]
		public string farBoneName;

		// Token: 0x04005837 RID: 22583
		[Header("Settings")]
		public Vector2 footSize;

		// Token: 0x04005838 RID: 22584
		public float footRayRaise = 2f;

		// Token: 0x04005839 RID: 22585
		public float comfyDistance = 1f;

		// Token: 0x0400583A RID: 22586
		public float centerOfGravityXOffset = -0.25f;

		// Token: 0x0400583B RID: 22587
		public float feetTooFarApartThreshold = 3f;

		// Token: 0x0400583C RID: 22588
		public float offBalanceThreshold = 1.4f;

		// Token: 0x0400583D RID: 22589
		public float minimumSpaceBetweenFeet = 0.5f;

		// Token: 0x0400583E RID: 22590
		public float maxNewStepDisplacement = 2f;

		// Token: 0x0400583F RID: 22591
		public float shuffleDistance = 1f;

		// Token: 0x04005840 RID: 22592
		public float baseLerpSpeed = 3.5f;

		// Token: 0x04005841 RID: 22593
		public SpineboyFootplanter.FootMovement forward;

		// Token: 0x04005842 RID: 22594
		public SpineboyFootplanter.FootMovement backward;

		// Token: 0x04005843 RID: 22595
		[Header("Debug")]
		[SerializeField]
		private float balance;

		// Token: 0x04005844 RID: 22596
		[SerializeField]
		private float distanceBetweenFeet;

		// Token: 0x04005845 RID: 22597
		[SerializeField]
		protected SpineboyFootplanter.Foot nearFoot;

		// Token: 0x04005846 RID: 22598
		[SerializeField]
		protected SpineboyFootplanter.Foot farFoot;

		// Token: 0x04005847 RID: 22599
		private Skeleton skeleton;

		// Token: 0x04005848 RID: 22600
		private Bone nearFootBone;

		// Token: 0x04005849 RID: 22601
		private Bone farFootBone;

		// Token: 0x0400584A RID: 22602
		private RaycastHit2D[] hits = new RaycastHit2D[1];

		// Token: 0x02000E5E RID: 3678
		[Serializable]
		public class FootMovement
		{
			// Token: 0x0400584B RID: 22603
			public AnimationCurve xMoveCurve;

			// Token: 0x0400584C RID: 22604
			public AnimationCurve raiseCurve;

			// Token: 0x0400584D RID: 22605
			public float maxRaise;

			// Token: 0x0400584E RID: 22606
			public float minDistanceCompensate;

			// Token: 0x0400584F RID: 22607
			public float maxDistanceCompensate;
		}

		// Token: 0x02000E5F RID: 3679
		[Serializable]
		public class Foot
		{
			// Token: 0x17000830 RID: 2096
			// (get) Token: 0x06005829 RID: 22569 RVA: 0x0003F099 File Offset: 0x0003D299
			public bool IsStepInProgress
			{
				get
				{
					return this.lerp < 1f;
				}
			}

			// Token: 0x17000831 RID: 2097
			// (get) Token: 0x0600582A RID: 22570 RVA: 0x0003F0A8 File Offset: 0x0003D2A8
			public bool IsPrettyMuchDoneStepping
			{
				get
				{
					return this.lerp > 0.7f;
				}
			}

			// Token: 0x0600582B RID: 22571 RVA: 0x0003F0B7 File Offset: 0x0003D2B7
			public void UpdateDistance(float centerOfGravityX)
			{
				this.displacementFromCenter = this.worldPos.x - centerOfGravityX;
				this.distanceFromCenter = Mathf.Abs(this.displacementFromCenter);
			}

			// Token: 0x0600582C RID: 22572 RVA: 0x00247138 File Offset: 0x00245338
			public void StartNewStep(float newDistance, float centerOfGravityX, float tentativeY, float footRayRaise, RaycastHit2D[] hits, Vector2 footSize)
			{
				this.lerp = 0f;
				this.worldPosPrev = this.worldPos;
				float num = centerOfGravityX - newDistance;
				Vector2 vector = new Vector2(num, tentativeY + footRayRaise);
				float num2 = 0f;
				Vector2 down = Vector2.down;
				ContactFilter2D contactFilter2D = default(ContactFilter2D);
				contactFilter2D.useTriggers = false;
				int num3 = Physics2D.BoxCast(vector, footSize, num2, down, contactFilter2D, hits);
				this.worldPosNext = ((num3 > 0) ? hits[0].point : new Vector2(num, tentativeY));
			}

			// Token: 0x0600582D RID: 22573 RVA: 0x002471B0 File Offset: 0x002453B0
			public void UpdateStepProgress(float deltaTime, float stepSpeed, float shuffleDistance, SpineboyFootplanter.FootMovement forwardMovement, SpineboyFootplanter.FootMovement backwardMovement)
			{
				if (!this.IsStepInProgress)
				{
					return;
				}
				this.lerp += deltaTime * stepSpeed;
				float num = this.worldPosNext.x - this.worldPosPrev.x;
				float num2 = Mathf.Sign(num);
				float num3 = Mathf.Abs(num);
				SpineboyFootplanter.FootMovement footMovement = (num2 > 0f) ? forwardMovement : backwardMovement;
				this.worldPos.x = Mathf.Lerp(this.worldPosPrev.x, this.worldPosNext.x, footMovement.xMoveCurve.Evaluate(this.lerp));
				float num4 = Mathf.Lerp(this.worldPosPrev.y, this.worldPosNext.y, this.lerp);
				if (num3 > shuffleDistance)
				{
					float num5 = Mathf.Clamp(num3 * 0.5f, 1f, 2f);
					this.worldPos.y = num4 + footMovement.raiseCurve.Evaluate(this.lerp) * footMovement.maxRaise * num5;
				}
				else
				{
					this.lerp += Time.deltaTime;
					this.worldPos.y = num4;
				}
				if (this.lerp > 1f)
				{
					this.lerp = 1f;
				}
			}

			// Token: 0x0600582E RID: 22574 RVA: 0x002472E0 File Offset: 0x002454E0
			public static float GetNewDisplacement(float otherLegDisplacementFromCenter, float comfyDistance, float minimumFootDistanceX, float maxNewStepDisplacement, SpineboyFootplanter.FootMovement forwardMovement, SpineboyFootplanter.FootMovement backwardMovement)
			{
				SpineboyFootplanter.FootMovement footMovement = (Mathf.Sign(otherLegDisplacementFromCenter) < 0f) ? forwardMovement : backwardMovement;
				float num = Random.Range(footMovement.minDistanceCompensate, footMovement.maxDistanceCompensate);
				float num2 = otherLegDisplacementFromCenter * num;
				if (Mathf.Abs(num2) > maxNewStepDisplacement || Mathf.Abs(otherLegDisplacementFromCenter) < minimumFootDistanceX)
				{
					num2 = comfyDistance * Mathf.Sign(num2) * num;
				}
				return num2;
			}

			// Token: 0x04005850 RID: 22608
			public Vector2 worldPos;

			// Token: 0x04005851 RID: 22609
			public float displacementFromCenter;

			// Token: 0x04005852 RID: 22610
			public float distanceFromCenter;

			// Token: 0x04005853 RID: 22611
			[Space]
			public float lerp;

			// Token: 0x04005854 RID: 22612
			public Vector2 worldPosPrev;

			// Token: 0x04005855 RID: 22613
			public Vector2 worldPosNext;
		}
	}
}
