using System;
using System.Collections.Generic;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E42 RID: 3650
	public class EquipSystemExample : MonoBehaviour, IHasSkeletonDataAsset
	{
		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060057B6 RID: 22454 RVA: 0x0003EBA5 File Offset: 0x0003CDA5
		SkeletonDataAsset IHasSkeletonDataAsset.SkeletonDataAsset
		{
			get
			{
				return this.skeletonDataAsset;
			}
		}

		// Token: 0x060057B7 RID: 22455 RVA: 0x00245980 File Offset: 0x00243B80
		public void Equip(EquipAssetExample asset)
		{
			EquipSystemExample.EquipType equipType = asset.equipType;
			EquipSystemExample.EquipHook equipHook = this.equippables.Find((EquipSystemExample.EquipHook x) => x.type == equipType);
			int slotIndex = this.skeletonDataAsset.GetSkeletonData(true).FindSlotIndex(equipHook.slot);
			Attachment attachment = this.GenerateAttachmentFromEquipAsset(asset, slotIndex, equipHook.templateSkin, equipHook.templateAttachment);
			this.target.Equip(slotIndex, equipHook.templateAttachment, attachment);
		}

		// Token: 0x060057B8 RID: 22456 RVA: 0x002459F8 File Offset: 0x00243BF8
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

		// Token: 0x060057B9 RID: 22457 RVA: 0x0003EBAD File Offset: 0x0003CDAD
		public void Done()
		{
			this.target.OptimizeSkin();
		}

		// Token: 0x040057AB RID: 22443
		public SkeletonDataAsset skeletonDataAsset;

		// Token: 0x040057AC RID: 22444
		public Material sourceMaterial;

		// Token: 0x040057AD RID: 22445
		public bool applyPMA = true;

		// Token: 0x040057AE RID: 22446
		public List<EquipSystemExample.EquipHook> equippables = new List<EquipSystemExample.EquipHook>();

		// Token: 0x040057AF RID: 22447
		public EquipsVisualsComponentExample target;

		// Token: 0x040057B0 RID: 22448
		public Dictionary<EquipAssetExample, Attachment> cachedAttachments = new Dictionary<EquipAssetExample, Attachment>();

		// Token: 0x02000E43 RID: 3651
		[Serializable]
		public class EquipHook
		{
			// Token: 0x040057B1 RID: 22449
			public EquipSystemExample.EquipType type;

			// Token: 0x040057B2 RID: 22450
			[SpineSlot("", "", false, true, false)]
			public string slot;

			// Token: 0x040057B3 RID: 22451
			[SpineSkin("", "", true, false, false)]
			public string templateSkin;

			// Token: 0x040057B4 RID: 22452
			[SpineAttachment(true, false, false, "", "", "templateSkin", true, false)]
			public string templateAttachment;
		}

		// Token: 0x02000E44 RID: 3652
		public enum EquipType
		{
			// Token: 0x040057B6 RID: 22454
			Gun,
			// Token: 0x040057B7 RID: 22455
			Goggles
		}
	}
}
