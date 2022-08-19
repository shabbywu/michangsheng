using System;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AF8 RID: 2808
	public static class SpriteAttachmentExtensions
	{
		// Token: 0x06004E53 RID: 20051 RVA: 0x00216482 File Offset: 0x00214682
		[Obsolete]
		public static RegionAttachment AttachUnitySprite(this Skeleton skeleton, string slotName, Sprite sprite, string shaderName = "Spine/Skeleton", bool applyPMA = true, float rotation = 0f)
		{
			return skeleton.AttachUnitySprite(slotName, sprite, Shader.Find(shaderName), applyPMA, rotation);
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x00216496 File Offset: 0x00214696
		[Obsolete]
		public static RegionAttachment AddUnitySprite(this SkeletonData skeletonData, string slotName, Sprite sprite, string skinName = "", string shaderName = "Spine/Skeleton", bool applyPMA = true, float rotation = 0f)
		{
			return skeletonData.AddUnitySprite(slotName, sprite, skinName, Shader.Find(shaderName), applyPMA, rotation);
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x002164AC File Offset: 0x002146AC
		[Obsolete]
		public static RegionAttachment AttachUnitySprite(this Skeleton skeleton, string slotName, Sprite sprite, Shader shader, bool applyPMA, float rotation = 0f)
		{
			RegionAttachment regionAttachment = applyPMA ? AttachmentRegionExtensions.ToRegionAttachmentPMAClone(sprite, shader, 4, false, null, rotation) : AttachmentRegionExtensions.ToRegionAttachment(sprite, new Material(shader), rotation);
			skeleton.FindSlot(slotName).Attachment = regionAttachment;
			return regionAttachment;
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x002164E8 File Offset: 0x002146E8
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
