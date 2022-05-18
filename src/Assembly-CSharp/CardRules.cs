using System;
using System.Collections.Generic;

// Token: 0x02000675 RID: 1653
public class CardRules
{
	// Token: 0x06002949 RID: 10569 RVA: 0x00142054 File Offset: 0x00140254
	public static void SortCards(List<Card> cards, bool ascending)
	{
		cards.Sort(delegate(Card a, Card b)
		{
			if (!ascending)
			{
				return -a.GetCardWeight.CompareTo(b.GetCardWeight) * 2 + a.GetCardSuit.CompareTo(b.GetCardSuit);
			}
			return a.GetCardWeight.CompareTo(b.GetCardWeight);
		});
	}

	// Token: 0x0600294A RID: 10570 RVA: 0x000201C2 File Offset: 0x0001E3C2
	public static bool IsSingle(Card[] cards)
	{
		return cards.Length == 1;
	}

	// Token: 0x0600294B RID: 10571 RVA: 0x000201CD File Offset: 0x0001E3CD
	public static bool IsDouble(Card[] cards)
	{
		return cards.Length == 2 && cards[0].GetCardWeight == cards[1].GetCardWeight;
	}

	// Token: 0x0600294C RID: 10572 RVA: 0x00142080 File Offset: 0x00140280
	public static bool IsStraight(Card[] cards)
	{
		if (cards.Length < 5 || cards.Length > 12)
		{
			return false;
		}
		for (int i = 0; i < cards.Length - 1; i++)
		{
			Weight getCardWeight = cards[i].GetCardWeight;
			if (cards[i + 1].GetCardWeight - getCardWeight != 1)
			{
				return false;
			}
			if (getCardWeight > Weight.One || cards[i + 1].GetCardWeight > Weight.One)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600294D RID: 10573 RVA: 0x001420DC File Offset: 0x001402DC
	public static bool IsDoubleStraight(Card[] cards)
	{
		if (cards.Length < 6 || cards.Length % 2 != 0)
		{
			return false;
		}
		for (int i = 0; i < cards.Length; i += 2)
		{
			if (cards[i + 1].GetCardWeight != cards[i].GetCardWeight)
			{
				return false;
			}
			if (i < cards.Length - 2)
			{
				if (cards[i + 2].GetCardWeight - cards[i].GetCardWeight != 1)
				{
					return false;
				}
				if (cards[i].GetCardWeight > Weight.One || cards[i + 2].GetCardWeight > Weight.One)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600294E RID: 10574 RVA: 0x0014215C File Offset: 0x0014035C
	public static bool IsTripleStraight(Card[] cards)
	{
		if (cards.Length < 6 || cards.Length % 3 != 0)
		{
			return false;
		}
		for (int i = 0; i < cards.Length; i += 3)
		{
			if (cards[i + 1].GetCardWeight != cards[i].GetCardWeight)
			{
				return false;
			}
			if (cards[i + 2].GetCardWeight != cards[i].GetCardWeight)
			{
				return false;
			}
			if (cards[i + 1].GetCardWeight != cards[i + 2].GetCardWeight)
			{
				return false;
			}
			if (i < cards.Length - 3)
			{
				if (cards[i + 3].GetCardWeight - cards[i].GetCardWeight != 1)
				{
					return false;
				}
				if (cards[i].GetCardWeight > Weight.One || cards[i + 3].GetCardWeight > Weight.One)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600294F RID: 10575 RVA: 0x00142210 File Offset: 0x00140410
	public static bool IsOnlyThree(Card[] cards)
	{
		return cards.Length % 3 == 0 && cards[0].GetCardWeight == cards[1].GetCardWeight && cards[1].GetCardWeight == cards[2].GetCardWeight && cards[0].GetCardWeight == cards[2].GetCardWeight;
	}

	// Token: 0x06002950 RID: 10576 RVA: 0x00142264 File Offset: 0x00140464
	public static bool IsThreeAndOne(Card[] cards)
	{
		return cards.Length == 4 && ((cards[0].GetCardWeight == cards[1].GetCardWeight && cards[1].GetCardWeight == cards[2].GetCardWeight) || (cards[1].GetCardWeight == cards[2].GetCardWeight && cards[2].GetCardWeight == cards[3].GetCardWeight));
	}

	// Token: 0x06002951 RID: 10577 RVA: 0x001422C8 File Offset: 0x001404C8
	public static bool IsThreeAndTwo(Card[] cards)
	{
		if (cards.Length != 5)
		{
			return false;
		}
		if (cards[0].GetCardWeight == cards[1].GetCardWeight && cards[1].GetCardWeight == cards[2].GetCardWeight)
		{
			if (cards[3].GetCardWeight == cards[4].GetCardWeight)
			{
				return true;
			}
		}
		else if (cards[2].GetCardWeight == cards[3].GetCardWeight && cards[3].GetCardWeight == cards[4].GetCardWeight && cards[0].GetCardWeight == cards[1].GetCardWeight)
		{
			return true;
		}
		return false;
	}

	// Token: 0x06002952 RID: 10578 RVA: 0x00142350 File Offset: 0x00140550
	public static bool IsBoom(Card[] cards)
	{
		return cards.Length == 4 && cards[0].GetCardWeight == cards[1].GetCardWeight && cards[1].GetCardWeight == cards[2].GetCardWeight && cards[2].GetCardWeight == cards[3].GetCardWeight;
	}

	// Token: 0x06002953 RID: 10579 RVA: 0x001423A4 File Offset: 0x001405A4
	public static bool IsJokerBoom(Card[] cards)
	{
		if (cards.Length != 2)
		{
			return false;
		}
		if (cards[0].GetCardWeight == Weight.SJoker)
		{
			return cards[1].GetCardWeight == Weight.LJoker;
		}
		return cards[0].GetCardWeight == Weight.LJoker && cards[1].GetCardWeight == Weight.SJoker;
	}

	// Token: 0x06002954 RID: 10580 RVA: 0x001423F4 File Offset: 0x001405F4
	public static bool PopEnable(Card[] cards, out CardsType type)
	{
		type = CardsType.None;
		bool result = false;
		switch (cards.Length)
		{
		case 1:
			result = true;
			type = CardsType.Single;
			break;
		case 2:
			if (CardRules.IsDouble(cards))
			{
				result = true;
				type = CardsType.Double;
			}
			else if (CardRules.IsJokerBoom(cards))
			{
				result = true;
				type = CardsType.JokerBoom;
			}
			break;
		case 3:
			if (CardRules.IsOnlyThree(cards))
			{
				result = true;
				type = CardsType.OnlyThree;
			}
			break;
		case 4:
			if (CardRules.IsBoom(cards))
			{
				result = true;
				type = CardsType.Boom;
			}
			else if (CardRules.IsThreeAndOne(cards))
			{
				result = true;
				type = CardsType.ThreeAndOne;
			}
			break;
		case 5:
			if (CardRules.IsStraight(cards))
			{
				result = true;
				type = CardsType.Straight;
			}
			else if (CardRules.IsThreeAndTwo(cards))
			{
				result = true;
				type = CardsType.ThreeAndTwo;
			}
			break;
		case 6:
			if (CardRules.IsStraight(cards))
			{
				result = true;
				type = CardsType.Straight;
			}
			else if (CardRules.IsTripleStraight(cards))
			{
				result = true;
				type = CardsType.TripleStraight;
			}
			else if (CardRules.IsDoubleStraight(cards))
			{
				result = true;
				type = CardsType.DoubleStraight;
			}
			break;
		case 7:
			if (CardRules.IsStraight(cards))
			{
				result = true;
				type = CardsType.Straight;
			}
			break;
		case 8:
			if (CardRules.IsStraight(cards))
			{
				result = true;
				type = CardsType.Straight;
			}
			else if (CardRules.IsDoubleStraight(cards))
			{
				result = true;
				type = CardsType.DoubleStraight;
			}
			break;
		case 9:
			if (CardRules.IsStraight(cards))
			{
				result = true;
				type = CardsType.Straight;
			}
			else if (CardRules.IsOnlyThree(cards))
			{
				result = true;
				type = CardsType.OnlyThree;
			}
			break;
		case 10:
			if (CardRules.IsStraight(cards))
			{
				result = true;
				type = CardsType.Straight;
			}
			else if (CardRules.IsDoubleStraight(cards))
			{
				result = true;
				type = CardsType.DoubleStraight;
			}
			break;
		case 11:
			if (CardRules.IsStraight(cards))
			{
				result = true;
				type = CardsType.Straight;
			}
			break;
		case 12:
			if (CardRules.IsStraight(cards))
			{
				result = true;
				type = CardsType.Straight;
			}
			else if (CardRules.IsDoubleStraight(cards))
			{
				result = true;
				type = CardsType.DoubleStraight;
			}
			else if (CardRules.IsOnlyThree(cards))
			{
				result = true;
				type = CardsType.OnlyThree;
			}
			break;
		case 14:
			if (CardRules.IsDoubleStraight(cards))
			{
				result = true;
				type = CardsType.DoubleStraight;
			}
			break;
		case 15:
			if (CardRules.IsOnlyThree(cards))
			{
				result = true;
				type = CardsType.OnlyThree;
			}
			break;
		case 16:
			if (CardRules.IsDoubleStraight(cards))
			{
				result = true;
				type = CardsType.DoubleStraight;
			}
			break;
		case 18:
			if (CardRules.IsDoubleStraight(cards))
			{
				result = true;
				type = CardsType.DoubleStraight;
			}
			else if (CardRules.IsOnlyThree(cards))
			{
				result = true;
				type = CardsType.OnlyThree;
			}
			break;
		case 20:
			if (CardRules.IsDoubleStraight(cards))
			{
				result = true;
				type = CardsType.DoubleStraight;
			}
			break;
		}
		return result;
	}
}
