using System;
using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

// Token: 0x0200042E RID: 1070
public class JiaoYiManager : MonoBehaviour, IESCClose
{
	// Token: 0x17000293 RID: 659
	// (get) Token: 0x06001C81 RID: 7297 RVA: 0x00017D2D File Offset: 0x00015F2D
	// (set) Token: 0x06001C80 RID: 7296 RVA: 0x000FBEBC File Offset: 0x000FA0BC
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

	// Token: 0x06001C82 RID: 7298 RVA: 0x00017D35 File Offset: 0x00015F35
	private void Awake()
	{
		JiaoYiManager.inst = this;
		this.canClick = true;
	}

	// Token: 0x06001C83 RID: 7299 RVA: 0x000FBF2C File Offset: 0x000FA12C
	private void Start()
	{
		this.ok.onClick.Clear();
		this.ok.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.JiaoYiBtn)));
		this.closeBtn.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.close)));
		this.updateMoney();
		ESCCloseManager.Inst.RegisterClose(this);
	}

	// Token: 0x06001C84 RID: 7300 RVA: 0x00017D44 File Offset: 0x00015F44
	public void close()
	{
		if (this.canClick)
		{
			Singleton.ints.exchengePlan.close();
			ESCCloseManager.Inst.UnRegisterClose(this);
		}
	}

	// Token: 0x06001C85 RID: 7301 RVA: 0x00017D68 File Offset: 0x00015F68
	public void JiaoYiBtn()
	{
		if (this.canClick && this.MonstarSay().Equals("ok"))
		{
			Singleton.ints.exchengePlan.confirm();
			this.updateMoney();
		}
	}

	// Token: 0x06001C86 RID: 7302 RVA: 0x000FBF9C File Offset: 0x000FA19C
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

	// Token: 0x06001C87 RID: 7303 RVA: 0x000FC044 File Offset: 0x000FA244
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

	// Token: 0x06001C88 RID: 7304 RVA: 0x000FC104 File Offset: 0x000FA304
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

	// Token: 0x06001C89 RID: 7305 RVA: 0x000FC178 File Offset: 0x000FA378
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

	// Token: 0x06001C8A RID: 7306 RVA: 0x00017D99 File Offset: 0x00015F99
	private void OnDestroy()
	{
		JiaoYiManager.inst = null;
	}

	// Token: 0x06001C8B RID: 7307 RVA: 0x000FC1EC File Offset: 0x000FA3EC
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

	// Token: 0x0400187C RID: 6268
	public static JiaoYiManager inst;

	// Token: 0x0400187D RID: 6269
	[SerializeField]
	private UIButton closeBtn;

	// Token: 0x0400187E RID: 6270
	[SerializeField]
	private List<UIToggle> UIToggles;

	// Token: 0x0400187F RID: 6271
	[SerializeField]
	private UIButton ok;

	// Token: 0x04001880 RID: 6272
	private int playerGetMoney;

	// Token: 0x04001881 RID: 6273
	private int playerMoney;

	// Token: 0x04001882 RID: 6274
	private int monstarMoney;

	// Token: 0x04001883 RID: 6275
	public List<ItemCellEX> playerItemList;

	// Token: 0x04001884 RID: 6276
	public List<ItemCellEX> npcItemList;

	// Token: 0x04001885 RID: 6277
	private bool _canClick;
}
