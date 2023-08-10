using System;
using System.Collections.Generic;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples;

public class EquipSystemExample : MonoBehaviour, IHasSkeletonDataAsset
{
	[Serializable]
	public class EquipHook
	{
		public EquipType type;

		[SpineSlot("", "", false, true, false)]
		public string slot;

		[SpineSkin("", "", true, false, false)]
		public string templateSkin;

		[SpineAttachment(true, false, false, "", "", "templateSkin", true, false)]
		public string templateAttachment;
	}

	public enum EquipType
	{
		Gun,
		Goggles
	}

	public SkeletonDataAsset skeletonDataAsset;

	public Material sourceMaterial;

	public bool applyPMA = true;

	public List<EquipHook> equippables = new List<EquipHook>();

	public EquipsVisualsComponentExample target;

	public Dictionary<EquipAssetExample, Attachment> cachedAttachments = new Dictionary<EquipAssetExample, Attachment>();

	SkeletonDataAsset IHasSkeletonDataAsset.SkeletonDataAsset => skeletonDataAsset;

	public void Equip(EquipAssetExample asset)
	{
		EquipType equipType = asset.equipType;
		EquipHook equipHook = equippables.Find((EquipHook x) => x.type == equipType);
		int slotIndex = skeletonDataAsset.GetSkeletonData(true).FindSlotIndex(equipHook.slot);
		Attachment attachment = GenerateAttachmentFromEquipAsset(asset, slotIndex, equipHook.templateSkin, equipHook.templateAttachment);
		target.Equip(slotIndex, equipHook.templateAttachment, attachment);
	}

	private Attachment GenerateAttachmentFromEquipAsset(EquipAssetExample asset, int slotIndex, string templateSkinName, string templateAttachmentName)
	{
		cachedAttachments.TryGetValue(asset, out var value);
		if (value == null)
		{
			value = AttachmentCloneExtensions.GetRemappedClone(skeletonDataAsset.GetSkeletonData(true).FindSkin(templateSkinName).GetAttachment(slotIndex, templateAttachmentName), asset.sprite, sourceMaterial, applyPMA, true, false);
			cachedAttachments.Add(asset, value);
		}
		return value;
	}

	public void Done()
	{
		target.OptimizeSkin();
	}
}
