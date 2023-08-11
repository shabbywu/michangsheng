using System;
using UnityEngine;

namespace Spine.Unity.Examples;

public class SpineboyFootplanter : MonoBehaviour
{
	[Serializable]
	public class FootMovement
	{
		public AnimationCurve xMoveCurve;

		public AnimationCurve raiseCurve;

		public float maxRaise;

		public float minDistanceCompensate;

		public float maxDistanceCompensate;
	}

	[Serializable]
	public class Foot
	{
		public Vector2 worldPos;

		public float displacementFromCenter;

		public float distanceFromCenter;

		[Space]
		public float lerp;

		public Vector2 worldPosPrev;

		public Vector2 worldPosNext;

		public bool IsStepInProgress => lerp < 1f;

		public bool IsPrettyMuchDoneStepping => lerp > 0.7f;

		public void UpdateDistance(float centerOfGravityX)
		{
			displacementFromCenter = worldPos.x - centerOfGravityX;
			distanceFromCenter = Mathf.Abs(displacementFromCenter);
		}

		public void StartNewStep(float newDistance, float centerOfGravityX, float tentativeY, float footRayRaise, RaycastHit2D[] hits, Vector2 footSize)
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			lerp = 0f;
			worldPosPrev = worldPos;
			float num = centerOfGravityX - newDistance;
			int num2 = Physics2D.BoxCast(new Vector2(num, tentativeY + footRayRaise), footSize, 0f, Vector2.down, new ContactFilter2D
			{
				useTriggers = false
			}, hits);
			worldPosNext = (Vector2)((num2 > 0) ? ((RaycastHit2D)(ref hits[0])).point : new Vector2(num, tentativeY));
		}

		public void UpdateStepProgress(float deltaTime, float stepSpeed, float shuffleDistance, FootMovement forwardMovement, FootMovement backwardMovement)
		{
			if (IsStepInProgress)
			{
				lerp += deltaTime * stepSpeed;
				float num = worldPosNext.x - worldPosPrev.x;
				float num2 = Mathf.Sign(num);
				float num3 = Mathf.Abs(num);
				FootMovement footMovement = ((num2 > 0f) ? forwardMovement : backwardMovement);
				worldPos.x = Mathf.Lerp(worldPosPrev.x, worldPosNext.x, footMovement.xMoveCurve.Evaluate(lerp));
				float num4 = Mathf.Lerp(worldPosPrev.y, worldPosNext.y, lerp);
				if (num3 > shuffleDistance)
				{
					float num5 = Mathf.Clamp(num3 * 0.5f, 1f, 2f);
					worldPos.y = num4 + footMovement.raiseCurve.Evaluate(lerp) * footMovement.maxRaise * num5;
				}
				else
				{
					lerp += Time.deltaTime;
					worldPos.y = num4;
				}
				if (lerp > 1f)
				{
					lerp = 1f;
				}
			}
		}

		public static float GetNewDisplacement(float otherLegDisplacementFromCenter, float comfyDistance, float minimumFootDistanceX, float maxNewStepDisplacement, FootMovement forwardMovement, FootMovement backwardMovement)
		{
			FootMovement footMovement = ((Mathf.Sign(otherLegDisplacementFromCenter) < 0f) ? forwardMovement : backwardMovement);
			float num = Random.Range(footMovement.minDistanceCompensate, footMovement.maxDistanceCompensate);
			float num2 = otherLegDisplacementFromCenter * num;
			if (Mathf.Abs(num2) > maxNewStepDisplacement || Mathf.Abs(otherLegDisplacementFromCenter) < minimumFootDistanceX)
			{
				num2 = comfyDistance * Mathf.Sign(num2) * num;
			}
			return num2;
		}
	}

	public float timeScale = 0.5f;

	[SpineBone("", "", true, false)]
	public string nearBoneName;

	[SpineBone("", "", true, false)]
	public string farBoneName;

	[Header("Settings")]
	public Vector2 footSize;

	public float footRayRaise = 2f;

	public float comfyDistance = 1f;

	public float centerOfGravityXOffset = -0.25f;

	public float feetTooFarApartThreshold = 3f;

	public float offBalanceThreshold = 1.4f;

	public float minimumSpaceBetweenFeet = 0.5f;

	public float maxNewStepDisplacement = 2f;

	public float shuffleDistance = 1f;

	public float baseLerpSpeed = 3.5f;

	public FootMovement forward;

	public FootMovement backward;

	[Header("Debug")]
	[SerializeField]
	private float balance;

	[SerializeField]
	private float distanceBetweenFeet;

	[SerializeField]
	protected Foot nearFoot;

	[SerializeField]
	protected Foot farFoot;

	private Skeleton skeleton;

	private Bone nearFootBone;

	private Bone farFootBone;

	private RaycastHit2D[] hits = (RaycastHit2D[])(object)new RaycastHit2D[1];

	public float Balance => balance;

	private void Start()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Expected O, but got Unknown
		Time.timeScale = timeScale;
		Vector3 position = ((Component)this).transform.position;
		nearFoot.worldPos = Vector2.op_Implicit(position);
		nearFoot.worldPos.x -= comfyDistance;
		nearFoot.worldPosPrev = (nearFoot.worldPosNext = nearFoot.worldPos);
		farFoot.worldPos = Vector2.op_Implicit(position);
		farFoot.worldPos.x += comfyDistance;
		farFoot.worldPosPrev = (farFoot.worldPosNext = farFoot.worldPos);
		SkeletonAnimation component = ((Component)this).GetComponent<SkeletonAnimation>();
		skeleton = ((SkeletonRenderer)component).Skeleton;
		component.UpdateLocal += new UpdateBonesDelegate(UpdateLocal);
		nearFootBone = skeleton.FindBone(nearBoneName);
		farFootBone = skeleton.FindBone(farBoneName);
		nearFoot.lerp = 1f;
		farFoot.lerp = 1f;
	}

	private void UpdateLocal(ISkeletonAnimation animated)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		Transform transform = ((Component)this).transform;
		Vector2 val = Vector2.op_Implicit(transform.position);
		float centerOfGravityX = val.x + centerOfGravityXOffset;
		nearFoot.UpdateDistance(centerOfGravityX);
		farFoot.UpdateDistance(centerOfGravityX);
		balance = nearFoot.displacementFromCenter + farFoot.displacementFromCenter;
		distanceBetweenFeet = Mathf.Abs(nearFoot.worldPos.x - farFoot.worldPos.x);
		bool flag = Mathf.Abs(balance) > offBalanceThreshold;
		if (distanceBetweenFeet > feetTooFarApartThreshold || flag)
		{
			Foot foot;
			Foot foot2;
			if (nearFoot.distanceFromCenter > farFoot.distanceFromCenter)
			{
				foot = nearFoot;
				foot2 = farFoot;
			}
			else
			{
				foot = farFoot;
				foot2 = nearFoot;
			}
			if (!foot.IsStepInProgress && foot2.IsPrettyMuchDoneStepping)
			{
				float newDisplacement = Foot.GetNewDisplacement(foot2.displacementFromCenter, comfyDistance, minimumSpaceBetweenFeet, maxNewStepDisplacement, forward, backward);
				foot.StartNewStep(newDisplacement, centerOfGravityX, val.y, footRayRaise, hits, footSize);
			}
		}
		float deltaTime = Time.deltaTime;
		float num = baseLerpSpeed;
		num += (Mathf.Abs(balance) - 0.6f) * 2.5f;
		nearFoot.UpdateStepProgress(deltaTime, num, shuffleDistance, forward, backward);
		farFoot.UpdateStepProgress(deltaTime, num, shuffleDistance, forward, backward);
		SkeletonExtensions.SetLocalPosition(nearFootBone, transform.InverseTransformPoint(Vector2.op_Implicit(nearFoot.worldPos)));
		SkeletonExtensions.SetLocalPosition(farFootBone, transform.InverseTransformPoint(Vector2.op_Implicit(farFoot.worldPos)));
	}

	private void OnDrawGizmos()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if (Application.isPlaying)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(Vector2.op_Implicit(nearFoot.worldPos), 0.15f);
			Gizmos.DrawWireSphere(Vector2.op_Implicit(nearFoot.worldPosNext), 0.15f);
			Gizmos.color = Color.magenta;
			Gizmos.DrawSphere(Vector2.op_Implicit(farFoot.worldPos), 0.15f);
			Gizmos.DrawWireSphere(Vector2.op_Implicit(farFoot.worldPosNext), 0.15f);
		}
	}
}
