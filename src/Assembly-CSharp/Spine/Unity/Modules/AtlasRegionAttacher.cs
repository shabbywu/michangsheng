using System;
using System.Collections.Generic;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Modules
{
	// Token: 0x02000AD4 RID: 2772
	public class AtlasRegionAttacher : MonoBehaviour
	{
		// Token: 0x06004DB9 RID: 19897 RVA: 0x00213A84 File Offset: 0x00211C84
		private void Awake()
		{
			SkeletonRenderer component = base.GetComponent<SkeletonRenderer>();
			component.OnRebuild += new SkeletonRenderer.SkeletonRendererDelegate(this.Apply);
			if (component.valid)
			{
				this.Apply(component);
			}
		}

		// Token: 0x06004DBA RID: 19898 RVA: 0x00004095 File Offset: 0x00002295
		private void Start()
		{
		}

		// Token: 0x06004DBB RID: 19899 RVA: 0x00213ABC File Offset: 0x00211CBC
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

		// Token: 0x04004CDB RID: 19675
		[SerializeField]
		protected SpineAtlasAsset atlasAsset;

		// Token: 0x04004CDC RID: 19676
		[SerializeField]
		protected bool inheritProperties = true;

		// Token: 0x04004CDD RID: 19677
		[SerializeField]
		protected List<AtlasRegionAttacher.SlotRegionPair> attachments = new List<AtlasRegionAttacher.SlotRegionPair>();

		// Token: 0x04004CDE RID: 19678
		private Atlas atlas;

		// Token: 0x020015B9 RID: 5561
		[Serializable]
		public class SlotRegionPair
		{
			// Token: 0x0400703A RID: 28730
			[SpineSlot("", "", false, true, false)]
			public string slot;

			// Token: 0x0400703B RID: 28731
			[SpineAtlasRegion("")]
			public string region;
		}
	}
}
