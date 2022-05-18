using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E55 RID: 3669
	public class SpawnFromSkeletonDataExample : MonoBehaviour
	{
		// Token: 0x06005805 RID: 22533 RVA: 0x0003EF66 File Offset: 0x0003D166
		private IEnumerator Start()
		{
			if (this.skeletonDataAsset == null)
			{
				yield break;
			}
			this.skeletonDataAsset.GetSkeletonData(false);
			yield return new WaitForSeconds(1f);
			Animation spineAnimation = this.skeletonDataAsset.GetSkeletonData(false).FindAnimation(this.startingAnimation);
			int num;
			for (int i = 0; i < this.count; i = num + 1)
			{
				SkeletonAnimation skeletonAnimation = SkeletonAnimation.NewSkeletonAnimationGameObject(this.skeletonDataAsset);
				this.DoExtraStuff(skeletonAnimation, spineAnimation);
				skeletonAnimation.gameObject.name = i.ToString();
				yield return new WaitForSeconds(0.125f);
				num = i;
			}
			yield break;
		}

		// Token: 0x06005806 RID: 22534 RVA: 0x0024678C File Offset: 0x0024498C
		private void DoExtraStuff(SkeletonAnimation sa, Animation spineAnimation)
		{
			sa.transform.localPosition = Random.insideUnitCircle * 6f;
			sa.transform.SetParent(base.transform, false);
			if (spineAnimation != null)
			{
				sa.Initialize(false);
				sa.AnimationState.SetAnimation(0, spineAnimation, true);
			}
		}

		// Token: 0x04005807 RID: 22535
		public SkeletonDataAsset skeletonDataAsset;

		// Token: 0x04005808 RID: 22536
		[Range(0f, 100f)]
		public int count = 20;

		// Token: 0x04005809 RID: 22537
		[SpineAnimation("", "skeletonDataAsset", true, false)]
		public string startingAnimation;
	}
}
