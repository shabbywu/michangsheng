using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity.Prototyping
{
	// Token: 0x02000E11 RID: 3601
	public class SkeletonColorInitialize : MonoBehaviour
	{
		// Token: 0x06005704 RID: 22276 RVA: 0x0003E35F File Offset: 0x0003C55F
		private void Start()
		{
			this.ApplySettings();
		}

		// Token: 0x06005705 RID: 22277 RVA: 0x0024377C File Offset: 0x0024197C
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

		// Token: 0x040056B1 RID: 22193
		public Color skeletonColor = Color.white;

		// Token: 0x040056B2 RID: 22194
		public List<SkeletonColorInitialize.SlotSettings> slotSettings = new List<SkeletonColorInitialize.SlotSettings>();

		// Token: 0x02000E12 RID: 3602
		[Serializable]
		public class SlotSettings
		{
			// Token: 0x040056B3 RID: 22195
			[SpineSlot("", "", false, true, false)]
			public string slot = string.Empty;

			// Token: 0x040056B4 RID: 22196
			public Color color = Color.white;
		}
	}
}
