using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200049C RID: 1180
public class HandCards : MonoBehaviour
{
	// Token: 0x06002542 RID: 9538 RVA: 0x00102CE1 File Offset: 0x00100EE1
	private void Start()
	{
		this.multiples = 1;
		this.identity = Identity.Farmer;
		this.library = new List<Card>();
	}

	// Token: 0x1700029B RID: 667
	// (get) Token: 0x06002544 RID: 9540 RVA: 0x00102D05 File Offset: 0x00100F05
	// (set) Token: 0x06002543 RID: 9539 RVA: 0x00102CFC File Offset: 0x00100EFC
	public int Integration
	{
		get
		{
			return this.integration;
		}
		set
		{
			this.integration = value;
		}
	}

	// Token: 0x1700029C RID: 668
	// (get) Token: 0x06002546 RID: 9542 RVA: 0x00102D1D File Offset: 0x00100F1D
	// (set) Token: 0x06002545 RID: 9541 RVA: 0x00102D0D File Offset: 0x00100F0D
	public int Multiples
	{
		get
		{
			return this.multiples;
		}
		set
		{
			this.multiples *= value;
		}
	}

	// Token: 0x1700029D RID: 669
	// (get) Token: 0x06002547 RID: 9543 RVA: 0x00102D25 File Offset: 0x00100F25
	public int CardsCount
	{
		get
		{
			return this.library.Count;
		}
	}

	// Token: 0x1700029E RID: 670
	// (get) Token: 0x06002549 RID: 9545 RVA: 0x00102D3B File Offset: 0x00100F3B
	// (set) Token: 0x06002548 RID: 9544 RVA: 0x00102D32 File Offset: 0x00100F32
	public Identity AccessIdentity
	{
		get
		{
			return this.identity;
		}
		set
		{
			this.identity = value;
		}
	}

	// Token: 0x1700029F RID: 671
	public Card this[int index]
	{
		get
		{
			return this.library[index];
		}
	}

	// Token: 0x170002A0 RID: 672
	public int this[Card card]
	{
		get
		{
			return this.library.IndexOf(card);
		}
	}

	// Token: 0x0600254C RID: 9548 RVA: 0x00102D5F File Offset: 0x00100F5F
	public void AddCard(Card card)
	{
		card.Attribution = this.cType;
		this.library.Add(card);
	}

	// Token: 0x0600254D RID: 9549 RVA: 0x00102D79 File Offset: 0x00100F79
	public void AddCardSprite(CardSprite sprite)
	{
		this.cardSpriteslist.Add(sprite);
	}

	// Token: 0x0600254E RID: 9550 RVA: 0x00102D87 File Offset: 0x00100F87
	public void PopCard(Card card)
	{
		this.library.Remove(card);
	}

	// Token: 0x0600254F RID: 9551 RVA: 0x00102D96 File Offset: 0x00100F96
	public void Sort()
	{
		CardRules.SortCards(this.library, false);
	}

	// Token: 0x04001E0F RID: 7695
	public CharacterType cType;

	// Token: 0x04001E10 RID: 7696
	public List<CardSprite> cardSpriteslist;

	// Token: 0x04001E11 RID: 7697
	private List<Card> library;

	// Token: 0x04001E12 RID: 7698
	private Identity identity;

	// Token: 0x04001E13 RID: 7699
	private int multiples;

	// Token: 0x04001E14 RID: 7700
	private int integration;
}
