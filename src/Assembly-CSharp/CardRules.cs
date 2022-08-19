using System;
using System.Collections.Generic;

// Token: 0x0200049A RID: 1178
public class CardRules
{
	// Token: 0x06002527 RID: 9511 RVA: 0x00102464 File Offset: 0x00100664
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

	// Token: 0x06002528 RID: 9512 RVA: 0x00102490 File Offset: 0x00100690
	public static bool IsSingle(Card[] cards)
	{
		return cards.Length == 1;
	}

	// Token: 0x06002529 RID: 9513 RVA: 0x0010249B File Offset: 0x0010069B
	public static bool IsDouble(Card[] cards)
	{
		return cards.Length == 2 && cards[0].GetCardWeight == cards[1].GetCardWeight;
	}

	// Token: 0x0600252A RID: 9514 RVA: 0x001024B8 File Offset: 0x001006B8
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

	// Token: 0x0600252B RID: 9515 RVA: 0x00102514 File Offset: 0x00100714
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

	// Token: 0x0600252C RID: 9516 RVA: 0x00102594 File Offset: 0x00100794
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

	// Token: 0x0600252D RID: 9517 RVA: 0x00102648 File Offset: 0x00100848
	public static bool IsOnlyThree(Card[] cards)
	{
		return cards.Length % 3 == 0 && cards[0].GetCardWeight == cards[1].GetCardWeight && cards[1].GetCardWeight == cards[2].GetCardWeight && cards[0].GetCardWeight == cards[2].GetCardWeight;
	}

	// Token: 0x0600252E RID: 9518 RVA: 0x0010269C File Offset: 0x0010089C
	public static bool IsThreeAndOne(Card[] cards)
	{
		return cards.Length == 4 && ((cards[0].GetCardWeight == cards[1].GetCardWeight && cards[1].GetCardWeight == cards[2].GetCardWeight) || (cards[1].GetCardWeight == cards[2].GetCardWeight && cards[2].GetCardWeight == cards[3].GetCardWeight));
	}

	// Token: 0x0600252F RID: 9519 RVA: 0x00102700 File Offset: 0x00100900
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

	// Token: 0x06002530 RID: 9520 RVA: 0x00102788 File Offset: 0x00100988
	public static bool IsBoom(Card[] cards)
	{
		return cards.Length == 4 && cards[0].GetCardWeight == cards[1].GetCardWeight && cards[1].GetCardWeight == cards[2].GetCardWeight && cards[2].GetCardWeight == cards[3].GetCardWeight;
	}

	// Token: 0x06002531 RID: 9521 RVA: 0x001027DC File Offset: 0x001009DC
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

	// Token: 0x06002532 RID: 9522 RVA: 0x0010282C File Offset: 0x00100A2C
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
