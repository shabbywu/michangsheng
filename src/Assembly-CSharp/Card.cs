using KBEngine;

public class Card
{
	private readonly string cardName;

	private readonly Weight weight;

	private readonly Suits color;

	private CharacterType belongTo;

	private bool makedSprite;

	public card card;

	public string GetCardName => cardName;

	public Weight GetCardWeight => weight;

	public Suits GetCardSuit => color;

	public bool isSprite
	{
		get
		{
			return makedSprite;
		}
		set
		{
			makedSprite = value;
		}
	}

	public CharacterType Attribution
	{
		get
		{
			return belongTo;
		}
		set
		{
			belongTo = value;
		}
	}

	public Card(string name, Weight weight, Suits color, CharacterType belongTo)
	{
		makedSprite = false;
		cardName = name;
		this.weight = weight;
		this.color = color;
		this.belongTo = belongTo;
	}

	public Card(string name, Weight weight, Suits color, CharacterType belongTo, card _card)
	{
		makedSprite = false;
		cardName = name;
		this.weight = weight;
		this.color = color;
		this.belongTo = belongTo;
		card = _card;
	}
}
