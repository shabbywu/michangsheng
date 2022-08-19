using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000ADE RID: 2782
	public class SpineBlinkPlayer : MonoBehaviour
	{
		// Token: 0x06004DE2 RID: 19938 RVA: 0x0021491C File Offset: 0x00212B1C
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

		// Token: 0x04004D34 RID: 19764
		private const int BlinkTrack = 1;

		// Token: 0x04004D35 RID: 19765
		public AnimationReferenceAsset blinkAnimation;

		// Token: 0x04004D36 RID: 19766
		public float minimumDelay = 0.15f;

		// Token: 0x04004D37 RID: 19767
		public float maximumDelay = 3f;
	}
}
