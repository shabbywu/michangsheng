using System;
using System.Collections.Generic;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AF0 RID: 2800
	public class EquipSystemExample : MonoBehaviour, IHasSkeletonDataAsset
	{
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06004E30 RID: 20016 RVA: 0x002159D8 File Offset: 0x00213BD8
		SkeletonDataAsset IHasSkeletonDataAsset.SkeletonDataAsset
		{
			get
			{
				return this.skeletonDataAsset;
			}
		}

		// Token: 0x06004E31 RID: 20017 RVA: 0x002159E0 File Offset: 0x00213BE0
		public void Equip(EquipAssetExample asset)
		{
			EquipSystemExample.EquipType equipType = asset.equipType;
			EquipSystemExample.EquipHook equipHook = this.equippables.Find((EquipSystemExample.EquipHook x) => x.type == equipType);
			int slotIndex = this.skeletonDataAsset.GetSkeletonData(true).FindSlotIndex(equipHook.slot);
			Attachment attachment = this.GenerateAttachmentFromEquipAsset(asset, slotIndex, equipHook.templateSkin, equipHook.templateAttachment);
			this.target.Equip(slotIndex, equipHook.templateAttachment, attachment);
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x00215A58 File Offset: 0x00213C58
		private Attachment GenerateAttachmentFromEquipAsset(EquipAssetExample asset, int slotIndex, string templateSkinName, string templateAttachmentName)
		{
			Attachment remappedClone;
			this.cachedAttachments.TryGetValue(asset, out remappedClone);
			if (remappedClone == null)
			{
				remappedClone = AttachmentCloneExtensions.GetRemappedClone(this.skeletonDataAsset.GetSkeletonData(true).FindSkin(templateSkinName).GetAttachment(slotIndex, templateAttachmentName), asset.sprite, this.sourceMaterial, this.applyPMA, true, false);
				this.cachedAttachments.Add(asset, remappedClone);
			}
			return remappedClone;
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x00215AB9 File Offset: 0x00213CB9
		public void Done()
		{
			this.target.OptimizeSkin();
		}

		// Token: 0x04004D94 RID: 19860
		public SkeletonDataAsset skeletonDataAsset;

		// Token: 0x04004D95 RID: 19861
		public Material sourceMaterial;

		// Token: 0x04004D96 RID: 19862
		public bool applyPMA = true;

		// Token: 0x04004D97 RID: 19863
		public List<EquipSystemExample.EquipHook> equippables = new List<EquipSystemExample.EquipHook>();

		// Token: 0x04004D98 RID: 19864
		public EquipsVisualsComponentExample target;

		// Token: 0x04004D99 RID: 19865
		public Dictionary<EquipAssetExample, Attachment> cachedAttachments = new Dictionary<EquipAssetExample, Attachment>();

		// Token: 0x020015C8 RID: 5576
		[Serializable]
		public class EquipHook
		{
			// Token: 0x0400706E RID: 28782
			public EquipSystemExample.EquipType type;

			// Token: 0x0400706F RID: 28783
			[SpineSlot("", "", false, true, false)]
			public string slot;

			// Token: 0x04007070 RID: 28784
			[SpineSkin("", "", true, false, false)]
			public string templateSkin;

			// Token: 0x04007071 RID: 28785
			[SpineAttachment(true, false, false, "", "", "templateSkin", true, false)]
			public string templateAttachment;
		}

		// Token: 0x020015C9 RID: 5577
		public enum EquipType
		{
			// Token: 0x04007073 RID: 28787
			Gun,
			// Token: 0x04007074 RID: 28788
			Goggles
		}
	}
}
