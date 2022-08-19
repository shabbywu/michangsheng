using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004A7 RID: 1191
public class DeskCardsCache
{
	// Token: 0x06002587 RID: 9607 RVA: 0x00004095 File Offset: 0x00002295
	public void Init()
	{
	}

	// Token: 0x170002A5 RID: 677
	// (get) Token: 0x06002588 RID: 9608 RVA: 0x001041FA File Offset: 0x001023FA
	public static DeskCardsCache Instance
	{
		get
		{
			if (DeskCardsCache.instance == null)
			{
				DeskCardsCache.instance = new DeskCardsCache();
			}
			return DeskCardsCache.instance;
		}
	}

	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x0600258A RID: 9610 RVA: 0x0010421B File Offset: 0x0010241B
	// (set) Token: 0x06002589 RID: 9609 RVA: 0x00104212 File Offset: 0x00102412
	public CardsType Rule
	{
		get
		{
			return this.rule;
		}
		set
		{
			this.rule = value;
		}
	}

	// Token: 0x170002A7 RID: 679
	public Card this[int index]
	{
		get
		{
			return this.library[index];
		}
	}

	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x0600258C RID: 9612 RVA: 0x00104231 File Offset: 0x00102431
	public int CardsCount
	{
		get
		{
			return this.library.Count;
		}
	}

	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x0600258D RID: 9613 RVA: 0x0010423E File Offset: 0x0010243E
	public int MinWeight
	{
		get
		{
			return (int)this.library[0].GetCardWeight;
		}
	}

	// Token: 0x170002AA RID: 682
	// (get) Token: 0x0600258E RID: 9614 RVA: 0x00104251 File Offset: 0x00102451
	public int TotalWeight
	{
		get
		{
			return GameController.GetWeight(this.library.ToArray(), this.rule);
		}
	}

	// Token: 0x0600258F RID: 9615 RVA: 0x00104269 File Offset: 0x00102469
	private DeskCardsCache()
	{
		this.library = new List<Card>();
		this.ctype = CharacterType.Desk;
		this.rule = CardsType.None;
	}

	// Token: 0x06002590 RID: 9616 RVA: 0x0010428C File Offset: 0x0010248C
	public Card Deal()
	{
		Card card = this.library[this.library.Count - 1];
		this.library.Remove(card);
		return card;
	}

	// Token: 0x06002591 RID: 9617 RVA: 0x001042C0 File Offset: 0x001024C0
	public void AddCard(Card card)
	{
		card.Attribution = this.ctype;
		this.library.Add(card);
	}

	// Token: 0x06002592 RID: 9618 RVA: 0x001042DC File Offset: 0x001024DC
	public void Clear()
	{
		if (this.library.Count != 0)
		{
			CardSprite[] componentsInChildren = GameObject.Find("Desk").GetComponentsInChildren<CardSprite>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].transform.parent = null;
				componentsInChildren[i].Destroy();
			}
			while (this.library.Count != 0)
			{
				Card card = this.library[this.library.Count - 1];
				this.library.Remove(card);
				Deck.Instance.AddCard(card);
			}
			this.rule = CardsType.None;
		}
	}

	// Token: 0x06002593 RID: 9619 RVA: 0x00104371 File Offset: 0x00102571
	public void Sort()
	{
		CardRules.SortCards(this.library, true);
	}

	// Token: 0x04001E46 RID: 7750
	private static DeskCardsCache instance;

	// Token: 0x04001E47 RID: 7751
	private List<Card> library;

	// Token: 0x04001E48 RID: 7752
	private CharacterType ctype;

	// Token: 0x04001E49 RID: 7753
	private CardsType rule;
}
