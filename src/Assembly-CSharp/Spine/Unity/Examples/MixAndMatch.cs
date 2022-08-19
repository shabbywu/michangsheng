using System;
using System.Collections;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AF1 RID: 2801
	public class MixAndMatch : MonoBehaviour
	{
		// Token: 0x06004E35 RID: 20021 RVA: 0x00215AEC File Offset: 0x00213CEC
		private void OnValidate()
		{
			if (this.sourceMaterial == null)
			{
				SkeletonAnimation component = base.GetComponent<SkeletonAnimation>();
				if (component != null)
				{
					this.sourceMaterial = component.SkeletonDataAsset.atlasAssets[0].PrimaryMaterial;
				}
			}
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x00215B2F File Offset: 0x00213D2F
		private IEnumerator Start()
		{
			yield return new WaitForSeconds(1f);
			this.Apply();
			yield break;
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x00215B40 File Offset: 0x00213D40
		private void Apply()
		{
			SkeletonAnimation component = base.GetComponent<SkeletonAnimation>();
			Skeleton skeleton = component.Skeleton;
			this.customSkin = (this.customSkin ?? new Skin("custom skin"));
			Skin skin = skeleton.Data.FindSkin(this.templateAttachmentsSkin);
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
				if (this.bbFollower != null)
				{
					this.bbFollower.Initialize(true);
				}
			}
			else
			{
				skeleton.SetSkin(this.customSkin);
			}
			skeleton.SetSlotsToSetupPose();
			component.Update(0f);
			Resources.UnloadUnusedAssets();
		}

		// Token: 0x04004D9A RID: 19866
		[SpineSkin("", "", true, false, false)]
		public string templateAttachmentsSkin = "base";

		// Token: 0x04004D9B RID: 19867
		public Material sourceMaterial;

		// Token: 0x04004D9C RID: 19868
		[Header("Visor")]
		public Sprite visorSprite;

		// Token: 0x04004D9D RID: 19869
		[SpineSlot("", "", false, true, false)]
		public string visorSlot;

		// Token: 0x04004D9E RID: 19870
		[SpineAttachment(true, false, false, "visorSlot", "", "baseSkinName", true, false)]
		public string visorKey = "goggles";

		// Token: 0x04004D9F RID: 19871
		[Header("Gun")]
		public Sprite gunSprite;

		// Token: 0x04004DA0 RID: 19872
		[SpineSlot("", "", false, true, false)]
		public string gunSlot;

		// Token: 0x04004DA1 RID: 19873
		[SpineAttachment(true, false, false, "gunSlot", "", "baseSkinName", true, false)]
		public string gunKey = "gun";

		// Token: 0x04004DA2 RID: 19874
		[Header("Runtime Repack")]
		public bool repack = true;

		// Token: 0x04004DA3 RID: 19875
		public BoundingBoxFollower bbFollower;

		// Token: 0x04004DA4 RID: 19876
		[Header("Do not assign")]
		public Texture2D runtimeAtlas;

		// Token: 0x04004DA5 RID: 19877
		public Material runtimeMaterial;

		// Token: 0x04004DA6 RID: 19878
		private Skin customSkin;
	}
}
