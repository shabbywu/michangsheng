using System;
using System.Collections;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AF2 RID: 2802
	public class MixAndMatchGraphic : MonoBehaviour
	{
		// Token: 0x06004E39 RID: 20025 RVA: 0x00215CE0 File Offset: 0x00213EE0
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

		// Token: 0x06004E3A RID: 20026 RVA: 0x00215D23 File Offset: 0x00213F23
		private IEnumerator Start()
		{
			yield return new WaitForSeconds(1f);
			this.Apply();
			yield break;
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x00215D34 File Offset: 0x00213F34
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

		// Token: 0x04004DA7 RID: 19879
		[SpineSkin("", "", true, false, false)]
		public string baseSkinName = "base";

		// Token: 0x04004DA8 RID: 19880
		public Material sourceMaterial;

		// Token: 0x04004DA9 RID: 19881
		[Header("Visor")]
		public Sprite visorSprite;

		// Token: 0x04004DAA RID: 19882
		[SpineSlot("", "", false, true, false)]
		public string visorSlot;

		// Token: 0x04004DAB RID: 19883
		[SpineAttachment(true, false, false, "visorSlot", "", "baseSkinName", true, false)]
		public string visorKey = "goggles";

		// Token: 0x04004DAC RID: 19884
		[Header("Gun")]
		public Sprite gunSprite;

		// Token: 0x04004DAD RID: 19885
		[SpineSlot("", "", false, true, false)]
		public string gunSlot;

		// Token: 0x04004DAE RID: 19886
		[SpineAttachment(true, false, false, "gunSlot", "", "baseSkinName", true, false)]
		public string gunKey = "gun";

		// Token: 0x04004DAF RID: 19887
		[Header("Runtime Repack Required!!")]
		public bool repack = true;

		// Token: 0x04004DB0 RID: 19888
		[Header("Do not assign")]
		public Texture2D runtimeAtlas;

		// Token: 0x04004DB1 RID: 19889
		public Material runtimeMaterial;

		// Token: 0x04004DB2 RID: 19890
		private Skin customSkin;
	}
}
