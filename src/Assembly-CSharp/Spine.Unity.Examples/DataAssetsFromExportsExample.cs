using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples;

public class DataAssetsFromExportsExample : MonoBehaviour
{
	public TextAsset skeletonJson;

	public TextAsset atlasText;

	public Texture2D[] textures;

	public Material materialPropertySource;

	private SpineAtlasAsset runtimeAtlasAsset;

	private SkeletonDataAsset runtimeSkeletonDataAsset;

	private SkeletonAnimation runtimeSkeletonAnimation;

	private void CreateRuntimeAssetsAndGameObject()
	{
		runtimeAtlasAsset = SpineAtlasAsset.CreateRuntimeInstance(atlasText, textures, materialPropertySource, true);
		runtimeSkeletonDataAsset = SkeletonDataAsset.CreateRuntimeInstance(skeletonJson, (AtlasAssetBase)(object)runtimeAtlasAsset, true, 0.01f);
	}

	private IEnumerator Start()
	{
		CreateRuntimeAssetsAndGameObject();
		runtimeSkeletonDataAsset.GetSkeletonData(false);
		yield return (object)new WaitForSeconds(0.5f);
		runtimeSkeletonAnimation = SkeletonAnimation.NewSkeletonAnimationGameObject(runtimeSkeletonDataAsset);
		((SkeletonRenderer)runtimeSkeletonAnimation).Initialize(false);
		((SkeletonRenderer)runtimeSkeletonAnimation).Skeleton.SetSkin("base");
		((SkeletonRenderer)runtimeSkeletonAnimation).Skeleton.SetSlotsToSetupPose();
		runtimeSkeletonAnimation.AnimationState.SetAnimation(0, "run", true);
		((Renderer)((Component)runtimeSkeletonAnimation).GetComponent<MeshRenderer>()).sortingOrder = 10;
		((Component)runtimeSkeletonAnimation).transform.Translate(Vector3.down * 2f);
	}
}
