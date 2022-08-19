using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x020002DC RID: 732
public class JiaoYiManager : MonoBehaviour, IESCClose
{
	// Token: 0x17000249 RID: 585
	// (get) Token: 0x0600196B RID: 6507 RVA: 0x000B5F91 File Offset: 0x000B4191
	// (set) Token: 0x0600196A RID: 6506 RVA: 0x000B5F24 File Offset: 0x000B4124
	public bool canClick
	{
		get
		{
			return this._canClick;
		}
		set
		{
			this._canClick = value;
			if (this._canClick)
			{
				for (int i = 0; i < this.UIToggles.Count; i++)
				{
					this.UIToggles[i].enabled = true;
				}
				return;
			}
			for (int j = 0; j < this.UIToggles.Count; j++)
			{
				this.UIToggles[j].enabled = false;
			}
		}
	}

	// Token: 0x0600196C RID: 6508 RVA: 0x000B5F99 File Offset: 0x000B4199
	private void Awake()
	{
		JiaoYiManager.inst = this;
		this.canClick = true;
	}

	// Token: 0x0600196D RID: 6509 RVA: 0x000B5FA8 File Offset: 0x000B41A8
	private void Start()
	{
		this.ok.onClick.Clear();
		this.ok.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.JiaoYiBtn)));
		this.closeBtn.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.close)));
		this.updateMoney();
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x0600196E RID: 6510 RVA: 0x000B6018 File Offset: 0x000B4218
	public void close()
	{
		if (this.canClick)
		{
			Singleton.ints.exchengePlan.close();
			ESCCloseManager.Inst.UnRegisterClose(this);
		}
	}

	// Token: 0x0600196F RID: 6511 RVA: 0x000B603C File Offset: 0x000B423C
	public void JiaoYiBtn()
	{
		if (this.canClick && this.MonstarSay().Equals("ok"))
		{
			Singleton.ints.exchengePlan.confirm();
			this.updateMoney();
		}
	}

	// Token: 0x06001970 RID: 6512 RVA: 0x000B6070 File Offset: 0x000B4270
	private string MonstarSay()
	{
		ExchangePlan exchengePlan = Singleton.ints.exchengePlan;
		string text = "ok";
		if (this.playerGetMoney >= 0 && this.monstarMoney < this.playerGetMoney)
		{
			int num = jsonData.instance.getRandom() % 10;
			text = Tools.getStr("exchengePlayer" + num);
			exchengePlan.MonstarterSay(text);
		}
		else if (this.playerGetMoney < 0 && this.playerMoney + this.playerGetMoney < 0)
		{
			int num2 = jsonData.instance.getRandom() % 10;
			text = Tools.getStr("exchengeMonstar" + num2);
			exchengePlan.MonstarterSay(text);
		}
		return text;
	}

	// Token: 0x06001971 RID: 6513 RVA: 0x000B6118 File Offset: 0x000B4318
	public void updateMoney()
	{
		ExchangePlan exchengePlan = Singleton.ints.exchengePlan;
		Avatar player = Tools.instance.getPlayer();
		this.playerMoney = (int)player.money;
		this.monstarMoney = (int)jsonData.instance.AvatarBackpackJsonData[string.Concat(exchengePlan.MonstarID)]["money"].n;
		exchengePlan.PlayerMoney.Set_money(this.playerMoney, false);
		exchengePlan.MonstarMoney.Set_money(this.monstarMoney, false);
		int playerSellItemMoney = this.GetPlayerSellItemMoney(exchengePlan.MonstarID);
		int npcSellItemMoney = this.GetNpcSellItemMoney(exchengePlan.MonstarID);
		this.playerGetMoney = playerSellItemMoney - npcSellItemMoney;
		exchengePlan.MonstarMoneyPay.Set_money(this.playerGetMoney, true);
	}

	// Token: 0x06001972 RID: 6514 RVA: 0x000B61D8 File Offset: 0x000B43D8
	public int GetPlayerSellItemMoney(int monsterId)
	{
		int num = 0;
		foreach (ItemCellEX itemCellEX in this.playerItemList)
		{
			itemCellEX.UpdateRefresh();
			if (itemCellEX.GetItem.itemID > 0)
			{
				num += itemCellEX.GetItem.GetJiaoYiPrice(monsterId, true, true);
			}
		}
		return num;
	}

	// Token: 0x06001973 RID: 6515 RVA: 0x000B624C File Offset: 0x000B444C
	public int GetNpcSellItemMoney(int monsterId)
	{
		int num = 0;
		foreach (ItemCellEX itemCellEX in this.npcItemList)
		{
			itemCellEX.UpdateRefresh();
			if (itemCellEX.GetItem.itemID > 0)
			{
				num += itemCellEX.GetItem.GetJiaoYiPrice(monsterId, false, true);
			}
		}
		return num;
	}

	// Token: 0x06001974 RID: 6516 RVA: 0x000B62C0 File Offset: 0x000B44C0
	private void OnDestroy()
	{
		JiaoYiManager.inst = null;
	}

	// Token: 0x06001975 RID: 6517 RVA: 0x000B62C8 File Offset: 0x000B44C8
	public bool TryEscClose()
	{
		if (this.canClick)
		{
			for (int i = 0; i < this.closeBtn.onClick.Count; i++)
			{
				if (this.closeBtn.onClick[i] != null)
				{
					this.closeBtn.onClick[i].Execute();
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x0400149C RID: 5276
	public static JiaoYiManager inst;

	// Token: 0x0400149D RID: 5277
	[SerializeField]
	private UIButton closeBtn;

	// Token: 0x0400149E RID: 5278
	[SerializeField]
	private List<UIToggle> UIToggles;

	// Token: 0x0400149F RID: 5279
	[SerializeField]
	private UIButton ok;

	// Token: 0x040014A0 RID: 5280
	private int playerGetMoney;

	// Token: 0x040014A1 RID: 5281
	private int playerMoney;

	// Token: 0x040014A2 RID: 5282
	private int monstarMoney;

	// Token: 0x040014A3 RID: 5283
	public List<ItemCellEX> playerItemList;

	// Token: 0x040014A4 RID: 5284
	public List<ItemCellEX> npcItemList;

	// Token: 0x040014A5 RID: 5285
	private bool _canClick;
}
