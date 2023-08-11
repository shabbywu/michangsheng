using UnityEngine;

namespace Spine.Unity.Examples;

public class SpineboyTargetController : MonoBehaviour
{
	public SkeletonAnimation skeletonAnimation;

	[SpineBone("", "skeletonAnimation", true, false)]
	public string boneName;

	public Camera camera;

	private Bone bone;

	private void OnValidate()
	{
		if ((Object)(object)skeletonAnimation == (Object)null)
		{
			skeletonAnimation = ((Component)this).GetComponent<SkeletonAnimation>();
		}
	}

	private void Start()
	{
		bone = ((SkeletonRenderer)skeletonAnimation).Skeleton.FindBone(boneName);
	}

	private void Update()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		Vector3 mousePosition = Input.mousePosition;
		Vector3 val = camera.ScreenToWorldPoint(mousePosition);
		Vector3 val2 = ((Component)skeletonAnimation).transform.InverseTransformPoint(val);
		val2.x *= ((SkeletonRenderer)skeletonAnimation).Skeleton.ScaleX;
		val2.y *= ((SkeletonRenderer)skeletonAnimation).Skeleton.ScaleY;
		SkeletonExtensions.SetLocalPosition(bone, val2);
	}
}
