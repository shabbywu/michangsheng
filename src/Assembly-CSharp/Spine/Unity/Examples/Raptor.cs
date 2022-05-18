using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E25 RID: 3621
	public class Raptor : MonoBehaviour
	{
		// Token: 0x06005740 RID: 22336 RVA: 0x0003E5BA File Offset: 0x0003C7BA
		private void Start()
		{
			this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			base.StartCoroutine(this.GunGrabRoutine());
		}

		// Token: 0x06005741 RID: 22337 RVA: 0x0003E5D5 File Offset: 0x0003C7D5
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

		// Token: 0x04005720 RID: 22304
		public AnimationReferenceAsset walk;

		// Token: 0x04005721 RID: 22305
		public AnimationReferenceAsset gungrab;

		// Token: 0x04005722 RID: 22306
		public AnimationReferenceAsset gunkeep;

		// Token: 0x04005723 RID: 22307
		private SkeletonAnimation skeletonAnimation;
	}
}
