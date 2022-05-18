using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008A9 RID: 2217
	[Serializable]
	public class Recipe
	{
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06003911 RID: 14609 RVA: 0x00029786 File Offset: 0x00027986
		public int Duration
		{
			get
			{
				return this.m_Duration;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06003912 RID: 14610 RVA: 0x0002978E File Offset: 0x0002798E
		public RequiredItem[] RequiredItems
		{
			get
			{
				return this.m_RequiredItems;
			}
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x000079B2 File Offset: 0x00005BB2
		public static implicit operator bool(Recipe recipe)
		{
			return recipe != null;
		}

		// Token: 0x04003343 RID: 13123
		[SerializeField]
		[Range(1f, 999f)]
		private int m_Duration = 1;

		// Token: 0x04003344 RID: 13124
		[SerializeField]
		private RequiredItem[] m_RequiredItems;
	}
}
