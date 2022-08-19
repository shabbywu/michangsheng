using System;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AEF RID: 2799
	public class EquipsVisualsComponentExample : MonoBehaviour
	{
		// Token: 0x06004E2B RID: 20011 RVA: 0x00215858 File Offset: 0x00213A58
		private void Start()
		{
			this.equipsSkin = new Skin("Equips");
			Skin skin = this.skeletonAnimation.Skeleton.Data.FindSkin(this.templateSkinName);
			if (skin != null)
			{
				SkinUtilities.AddAttachments(this.equipsSkin, skin);
			}
			this.skeletonAnimation.Skeleton.Skin = this.equipsSkin;
			this.RefreshSkeletonAttachments();
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x002158BC File Offset: 0x00213ABC
		public void Equip(int slotIndex, string attachmentName, Attachment attachment)
		{
			this.equipsSkin.AddAttachment(slotIndex, attachmentName, attachment);
			this.skeletonAnimation.Skeleton.SetSkin(this.equipsSkin);
			this.RefreshSkeletonAttachments();
		}

		// Token: 0x06004E2D RID: 20013 RVA: 0x002158E8 File Offset: 0x00213AE8
		public void OptimizeSkin()
		{
			this.collectedSkin = (this.collectedSkin ?? new Skin("Collected skin"));
			SkinUtilities.Clear(this.collectedSkin);
			SkinUtilities.AddAttachments(this.collectedSkin, this.skeletonAnimation.Skeleton.Data.DefaultSkin);
			SkinUtilities.AddAttachments(this.collectedSkin, this.equipsSkin);
			Skin repackedSkin = AtlasUtilities.GetRepackedSkin(this.collectedSkin, "Repacked skin", this.skeletonAnimation.SkeletonDataAsset.atlasAssets[0].PrimaryMaterial, ref this.runtimeMaterial, ref this.runtimeAtlas, 1024, 2, 4, false, true);
			SkinUtilities.Clear(this.collectedSkin);
			this.skeletonAnimation.Skeleton.Skin = repackedSkin;
			this.RefreshSkeletonAttachments();
		}

		// Token: 0x06004E2E RID: 20014 RVA: 0x002159AA File Offset: 0x00213BAA
		private void RefreshSkeletonAttachments()
		{
			this.skeletonAnimation.Skeleton.SetSlotsToSetupPose();
			this.skeletonAnimation.AnimationState.Apply(this.skeletonAnimation.Skeleton);
		}

		// Token: 0x04004D8E RID: 19854
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x04004D8F RID: 19855
		[SpineSkin("", "", true, false, false)]
		public string templateSkinName;

		// Token: 0x04004D90 RID: 19856
		private Skin equipsSkin;

		// Token: 0x04004D91 RID: 19857
		private Skin collectedSkin;

		// Token: 0x04004D92 RID: 19858
		public Material runtimeMaterial;

		// Token: 0x04004D93 RID: 19859
		public Texture2D runtimeAtlas;
	}
}
