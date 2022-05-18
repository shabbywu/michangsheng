using System;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E51 RID: 3665
	public static class SpriteAttachmentExtensions
	{
		// Token: 0x060057F4 RID: 22516 RVA: 0x0003EE4E File Offset: 0x0003D04E
		[Obsolete]
		public static RegionAttachment AttachUnitySprite(this Skeleton skeleton, string slotName, Sprite sprite, string shaderName = "Spine/Skeleton", bool applyPMA = true, float rotation = 0f)
		{
			return skeleton.AttachUnitySprite(slotName, sprite, Shader.Find(shaderName), applyPMA, rotation);
		}

		// Token: 0x060057F5 RID: 22517 RVA: 0x0003EE62 File Offset: 0x0003D062
		[Obsolete]
		public static RegionAttachment AddUnitySprite(this SkeletonData skeletonData, string slotName, Sprite sprite, string skinName = "", string shaderName = "Spine/Skeleton", bool applyPMA = true, float rotation = 0f)
		{
			return skeletonData.AddUnitySprite(slotName, sprite, skinName, Shader.Find(shaderName), applyPMA, rotation);
		}

		// Token: 0x060057F6 RID: 22518 RVA: 0x00246460 File Offset: 0x00244660
		[Obsolete]
		public static RegionAttachment AttachUnitySprite(this Skeleton skeleton, string slotName, Sprite sprite, Shader shader, bool applyPMA, float rotation = 0f)
		{
			RegionAttachment regionAttachment = applyPMA ? AttachmentRegionExtensions.ToRegionAttachmentPMAClone(sprite, shader, 4, false, null, rotation) : AttachmentRegionExtensions.ToRegionAttachment(sprite, new Material(shader), rotation);
			skeleton.FindSlot(slotName).Attachment = regionAttachment;
			return regionAttachment;
		}

		// Token: 0x060057F7 RID: 22519 RVA: 0x0024649C File Offset: 0x0024469C
		[Obsolete]
		public static RegionAttachment AddUnitySprite(this SkeletonData skeletonData, string slotName, Sprite sprite, string skinName, Shader shader, bool applyPMA, float rotation = 0f)
		{
			RegionAttachment regionAttachment = applyPMA ? AttachmentRegionExtensions.ToRegionAttachmentPMAClone(sprite, shader, 4, false, null, rotation) : AttachmentRegionExtensions.ToRegionAttachment(sprite, new Material(shader), rotation);
			int num = skeletonData.FindSlotIndex(slotName);
			Skin skin = skeletonData.DefaultSkin;
			if (skinName != "")
			{
				skin = skeletonData.FindSkin(skinName);
			}
			skin.AddAttachment(num, regionAttachment.Name, regionAttachment);
			return regionAttachment;
		}
	}
}
