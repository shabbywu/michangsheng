using UnityEngine;

namespace Spine.Unity.Examples;

public class Goblins : MonoBehaviour
{
	private SkeletonAnimation skeletonAnimation;

	private Bone headBone;

	private bool girlSkin;

	[Range(-360f, 360f)]
	public float extraRotation;

	public void Start()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		skeletonAnimation = ((Component)this).GetComponent<SkeletonAnimation>();
		headBone = ((SkeletonRenderer)skeletonAnimation).Skeleton.FindBone("head");
		skeletonAnimation.UpdateLocal += new UpdateBonesDelegate(UpdateLocal);
	}

	public void UpdateLocal(ISkeletonAnimation skeletonRenderer)
	{
		Bone obj = headBone;
		obj.Rotation += extraRotation;
	}

	public void OnMouseDown()
	{
		((SkeletonRenderer)skeletonAnimation).Skeleton.SetSkin(girlSkin ? "goblin" : "goblingirl");
		((SkeletonRenderer)skeletonAnimation).Skeleton.SetSlotsToSetupPose();
		girlSkin = !girlSkin;
		if (girlSkin)
		{
			((SkeletonRenderer)skeletonAnimation).Skeleton.SetAttachment("right hand item", (string)null);
			((SkeletonRenderer)skeletonAnimation).Skeleton.SetAttachment("left hand item", "spear");
		}
		else
		{
			((SkeletonRenderer)skeletonAnimation).Skeleton.SetAttachment("left hand item", "dagger");
		}
	}
}
