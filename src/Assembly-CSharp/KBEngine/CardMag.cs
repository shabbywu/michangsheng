using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02000C74 RID: 3188
	public class CardMag
	{
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060057FA RID: 22522 RVA: 0x00248934 File Offset: 0x00246B34
		public int Count
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x060057FB RID: 22523 RVA: 0x00248937 File Offset: 0x00246B37
		public void Clear()
		{
			this._cardlist.Clear();
		}

		// Token: 0x060057FC RID: 22524 RVA: 0x00248944 File Offset: 0x00246B44
		public card addCard(int cardType, int CardNum)
		{
			card card = new card(cardType);
			for (int i = 0; i < CardNum; i++)
			{
				this._cardlist.Add(card);
			}
			MessageMag.Instance.Send("Fight_CardChange", null);
			this.entity.fightTemp.SetAddLingQi(cardType, CardNum);
			return card;
		}

		// Token: 0x060057FD RID: 22525 RVA: 0x00248994 File Offset: 0x00246B94
		public void removeCard(int cardType, int CardNum)
		{
			if (CardNum == 0)
			{
				return;
			}
			int num = 0;
			for (int i = this._cardlist.Count - 1; i >= 0; i--)
			{
				if (this._cardlist[i].cardType == cardType)
				{
					this._cardlist.RemoveAt(i);
					num++;
					if (num == CardNum)
					{
						break;
					}
				}
			}
			MessageMag.Instance.Send("Fight_CardChange", null);
		}

		// Token: 0x060057FE RID: 22526 RVA: 0x002489F7 File Offset: 0x00246BF7
		public void removeCard(card _card)
		{
			MessageMag.Instance.Send("Fight_CardChange", null);
			this._cardlist.Remove(_card);
		}

		// Token: 0x060057FF RID: 22527 RVA: 0x00248A18 File Offset: 0x00246C18
		public card ChengCard(int oldType, int NewType)
		{
			foreach (card card in this._cardlist)
			{
				if (card.cardType == oldType)
				{
					card.cardType = NewType;
					return card;
				}
			}
			return null;
		}

		// Token: 0x06005800 RID: 22528 RVA: 0x00248A7C File Offset: 0x00246C7C
		public card getRandomCard()
		{
			List<card> list = this._cardlist.FindAll((card aa) => aa.cardType < 5);
			if (list.Count > 0)
			{
				return list[jsonData.GetRandom() % list.Count];
			}
			return null;
		}

		// Token: 0x06005801 RID: 22529 RVA: 0x00248AD1 File Offset: 0x00246CD1
		public CardMag(Entity avater)
		{
			this.entity = (Avatar)avater;
		}

		// Token: 0x06005802 RID: 22530 RVA: 0x00248AF0 File Offset: 0x00246CF0
		public int getCardNum()
		{
			return this._cardlist.Count;
		}

		// Token: 0x06005803 RID: 22531 RVA: 0x00248B00 File Offset: 0x00246D00
		public void ListRemoveCard(ref List<int> list32, int _cardType, int Num)
		{
			if (list32[_cardType] >= Num)
			{
				list32[_cardType] -= Num;
				return;
			}
			int num = Num - list32[_cardType];
			if (this.entity.buffmag.HasBuffSeid(59))
			{
				foreach (List<int> list33 in this.entity.buffmag.getBuffBySeid(59))
				{
					JSONObject jsonobject = jsonData.instance.BuffSeidJsonData[59][list33[2]];
					int cardType = (int)jsonobject["value1"].n;
					if ((int)jsonobject["value2"].n == _cardType)
					{
						this.ListRemoveCard(ref list32, cardType, num);
					}
				}
			}
		}

		// Token: 0x06005804 RID: 22532 RVA: 0x00248BE0 File Offset: 0x00246DE0
		public int getCardTypeNum(int cardType)
		{
			return this[cardType];
		}

		// Token: 0x06005805 RID: 22533 RVA: 0x00248BE9 File Offset: 0x00246DE9
		public bool HasNoEnoughNum(int lingQiType, int num)
		{
			return this.getCardTypeNum(lingQiType) < num;
		}

		// Token: 0x06005806 RID: 22534 RVA: 0x00248BF8 File Offset: 0x00246DF8
		public bool HasEnoughNum(int lingQiType, int num)
		{
			return this.getCardTypeNum(lingQiType) >= num;
		}

		// Token: 0x06005807 RID: 22535 RVA: 0x00248C08 File Offset: 0x00246E08
		public List<int> ToListInt32()
		{
			List<int> crystal = new List<int>();
			for (int i = 0; i < 6; i++)
			{
				crystal.Add(0);
			}
			this._cardlist.ForEach(delegate(card a)
			{
				List<int> crystal = crystal;
				int cardType = a.cardType;
				crystal[cardType]++;
			});
			return crystal;
		}

		// Token: 0x1700066F RID: 1647
		public int this[int cardType]
		{
			get
			{
				int num = 0;
				using (List<card>.Enumerator enumerator = this._cardlist.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.cardType == cardType)
						{
							num++;
						}
					}
				}
				return num;
			}
		}

		// Token: 0x040051F8 RID: 20984
		public Avatar entity;

		// Token: 0x040051F9 RID: 20985
		public List<card> _cardlist = new List<card>();
	}
}
