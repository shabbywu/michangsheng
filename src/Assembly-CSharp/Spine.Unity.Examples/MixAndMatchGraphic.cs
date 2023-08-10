using System.Collections;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples;

public class MixAndMatchGraphic : MonoBehaviour
{
	[SpineSkin("", "", true, false, false)]
	public string baseSkinName = "base";

	public Material sourceMaterial;

	[Header("Visor")]
	public Sprite visorSprite;

	[SpineSlot("", "", false, true, false)]
	public string visorSlot;

	[SpineAttachment(true, false, false, "visorSlot", "", "baseSkinName", true, false)]
	public string visorKey = "goggles";

	[Header("Gun")]
	public Sprite gunSprite;

	[SpineSlot("", "", false, true, false)]
	public string gunSlot;

	[SpineAttachment(true, false, false, "gunSlot", "", "baseSkinName", true, false)]
	public string gunKey = "gun";

	[Header("Runtime Repack Required!!")]
	public bool repack = true;

	[Header("Do not assign")]
	public Texture2D runtimeAtlas;

	public Material runtimeMaterial;

	private Skin customSkin;

	private void OnValidate()
	{
		if ((Object)(object)sourceMaterial == (Object)null)
		{
			SkeletonGraphic component = ((Component)this).GetComponent<SkeletonGraphic>();
			if ((Object)(object)component != (Object)null)
			{
				sourceMaterial = component.SkeletonDataAsset.atlasAssets[0].PrimaryMaterial;
			}
		}
	}

	private IEnumerator Start()
	{
		yield return (object)new WaitForSeconds(1f);
		Apply();
	}

	[ContextMenu("Apply")]
	private void Apply()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		SkeletonGraphic component = ((Component)this).GetComponent<SkeletonGraphic>();
		Skeleton skeleton = component.Skeleton;
		customSkin = (Skin)(((object)customSkin) ?? ((object)new Skin("custom skin")));
		Skin obj = skeleton.Data.FindSkin(baseSkinName);
		int num = skeleton.FindSlotIndex(visorSlot);
		Attachment remappedClone = AttachmentCloneExtensions.GetRemappedClone(obj.GetAttachment(num, visorKey), visorSprite, sourceMaterial, true, true, false);
		SkinUtilities.SetAttachment(customSkin, num, visorKey, remappedClone);
		int num2 = skeleton.FindSlotIndex(gunSlot);
		Attachment remappedClone2 = AttachmentCloneExtensions.GetRemappedClone(obj.GetAttachment(num2, gunKey), gunSprite, sourceMaterial, true, true, false);
		if (remappedClone2 != null)
		{
			SkinUtilities.SetAttachment(customSkin, num2, gunKey, remappedClone2);
		}
		if (repack)
		{
			Skin val = new Skin("repacked skin");
			SkinUtilities.AddAttachments(val, skeleton.Data.DefaultSkin);
			SkinUtilities.AddAttachments(val, customSkin);
			val = AtlasUtilities.GetRepackedSkin(val, "repacked skin", sourceMaterial, ref runtimeMaterial, ref runtimeAtlas, 1024, 2, (TextureFormat)4, false, true);
			skeleton.SetSkin(val);
		}
		else
		{
			skeleton.SetSkin(customSkin);
		}
		skeleton.SetToSetupPose();
		component.Update(0f);
		component.OverrideTexture = (Texture)(object)runtimeAtlas;
		Resources.UnloadUnusedAssets();
	}
}
