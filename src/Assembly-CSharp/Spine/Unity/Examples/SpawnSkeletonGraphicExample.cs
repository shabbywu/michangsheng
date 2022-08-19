using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AFD RID: 2813
	public class SpawnSkeletonGraphicExample : MonoBehaviour
	{
		// Token: 0x06004E67 RID: 20071 RVA: 0x0021693F File Offset: 0x00214B3F
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

		// Token: 0x04004DDE RID: 19934
		public SkeletonDataAsset skeletonDataAsset;

		// Token: 0x04004DDF RID: 19935
		[SpineAnimation("", "skeletonDataAsset", true, false)]
		public string startingAnimation;

		// Token: 0x04004DE0 RID: 19936
		[SpineSkin("", "skeletonDataAsset", true, false, false)]
		public string startingSkin = "base";

		// Token: 0x04004DE1 RID: 19937
		public Material skeletonGraphicMaterial;
	}
}
