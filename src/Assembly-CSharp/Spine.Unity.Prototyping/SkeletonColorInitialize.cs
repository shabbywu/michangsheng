using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity.Prototyping;

public class SkeletonColorInitialize : MonoBehaviour
{
	[Serializable]
	public class SlotSettings
	{
		[SpineSlot("", "", false, true, false)]
		public string slot = string.Empty;

		public Color color = Color.white;
	}

	public Color skeletonColor = Color.white;

	public List<SlotSettings> slotSettings = new List<SlotSettings>();

	private void Start()
	{
		ApplySettings();
	}

	private void ApplySettings()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		ISkeletonComponent component = ((Component)this).GetComponent<ISkeletonComponent>();
		if (component == null)
		{
			return;
		}
		Skeleton skeleton = component.Skeleton;
		SkeletonExtensions.SetColor(skeleton, skeletonColor);
		foreach (SlotSettings slotSetting in slotSettings)
		{
			Slot val = skeleton.FindSlot(slotSetting.slot);
			if (val != null)
			{
				SkeletonExtensions.SetColor(val, slotSetting.color);
			}
		}
	}
}
