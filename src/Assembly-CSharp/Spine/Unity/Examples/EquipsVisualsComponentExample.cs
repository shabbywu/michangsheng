using System;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E41 RID: 3649
	public class EquipsVisualsComponentExample : MonoBehaviour
	{
		// Token: 0x060057B1 RID: 22449 RVA: 0x00245858 File Offset: 0x00243A58
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

		// Token: 0x060057B2 RID: 22450 RVA: 0x0003EB4B File Offset: 0x0003CD4B
		public void Equip(int slotIndex, string attachmentName, Attachment attachment)
		{
			this.equipsSkin.AddAttachment(slotIndex, attachmentName, attachment);
			this.skeletonAnimation.Skeleton.SetSkin(this.equipsSkin);
			this.RefreshSkeletonAttachments();
		}

		// Token: 0x060057B3 RID: 22451 RVA: 0x002458BC File Offset: 0x00243ABC
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

		// Token: 0x060057B4 RID: 22452 RVA: 0x0003EB77 File Offset: 0x0003CD77
		private void RefreshSkeletonAttachments()
		{
			this.skeletonAnimation.Skeleton.SetSlotsToSetupPose();
			this.skeletonAnimation.AnimationState.Apply(this.skeletonAnimation.Skeleton);
		}

		// Token: 0x040057A5 RID: 22437
		public SkeletonAnimation skeletonAnimation;

		// Token: 0x040057A6 RID: 22438
		[SpineSkin("", "", true, false, false)]
		public string templateSkinName;

		// Token: 0x040057A7 RID: 22439
		private Skin equipsSkin;

		// Token: 0x040057A8 RID: 22440
		private Skin collectedSkin;

		// Token: 0x040057A9 RID: 22441
		public Material runtimeMaterial;

		// Token: 0x040057AA RID: 22442
		public Texture2D runtimeAtlas;
	}
}
