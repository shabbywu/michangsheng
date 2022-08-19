using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AD7 RID: 2775
	public class DataAssetsFromExportsExample : MonoBehaviour
	{
		// Token: 0x06004DC5 RID: 19909 RVA: 0x0021400B File Offset: 0x0021220B
		private void CreateRuntimeAssetsAndGameObject()
		{
			this.runtimeAtlasAsset = SpineAtlasAsset.CreateRuntimeInstance(this.atlasText, this.textures, this.materialPropertySource, true);
			this.runtimeSkeletonDataAsset = SkeletonDataAsset.CreateRuntimeInstance(this.skeletonJson, this.runtimeAtlasAsset, true, 0.01f);
		}

		// Token: 0x06004DC6 RID: 19910 RVA: 0x00214048 File Offset: 0x00212248
		private IEnumerator Start()
		{
			this.CreateRuntimeAssetsAndGameObject();
			this.runtimeSkeletonDataAsset.GetSkeletonData(false);
			yield return new WaitForSeconds(0.5f);
			this.runtimeSkeletonAnimation = SkeletonAnimation.NewSkeletonAnimationGameObject(this.runtimeSkeletonDataAsset);
			this.runtimeSkeletonAnimation.Initialize(false);
			this.runtimeSkeletonAnimation.Skeleton.SetSkin("base");
			this.runtimeSkeletonAnimation.Skeleton.SetSlotsToSetupPose();
			this.runtimeSkeletonAnimation.AnimationState.SetAnimation(0, "run", true);
			this.runtimeSkeletonAnimation.GetComponent<MeshRenderer>().sortingOrder = 10;
			this.runtimeSkeletonAnimation.transform.Translate(Vector3.down * 2f);
			yield break;
		}

		// Token: 0x04004CF4 RID: 19700
		public TextAsset skeletonJson;

		// Token: 0x04004CF5 RID: 19701
		public TextAsset atlasText;

		// Token: 0x04004CF6 RID: 19702
		public Texture2D[] textures;

		// Token: 0x04004CF7 RID: 19703
		public Material materialPropertySource;

		// Token: 0x04004CF8 RID: 19704
		private SpineAtlasAsset runtimeAtlasAsset;

		// Token: 0x04004CF9 RID: 19705
		private SkeletonDataAsset runtimeSkeletonDataAsset;

		// Token: 0x04004CFA RID: 19706
		private SkeletonAnimation runtimeSkeletonAnimation;
	}
}
