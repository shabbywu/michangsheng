using System.Collections.Generic;
using UnityEngine;

public class DeskCardsCache
{
	private static DeskCardsCache instance;

	private List<Card> library;

	private CharacterType ctype;

	private CardsType rule;

	public static DeskCardsCache Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new DeskCardsCache();
			}
			return instance;
		}
	}

	public CardsType Rule
	{
		get
		{
			return rule;
		}
		set
		{
			rule = value;
		}
	}

	public Card this[int index] => library[index];

	public int CardsCount => library.Count;

	public int MinWeight => (int)library[0].GetCardWeight;

	public int TotalWeight => GameController.GetWeight(library.ToArray(), rule);

	public void Init()
	{
	}

	private DeskCardsCache()
	{
		library = new List<Card>();
		ctype = CharacterType.Desk;
		rule = CardsType.None;
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

	public void Clear()
	{
		if (library.Count != 0)
		{
			CardSprite[] componentsInChildren = GameObject.Find("Desk").GetComponentsInChildren<CardSprite>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				((Component)componentsInChildren[i]).transform.parent = null;
				componentsInChildren[i].Destroy();
			}
			while (library.Count != 0)
			{
				Card card = library[library.Count - 1];
				library.Remove(card);
				Deck.Instance.AddCard(card);
			}
			rule = CardsType.None;
		}
	}

	public void Sort()
	{
		CardRules.SortCards(library, ascending: true);
	}
}
