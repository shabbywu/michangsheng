using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000A84 RID: 2692
	public class FightStaticDrawCard : MonoBehaviour
	{
		// Token: 0x06004B7E RID: 19326 RVA: 0x002006BC File Offset: 0x001FE8BC
		public int getCard()
		{
			int result = -1;
			if (this.allUsedCard < this.cardType.Count)
			{
				result = this.cardType[this.allUsedCard];
			}
			this.allUsedCard++;
			return result;
		}

		// Token: 0x04004A99 RID: 19097
		public List<int> cardType = new List<int>();

		// Token: 0x04004A9A RID: 19098
		private int nowGuildIndex;

		// Token: 0x04004A9B RID: 19099
		private int allUsedCard;
	}
}
