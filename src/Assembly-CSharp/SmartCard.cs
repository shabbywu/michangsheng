using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200077B RID: 1915
public abstract class SmartCard : MonoBehaviour
{
	// Token: 0x060030D8 RID: 12504 RVA: 0x00023E4F File Offset: 0x0002204F
	private void Start()
	{
		this.computerNotice = base.transform.Find("ComputerNotice").gameObject;
		OrderController.Instance.smartCard += this.AutoDiscardCard;
	}

	// Token: 0x060030D9 RID: 12505 RVA: 0x0018418C File Offset: 0x0018238C
	protected void AutoDiscardCard(bool isNone)
	{
		HandCards component = base.gameObject.GetComponent<HandCards>();
		if (OrderController.Instance.Type == component.cType)
		{
			base.StartCoroutine(this.DelayDiscardCard(isNone));
		}
	}

	// Token: 0x060030DA RID: 12506
	public abstract List<Card> FirstCard();

	// Token: 0x060030DB RID: 12507 RVA: 0x00023EA3 File Offset: 0x000220A3
	public virtual IEnumerator DelayDiscardCard(bool isNone)
	{
		yield return new WaitForSeconds(1f);
		CardsType cardsType = isNone ? CardsType.None : DeskCardsCache.Instance.Rule;
		int totalWeight = DeskCardsCache.Instance.TotalWeight;
		switch (cardsType)
		{
		case CardsType.None:
		{
			List<Card> list = this.FirstCard();
			if (list.Count != 0)
			{
				this.RemoveCards(list);
				this.DiscardCards(list, this.GetSprite(list));
			}
			break;
		}
		case CardsType.JokerBoom:
			OrderController.Instance.Turn();
			this.ShowNotice();
			break;
		case CardsType.Boom:
		{
			List<Card> list2 = this.FindBoom(this.GetAllCards(null), totalWeight, false);
			if (list2.Count != 0)
			{
				GameController component = GameObject.Find("GameController").GetComponent<GameController>();
				if (list2.Count == 4)
				{
					component.Multiples = 2;
				}
				else if (list2.Count == 2)
				{
					component.Multiples = 4;
				}
				this.RemoveCards(list2);
				this.DiscardCards(list2, this.GetSprite(list2));
			}
			else
			{
				OrderController.Instance.Turn();
				this.ShowNotice();
			}
			break;
		}
		case CardsType.OnlyThree:
		{
			List<Card> list3 = this.FindOnlyThree(this.GetAllCards(null), totalWeight, false);
			if (list3.Count != 0)
			{
				this.RemoveCards(list3);
				this.DiscardCards(list3, this.GetSprite(list3));
			}
			else
			{
				OrderController.Instance.Turn();
				this.ShowNotice();
			}
			break;
		}
		case CardsType.ThreeAndOne:
		{
			List<Card> list4 = this.FindThreeAndOne(this.GetAllCards(null), totalWeight, false);
			if (list4.Count != 0)
			{
				this.RemoveCards(list4);
				this.DiscardCards(list4, this.GetSprite(list4));
			}
			else
			{
				OrderController.Instance.Turn();
				this.ShowNotice();
			}
			break;
		}
		case CardsType.ThreeAndTwo:
		{
			List<Card> list5 = this.FindThreeAndTwo(this.GetAllCards(null), totalWeight, false);
			if (list5.Count != 0)
			{
				this.RemoveCards(list5);
				this.DiscardCards(list5, this.GetSprite(list5));
			}
			else
			{
				OrderController.Instance.Turn();
				this.ShowNotice();
			}
			break;
		}
		case CardsType.Straight:
		{
			List<Card> list6 = this.FindStraight(this.GetAllCards(null), DeskCardsCache.Instance.MinWeight, DeskCardsCache.Instance.CardsCount, false);
			if (list6.Count != 0)
			{
				this.RemoveCards(list6);
				this.DiscardCards(list6, this.GetSprite(list6));
			}
			else
			{
				List<Card> list7 = this.FindBoom(this.GetAllCards(null), 0, true);
				if (list7.Count != 0)
				{
					this.RemoveCards(list7);
					this.DiscardCards(list7, this.GetSprite(list7));
				}
				else
				{
					OrderController.Instance.Turn();
					this.ShowNotice();
				}
			}
			break;
		}
		case CardsType.DoubleStraight:
		{
			List<Card> list8 = this.FindStraight(this.GetAllCards(null), DeskCardsCache.Instance.MinWeight, DeskCardsCache.Instance.CardsCount, false);
			if (list8.Count != 0)
			{
				this.RemoveCards(list8);
				this.DiscardCards(list8, this.GetSprite(list8));
			}
			else
			{
				List<Card> list9 = this.FindBoom(this.GetAllCards(null), 0, true);
				if (list9.Count != 0)
				{
					this.RemoveCards(list9);
					this.DiscardCards(list9, this.GetSprite(list9));
				}
				else
				{
					OrderController.Instance.Turn();
					this.ShowNotice();
				}
			}
			break;
		}
		case CardsType.TripleStraight:
		{
			List<Card> list10 = this.FindBoom(this.GetAllCards(null), 0, true);
			if (list10.Count != 0)
			{
				this.RemoveCards(list10);
				this.DiscardCards(list10, this.GetSprite(list10));
			}
			else
			{
				OrderController.Instance.Turn();
				this.ShowNotice();
			}
			break;
		}
		case CardsType.Double:
		{
			List<Card> list11 = this.FindDouble(this.GetAllCards(null), totalWeight, false);
			if (list11.Count != 0)
			{
				this.RemoveCards(list11);
				this.DiscardCards(list11, this.GetSprite(list11));
			}
			else
			{
				OrderController.Instance.Turn();
				this.ShowNotice();
			}
			break;
		}
		case CardsType.Single:
		{
			List<Card> list12 = this.FindSingle(this.GetAllCards(null), totalWeight, false);
			if (list12.Count != 0)
			{
				this.RemoveCards(list12);
				this.DiscardCards(list12, this.GetSprite(list12));
			}
			else
			{
				OrderController.Instance.Turn();
				this.ShowNotice();
			}
			break;
		}
		}
		yield break;
	}

	// Token: 0x060030DC RID: 12508 RVA: 0x001841C8 File Offset: 0x001823C8
	protected void DiscardCards(List<Card> selectedCardsList, List<CardSprite> selectedSpriteList)
	{
		CardsType rule;
		if (CardRules.PopEnable(selectedCardsList.ToArray(), out rule))
		{
			HandCards component = base.gameObject.GetComponent<HandCards>();
			DeskCardsCache.Instance.Clear();
			DeskCardsCache.Instance.Rule = rule;
			for (int i = 0; i < selectedSpriteList.Count; i++)
			{
				DeskCardsCache.Instance.AddCard(selectedSpriteList[i].Poker);
				selectedSpriteList[i].transform.parent = GameObject.Find("Desk").transform;
				selectedSpriteList[i].Poker = selectedSpriteList[i].Poker;
			}
			DeskCardsCache.Instance.Sort();
			GameController.AdjustCardSpritsPosition(CharacterType.Desk);
			GameController.AdjustCardSpritsPosition(component.cType);
			GameController.UpdateLeftCardsCount(component.cType, component.CardsCount);
			if (component.CardsCount == 0)
			{
				GameObject.Find("GameController").GetComponent<GameController>().GameOver();
				return;
			}
			OrderController.Instance.Biggest = component.cType;
			OrderController.Instance.Turn();
		}
	}

	// Token: 0x060030DD RID: 12509 RVA: 0x001842CC File Offset: 0x001824CC
	protected List<Card> GetAllCards(List<Card> exclude = null)
	{
		List<Card> list = new List<Card>();
		HandCards component = base.gameObject.GetComponent<HandCards>();
		for (int i = 0; i < component.CardsCount; i++)
		{
			bool flag = false;
			if (exclude != null)
			{
				for (int j = 0; j < exclude.Count; j++)
				{
					if (component[i] == exclude[j])
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				list.Add(component[i]);
			}
		}
		CardRules.SortCards(list, true);
		return list;
	}

	// Token: 0x060030DE RID: 12510 RVA: 0x00184348 File Offset: 0x00182548
	protected List<CardSprite> GetSprite(List<Card> cards)
	{
		CardSprite[] componentsInChildren = GameObject.Find(base.gameObject.GetComponent<HandCards>().cType.ToString()).GetComponentsInChildren<CardSprite>();
		List<CardSprite> list = new List<CardSprite>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			for (int j = 0; j < cards.Count; j++)
			{
				if (cards[j] == componentsInChildren[i].Poker)
				{
					list.Add(componentsInChildren[i]);
					break;
				}
			}
		}
		return list;
	}

	// Token: 0x060030DF RID: 12511 RVA: 0x001843C0 File Offset: 0x001825C0
	protected void RemoveCards(List<Card> cards)
	{
		HandCards component = base.gameObject.GetComponent<HandCards>();
		for (int i = 0; i < cards.Count; i++)
		{
			for (int j = 0; j < component.CardsCount; j++)
			{
				if (cards[i] == component[j])
				{
					component.PopCard(cards[i]);
					break;
				}
			}
		}
	}

	// Token: 0x060030E0 RID: 12512 RVA: 0x0018441C File Offset: 0x0018261C
	protected List<Card> FindBoom(List<Card> allCards, int weight, bool equal)
	{
		List<Card> list = new List<Card>();
		for (int i = 0; i < allCards.Count; i++)
		{
			if (i <= allCards.Count - 4 && allCards[i].GetCardWeight == allCards[i + 1].GetCardWeight && allCards[i].GetCardWeight == allCards[i + 2].GetCardWeight && allCards[i].GetCardWeight == allCards[i + 3].GetCardWeight)
			{
				int num = (int)(allCards[i].GetCardWeight + (int)allCards[i + 1].GetCardWeight + (int)allCards[i + 2].GetCardWeight + (int)allCards[i + 4].GetCardWeight);
				if (equal)
				{
					if (num >= weight)
					{
						list.Add(allCards[i]);
						list.Add(allCards[i + 1]);
						list.Add(allCards[i + 2]);
						list.Add(allCards[i + 3]);
						break;
					}
				}
				else if (num > weight)
				{
					list.Add(allCards[i]);
					list.Add(allCards[i + 1]);
					list.Add(allCards[i + 2]);
					list.Add(allCards[i + 3]);
					break;
				}
			}
		}
		if (list.Count == 0)
		{
			for (int j = 0; j < allCards.Count; j++)
			{
				if (j < allCards.Count - 1 && allCards[j].GetCardWeight == Weight.SJoker && allCards[j + 1].GetCardWeight == Weight.LJoker)
				{
					list.Add(allCards[j]);
					list.Add(allCards[j + 1]);
				}
			}
		}
		return list;
	}

	// Token: 0x060030E1 RID: 12513 RVA: 0x001845D4 File Offset: 0x001827D4
	protected List<Card> FindDouble(List<Card> allCards, int weight, bool equal)
	{
		List<Card> list = new List<Card>();
		for (int i = 0; i < allCards.Count; i++)
		{
			if (i < allCards.Count - 1 && allCards[i].GetCardWeight == allCards[i + 1].GetCardWeight)
			{
				int num = (int)(allCards[i].GetCardWeight + (int)allCards[i + 1].GetCardWeight);
				if (equal)
				{
					if (num >= weight)
					{
						list.Add(allCards[i]);
						list.Add(allCards[i + 1]);
						break;
					}
				}
				else if (num > weight)
				{
					list.Add(allCards[i]);
					list.Add(allCards[i + 1]);
					break;
				}
			}
		}
		return list;
	}

	// Token: 0x060030E2 RID: 12514 RVA: 0x0018468C File Offset: 0x0018288C
	protected List<Card> FindSingle(List<Card> allCards, int weight, bool equal)
	{
		List<Card> list = new List<Card>();
		for (int i = 0; i < allCards.Count; i++)
		{
			if (equal)
			{
				if (allCards[i].GetCardWeight >= (Weight)weight)
				{
					list.Add(allCards[i]);
					break;
				}
			}
			else if (allCards[i].GetCardWeight > (Weight)weight)
			{
				list.Add(allCards[i]);
				break;
			}
		}
		return list;
	}

	// Token: 0x060030E3 RID: 12515 RVA: 0x001846F0 File Offset: 0x001828F0
	protected List<Card> FindOnlyThree(List<Card> allCards, int weight, bool equal)
	{
		List<Card> list = new List<Card>();
		for (int i = 0; i < allCards.Count; i++)
		{
			if (i <= allCards.Count - 3 && allCards[i].GetCardWeight == allCards[i + 1].GetCardWeight && allCards[i].GetCardWeight == allCards[i + 2].GetCardWeight)
			{
				int num = (int)(allCards[i].GetCardWeight + (int)allCards[i + 1].GetCardWeight + (int)allCards[i + 2].GetCardWeight);
				if (equal)
				{
					if (num >= weight)
					{
						list.Add(allCards[i]);
						list.Add(allCards[i + 1]);
						list.Add(allCards[i + 2]);
						break;
					}
				}
				else if (num > weight)
				{
					list.Add(allCards[i]);
					list.Add(allCards[i + 1]);
					list.Add(allCards[i + 2]);
					break;
				}
			}
		}
		return list;
	}

	// Token: 0x060030E4 RID: 12516 RVA: 0x001847F8 File Offset: 0x001829F8
	protected List<Card> FindStraight(List<Card> allCards, int minWeight, int length, bool equal)
	{
		List<Card> list = new List<Card>();
		int num = 1;
		List<int> list2 = new List<int>();
		for (int i = 0; i < allCards.Count; i++)
		{
			if (i < allCards.Count - 4)
			{
				int getCardWeight = (int)allCards[i].GetCardWeight;
				if (equal)
				{
					if (getCardWeight >= minWeight)
					{
						num = 1;
						list2.Clear();
						for (int j = i + 1; j < allCards.Count; j++)
						{
							if (allCards[j].GetCardWeight > Weight.One)
							{
								break;
							}
							if (allCards[j].GetCardWeight - (Weight)getCardWeight == num)
							{
								num++;
								list2.Add(j);
							}
							if (num == length)
							{
								break;
							}
						}
					}
				}
				else if (getCardWeight > minWeight)
				{
					num = 1;
					list2.Clear();
					int num2 = i + 1;
					while (num2 < allCards.Count && allCards[num2].GetCardWeight <= Weight.One)
					{
						if (allCards[num2].GetCardWeight - (Weight)getCardWeight == num)
						{
							num++;
							list2.Add(num2);
						}
						if (num == length)
						{
							break;
						}
						num2++;
					}
				}
			}
			if (num == length)
			{
				list2.Insert(0, i);
				break;
			}
		}
		if (num == length)
		{
			for (int k = 0; k < list2.Count; k++)
			{
				list.Add(allCards[list2[k]]);
			}
		}
		return list;
	}

	// Token: 0x060030E5 RID: 12517 RVA: 0x00184940 File Offset: 0x00182B40
	protected List<Card> FindDoubleStraight(List<Card> allCards, int minWeight, int length)
	{
		List<Card> list = new List<Card>();
		int num = 0;
		List<int> list2 = new List<int>();
		for (int i = 0; i < allCards.Count; i++)
		{
			if (i < allCards.Count - 4)
			{
				int getCardWeight = (int)allCards[i].GetCardWeight;
				if (getCardWeight > minWeight)
				{
					num = 0;
					list2.Clear();
					int num2 = 0;
					int num3 = i + 1;
					while (num3 < allCards.Count && allCards[num3].GetCardWeight <= Weight.One)
					{
						if (allCards[num3].GetCardWeight - (Weight)getCardWeight == num)
						{
							num2++;
							if (num2 % 2 == 1)
							{
								num++;
							}
							list2.Add(num3);
						}
						if (num == length / 2)
						{
							break;
						}
						num3++;
					}
				}
			}
			if (num == length / 2)
			{
				list2.Insert(0, i);
				break;
			}
		}
		if (num == length / 2)
		{
			for (int j = 0; j < list2.Count; j++)
			{
				list.Add(allCards[list2[j]]);
			}
		}
		return list;
	}

	// Token: 0x060030E6 RID: 12518 RVA: 0x00184A38 File Offset: 0x00182C38
	protected List<Card> FindThreeAndTwo(List<Card> allCards, int weight, bool equal)
	{
		List<Card> list = this.FindOnlyThree(allCards, weight, equal);
		if (list.Count != 0)
		{
			List<Card> allCards2 = this.GetAllCards(list);
			List<Card> collection = this.FindDouble(allCards2, 0, true);
			list.AddRange(collection);
		}
		else
		{
			list.Clear();
		}
		return list;
	}

	// Token: 0x060030E7 RID: 12519 RVA: 0x00184A7C File Offset: 0x00182C7C
	protected List<Card> FindThreeAndOne(List<Card> allCards, int weight, bool equal)
	{
		List<Card> list = this.FindOnlyThree(allCards, weight, equal);
		if (list.Count != 0)
		{
			List<Card> allCards2 = this.GetAllCards(list);
			List<Card> collection = this.FindSingle(allCards2, 0, true);
			list.AddRange(collection);
		}
		else
		{
			list.Clear();
		}
		return list;
	}

	// Token: 0x060030E8 RID: 12520 RVA: 0x00184AC0 File Offset: 0x00182CC0
	protected void ShowNotice()
	{
		this.computerNotice.SetActive(true);
		this.computerNotice.GetComponent<TweenAlpha>().ResetToBeginning();
		this.computerNotice.GetComponent<TweenAlpha>().PlayForward();
		base.StartCoroutine(this.DisActiveNotice(this.computerNotice));
	}

	// Token: 0x060030E9 RID: 12521 RVA: 0x00023EB9 File Offset: 0x000220B9
	protected IEnumerator DisActiveNotice(GameObject notice)
	{
		yield return new WaitForSeconds(2f);
		this.computerNotice.SetActive(false);
		yield break;
	}

	// Token: 0x04002CCA RID: 11466
	protected GameObject computerNotice;
}
