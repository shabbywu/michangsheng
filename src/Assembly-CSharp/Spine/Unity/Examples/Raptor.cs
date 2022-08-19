using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000ADC RID: 2780
	public class Raptor : MonoBehaviour
	{
		// Token: 0x06004DDC RID: 19932 RVA: 0x00214893 File Offset: 0x00212A93
		private void Start()
		{
			this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			base.StartCoroutine(this.GunGrabRoutine());
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x002148AE File Offset: 0x00212AAE
		private IEnumerator GunGrabRoutine()
		{
			this.skeletonAnimation.AnimationState.SetAnimation(0, this.walk, true);
			for (;;)
			{
				yield return new WaitForSeconds(Random.Range(0.5f, 3f));
				this.skeletonAnimation.AnimationState.SetAnimation(1, this.gungrab, false);
				yield return new WaitForSeconds(Random.Range(0.5f, 3f));
				this.skeletonAnimation.AnimationState.SetAnimation(1, this.gunkeep, false);
			}
			yield break;
		}

		// Token: 0x04004D26 RID: 19750
		public AnimationReferenceAsset walk;

		// Token: 0x04004D27 RID: 19751
		public AnimationReferenceAsset gungrab;

		// Token: 0x04004D28 RID: 19752
		public AnimationReferenceAsset gunkeep;

		// Token: 0x04004D29 RID: 19753
		private SkeletonAnimation skeletonAnimation;
	}
}
