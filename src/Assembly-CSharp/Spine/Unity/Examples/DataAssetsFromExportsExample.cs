using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E1D RID: 3613
	public class DataAssetsFromExportsExample : MonoBehaviour
	{
		// Token: 0x0600571D RID: 22301 RVA: 0x0003E48D File Offset: 0x0003C68D
		private void CreateRuntimeAssetsAndGameObject()
		{
			this.runtimeAtlasAsset = SpineAtlasAsset.CreateRuntimeInstance(this.atlasText, this.textures, this.materialPropertySource, true);
			this.runtimeSkeletonDataAsset = SkeletonDataAsset.CreateRuntimeInstance(this.skeletonJson, this.runtimeAtlasAsset, true, 0.01f);
		}

		// Token: 0x0600571E RID: 22302 RVA: 0x0003E4CA File Offset: 0x0003C6CA
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

		// Token: 0x040056DF RID: 22239
		public TextAsset skeletonJson;

		// Token: 0x040056E0 RID: 22240
		public TextAsset atlasText;

		// Token: 0x040056E1 RID: 22241
		public Texture2D[] textures;

		// Token: 0x040056E2 RID: 22242
		public Material materialPropertySource;

		// Token: 0x040056E3 RID: 22243
		private SpineAtlasAsset runtimeAtlasAsset;

		// Token: 0x040056E4 RID: 22244
		private SkeletonDataAsset runtimeSkeletonDataAsset;

		// Token: 0x040056E5 RID: 22245
		private SkeletonAnimation runtimeSkeletonAnimation;
	}
}
