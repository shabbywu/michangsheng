using System;
using KBEngine;

// Token: 0x02000674 RID: 1652
public class Card
{
	// Token: 0x06002940 RID: 10560 RVA: 0x00020128 File Offset: 0x0001E328
	public Card(string name, Weight weight, Suits color, CharacterType belongTo)
	{
		this.makedSprite = false;
		this.cardName = name;
		this.weight = weight;
		this.color = color;
		this.belongTo = belongTo;
	}

	// Token: 0x06002941 RID: 10561 RVA: 0x00020154 File Offset: 0x0001E354
	public Card(string name, Weight weight, Suits color, CharacterType belongTo, card _card)
	{
		this.makedSprite = false;
		this.cardName = name;
		this.weight = weight;
		this.color = color;
		this.belongTo = belongTo;
		this.card = _card;
	}

	// Token: 0x170002F3 RID: 755
	// (get) Token: 0x06002942 RID: 10562 RVA: 0x00020188 File Offset: 0x0001E388
	public string GetCardName
	{
		get
		{
			return this.cardName;
		}
	}

	// Token: 0x170002F4 RID: 756
	// (get) Token: 0x06002943 RID: 10563 RVA: 0x00020190 File Offset: 0x0001E390
	public Weight GetCardWeight
	{
		get
		{
			return this.weight;
		}
	}

	// Token: 0x170002F5 RID: 757
	// (get) Token: 0x06002944 RID: 10564 RVA: 0x00020198 File Offset: 0x0001E398
	public Suits GetCardSuit
	{
		get
		{
			return this.color;
		}
	}

	// Token: 0x170002F6 RID: 758
	// (get) Token: 0x06002946 RID: 10566 RVA: 0x000201A9 File Offset: 0x0001E3A9
	// (set) Token: 0x06002945 RID: 10565 RVA: 0x000201A0 File Offset: 0x0001E3A0
	public bool isSprite
	{
		get
		{
			return this.makedSprite;
		}
		set
		{
			this.makedSprite = value;
		}
	}

	// Token: 0x170002F7 RID: 759
	// (get) Token: 0x06002948 RID: 10568 RVA: 0x000201BA File Offset: 0x0001E3BA
	// (set) Token: 0x06002947 RID: 10567 RVA: 0x000201B1 File Offset: 0x0001E3B1
	public CharacterType Attribution
	{
		get
		{
			return this.belongTo;
		}
		set
		{
			this.belongTo = value;
		}
	}

	// Token: 0x04002318 RID: 8984
	private readonly string cardName;

	// Token: 0x04002319 RID: 8985
	private readonly Weight weight;

	// Token: 0x0400231A RID: 8986
	private readonly Suits color;

	// Token: 0x0400231B RID: 8987
	private CharacterType belongTo;

	// Token: 0x0400231C RID: 8988
	private bool makedSprite;

	// Token: 0x0400231D RID: 8989
	public card card;
}
