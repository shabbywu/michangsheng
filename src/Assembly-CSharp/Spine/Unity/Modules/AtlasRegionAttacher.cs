using System;
using System.Collections.Generic;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Modules
{
	// Token: 0x02000E17 RID: 3607
	public class AtlasRegionAttacher : MonoBehaviour
	{
		// Token: 0x0600570F RID: 22287 RVA: 0x002439D8 File Offset: 0x00241BD8
		private void Awake()
		{
			SkeletonRenderer component = base.GetComponent<SkeletonRenderer>();
			component.OnRebuild += new SkeletonRenderer.SkeletonRendererDelegate(this.Apply);
			if (component.valid)
			{
				this.Apply(component);
			}
		}

		// Token: 0x06005710 RID: 22288 RVA: 0x000042DD File Offset: 0x000024DD
		private void Start()
		{
		}

		// Token: 0x06005711 RID: 22289 RVA: 0x00243A10 File Offset: 0x00241C10
		private void Apply(SkeletonRenderer skeletonRenderer)
		{
			if (!base.enabled)
			{
				return;
			}
			this.atlas = this.atlasAsset.GetAtlas();
			if (this.atlas == null)
			{
				return;
			}
			float scale = skeletonRenderer.skeletonDataAsset.scale;
			foreach (AtlasRegionAttacher.SlotRegionPair slotRegionPair in this.attachments)
			{
				Slot slot = skeletonRenderer.Skeleton.FindSlot(slotRegionPair.slot);
				Attachment attachment = slot.Attachment;
				AtlasRegion atlasRegion = this.atlas.FindRegion(slotRegionPair.region);
				if (atlasRegion == null)
				{
					slot.Attachment = null;
				}
				else if (this.inheritProperties && attachment != null)
				{
					slot.Attachment = AttachmentCloneExtensions.GetRemappedClone(attachment, atlasRegion, true, true, scale);
				}
				else
				{
					RegionAttachment attachment2 = AttachmentRegionExtensions.ToRegionAttachment(atlasRegion, atlasRegion.name, scale, 0f);
					slot.Attachment = attachment2;
				}
			}
		}

		// Token: 0x040056BE RID: 22206
		[SerializeField]
		protected SpineAtlasAsset atlasAsset;

		// Token: 0x040056BF RID: 22207
		[SerializeField]
		protected bool inheritProperties = true;

		// Token: 0x040056C0 RID: 22208
		[SerializeField]
		protected List<AtlasRegionAttacher.SlotRegionPair> attachments = new List<AtlasRegionAttacher.SlotRegionPair>();

		// Token: 0x040056C1 RID: 22209
		private Atlas atlas;

		// Token: 0x02000E18 RID: 3608
		[Serializable]
		public class SlotRegionPair
		{
			// Token: 0x040056C2 RID: 22210
			[SpineSlot("", "", false, true, false)]
			public string slot;

			// Token: 0x040056C3 RID: 22211
			[SpineAtlasRegion("")]
			public string region;
		}
	}
}
