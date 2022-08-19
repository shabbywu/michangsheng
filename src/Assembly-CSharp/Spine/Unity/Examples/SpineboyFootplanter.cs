using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000B01 RID: 2817
	public class SpineboyFootplanter : MonoBehaviour
	{
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06004E73 RID: 20083 RVA: 0x00216D20 File Offset: 0x00214F20
		public float Balance
		{
			get
			{
				return this.balance;
			}
		}

		// Token: 0x06004E74 RID: 20084 RVA: 0x00216D28 File Offset: 0x00214F28
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

		// Token: 0x06004E75 RID: 20085 RVA: 0x00216E60 File Offset: 0x00215060
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

		// Token: 0x06004E76 RID: 20086 RVA: 0x00217064 File Offset: 0x00215264
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

		// Token: 0x04004DFE RID: 19966
		public float timeScale = 0.5f;

		// Token: 0x04004DFF RID: 19967
		[SpineBone("", "", true, false)]
		public string nearBoneName;

		// Token: 0x04004E00 RID: 19968
		[SpineBone("", "", true, false)]
		public string farBoneName;

		// Token: 0x04004E01 RID: 19969
		[Header("Settings")]
		public Vector2 footSize;

		// Token: 0x04004E02 RID: 19970
		public float footRayRaise = 2f;

		// Token: 0x04004E03 RID: 19971
		public float comfyDistance = 1f;

		// Token: 0x04004E04 RID: 19972
		public float centerOfGravityXOffset = -0.25f;

		// Token: 0x04004E05 RID: 19973
		public float feetTooFarApartThreshold = 3f;

		// Token: 0x04004E06 RID: 19974
		public float offBalanceThreshold = 1.4f;

		// Token: 0x04004E07 RID: 19975
		public float minimumSpaceBetweenFeet = 0.5f;

		// Token: 0x04004E08 RID: 19976
		public float maxNewStepDisplacement = 2f;

		// Token: 0x04004E09 RID: 19977
		public float shuffleDistance = 1f;

		// Token: 0x04004E0A RID: 19978
		public float baseLerpSpeed = 3.5f;

		// Token: 0x04004E0B RID: 19979
		public SpineboyFootplanter.FootMovement forward;

		// Token: 0x04004E0C RID: 19980
		public SpineboyFootplanter.FootMovement backward;

		// Token: 0x04004E0D RID: 19981
		[Header("Debug")]
		[SerializeField]
		private float balance;

		// Token: 0x04004E0E RID: 19982
		[SerializeField]
		private float distanceBetweenFeet;

		// Token: 0x04004E0F RID: 19983
		[SerializeField]
		protected SpineboyFootplanter.Foot nearFoot;

		// Token: 0x04004E10 RID: 19984
		[SerializeField]
		protected SpineboyFootplanter.Foot farFoot;

		// Token: 0x04004E11 RID: 19985
		private Skeleton skeleton;

		// Token: 0x04004E12 RID: 19986
		private Bone nearFootBone;

		// Token: 0x04004E13 RID: 19987
		private Bone farFootBone;

		// Token: 0x04004E14 RID: 19988
		private RaycastHit2D[] hits = new RaycastHit2D[1];

		// Token: 0x020015D2 RID: 5586
		[Serializable]
		public class FootMovement
		{
			// Token: 0x0400708D RID: 28813
			public AnimationCurve xMoveCurve;

			// Token: 0x0400708E RID: 28814
			public AnimationCurve raiseCurve;

			// Token: 0x0400708F RID: 28815
			public float maxRaise;

			// Token: 0x04007090 RID: 28816
			public float minDistanceCompensate;

			// Token: 0x04007091 RID: 28817
			public float maxDistanceCompensate;
		}

		// Token: 0x020015D3 RID: 5587
		[Serializable]
		public class Foot
		{
			// Token: 0x17000B63 RID: 2915
			// (get) Token: 0x06008517 RID: 34071 RVA: 0x002E3E26 File Offset: 0x002E2026
			public bool IsStepInProgress
			{
				get
				{
					return this.lerp < 1f;
				}
			}

			// Token: 0x17000B64 RID: 2916
			// (get) Token: 0x06008518 RID: 34072 RVA: 0x002E3E35 File Offset: 0x002E2035
			public bool IsPrettyMuchDoneStepping
			{
				get
				{
					return this.lerp > 0.7f;
				}
			}

			// Token: 0x06008519 RID: 34073 RVA: 0x002E3E44 File Offset: 0x002E2044
			public void UpdateDistance(float centerOfGravityX)
			{
				this.displacementFromCenter = this.worldPos.x - centerOfGravityX;
				this.distanceFromCenter = Mathf.Abs(this.displacementFromCenter);
			}

			// Token: 0x0600851A RID: 34074 RVA: 0x002E3E6C File Offset: 0x002E206C
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

			// Token: 0x0600851B RID: 34075 RVA: 0x002E3EE4 File Offset: 0x002E20E4
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

			// Token: 0x0600851C RID: 34076 RVA: 0x002E4014 File Offset: 0x002E2214
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

			// Token: 0x04007092 RID: 28818
			public Vector2 worldPos;

			// Token: 0x04007093 RID: 28819
			public float displacementFromCenter;

			// Token: 0x04007094 RID: 28820
			public float distanceFromCenter;

			// Token: 0x04007095 RID: 28821
			[Space]
			public float lerp;

			// Token: 0x04007096 RID: 28822
			public Vector2 worldPosPrev;

			// Token: 0x04007097 RID: 28823
			public Vector2 worldPosNext;
		}
	}
}
