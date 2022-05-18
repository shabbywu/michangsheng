using System;
using System.Collections.Generic;

// Token: 0x02000686 RID: 1670
public class Deck
{
	// Token: 0x17000309 RID: 777
	// (get) Token: 0x060029BB RID: 10683 RVA: 0x00020680 File Offset: 0x0001E880
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

	// Token: 0x1700030A RID: 778
	// (get) Token: 0x060029BC RID: 10684 RVA: 0x00020698 File Offset: 0x0001E898
	public int CardsCount
	{
		get
		{
			return this.library.Count;
		}
	}

	// Token: 0x1700030B RID: 779
	public Card this[int index]
	{
		get
		{
			return this.library[index];
		}
	}

	// Token: 0x060029BE RID: 10686 RVA: 0x000206B3 File Offset: 0x0001E8B3
	private Deck()
	{
		this.library = new List<Card>();
		this.ctype = CharacterType.Library;
		this.CreateDeck();
	}

	// Token: 0x060029BF RID: 10687 RVA: 0x00143CDC File Offset: 0x00141EDC
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

	// Token: 0x060029C0 RID: 10688 RVA: 0x00143D88 File Offset: 0x00141F88
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

	// Token: 0x060029C1 RID: 10689 RVA: 0x00143E58 File Offset: 0x00142058
	public Card Deal()
	{
		Card card = this.library[this.library.Count - 1];
		this.library.Remove(card);
		return card;
	}

	// Token: 0x060029C2 RID: 10690 RVA: 0x000206D3 File Offset: 0x0001E8D3
	public void AddCard(Card card)
	{
		card.Attribution = this.ctype;
		this.library.Add(card);
	}

	// Token: 0x04002369 RID: 9065
	private static Deck instance;

	// Token: 0x0400236A RID: 9066
	private List<Card> library;

	// Token: 0x0400236B RID: 9067
	private CharacterType ctype;
}
