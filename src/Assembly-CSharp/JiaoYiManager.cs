using System.Collections.Generic;
using GUIPackage;
using KBEngine;
using UnityEngine;

public class JiaoYiManager : MonoBehaviour, IESCClose
{
	public static JiaoYiManager inst;

	[SerializeField]
	private UIButton closeBtn;

	[SerializeField]
	private List<UIToggle> UIToggles;

	[SerializeField]
	private UIButton ok;

	private int playerGetMoney;

	private int playerMoney;

	private int monstarMoney;

	public List<ItemCellEX> playerItemList;

	public List<ItemCellEX> npcItemList;

	private bool _canClick;

	public bool canClick
	{
		get
		{
			return _canClick;
		}
		set
		{
			_canClick = value;
			if (_canClick)
			{
				for (int i = 0; i < UIToggles.Count; i++)
				{
					((Behaviour)UIToggles[i]).enabled = true;
				}
			}
			else
			{
				for (int j = 0; j < UIToggles.Count; j++)
				{
					((Behaviour)UIToggles[j]).enabled = false;
				}
			}
		}
	}

	private void Awake()
	{
		inst = this;
		canClick = true;
	}

	private void Start()
	{
		ok.onClick.Clear();
		ok.onClick.Add(new EventDelegate(JiaoYiBtn));
		closeBtn.onClick.Add(new EventDelegate(close));
		updateMoney();
		ESCCloseManager.Inst.RegisterClose(this);
	}

	public void close()
	{
		if (canClick)
		{
			Singleton.ints.exchengePlan.close();
			ESCCloseManager.Inst.UnRegisterClose(this);
		}
	}

	public void JiaoYiBtn()
	{
		if (canClick && MonstarSay().Equals("ok"))
		{
			Singleton.ints.exchengePlan.confirm();
			updateMoney();
		}
	}

	private string MonstarSay()
	{
		ExchangePlan exchengePlan = Singleton.ints.exchengePlan;
		string text = "ok";
		if (playerGetMoney >= 0 && monstarMoney < playerGetMoney)
		{
			int num = jsonData.instance.getRandom() % 10;
			text = Tools.getStr("exchengePlayer" + num);
			exchengePlan.MonstarterSay(text);
		}
		else if (playerGetMoney < 0 && playerMoney + playerGetMoney < 0)
		{
			int num2 = jsonData.instance.getRandom() % 10;
			text = Tools.getStr("exchengeMonstar" + num2);
			exchengePlan.MonstarterSay(text);
		}
		return text;
	}

	public void updateMoney()
	{
		ExchangePlan exchengePlan = Singleton.ints.exchengePlan;
		Avatar player = Tools.instance.getPlayer();
		playerMoney = (int)player.money;
		monstarMoney = (int)jsonData.instance.AvatarBackpackJsonData[string.Concat(exchengePlan.MonstarID)]["money"].n;
		exchengePlan.PlayerMoney.Set_money(playerMoney);
		exchengePlan.MonstarMoney.Set_money(monstarMoney);
		int playerSellItemMoney = GetPlayerSellItemMoney(exchengePlan.MonstarID);
		int npcSellItemMoney = GetNpcSellItemMoney(exchengePlan.MonstarID);
		playerGetMoney = playerSellItemMoney - npcSellItemMoney;
		exchengePlan.MonstarMoneyPay.Set_money(playerGetMoney, isShowFuHao: true);
	}

	public int GetPlayerSellItemMoney(int monsterId)
	{
		int num = 0;
		foreach (ItemCellEX playerItem in playerItemList)
		{
			playerItem.UpdateRefresh();
			if (playerItem.GetItem.itemID > 0)
			{
				num += playerItem.GetItem.GetJiaoYiPrice(monsterId, isPlayer: true, zongjia: true);
			}
		}
		return num;
	}

	public int GetNpcSellItemMoney(int monsterId)
	{
		int num = 0;
		foreach (ItemCellEX npcItem in npcItemList)
		{
			npcItem.UpdateRefresh();
			if (npcItem.GetItem.itemID > 0)
			{
				num += npcItem.GetItem.GetJiaoYiPrice(monsterId, isPlayer: false, zongjia: true);
			}
		}
		return num;
	}

	private void OnDestroy()
	{
		inst = null;
	}

	public bool TryEscClose()
	{
		if (canClick)
		{
			for (int i = 0; i < closeBtn.onClick.Count; i++)
			{
				if (closeBtn.onClick[i] != null)
				{
					closeBtn.onClick[i].Execute();
				}
			}
			return true;
		}
		return false;
	}
}
