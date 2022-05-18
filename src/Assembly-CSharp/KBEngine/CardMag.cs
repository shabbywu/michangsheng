using System;
using System.Collections.Generic;

namespace KBEngine
{
	// Token: 0x02001018 RID: 4120
	public class CardMag
	{
		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06006275 RID: 25205 RVA: 0x000442CC File Offset: 0x000424CC
		public int Count
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x06006276 RID: 25206 RVA: 0x000442CF File Offset: 0x000424CF
		public void Clear()
		{
			this._cardlist.Clear();
		}

		// Token: 0x06006277 RID: 25207 RVA: 0x002748C0 File Offset: 0x00272AC0
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

		// Token: 0x06006278 RID: 25208 RVA: 0x00274910 File Offset: 0x00272B10
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

		// Token: 0x06006279 RID: 25209 RVA: 0x000442DC File Offset: 0x000424DC
		public void removeCard(card _card)
		{
			MessageMag.Instance.Send("Fight_CardChange", null);
			this._cardlist.Remove(_card);
		}

		// Token: 0x0600627A RID: 25210 RVA: 0x00274974 File Offset: 0x00272B74
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

		// Token: 0x0600627B RID: 25211 RVA: 0x002749D8 File Offset: 0x00272BD8
		public card getRandomCard()
		{
			List<card> list = this._cardlist.FindAll((card aa) => aa.cardType < 5);
			if (list.Count > 0)
			{
				return list[jsonData.GetRandom() % list.Count];
			}
			return null;
		}

		// Token: 0x0600627C RID: 25212 RVA: 0x000442FB File Offset: 0x000424FB
		public CardMag(Entity avater)
		{
			this.entity = (Avatar)avater;
		}

		// Token: 0x0600627D RID: 25213 RVA: 0x0004431A File Offset: 0x0004251A
		public int getCardNum()
		{
			return this._cardlist.Count;
		}

		// Token: 0x0600627E RID: 25214 RVA: 0x00274A30 File Offset: 0x00272C30
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

		// Token: 0x0600627F RID: 25215 RVA: 0x00044327 File Offset: 0x00042527
		public int getCardTypeNum(int cardType)
		{
			return this[cardType];
		}

		// Token: 0x06006280 RID: 25216 RVA: 0x00044330 File Offset: 0x00042530
		public bool HasNoEnoughNum(int lingQiType, int num)
		{
			return this.getCardTypeNum(lingQiType) < num;
		}

		// Token: 0x06006281 RID: 25217 RVA: 0x0004433F File Offset: 0x0004253F
		public bool HasEnoughNum(int lingQiType, int num)
		{
			return this.getCardTypeNum(lingQiType) >= num;
		}

		// Token: 0x06006282 RID: 25218 RVA: 0x00274B10 File Offset: 0x00272D10
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

		// Token: 0x170008C0 RID: 2240
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

		// Token: 0x04005CE8 RID: 23784
		public Avatar entity;

		// Token: 0x04005CE9 RID: 23785
		public List<card> _cardlist = new List<card>();
	}
}
