using System;
using KBEngine;

// Token: 0x02000499 RID: 1177
public class Card
{
	// Token: 0x0600251E RID: 9502 RVA: 0x001023CA File Offset: 0x001005CA
	public Card(string name, Weight weight, Suits color, CharacterType belongTo)
	{
		this.makedSprite = false;
		this.cardName = name;
		this.weight = weight;
		this.color = color;
		this.belongTo = belongTo;
	}

	// Token: 0x0600251F RID: 9503 RVA: 0x001023F6 File Offset: 0x001005F6
	public Card(string name, Weight weight, Suits color, CharacterType belongTo, card _card)
	{
		this.makedSprite = false;
		this.cardName = name;
		this.weight = weight;
		this.color = color;
		this.belongTo = belongTo;
		this.card = _card;
	}

	// Token: 0x17000294 RID: 660
	// (get) Token: 0x06002520 RID: 9504 RVA: 0x0010242A File Offset: 0x0010062A
	public string GetCardName
	{
		get
		{
			return this.cardName;
		}
	}

	// Token: 0x17000295 RID: 661
	// (get) Token: 0x06002521 RID: 9505 RVA: 0x00102432 File Offset: 0x00100632
	public Weight GetCardWeight
	{
		get
		{
			return this.weight;
		}
	}

	// Token: 0x17000296 RID: 662
	// (get) Token: 0x06002522 RID: 9506 RVA: 0x0010243A File Offset: 0x0010063A
	public Suits GetCardSuit
	{
		get
		{
			return this.color;
		}
	}

	// Token: 0x17000297 RID: 663
	// (get) Token: 0x06002524 RID: 9508 RVA: 0x0010244B File Offset: 0x0010064B
	// (set) Token: 0x06002523 RID: 9507 RVA: 0x00102442 File Offset: 0x00100642
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

	// Token: 0x17000298 RID: 664
	// (get) Token: 0x06002526 RID: 9510 RVA: 0x0010245C File Offset: 0x0010065C
	// (set) Token: 0x06002525 RID: 9509 RVA: 0x00102453 File Offset: 0x00100653
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

	// Token: 0x04001E02 RID: 7682
	private readonly string cardName;

	// Token: 0x04001E03 RID: 7683
	private readonly Weight weight;

	// Token: 0x04001E04 RID: 7684
	private readonly Suits color;

	// Token: 0x04001E05 RID: 7685
	private CharacterType belongTo;

	// Token: 0x04001E06 RID: 7686
	private bool makedSprite;

	// Token: 0x04001E07 RID: 7687
	public card card;
}
