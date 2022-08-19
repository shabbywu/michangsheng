using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005DA RID: 1498
	[Serializable]
	public class RequiredItem
	{
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06003042 RID: 12354 RVA: 0x0015A3C5 File Offset: 0x001585C5
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06003043 RID: 12355 RVA: 0x0015A3CD File Offset: 0x001585CD
		public int Amount
		{
			get
			{
				return this.m_Amount;
			}
		}

		// Token: 0x04002A83 RID: 10883
		[SerializeField]
		private string m_Name;

		// Token: 0x04002A84 RID: 10884
		[SerializeField]
		private int m_Amount;
	}
}
