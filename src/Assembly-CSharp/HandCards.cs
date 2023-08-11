using System.Collections.Generic;
using UnityEngine;

public class HandCards : MonoBehaviour
{
	public CharacterType cType;

	public List<CardSprite> cardSpriteslist;

	private List<Card> library;

	private Identity identity;

	private int multiples;

	private int integration;

	public int Integration
	{
		get
		{
			return integration;
		}
		set
		{
			integration = value;
		}
	}

	public int Multiples
	{
		get
		{
			return multiples;
		}
		set
		{
			multiples *= value;
		}
	}

	public int CardsCount => library.Count;

	public Identity AccessIdentity
	{
		get
		{
			return identity;
		}
		set
		{
			identity = value;
		}
	}

	public Card this[int index] => library[index];

	public int this[Card card] => library.IndexOf(card);

	private void Start()
	{
		multiples = 1;
		identity = Identity.Farmer;
		library = new List<Card>();
	}

	public void AddCard(Card card)
	{
		card.Attribution = cType;
		library.Add(card);
	}

	public void AddCardSprite(CardSprite sprite)
	{
		cardSpriteslist.Add(sprite);
	}

	public void PopCard(Card card)
	{
		library.Remove(card);
	}

	public void Sort()
	{
		CardRules.SortCards(library, ascending: false);
	}
}
