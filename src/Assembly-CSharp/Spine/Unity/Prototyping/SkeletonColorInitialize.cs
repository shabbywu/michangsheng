using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity.Prototyping
{
	// Token: 0x02000AD2 RID: 2770
	public class SkeletonColorInitialize : MonoBehaviour
	{
		// Token: 0x06004DB3 RID: 19891 RVA: 0x002137EA File Offset: 0x002119EA
		private void Start()
		{
			this.ApplySettings();
		}

		// Token: 0x06004DB4 RID: 19892 RVA: 0x002137F4 File Offset: 0x002119F4
		private void ApplySettings()
		{
			ISkeletonComponent component = base.GetComponent<ISkeletonComponent>();
			if (component != null)
			{
				Skeleton skeleton = component.Skeleton;
				SkeletonExtensions.SetColor(skeleton, this.skeletonColor);
				foreach (SkeletonColorInitialize.SlotSettings slotSettings in this.slotSettings)
				{
					Slot slot = skeleton.FindSlot(slotSettings.slot);
					if (slot != null)
					{
						SkeletonExtensions.SetColor(slot, slotSettings.color);
					}
				}
			}
		}

		// Token: 0x04004CD6 RID: 19670
		public Color skeletonColor = Color.white;

		// Token: 0x04004CD7 RID: 19671
		public List<SkeletonColorInitialize.SlotSettings> slotSettings = new List<SkeletonColorInitialize.SlotSettings>();

		// Token: 0x020015B5 RID: 5557
		[Serializable]
		public class SlotSettings
		{
			// Token: 0x04007032 RID: 28722
			[SpineSlot("", "", false, true, false)]
			public string slot = string.Empty;

			// Token: 0x04007033 RID: 28723
			public Color color = Color.white;
		}
	}
}
