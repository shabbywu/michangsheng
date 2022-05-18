using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E29 RID: 3625
	public class SpineBlinkPlayer : MonoBehaviour
	{
		// Token: 0x06005752 RID: 22354 RVA: 0x0003E671 File Offset: 0x0003C871
		private IEnumerator Start()
		{
			SkeletonAnimation skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			if (skeletonAnimation == null)
			{
				yield break;
			}
			for (;;)
			{
				skeletonAnimation.AnimationState.SetAnimation(1, this.blinkAnimation, false);
				yield return new WaitForSeconds(Random.Range(this.minimumDelay, this.maximumDelay));
			}
			yield break;
		}

		// Token: 0x04005734 RID: 22324
		private const int BlinkTrack = 1;

		// Token: 0x04005735 RID: 22325
		public AnimationReferenceAsset blinkAnimation;

		// Token: 0x04005736 RID: 22326
		public float minimumDelay = 0.15f;

		// Token: 0x04005737 RID: 22327
		public float maximumDelay = 3f;
	}
}
