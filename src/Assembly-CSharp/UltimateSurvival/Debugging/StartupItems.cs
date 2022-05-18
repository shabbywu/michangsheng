using System;
using UnityEngine;

namespace UltimateSurvival.Debugging
{
	// Token: 0x02000961 RID: 2401
	public class StartupItems : MonoBehaviour
	{
		// Token: 0x06003D50 RID: 15696 RVA: 0x001B3D04 File Offset: 0x001B1F04
		private void Start()
		{
			foreach (StartupItems.ItemToAdd itemToAdd in this.m_InventoryItems)
			{
			}
			foreach (StartupItems.ItemToAdd itemToAdd2 in this.m_HotbarItems)
			{
			}
		}

		// Token: 0x04003781 RID: 14209
		[SerializeField]
		[Reorderable]
		private ReorderableItemToAddList m_InventoryItems;

		// Token: 0x04003782 RID: 14210
		[SerializeField]
		[Reorderable]
		private ReorderableItemToAddList m_HotbarItems;

		// Token: 0x02000962 RID: 2402
		[Serializable]
		public class ItemToAdd
		{
			// Token: 0x170006A7 RID: 1703
			// (get) Token: 0x06003D52 RID: 15698 RVA: 0x0002C380 File Offset: 0x0002A580
			public string Name
			{
				get
				{
					return this.m_Name;
				}
			}

			// Token: 0x170006A8 RID: 1704
			// (get) Token: 0x06003D53 RID: 15699 RVA: 0x0002C388 File Offset: 0x0002A588
			public int Count
			{
				get
				{
					return this.m_Count;
				}
			}

			// Token: 0x04003783 RID: 14211
			[SerializeField]
			private string m_Name;

			// Token: 0x04003784 RID: 14212
			[SerializeField]
			[Clamp(1f, 9999f)]
			private int m_Count = 1;
		}
	}
}
