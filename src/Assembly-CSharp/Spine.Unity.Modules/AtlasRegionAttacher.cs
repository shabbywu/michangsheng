using System;
using System.Collections.Generic;
using Spine.Unity.Modules.AttachmentTools;
using UnityEngine;

namespace Spine.Unity.Modules;

public class AtlasRegionAttacher : MonoBehaviour
{
	[Serializable]
	public class SlotRegionPair
	{
		[SpineSlot("", "", false, true, false)]
		public string slot;

		[SpineAtlasRegion("")]
		public string region;
	}

	[SerializeField]
	protected SpineAtlasAsset atlasAsset;

	[SerializeField]
	protected bool inheritProperties = true;

	[SerializeField]
	protected List<SlotRegionPair> attachments = new List<SlotRegionPair>();

	private Atlas atlas;

	private void Awake()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		SkeletonRenderer component = ((Component)this).GetComponent<SkeletonRenderer>();
		component.OnRebuild += new SkeletonRendererDelegate(Apply);
		if (component.valid)
		{
			Apply(component);
		}
	}

	private void Start()
	{
	}

	private void Apply(SkeletonRenderer skeletonRenderer)
	{
		if (!((Behaviour)this).enabled)
		{
			return;
		}
		atlas = ((AtlasAssetBase)atlasAsset).GetAtlas();
		if (atlas == null)
		{
			return;
		}
		float scale = skeletonRenderer.skeletonDataAsset.scale;
		foreach (SlotRegionPair attachment3 in attachments)
		{
			Slot val = skeletonRenderer.Skeleton.FindSlot(attachment3.slot);
			Attachment attachment = val.Attachment;
			AtlasRegion val2 = atlas.FindRegion(attachment3.region);
			if (val2 == null)
			{
				val.Attachment = null;
				continue;
			}
			if (inheritProperties && attachment != null)
			{
				val.Attachment = AttachmentCloneExtensions.GetRemappedClone(attachment, val2, true, true, scale);
				continue;
			}
			RegionAttachment attachment2 = AttachmentRegionExtensions.ToRegionAttachment(val2, val2.name, scale, 0f);
			val.Attachment = (Attachment)(object)attachment2;
		}
	}
}
