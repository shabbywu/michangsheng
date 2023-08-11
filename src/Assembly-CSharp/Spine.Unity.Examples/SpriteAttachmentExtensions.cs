using System;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples;

public static class SpriteAttachmentExtensions
{
	[Obsolete]
	public static RegionAttachment AttachUnitySprite(this Skeleton skeleton, string slotName, Sprite sprite, string shaderName = "Spine/Skeleton", bool applyPMA = true, float rotation = 0f)
	{
		return skeleton.AttachUnitySprite(slotName, sprite, Shader.Find(shaderName), applyPMA, rotation);
	}

	[Obsolete]
	public static RegionAttachment AddUnitySprite(this SkeletonData skeletonData, string slotName, Sprite sprite, string skinName = "", string shaderName = "Spine/Skeleton", bool applyPMA = true, float rotation = 0f)
	{
		return skeletonData.AddUnitySprite(slotName, sprite, skinName, Shader.Find(shaderName), applyPMA, rotation);
	}

	[Obsolete]
	public static RegionAttachment AttachUnitySprite(this Skeleton skeleton, string slotName, Sprite sprite, Shader shader, bool applyPMA, float rotation = 0f)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		RegionAttachment val = (applyPMA ? AttachmentRegionExtensions.ToRegionAttachmentPMAClone(sprite, shader, (TextureFormat)4, false, (Material)null, rotation) : AttachmentRegionExtensions.ToRegionAttachment(sprite, new Material(shader), rotation));
		skeleton.FindSlot(slotName).Attachment = (Attachment)(object)val;
		return val;
	}

	[Obsolete]
	public static RegionAttachment AddUnitySprite(this SkeletonData skeletonData, string slotName, Sprite sprite, string skinName, Shader shader, bool applyPMA, float rotation = 0f)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		RegionAttachment val = (applyPMA ? AttachmentRegionExtensions.ToRegionAttachmentPMAClone(sprite, shader, (TextureFormat)4, false, (Material)null, rotation) : AttachmentRegionExtensions.ToRegionAttachment(sprite, new Material(shader), rotation));
		int num = skeletonData.FindSlotIndex(slotName);
		Skin val2 = skeletonData.DefaultSkin;
		if (skinName != "")
		{
			val2 = skeletonData.FindSkin(skinName);
		}
		val2.AddAttachment(num, ((Attachment)val).Name, (Attachment)(object)val);
		return val;
	}
}
