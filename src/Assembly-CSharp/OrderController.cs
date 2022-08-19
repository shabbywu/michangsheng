using System;

// Token: 0x020004CE RID: 1230
public class OrderController
{
	// Token: 0x14000036 RID: 54
	// (add) Token: 0x060027AD RID: 10157 RVA: 0x00128CF0 File Offset: 0x00126EF0
	// (remove) Token: 0x060027AE RID: 10158 RVA: 0x00128D28 File Offset: 0x00126F28
	public event CardEvent smartCard;

	// Token: 0x14000037 RID: 55
	// (add) Token: 0x060027AF RID: 10159 RVA: 0x00128D60 File Offset: 0x00126F60
	// (remove) Token: 0x060027B0 RID: 10160 RVA: 0x00128D98 File Offset: 0x00126F98
	public event CardEvent activeButton;

	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x060027B1 RID: 10161 RVA: 0x00128DCD File Offset: 0x00126FCD
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

	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x060027B2 RID: 10162 RVA: 0x00128DE5 File Offset: 0x00126FE5
	public CharacterType Type
	{
		get
		{
			return this.currentAuthority;
		}
	}

	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x060027B4 RID: 10164 RVA: 0x00128DF6 File Offset: 0x00126FF6
	// (set) Token: 0x060027B3 RID: 10163 RVA: 0x00128DED File Offset: 0x00126FED
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

	// Token: 0x060027B5 RID: 10165 RVA: 0x00128DFE File Offset: 0x00126FFE
	private OrderController()
	{
		this.currentAuthority = CharacterType.Desk;
	}

	// Token: 0x060027B6 RID: 10166 RVA: 0x00128E0D File Offset: 0x0012700D
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

	// Token: 0x060027B7 RID: 10167 RVA: 0x00128E40 File Offset: 0x00127040
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

	// Token: 0x060027B8 RID: 10168 RVA: 0x00128EBC File Offset: 0x001270BC
	public void ResetButton()
	{
		this.activeButton = null;
	}

	// Token: 0x060027B9 RID: 10169 RVA: 0x00128EC5 File Offset: 0x001270C5
	public void ResetSmartCard()
	{
		this.smartCard = null;
	}

	// Token: 0x04002288 RID: 8840
	private CharacterType biggest;

	// Token: 0x04002289 RID: 8841
	private CharacterType currentAuthority;

	// Token: 0x0400228A RID: 8842
	private static OrderController instance;
}
