using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AFC RID: 2812
	public class SpawnFromSkeletonDataExample : MonoBehaviour
	{
		// Token: 0x06004E64 RID: 20068 RVA: 0x002168C7 File Offset: 0x00214AC7
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

		// Token: 0x06004E65 RID: 20069 RVA: 0x002168D8 File Offset: 0x00214AD8
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

		// Token: 0x04004DDB RID: 19931
		public SkeletonDataAsset skeletonDataAsset;

		// Token: 0x04004DDC RID: 19932
		[Range(0f, 100f)]
		public int count = 20;

		// Token: 0x04004DDD RID: 19933
		[SpineAnimation("", "skeletonDataAsset", true, false)]
		public string startingAnimation;
	}
}
