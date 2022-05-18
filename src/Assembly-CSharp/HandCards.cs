using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000678 RID: 1656
public class HandCards : MonoBehaviour
{
	// Token: 0x06002966 RID: 10598 RVA: 0x0002026E File Offset: 0x0001E46E
	private void Start()
	{
		this.multiples = 1;
		this.identity = Identity.Farmer;
		this.library = new List<Card>();
	}

	// Token: 0x170002FA RID: 762
	// (get) Token: 0x06002968 RID: 10600 RVA: 0x00020292 File Offset: 0x0001E492
	// (set) Token: 0x06002967 RID: 10599 RVA: 0x00020289 File Offset: 0x0001E489
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

	// Token: 0x170002FB RID: 763
	// (get) Token: 0x0600296A RID: 10602 RVA: 0x000202AA File Offset: 0x0001E4AA
	// (set) Token: 0x06002969 RID: 10601 RVA: 0x0002029A File Offset: 0x0001E49A
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

	// Token: 0x170002FC RID: 764
	// (get) Token: 0x0600296B RID: 10603 RVA: 0x000202B2 File Offset: 0x0001E4B2
	public int CardsCount
	{
		get
		{
			return this.library.Count;
		}
	}

	// Token: 0x170002FD RID: 765
	// (get) Token: 0x0600296D RID: 10605 RVA: 0x000202C8 File Offset: 0x0001E4C8
	// (set) Token: 0x0600296C RID: 10604 RVA: 0x000202BF File Offset: 0x0001E4BF
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

	// Token: 0x170002FE RID: 766
	public Card this[int index]
	{
		get
		{
			return this.library[index];
		}
	}

	// Token: 0x170002FF RID: 767
	public int this[Card card]
	{
		get
		{
			return this.library.IndexOf(card);
		}
	}

	// Token: 0x06002970 RID: 10608 RVA: 0x000202EC File Offset: 0x0001E4EC
	public void AddCard(Card card)
	{
		card.Attribution = this.cType;
		this.library.Add(card);
	}

	// Token: 0x06002971 RID: 10609 RVA: 0x00020306 File Offset: 0x0001E506
	public void AddCardSprite(CardSprite sprite)
	{
		this.cardSpriteslist.Add(sprite);
	}

	// Token: 0x06002972 RID: 10610 RVA: 0x00020314 File Offset: 0x0001E514
	public void PopCard(Card card)
	{
		this.library.Remove(card);
	}

	// Token: 0x06002973 RID: 10611 RVA: 0x00020323 File Offset: 0x0001E523
	public void Sort()
	{
		CardRules.SortCards(this.library, false);
	}

	// Token: 0x04002326 RID: 8998
	public CharacterType cType;

	// Token: 0x04002327 RID: 8999
	public List<CardSprite> cardSpriteslist;

	// Token: 0x04002328 RID: 9000
	private List<Card> library;

	// Token: 0x04002329 RID: 9001
	private Identity identity;

	// Token: 0x0400232A RID: 9002
	private int multiples;

	// Token: 0x0400232B RID: 9003
	private int integration;
}
