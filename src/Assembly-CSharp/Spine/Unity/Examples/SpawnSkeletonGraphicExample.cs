using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E57 RID: 3671
	public class SpawnSkeletonGraphicExample : MonoBehaviour
	{
		// Token: 0x0600580E RID: 22542 RVA: 0x0003EF9C File Offset: 0x0003D19C
		private IEnumerator Start()
		{
			if (this.skeletonDataAsset == null)
			{
				yield break;
			}
			this.skeletonDataAsset.GetSkeletonData(false);
			yield return new WaitForSeconds(1f);
			SkeletonGraphic skeletonGraphic = SkeletonGraphic.NewSkeletonGraphicGameObject(this.skeletonDataAsset, base.transform, this.skeletonGraphicMaterial);
			skeletonGraphic.gameObject.name = "SkeletonGraphic Instance";
			skeletonGraphic.Initialize(false);
			skeletonGraphic.Skeleton.SetSkin(this.startingSkin);
			skeletonGraphic.Skeleton.SetSlotsToSetupPose();
			skeletonGraphic.AnimationState.SetAnimation(0, this.startingAnimation, true);
			yield break;
		}

		// Token: 0x0400580F RID: 22543
		public SkeletonDataAsset skeletonDataAsset;

		// Token: 0x04005810 RID: 22544
		[SpineAnimation("", "skeletonDataAsset", true, false)]
		public string startingAnimation;

		// Token: 0x04005811 RID: 22545
		[SpineSkin("", "skeletonDataAsset", true, false, false)]
		public string startingSkin = "base";

		// Token: 0x04005812 RID: 22546
		public Material skeletonGraphicMaterial;
	}
}
