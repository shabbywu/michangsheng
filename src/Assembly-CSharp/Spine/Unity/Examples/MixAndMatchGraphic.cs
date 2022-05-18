using System;
using System.Collections;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E48 RID: 3656
	public class MixAndMatchGraphic : MonoBehaviour
	{
		// Token: 0x060057C8 RID: 22472 RVA: 0x00245C64 File Offset: 0x00243E64
		private void OnValidate()
		{
			if (this.sourceMaterial == null)
			{
				SkeletonGraphic component = base.GetComponent<SkeletonGraphic>();
				if (component != null)
				{
					this.sourceMaterial = component.SkeletonDataAsset.atlasAssets[0].PrimaryMaterial;
				}
			}
		}

		// Token: 0x060057C9 RID: 22473 RVA: 0x0003EC45 File Offset: 0x0003CE45
		private IEnumerator Start()
		{
			yield return new WaitForSeconds(1f);
			this.Apply();
			yield break;
		}

		// Token: 0x060057CA RID: 22474 RVA: 0x00245CA8 File Offset: 0x00243EA8
		[ContextMenu("Apply")]
		private void Apply()
		{
			SkeletonGraphic component = base.GetComponent<SkeletonGraphic>();
			Skeleton skeleton = component.Skeleton;
			this.customSkin = (this.customSkin ?? new Skin("custom skin"));
			Skin skin = skeleton.Data.FindSkin(this.baseSkinName);
			int num = skeleton.FindSlotIndex(this.visorSlot);
			Attachment remappedClone = AttachmentCloneExtensions.GetRemappedClone(skin.GetAttachment(num, this.visorKey), this.visorSprite, this.sourceMaterial, true, true, false);
			SkinUtilities.SetAttachment(this.customSkin, num, this.visorKey, remappedClone);
			int num2 = skeleton.FindSlotIndex(this.gunSlot);
			Attachment remappedClone2 = AttachmentCloneExtensions.GetRemappedClone(skin.GetAttachment(num2, this.gunKey), this.gunSprite, this.sourceMaterial, true, true, false);
			if (remappedClone2 != null)
			{
				SkinUtilities.SetAttachment(this.customSkin, num2, this.gunKey, remappedClone2);
			}
			if (this.repack)
			{
				Skin skin2 = new Skin("repacked skin");
				SkinUtilities.AddAttachments(skin2, skeleton.Data.DefaultSkin);
				SkinUtilities.AddAttachments(skin2, this.customSkin);
				skin2 = AtlasUtilities.GetRepackedSkin(skin2, "repacked skin", this.sourceMaterial, ref this.runtimeMaterial, ref this.runtimeAtlas, 1024, 2, 4, false, true);
				skeleton.SetSkin(skin2);
			}
			else
			{
				skeleton.SetSkin(this.customSkin);
			}
			skeleton.SetToSetupPose();
			component.Update(0f);
			component.OverrideTexture = this.runtimeAtlas;
			Resources.UnloadUnusedAssets();
		}

		// Token: 0x040057C9 RID: 22473
		[SpineSkin("", "", true, false, false)]
		public string baseSkinName = "base";

		// Token: 0x040057CA RID: 22474
		public Material sourceMaterial;

		// Token: 0x040057CB RID: 22475
		[Header("Visor")]
		public Sprite visorSprite;

		// Token: 0x040057CC RID: 22476
		[SpineSlot("", "", false, true, false)]
		public string visorSlot;

		// Token: 0x040057CD RID: 22477
		[SpineAttachment(true, false, false, "visorSlot", "", "baseSkinName", true, false)]
		public string visorKey = "goggles";

		// Token: 0x040057CE RID: 22478
		[Header("Gun")]
		public Sprite gunSprite;

		// Token: 0x040057CF RID: 22479
		[SpineSlot("", "", false, true, false)]
		public string gunSlot;

		// Token: 0x040057D0 RID: 22480
		[SpineAttachment(true, false, false, "gunSlot", "", "baseSkinName", true, false)]
		public string gunKey = "gun";

		// Token: 0x040057D1 RID: 22481
		[Header("Runtime Repack Required!!")]
		public bool repack = true;

		// Token: 0x040057D2 RID: 22482
		[Header("Do not assign")]
		public Texture2D runtimeAtlas;

		// Token: 0x040057D3 RID: 22483
		public Material runtimeMaterial;

		// Token: 0x040057D4 RID: 22484
		private Skin customSkin;
	}
}
