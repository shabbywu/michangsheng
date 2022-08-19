using System;
using System.Collections.Generic;

// Token: 0x020004A6 RID: 1190
public class Deck
{
	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x0600257F RID: 9599 RVA: 0x00103FDC File Offset: 0x001021DC
	public static Deck Instance
	{
		get
		{
			if (Deck.instance == null)
			{
				Deck.instance = new Deck();
			}
			return Deck.instance;
		}
	}

	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x06002580 RID: 9600 RVA: 0x00103FF4 File Offset: 0x001021F4
	public int CardsCount
	{
		get
		{
			return this.library.Count;
		}
	}

	// Token: 0x170002A4 RID: 676
	public Card this[int index]
	{
		get
		{
			return this.library[index];
		}
	}

	// Token: 0x06002582 RID: 9602 RVA: 0x0010400F File Offset: 0x0010220F
	private Deck()
	{
		this.library = new List<Card>();
		this.ctype = CharacterType.Library;
		this.CreateDeck();
	}

	// Token: 0x06002583 RID: 9603 RVA: 0x00104030 File Offset: 0x00102230
	private void CreateDeck()
	{
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 13; j++)
			{
				Weight weight = (Weight)j;
				Suits color = (Suits)i;
				Card item = new Card(color.ToString() + weight.ToString(), weight, color, this.ctype);
				this.library.Add(item);
			}
		}
		Card item2 = new Card("SJoker", Weight.SJoker, Suits.None, this.ctype);
		Card item3 = new Card("LJoker", Weight.LJoker, Suits.None, this.ctype);
		this.library.Add(item2);
		this.library.Add(item3);
	}

	// Token: 0x06002584 RID: 9604 RVA: 0x001040DC File Offset: 0x001022DC
	public void Shuffle()
	{
		if (this.CardsCount == 54)
		{
			Random random = new Random();
			List<Card> list = new List<Card>();
			foreach (Card item in this.library)
			{
				list.Insert(random.Next(list.Count + 1), item);
			}
			this.library.Clear();
			foreach (Card item2 in list)
			{
				this.library.Add(item2);
			}
			list.Clear();
		}
	}

	// Token: 0x06002585 RID: 9605 RVA: 0x001041AC File Offset: 0x001023AC
	public Card Deal()
	{
		Card card = this.library[this.library.Count - 1];
		this.library.Remove(card);
		return card;
	}

	// Token: 0x06002586 RID: 9606 RVA: 0x001041E0 File Offset: 0x001023E0
	public void AddCard(Card card)
	{
		card.Attribution = this.ctype;
		this.library.Add(card);
	}

	// Token: 0x04001E43 RID: 7747
	private static Deck instance;

	// Token: 0x04001E44 RID: 7748
	private List<Card> library;

	// Token: 0x04001E45 RID: 7749
	private CharacterType ctype;
}
