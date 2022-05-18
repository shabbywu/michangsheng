using System;
using System.Collections.Generic;
using UnityEngine;

namespace YSGame
{
	// Token: 0x02000DB6 RID: 3510
	public class FightStaticDrawCard : MonoBehaviour
	{
		// Token: 0x0600549E RID: 21662 RVA: 0x00232110 File Offset: 0x00230310
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

		// Token: 0x04005458 RID: 21592
		public List<int> cardType = new List<int>();

		// Token: 0x04005459 RID: 21593
		private int nowGuildIndex;

		// Token: 0x0400545A RID: 21594
		private int allUsedCard;
	}
}
