using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008A8 RID: 2216
	[Serializable]
	public class RequiredItem
	{
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600390E RID: 14606 RVA: 0x00029776 File Offset: 0x00027976
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x0002977E File Offset: 0x0002797E
		public int Amount
		{
			get
			{
				return this.m_Amount;
			}
		}

		// Token: 0x04003341 RID: 13121
		[SerializeField]
		private string m_Name;

		// Token: 0x04003342 RID: 13122
		[SerializeField]
		private int m_Amount;
	}
}
