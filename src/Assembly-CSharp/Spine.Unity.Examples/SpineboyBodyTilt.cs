using UnityEngine;

namespace Spine.Unity.Examples;

public class SpineboyBodyTilt : MonoBehaviour
{
	[Header("Settings")]
	public SpineboyFootplanter planter;

	[SpineBone("", "", true, false)]
	public string hip = "hip";

	[SpineBone("", "", true, false)]
	public string head = "head";

	public float hipTiltScale = 7f;

	public float headTiltScale = 0.7f;

	public float hipRotationMoveScale = 60f;

	[Header("Debug")]
	public float hipRotationTarget;

	public float hipRotationSmoothed;

	public float baseHeadRotation;

	private Bone hipBone;

	private Bone headBone;

	private void Start()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		SkeletonAnimation component = ((Component)this).GetComponent<SkeletonAnimation>();
		Skeleton skeleton = ((SkeletonRenderer)component).Skeleton;
		hipBone = skeleton.FindBone(hip);
		headBone = skeleton.FindBone(head);
		baseHeadRotation = headBone.Rotation;
		component.UpdateLocal += new UpdateBonesDelegate(UpdateLocal);
	}

	private void UpdateLocal(ISkeletonAnimation animated)
	{
		hipRotationTarget = planter.Balance * hipTiltScale;
		hipRotationSmoothed = Mathf.MoveTowards(hipRotationSmoothed, hipRotationTarget, Time.deltaTime * hipRotationMoveScale * Mathf.Abs(2f * planter.Balance / planter.offBalanceThreshold));
		hipBone.Rotation = hipRotationSmoothed;
		headBone.Rotation = baseHeadRotation + (0f - hipRotationSmoothed) * headTiltScale;
	}
}
