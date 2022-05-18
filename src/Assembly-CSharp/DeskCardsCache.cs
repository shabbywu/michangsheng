using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000687 RID: 1671
public class DeskCardsCache
{
	// Token: 0x060029C3 RID: 10691 RVA: 0x000042DD File Offset: 0x000024DD
	public void Init()
	{
	}

	// Token: 0x1700030C RID: 780
	// (get) Token: 0x060029C4 RID: 10692 RVA: 0x000206ED File Offset: 0x0001E8ED
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

	// Token: 0x1700030D RID: 781
	// (get) Token: 0x060029C6 RID: 10694 RVA: 0x0002070E File Offset: 0x0001E90E
	// (set) Token: 0x060029C5 RID: 10693 RVA: 0x00020705 File Offset: 0x0001E905
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

	// Token: 0x1700030E RID: 782
	public Card this[int index]
	{
		get
		{
			return this.library[index];
		}
	}

	// Token: 0x1700030F RID: 783
	// (get) Token: 0x060029C8 RID: 10696 RVA: 0x00020724 File Offset: 0x0001E924
	public int CardsCount
	{
		get
		{
			return this.library.Count;
		}
	}

	// Token: 0x17000310 RID: 784
	// (get) Token: 0x060029C9 RID: 10697 RVA: 0x00020731 File Offset: 0x0001E931
	public int MinWeight
	{
		get
		{
			return (int)this.library[0].GetCardWeight;
		}
	}

	// Token: 0x17000311 RID: 785
	// (get) Token: 0x060029CA RID: 10698 RVA: 0x00020744 File Offset: 0x0001E944
	public int TotalWeight
	{
		get
		{
			return GameController.GetWeight(this.library.ToArray(), this.rule);
		}
	}

	// Token: 0x060029CB RID: 10699 RVA: 0x0002075C File Offset: 0x0001E95C
	private DeskCardsCache()
	{
		this.library = new List<Card>();
		this.ctype = CharacterType.Desk;
		this.rule = CardsType.None;
	}

	// Token: 0x060029CC RID: 10700 RVA: 0x00143E8C File Offset: 0x0014208C
	public Card Deal()
	{
		Card card = this.library[this.library.Count - 1];
		this.library.Remove(card);
		return card;
	}

	// Token: 0x060029CD RID: 10701 RVA: 0x0002077D File Offset: 0x0001E97D
	public void AddCard(Card card)
	{
		card.Attribution = this.ctype;
		this.library.Add(card);
	}

	// Token: 0x060029CE RID: 10702 RVA: 0x00143EC0 File Offset: 0x001420C0
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

	// Token: 0x060029CF RID: 10703 RVA: 0x00020797 File Offset: 0x0001E997
	public void Sort()
	{
		CardRules.SortCards(this.library, true);
	}

	// Token: 0x0400236C RID: 9068
	private static DeskCardsCache instance;

	// Token: 0x0400236D RID: 9069
	private List<Card> library;

	// Token: 0x0400236E RID: 9070
	private CharacterType ctype;

	// Token: 0x0400236F RID: 9071
	private CardsType rule;
}
