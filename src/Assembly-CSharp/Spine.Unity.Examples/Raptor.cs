using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples;

public class Raptor : MonoBehaviour
{
	public AnimationReferenceAsset walk;

	public AnimationReferenceAsset gungrab;

	public AnimationReferenceAsset gunkeep;

	private SkeletonAnimation skeletonAnimation;

	private void Start()
	{
		skeletonAnimation = ((Component)this).GetComponent<SkeletonAnimation>();
		((MonoBehaviour)this).StartCoroutine(GunGrabRoutine());
	}

	private IEnumerator GunGrabRoutine()
	{
		skeletonAnimation.AnimationState.SetAnimation(0, AnimationReferenceAsset.op_Implicit(walk), true);
		while (true)
		{
			yield return (object)new WaitForSeconds(Random.Range(0.5f, 3f));
			skeletonAnimation.AnimationState.SetAnimation(1, AnimationReferenceAsset.op_Implicit(gungrab), false);
			yield return (object)new WaitForSeconds(Random.Range(0.5f, 3f));
			skeletonAnimation.AnimationState.SetAnimation(1, AnimationReferenceAsset.op_Implicit(gunkeep), false);
		}
	}
}
