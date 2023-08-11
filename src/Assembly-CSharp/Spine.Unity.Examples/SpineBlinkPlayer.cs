using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples;

public class SpineBlinkPlayer : MonoBehaviour
{
	private const int BlinkTrack = 1;

	public AnimationReferenceAsset blinkAnimation;

	public float minimumDelay = 0.15f;

	public float maximumDelay = 3f;

	private IEnumerator Start()
	{
		SkeletonAnimation skeletonAnimation = ((Component)this).GetComponent<SkeletonAnimation>();
		if ((Object)(object)skeletonAnimation == (Object)null)
		{
			yield break;
		}
		while (true)
		{
			skeletonAnimation.AnimationState.SetAnimation(1, AnimationReferenceAsset.op_Implicit(blinkAnimation), false);
			yield return (object)new WaitForSeconds(Random.Range(minimumDelay, maximumDelay));
		}
	}
}
