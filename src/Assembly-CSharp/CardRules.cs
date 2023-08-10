using System.Collections.Generic;

public class CardRules
{
	public static void SortCards(List<Card> cards, bool ascending)
	{
		cards.Sort((Card a, Card b) => (!ascending) ? (-a.GetCardWeight.CompareTo(b.GetCardWeight) * 2 + a.GetCardSuit.CompareTo(b.GetCardSuit)) : a.GetCardWeight.CompareTo(b.GetCardWeight));
	}

	public static bool IsSingle(Card[] cards)
	{
		if (cards.Length == 1)
		{
			return true;
		}
		return false;
	}

	public static bool IsDouble(Card[] cards)
	{
		if (cards.Length == 2 && cards[0].GetCardWeight == cards[1].GetCardWeight)
		{
			return true;
		}
		return false;
	}

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

	public static bool IsOnlyThree(Card[] cards)
	{
		if (cards.Length % 3 != 0)
		{
			return false;
		}
		if (cards[0].GetCardWeight != cards[1].GetCardWeight)
		{
			return false;
		}
		if (cards[1].GetCardWeight != cards[2].GetCardWeight)
		{
			return false;
		}
		if (cards[0].GetCardWeight != cards[2].GetCardWeight)
		{
			return false;
		}
		return true;
	}

	public static bool IsThreeAndOne(Card[] cards)
	{
		if (cards.Length != 4)
		{
			return false;
		}
		if (cards[0].GetCardWeight == cards[1].GetCardWeight && cards[1].GetCardWeight == cards[2].GetCardWeight)
		{
			return true;
		}
		if (cards[1].GetCardWeight == cards[2].GetCardWeight && cards[2].GetCardWeight == cards[3].GetCardWeight)
		{
			return true;
		}
		return false;
	}

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

	public static bool IsBoom(Card[] cards)
	{
		if (cards.Length != 4)
		{
			return false;
		}
		if (cards[0].GetCardWeight != cards[1].GetCardWeight)
		{
			return false;
		}
		if (cards[1].GetCardWeight != cards[2].GetCardWeight)
		{
			return false;
		}
		if (cards[2].GetCardWeight != cards[3].GetCardWeight)
		{
			return false;
		}
		return true;
	}

	public static bool IsJokerBoom(Card[] cards)
	{
		if (cards.Length != 2)
		{
			return false;
		}
		if (cards[0].GetCardWeight == Weight.SJoker)
		{
			if (cards[1].GetCardWeight == Weight.LJoker)
			{
				return true;
			}
			return false;
		}
		if (cards[0].GetCardWeight == Weight.LJoker)
		{
			if (cards[1].GetCardWeight == Weight.SJoker)
			{
				return true;
			}
			return false;
		}
		return false;
	}

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
			if (IsDouble(cards))
			{
				result = true;
				type = CardsType.Double;
			}
			else if (IsJokerBoom(cards))
			{
				result = true;
				type = CardsType.JokerBoom;
			}
			break;
		case 3:
			if (IsOnlyThree(cards))
			{
				result = true;
				type = CardsType.OnlyThree;
			}
			break;
		case 4:
			if (IsBoom(cards))
			{
				result = true;
				type = CardsType.Boom;
			}
			else if (IsThreeAndOne(cards))
			{
				result = true;
				type = CardsType.ThreeAndOne;
			}
			break;
		case 5:
			if (IsStraight(cards))
			{
				result = true;
				type = CardsType.Straight;
			}
			else if (IsThreeAndTwo(cards))
			{
				result = true;
				type = CardsType.ThreeAndTwo;
			}
			break;
		case 6:
			if (IsStraight(cards))
			{
				result = true;
				type = CardsType.Straight;
			}
			else if (IsTripleStraight(cards))
			{
				result = true;
				type = CardsType.TripleStraight;
			}
			else if (IsDoubleStraight(cards))
			{
				result = true;
				type = CardsType.DoubleStraight;
			}
			break;
		case 7:
			if (IsStraight(cards))
			{
				result = true;
				type = CardsType.Straight;
			}
			break;
		case 8:
			if (IsStraight(cards))
			{
				result = true;
				type = CardsType.Straight;
			}
			else if (IsDoubleStraight(cards))
			{
				result = true;
				type = CardsType.DoubleStraight;
			}
			break;
		case 9:
			if (IsStraight(cards))
			{
				result = true;
				type = CardsType.Straight;
			}
			else if (IsOnlyThree(cards))
			{
				result = true;
				type = CardsType.OnlyThree;
			}
			break;
		case 10:
			if (IsStraight(cards))
			{
				result = true;
				type = CardsType.Straight;
			}
			else if (IsDoubleStraight(cards))
			{
				result = true;
				type = CardsType.DoubleStraight;
			}
			break;
		case 11:
			if (IsStraight(cards))
			{
				result = true;
				type = CardsType.Straight;
			}
			break;
		case 12:
			if (IsStraight(cards))
			{
				result = true;
				type = CardsType.Straight;
			}
			else if (IsDoubleStraight(cards))
			{
				result = true;
				type = CardsType.DoubleStraight;
			}
			else if (IsOnlyThree(cards))
			{
				result = true;
				type = CardsType.OnlyThree;
			}
			break;
		case 14:
			if (IsDoubleStraight(cards))
			{
				result = true;
				type = CardsType.DoubleStraight;
			}
			break;
		case 15:
			if (IsOnlyThree(cards))
			{
				result = true;
				type = CardsType.OnlyThree;
			}
			break;
		case 16:
			if (IsDoubleStraight(cards))
			{
				result = true;
				type = CardsType.DoubleStraight;
			}
			break;
		case 18:
			if (IsDoubleStraight(cards))
			{
				result = true;
				type = CardsType.DoubleStraight;
			}
			else if (IsOnlyThree(cards))
			{
				result = true;
				type = CardsType.OnlyThree;
			}
			break;
		case 20:
			if (IsDoubleStraight(cards))
			{
				result = true;
				type = CardsType.DoubleStraight;
			}
			break;
		}
		return result;
	}
}
