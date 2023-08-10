using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples;

public class SpawnSkeletonGraphicExample : MonoBehaviour
{
	public SkeletonDataAsset skeletonDataAsset;

	[SpineAnimation("", "skeletonDataAsset", true, false)]
	public string startingAnimation;

	[SpineSkin("", "skeletonDataAsset", true, false, false)]
	public string startingSkin = "base";

	public Material skeletonGraphicMaterial;

	private IEnumerator Start()
	{
		if (!((Object)(object)skeletonDataAsset == (Object)null))
		{
			skeletonDataAsset.GetSkeletonData(false);
			yield return (object)new WaitForSeconds(1f);
			SkeletonGraphic obj = SkeletonGraphic.NewSkeletonGraphicGameObject(skeletonDataAsset, ((Component)this).transform, skeletonGraphicMaterial);
			((Object)((Component)obj).gameObject).name = "SkeletonGraphic Instance";
			obj.Initialize(false);
			obj.Skeleton.SetSkin(startingSkin);
			obj.Skeleton.SetSlotsToSetupPose();
			obj.AnimationState.SetAnimation(0, startingAnimation, true);
		}
	}
}
