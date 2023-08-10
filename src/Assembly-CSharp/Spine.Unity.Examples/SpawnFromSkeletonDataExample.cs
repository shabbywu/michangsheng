using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples;

public class SpawnFromSkeletonDataExample : MonoBehaviour
{
	public SkeletonDataAsset skeletonDataAsset;

	[Range(0f, 100f)]
	public int count = 20;

	[SpineAnimation("", "skeletonDataAsset", true, false)]
	public string startingAnimation;

	private IEnumerator Start()
	{
		if (!((Object)(object)skeletonDataAsset == (Object)null))
		{
			skeletonDataAsset.GetSkeletonData(false);
			yield return (object)new WaitForSeconds(1f);
			Animation spineAnimation = skeletonDataAsset.GetSkeletonData(false).FindAnimation(startingAnimation);
			for (int i = 0; i < count; i++)
			{
				SkeletonAnimation val = SkeletonAnimation.NewSkeletonAnimationGameObject(skeletonDataAsset);
				DoExtraStuff(val, spineAnimation);
				((Object)((Component)val).gameObject).name = i.ToString();
				yield return (object)new WaitForSeconds(0.125f);
			}
		}
	}

	private void DoExtraStuff(SkeletonAnimation sa, Animation spineAnimation)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		((Component)sa).transform.localPosition = Vector2.op_Implicit(Random.insideUnitCircle * 6f);
		((Component)sa).transform.SetParent(((Component)this).transform, false);
		if (spineAnimation != null)
		{
			((SkeletonRenderer)sa).Initialize(false);
			sa.AnimationState.SetAnimation(0, spineAnimation, true);
		}
	}
}
