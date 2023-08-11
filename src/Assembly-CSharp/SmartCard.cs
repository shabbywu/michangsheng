using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SmartCard : MonoBehaviour
{
	protected GameObject computerNotice;

	private void Start()
	{
		computerNotice = ((Component)((Component)this).transform.Find("ComputerNotice")).gameObject;
		OrderController.Instance.smartCard += AutoDiscardCard;
	}

	protected void AutoDiscardCard(bool isNone)
	{
		HandCards component = ((Component)this).gameObject.GetComponent<HandCards>();
		if (OrderController.Instance.Type == component.cType)
		{
			((MonoBehaviour)this).StartCoroutine(DelayDiscardCard(isNone));
		}
	}

	public abstract List<Card> FirstCard();

	public virtual IEnumerator DelayDiscardCard(bool isNone)
	{
		yield return (object)new WaitForSeconds(1f);
		CardsType cardsType = ((!isNone) ? DeskCardsCache.Instance.Rule : CardsType.None);
		int totalWeight = DeskCardsCache.Instance.TotalWeight;
		switch (cardsType)
		{
		case CardsType.None:
		{
			List<Card> list11 = FirstCard();
			if (list11.Count != 0)
			{
				RemoveCards(list11);
				DiscardCards(list11, GetSprite(list11));
			}
			break;
		}
		case CardsType.JokerBoom:
			OrderController.Instance.Turn();
			ShowNotice();
			break;
		case CardsType.Boom:
		{
			List<Card> list12 = FindBoom(GetAllCards(), totalWeight, equal: false);
			if (list12.Count != 0)
			{
				GameController component = GameObject.Find("GameController").GetComponent<GameController>();
				if (list12.Count == 4)
				{
					component.Multiples = 2;
				}
				else if (list12.Count == 2)
				{
					component.Multiples = 4;
				}
				RemoveCards(list12);
				DiscardCards(list12, GetSprite(list12));
			}
			else
			{
				OrderController.Instance.Turn();
				ShowNotice();
			}
			break;
		}
		case CardsType.Double:
		{
			List<Card> list6 = FindDouble(GetAllCards(), totalWeight, equal: false);
			if (list6.Count != 0)
			{
				RemoveCards(list6);
				DiscardCards(list6, GetSprite(list6));
			}
			else
			{
				OrderController.Instance.Turn();
				ShowNotice();
			}
			break;
		}
		case CardsType.Single:
		{
			List<Card> list9 = FindSingle(GetAllCards(), totalWeight, equal: false);
			if (list9.Count != 0)
			{
				RemoveCards(list9);
				DiscardCards(list9, GetSprite(list9));
			}
			else
			{
				OrderController.Instance.Turn();
				ShowNotice();
			}
			break;
		}
		case CardsType.OnlyThree:
		{
			List<Card> list3 = FindOnlyThree(GetAllCards(), totalWeight, equal: false);
			if (list3.Count != 0)
			{
				RemoveCards(list3);
				DiscardCards(list3, GetSprite(list3));
			}
			else
			{
				OrderController.Instance.Turn();
				ShowNotice();
			}
			break;
		}
		case CardsType.Straight:
		{
			List<Card> list7 = FindStraight(GetAllCards(), DeskCardsCache.Instance.MinWeight, DeskCardsCache.Instance.CardsCount, equal: false);
			if (list7.Count != 0)
			{
				RemoveCards(list7);
				DiscardCards(list7, GetSprite(list7));
				break;
			}
			List<Card> list8 = FindBoom(GetAllCards(), 0, equal: true);
			if (list8.Count != 0)
			{
				RemoveCards(list8);
				DiscardCards(list8, GetSprite(list8));
			}
			else
			{
				OrderController.Instance.Turn();
				ShowNotice();
			}
			break;
		}
		case CardsType.ThreeAndOne:
		{
			List<Card> list2 = FindThreeAndOne(GetAllCards(), totalWeight, equal: false);
			if (list2.Count != 0)
			{
				RemoveCards(list2);
				DiscardCards(list2, GetSprite(list2));
			}
			else
			{
				OrderController.Instance.Turn();
				ShowNotice();
			}
			break;
		}
		case CardsType.ThreeAndTwo:
		{
			List<Card> list10 = FindThreeAndTwo(GetAllCards(), totalWeight, equal: false);
			if (list10.Count != 0)
			{
				RemoveCards(list10);
				DiscardCards(list10, GetSprite(list10));
			}
			else
			{
				OrderController.Instance.Turn();
				ShowNotice();
			}
			break;
		}
		case CardsType.DoubleStraight:
		{
			List<Card> list4 = FindStraight(GetAllCards(), DeskCardsCache.Instance.MinWeight, DeskCardsCache.Instance.CardsCount, equal: false);
			if (list4.Count != 0)
			{
				RemoveCards(list4);
				DiscardCards(list4, GetSprite(list4));
				break;
			}
			List<Card> list5 = FindBoom(GetAllCards(), 0, equal: true);
			if (list5.Count != 0)
			{
				RemoveCards(list5);
				DiscardCards(list5, GetSprite(list5));
			}
			else
			{
				OrderController.Instance.Turn();
				ShowNotice();
			}
			break;
		}
		case CardsType.TripleStraight:
		{
			List<Card> list = FindBoom(GetAllCards(), 0, equal: true);
			if (list.Count != 0)
			{
				RemoveCards(list);
				DiscardCards(list, GetSprite(list));
			}
			else
			{
				OrderController.Instance.Turn();
				ShowNotice();
			}
			break;
		}
		}
	}

	protected void DiscardCards(List<Card> selectedCardsList, List<CardSprite> selectedSpriteList)
	{
		if (CardRules.PopEnable(selectedCardsList.ToArray(), out var type))
		{
			HandCards component = ((Component)this).gameObject.GetComponent<HandCards>();
			DeskCardsCache.Instance.Clear();
			DeskCardsCache.Instance.Rule = type;
			for (int i = 0; i < selectedSpriteList.Count; i++)
			{
				DeskCardsCache.Instance.AddCard(selectedSpriteList[i].Poker);
				((Component)selectedSpriteList[i]).transform.parent = GameObject.Find("Desk").transform;
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

	protected List<Card> GetAllCards(List<Card> exclude = null)
	{
		List<Card> list = new List<Card>();
		HandCards component = ((Component)this).gameObject.GetComponent<HandCards>();
		bool flag = false;
		for (int i = 0; i < component.CardsCount; i++)
		{
			flag = false;
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
		CardRules.SortCards(list, ascending: true);
		return list;
	}

	protected List<CardSprite> GetSprite(List<Card> cards)
	{
		CardSprite[] componentsInChildren = GameObject.Find(((Component)this).gameObject.GetComponent<HandCards>().cType.ToString()).GetComponentsInChildren<CardSprite>();
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

	protected void RemoveCards(List<Card> cards)
	{
		HandCards component = ((Component)this).gameObject.GetComponent<HandCards>();
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

	protected List<Card> FindBoom(List<Card> allCards, int weight, bool equal)
	{
		List<Card> list = new List<Card>();
		for (int i = 0; i < allCards.Count; i++)
		{
			if (i > allCards.Count - 4 || allCards[i].GetCardWeight != allCards[i + 1].GetCardWeight || allCards[i].GetCardWeight != allCards[i + 2].GetCardWeight || allCards[i].GetCardWeight != allCards[i + 3].GetCardWeight)
			{
				continue;
			}
			int num = (int)((int)allCards[i].GetCardWeight + (int)allCards[i + 1].GetCardWeight + allCards[i + 2].GetCardWeight) + (int)allCards[i + 4].GetCardWeight;
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

	protected List<Card> FindDouble(List<Card> allCards, int weight, bool equal)
	{
		List<Card> list = new List<Card>();
		for (int i = 0; i < allCards.Count; i++)
		{
			if (i >= allCards.Count - 1 || allCards[i].GetCardWeight != allCards[i + 1].GetCardWeight)
			{
				continue;
			}
			int num = (int)allCards[i].GetCardWeight + (int)allCards[i + 1].GetCardWeight;
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
		return list;
	}

	protected List<Card> FindSingle(List<Card> allCards, int weight, bool equal)
	{
		List<Card> list = new List<Card>();
		for (int i = 0; i < allCards.Count; i++)
		{
			if (equal)
			{
				if ((int)allCards[i].GetCardWeight >= weight)
				{
					list.Add(allCards[i]);
					break;
				}
			}
			else if ((int)allCards[i].GetCardWeight > weight)
			{
				list.Add(allCards[i]);
				break;
			}
		}
		return list;
	}

	protected List<Card> FindOnlyThree(List<Card> allCards, int weight, bool equal)
	{
		List<Card> list = new List<Card>();
		for (int i = 0; i < allCards.Count; i++)
		{
			if (i > allCards.Count - 3 || allCards[i].GetCardWeight != allCards[i + 1].GetCardWeight || allCards[i].GetCardWeight != allCards[i + 2].GetCardWeight)
			{
				continue;
			}
			int num = (int)((int)allCards[i].GetCardWeight + (int)allCards[i + 1].GetCardWeight + allCards[i + 2].GetCardWeight);
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
		return list;
	}

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
						for (int j = i + 1; j < allCards.Count && allCards[j].GetCardWeight <= Weight.One; j++)
						{
							if (allCards[j].GetCardWeight - getCardWeight == (Weight)num)
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
					for (int k = i + 1; k < allCards.Count && allCards[k].GetCardWeight <= Weight.One; k++)
					{
						if (allCards[k].GetCardWeight - getCardWeight == (Weight)num)
						{
							num++;
							list2.Add(k);
						}
						if (num == length)
						{
							break;
						}
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
			for (int l = 0; l < list2.Count; l++)
			{
				list.Add(allCards[list2[l]]);
			}
		}
		return list;
	}

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
					for (int j = i + 1; j < allCards.Count && allCards[j].GetCardWeight <= Weight.One; j++)
					{
						if (allCards[j].GetCardWeight - getCardWeight == (Weight)num)
						{
							num2++;
							if (num2 % 2 == 1)
							{
								num++;
							}
							list2.Add(j);
						}
						if (num == length / 2)
						{
							break;
						}
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
			for (int k = 0; k < list2.Count; k++)
			{
				list.Add(allCards[list2[k]]);
			}
		}
		return list;
	}

	protected List<Card> FindThreeAndTwo(List<Card> allCards, int weight, bool equal)
	{
		List<Card> list = FindOnlyThree(allCards, weight, equal);
		if (list.Count != 0)
		{
			List<Card> allCards2 = GetAllCards(list);
			List<Card> collection = FindDouble(allCards2, 0, equal: true);
			list.AddRange(collection);
		}
		else
		{
			list.Clear();
		}
		return list;
	}

	protected List<Card> FindThreeAndOne(List<Card> allCards, int weight, bool equal)
	{
		List<Card> list = FindOnlyThree(allCards, weight, equal);
		if (list.Count != 0)
		{
			List<Card> allCards2 = GetAllCards(list);
			List<Card> collection = FindSingle(allCards2, 0, equal: true);
			list.AddRange(collection);
		}
		else
		{
			list.Clear();
		}
		return list;
	}

	protected void ShowNotice()
	{
		computerNotice.SetActive(true);
		computerNotice.GetComponent<TweenAlpha>().ResetToBeginning();
		computerNotice.GetComponent<TweenAlpha>().PlayForward();
		((MonoBehaviour)this).StartCoroutine(DisActiveNotice(computerNotice));
	}

	protected IEnumerator DisActiveNotice(GameObject notice)
	{
		yield return (object)new WaitForSeconds(2f);
		computerNotice.SetActive(false);
	}
}
