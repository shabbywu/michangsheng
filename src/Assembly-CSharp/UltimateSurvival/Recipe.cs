using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005DB RID: 1499
	[Serializable]
	public class Recipe
	{
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06003045 RID: 12357 RVA: 0x0015A3D5 File Offset: 0x001585D5
		public int Duration
		{
			get
			{
				return this.m_Duration;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06003046 RID: 12358 RVA: 0x0015A3DD File Offset: 0x001585DD
		public RequiredItem[] RequiredItems
		{
			get
			{
				return this.m_RequiredItems;
			}
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x00014667 File Offset: 0x00012867
		public static implicit operator bool(Recipe recipe)
		{
			return recipe != null;
		}

		// Token: 0x04002A85 RID: 10885
		[SerializeField]
		[Range(1f, 999f)]
		private int m_Duration = 1;

		// Token: 0x04002A86 RID: 10886
		[SerializeField]
		private RequiredItem[] m_RequiredItems;
	}
}
