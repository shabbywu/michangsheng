using System;
using UnityEngine;

namespace UltimateSurvival.Debugging
{
	// Token: 0x0200065E RID: 1630
	public class StartupItems : MonoBehaviour
	{
		// Token: 0x060033C2 RID: 13250 RVA: 0x0016AE20 File Offset: 0x00169020
		private void Start()
		{
			foreach (StartupItems.ItemToAdd itemToAdd in this.m_InventoryItems)
			{
			}
			foreach (StartupItems.ItemToAdd itemToAdd2 in this.m_HotbarItems)
			{
			}
		}

		// Token: 0x04002DFC RID: 11772
		[SerializeField]
		[Reorderable]
		private ReorderableItemToAddList m_InventoryItems;

		// Token: 0x04002DFD RID: 11773
		[SerializeField]
		[Reorderable]
		private ReorderableItemToAddList m_HotbarItems;

		// Token: 0x020014ED RID: 5357
		[Serializable]
		public class ItemToAdd
		{
			// Token: 0x17000B1D RID: 2845
			// (get) Token: 0x0600826A RID: 33386 RVA: 0x002DB1F0 File Offset: 0x002D93F0
			public string Name
			{
				get
				{
					return this.m_Name;
				}
			}

			// Token: 0x17000B1E RID: 2846
			// (get) Token: 0x0600826B RID: 33387 RVA: 0x002DB1F8 File Offset: 0x002D93F8
			public int Count
			{
				get
				{
					return this.m_Count;
				}
			}

			// Token: 0x04006DC7 RID: 28103
			[SerializeField]
			private string m_Name;

			// Token: 0x04006DC8 RID: 28104
			[SerializeField]
			[Clamp(1f, 9999f)]
			private int m_Count = 1;
		}
	}
}
