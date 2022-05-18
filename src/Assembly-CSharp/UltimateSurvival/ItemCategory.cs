using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000899 RID: 2201
	[Serializable]
	public class ItemCategory
	{
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060038A3 RID: 14499 RVA: 0x000293DF File Offset: 0x000275DF
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060038A4 RID: 14500 RVA: 0x000293E7 File Offset: 0x000275E7
		public ItemData[] Items
		{
			get
			{
				return this.m_Items;
			}
		}

		// Token: 0x040032FD RID: 13053
		[SerializeField]
		private string m_Name;

		// Token: 0x040032FE RID: 13054
		[SerializeField]
		private ItemData[] m_Items;
	}
}
