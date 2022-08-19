using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005D4 RID: 1492
	[Serializable]
	public class ItemCategory
	{
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06003007 RID: 12295 RVA: 0x00159906 File Offset: 0x00157B06
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06003008 RID: 12296 RVA: 0x0015990E File Offset: 0x00157B0E
		public ItemData[] Items
		{
			get
			{
				return this.m_Items;
			}
		}

		// Token: 0x04002A67 RID: 10855
		[SerializeField]
		private string m_Name;

		// Token: 0x04002A68 RID: 10856
		[SerializeField]
		private ItemData[] m_Items;
	}
}
