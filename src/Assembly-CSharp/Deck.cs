using System;
using System.Collections.Generic;

public class Deck
{
	private static Deck instance;

	private List<Card> library;

	private CharacterType ctype;

	public static Deck Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new Deck();
			}
			return instance;
		}
	}

	public int CardsCount => library.Count;

	public Card this[int index] => library[index];

	private Deck()
	{
		library = new List<Card>();
		ctype = CharacterType.Library;
		CreateDeck();
	}

	private void CreateDeck()
	{
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 13; j++)
			{
				Weight weight = (Weight)j;
				Suits color = (Suits)i;
				Card item = new Card(color.ToString() + weight, weight, color, ctype);
				library.Add(item);
			}
		}
		Card item2 = new Card("SJoker", Weight.SJoker, Suits.None, ctype);
		Card item3 = new Card("LJoker", Weight.LJoker, Suits.None, ctype);
		library.Add(item2);
		library.Add(item3);
	}

	public void Shuffle()
	{
		if (CardsCount != 54)
		{
			return;
		}
		Random random = new Random();
		List<Card> list = new List<Card>();
		foreach (Card item in library)
		{
			list.Insert(random.Next(list.Count + 1), item);
		}
		library.Clear();
		foreach (Card item2 in list)
		{
			library.Add(item2);
		}
		list.Clear();
	}

	public Card Deal()
	{
		Card card = library[library.Count - 1];
		library.Remove(card);
		return card;
	}

	public void AddCard(Card card)
	{
		card.Attribution = ctype;
		library.Add(card);
	}
}
