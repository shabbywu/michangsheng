using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples;

public class EquipsVisualsComponentExample : MonoBehaviour
{
	public SkeletonAnimation skeletonAnimation;

	[SpineSkin("", "", true, false, false)]
	public string templateSkinName;

	private Skin equipsSkin;

	private Skin collectedSkin;

	public Material runtimeMaterial;

	public Texture2D runtimeAtlas;

	private void Start()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		equipsSkin = new Skin("Equips");
		Skin val = ((SkeletonRenderer)skeletonAnimation).Skeleton.Data.FindSkin(templateSkinName);
		if (val != null)
		{
			SkinUtilities.AddAttachments(equipsSkin, val);
		}
		((SkeletonRenderer)skeletonAnimation).Skeleton.Skin = equipsSkin;
		RefreshSkeletonAttachments();
	}

	public void Equip(int slotIndex, string attachmentName, Attachment attachment)
	{
		equipsSkin.AddAttachment(slotIndex, attachmentName, attachment);
		((SkeletonRenderer)skeletonAnimation).Skeleton.SetSkin(equipsSkin);
		RefreshSkeletonAttachments();
	}

	public void OptimizeSkin()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		collectedSkin = (Skin)(((object)collectedSkin) ?? ((object)new Skin("Collected skin")));
		SkinUtilities.Clear(collectedSkin);
		SkinUtilities.AddAttachments(collectedSkin, ((SkeletonRenderer)skeletonAnimation).Skeleton.Data.DefaultSkin);
		SkinUtilities.AddAttachments(collectedSkin, equipsSkin);
		Skin repackedSkin = AtlasUtilities.GetRepackedSkin(collectedSkin, "Repacked skin", ((SkeletonRenderer)skeletonAnimation).SkeletonDataAsset.atlasAssets[0].PrimaryMaterial, ref runtimeMaterial, ref runtimeAtlas, 1024, 2, (TextureFormat)4, false, true);
		SkinUtilities.Clear(collectedSkin);
		((SkeletonRenderer)skeletonAnimation).Skeleton.Skin = repackedSkin;
		RefreshSkeletonAttachments();
	}

	private void RefreshSkeletonAttachments()
	{
		((SkeletonRenderer)skeletonAnimation).Skeleton.SetSlotsToSetupPose();
		skeletonAnimation.AnimationState.Apply(((SkeletonRenderer)skeletonAnimation).Skeleton);
	}
}
