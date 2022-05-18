using System;
using System.Collections;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E46 RID: 3654
	public class MixAndMatch : MonoBehaviour
	{
		// Token: 0x060057BE RID: 22462 RVA: 0x00245A5C File Offset: 0x00243C5C
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

		// Token: 0x060057BF RID: 22463 RVA: 0x0003EBEF File Offset: 0x0003CDEF
		private IEnumerator Start()
		{
			yield return new WaitForSeconds(1f);
			this.Apply();
			yield break;
		}

		// Token: 0x060057C0 RID: 22464 RVA: 0x00245AA0 File Offset: 0x00243CA0
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

		// Token: 0x040057B9 RID: 22457
		[SpineSkin("", "", true, false, false)]
		public string templateAttachmentsSkin = "base";

		// Token: 0x040057BA RID: 22458
		public Material sourceMaterial;

		// Token: 0x040057BB RID: 22459
		[Header("Visor")]
		public Sprite visorSprite;

		// Token: 0x040057BC RID: 22460
		[SpineSlot("", "", false, true, false)]
		public string visorSlot;

		// Token: 0x040057BD RID: 22461
		[SpineAttachment(true, false, false, "visorSlot", "", "baseSkinName", true, false)]
		public string visorKey = "goggles";

		// Token: 0x040057BE RID: 22462
		[Header("Gun")]
		public Sprite gunSprite;

		// Token: 0x040057BF RID: 22463
		[SpineSlot("", "", false, true, false)]
		public string gunSlot;

		// Token: 0x040057C0 RID: 22464
		[SpineAttachment(true, false, false, "gunSlot", "", "baseSkinName", true, false)]
		public string gunKey = "gun";

		// Token: 0x040057C1 RID: 22465
		[Header("Runtime Repack")]
		public bool repack = true;

		// Token: 0x040057C2 RID: 22466
		public BoundingBoxFollower bbFollower;

		// Token: 0x040057C3 RID: 22467
		[Header("Do not assign")]
		public Texture2D runtimeAtlas;

		// Token: 0x040057C4 RID: 22468
		public Material runtimeMaterial;

		// Token: 0x040057C5 RID: 22469
		private Skin customSkin;
	}
}
