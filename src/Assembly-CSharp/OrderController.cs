using System;

// Token: 0x02000734 RID: 1844
public class OrderController
{
	// Token: 0x14000036 RID: 54
	// (add) Token: 0x06002EBB RID: 11963 RVA: 0x001746E4 File Offset: 0x001728E4
	// (remove) Token: 0x06002EBC RID: 11964 RVA: 0x0017471C File Offset: 0x0017291C
	public event CardEvent smartCard;

	// Token: 0x14000037 RID: 55
	// (add) Token: 0x06002EBD RID: 11965 RVA: 0x00174754 File Offset: 0x00172954
	// (remove) Token: 0x06002EBE RID: 11966 RVA: 0x0017478C File Offset: 0x0017298C
	public event CardEvent activeButton;

	// Token: 0x170003F9 RID: 1017
	// (get) Token: 0x06002EBF RID: 11967 RVA: 0x00022AEA File Offset: 0x00020CEA
	public static OrderController Instance
	{
		get
		{
			if (OrderController.instance == null)
			{
				OrderController.instance = new OrderController();
			}
			return OrderController.instance;
		}
	}

	// Token: 0x170003FA RID: 1018
	// (get) Token: 0x06002EC0 RID: 11968 RVA: 0x00022B02 File Offset: 0x00020D02
	public CharacterType Type
	{
		get
		{
			return this.currentAuthority;
		}
	}

	// Token: 0x170003FB RID: 1019
	// (get) Token: 0x06002EC2 RID: 11970 RVA: 0x00022B13 File Offset: 0x00020D13
	// (set) Token: 0x06002EC1 RID: 11969 RVA: 0x00022B0A File Offset: 0x00020D0A
	public CharacterType Biggest
	{
		get
		{
			return this.biggest;
		}
		set
		{
			this.biggest = value;
		}
	}

	// Token: 0x06002EC3 RID: 11971 RVA: 0x00022B1B File Offset: 0x00020D1B
	private OrderController()
	{
		this.currentAuthority = CharacterType.Desk;
	}

	// Token: 0x06002EC4 RID: 11972 RVA: 0x00022B2A File Offset: 0x00020D2A
	public void Init(CharacterType type)
	{
		this.currentAuthority = type;
		this.Biggest = type;
		if (this.currentAuthority == CharacterType.Player)
		{
			this.activeButton(false);
			return;
		}
		this.smartCard(true);
	}

	// Token: 0x06002EC5 RID: 11973 RVA: 0x001747C4 File Offset: 0x001729C4
	public void Turn()
	{
		this.currentAuthority++;
		if (this.currentAuthority == CharacterType.Desk)
		{
			this.currentAuthority = CharacterType.Player;
		}
		if (this.currentAuthority == CharacterType.ComputerOne || this.currentAuthority == CharacterType.ComputerTwo)
		{
			this.smartCard(this.biggest == this.currentAuthority);
			return;
		}
		if (this.currentAuthority == CharacterType.Player)
		{
			this.activeButton(this.biggest != this.currentAuthority);
		}
	}

	// Token: 0x06002EC6 RID: 11974 RVA: 0x00022B5C File Offset: 0x00020D5C
	public void ResetButton()
	{
		this.activeButton = null;
	}

	// Token: 0x06002EC7 RID: 11975 RVA: 0x00022B65 File Offset: 0x00020D65
	public void ResetSmartCard()
	{
		this.smartCard = null;
	}

	// Token: 0x040029D6 RID: 10710
	private CharacterType biggest;

	// Token: 0x040029D7 RID: 10711
	private CharacterType currentAuthority;

	// Token: 0x040029D8 RID: 10712
	private static OrderController instance;
}
