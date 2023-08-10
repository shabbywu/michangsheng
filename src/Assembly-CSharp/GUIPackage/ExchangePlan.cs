using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

namespace GUIPackage;

public class ExchangePlan : MonoBehaviour
{
	public GameObject playerInventory;

	public GameObject MonstarInventory;

	public Inventory2 inventoryPlayer;

	public Inventory2 inventoryMonstar;

	public Money PlayerMoney;

	public Money MonstarMoney;

	public Money PlayerMoneyPay;

	public Money MonstarMoneyPay;

	public Text playerText;

	public Text MonstarText;

	public PlayerSetRandomFace MonstarFace;

	public selectPage selectPagePlayer;

	public selectPage selectPageMonstar;

	public GameObject monstarSayText;

	public GameObject playerSayText;

	public WuDaoHelp daoHelp;

	private Avatar avatar;

	public int MonstarID;

	public int type;

	public Transform UGUITransform;

	private void Awake()
	{
		Singleton.ints.exchengePlan = this;
		init();
	}

	public void init()
	{
		avatar = Tools.instance.getPlayer();
		((Component)this).gameObject.SetActive(false);
		playerInventory = ((Component)((Component)this).transform.Find("Panel/inventoryGui")).gameObject;
		MonstarInventory = ((Component)((Component)this).transform.Find("Panel/inventoryGuiAvatar")).gameObject;
		inventoryPlayer = ((Component)((Component)this).transform.Find("Panel/Inventory2")).GetComponent<Inventory2>();
		inventoryMonstar = ((Component)((Component)this).transform.Find("Panel/Inventory3")).GetComponent<Inventory2>();
		monstarSayText.SetActive(false);
		playerSayText.SetActive(false);
		if (type == 1)
		{
			Tools.canClickFlag = false;
		}
	}

	public void showPlan()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		((Component)this).transform.parent = ((Component)UI_Manager.inst).gameObject.transform;
		((Component)this).transform.localPosition = Vector3.zero;
		((Component)this).transform.localScale = new Vector3(0.752f, 0.752f, 1f);
		((Component)this).gameObject.SetActive(true);
	}

	public void PlayerSay(string text)
	{
		playerSayText.SetActive(true);
		playerSayText.GetComponentInChildren<UILabel>().text = text;
		if (type == 1)
		{
			((Component)PlayerMoney).gameObject.SetActive(false);
			((Component)playerText).gameObject.SetActive(false);
		}
		((MonoBehaviour)this).Invoke("UnActivePlayerText", 1.5f);
	}

	public void UnActivePlayerText()
	{
		playerSayText.SetActive(false);
		if (type == 1)
		{
			((Component)PlayerMoney).gameObject.SetActive(true);
			((Component)playerText).gameObject.SetActive(true);
		}
	}

	public void MonstarterSay(string text)
	{
		monstarSayText.SetActive(true);
		monstarSayText.GetComponentInChildren<UILabel>().text = text;
		if (type == 1)
		{
			((Component)MonstarMoney).gameObject.SetActive(false);
			((Component)MonstarText).gameObject.SetActive(false);
		}
		((MonoBehaviour)this).Invoke("UnActiveMonstarterText", 1.5f);
	}

	public void UnActiveMonstarterText()
	{
		monstarSayText.SetActive(false);
		if (type == 1)
		{
			((Component)MonstarMoney).gameObject.SetActive(true);
			((Component)MonstarText).gameObject.SetActive(true);
		}
	}

	private void Start()
	{
		initPlan();
	}

	public void updateMoney()
	{
		int buyMoney = GetBuyMoney(inventoryMonstar, isPlayer: false);
		int buyMoney2 = GetBuyMoney(inventoryPlayer, isPlayer: true);
		PlayerMoney.Set_money((int)avatar.money - buyMoney);
		MonstarMoney.Set_money((int)jsonData.instance.AvatarBackpackJsonData[string.Concat(MonstarID)]["money"].n - buyMoney2);
		PlayerMoneyPay.Set_money(buyMoney);
		MonstarMoneyPay.Set_money(buyMoney2);
	}

	public int GetBuyMoney(Inventory2 tempInventory, bool isPlayer)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 24;
		if (tempInventory.isNewJiaoYi)
		{
			num3 = 15;
		}
		foreach (item item in tempInventory.inventory)
		{
			if (num2 >= num3 && item.itemID > 0)
			{
				num += item.GetJiaoYiPrice(MonstarID, isPlayer, zongjia: true);
			}
			num2++;
		}
		return num;
	}

	public virtual void initPlan()
	{
		inventoryPlayer.resteAllInventoryItem();
		inventoryMonstar.resteAllInventoryItem();
		inventoryPlayer.LoadInventory();
		inventoryMonstar.MonstarLoadInventory(MonstarID);
		playerText.text = Tools.getMonstarTitle(1);
		MonstarText.text = Tools.getMonstarTitle(MonstarID);
		MonstarFace.randomAvatar(MonstarID);
		try
		{
			SetXiHaoItem();
		}
		catch (Exception ex)
		{
			Debug.Log((object)ex);
		}
		UINPCJiaoHu.AllShouldHide = true;
	}

	public void SetXiHaoItem()
	{
		if (!jsonData.instance.AvatarBackpackJsonData[string.Concat(MonstarID)].HasField("XinQuType"))
		{
			jsonData.instance.MonstarCreatInterstingType(MonstarID);
		}
		string text = "感兴趣的物品：";
		foreach (JSONObject item in jsonData.instance.AvatarBackpackJsonData[string.Concat(MonstarID)]["XinQuType"].list)
		{
			string text2 = (string)jsonData.instance.AllItemLeiXin[string.Concat(item["type"].I)][(object)"name"];
			text = text + "\n" + text2 + "(+[007b06]" + item["percent"].I + "[-]%)";
		}
		daoHelp.text = text;
	}

	public virtual void close()
	{
		inventoryPlayer.showTooltip = false;
		inventoryMonstar.showTooltip = false;
		Object.Destroy((Object)(object)((Component)this).gameObject);
		UINPCJiaoHu.AllShouldHide = false;
	}

	private void OnDestroy()
	{
		Tools.canClickFlag = true;
	}

	public void confirm()
	{
		int buyMoney = GetBuyMoney(inventoryMonstar, isPlayer: false);
		int buyMoney2 = GetBuyMoney(inventoryPlayer, isPlayer: true);
		if (!inventoryPlayer.isNewJiaoYi)
		{
			if ((int)avatar.money - buyMoney < 0)
			{
				UIPopTip.Inst.Pop("灵石不足无法交易");
				return;
			}
			if ((int)jsonData.instance.AvatarBackpackJsonData[MonstarID.ToString()]["money"].n - buyMoney2 < 0)
			{
				UIPopTip.Inst.Pop("灵石不足无法交易");
				return;
			}
		}
		avatar.money -= (ulong)buyMoney;
		avatar.money += (ulong)buyMoney2;
		jsonData.instance.AvatarBackpackJsonData[string.Concat(MonstarID)]["money"].n -= buyMoney2;
		jsonData.instance.AvatarBackpackJsonData[string.Concat(MonstarID)]["money"].n += buyMoney;
		int num = 0;
		int num2 = 24;
		if (inventoryPlayer.isNewJiaoYi)
		{
			num2 = 15;
		}
		foreach (item item in inventoryMonstar.inventory)
		{
			if (num >= num2 && item.itemID > 0)
			{
				Tools.instance.NewAddItem(item.itemID, item.itemNum, item.Seid);
				jsonData.instance.MonstarRemoveItem(MonstarID, item.UUID, item.itemNum);
			}
			num++;
		}
		int num3 = 0;
		foreach (item item2 in inventoryPlayer.inventory)
		{
			if (num3 >= num2 && item2.itemID > 0)
			{
				jsonData.instance.MonstarAddItem(MonstarID, item2.UUID, item2.itemID, item2.itemNum, item2.Seid);
				Tools.instance.RemoveItem(item2.UUID, item2.itemNum);
			}
			num3++;
		}
		MusicMag.instance.PlayEffectMusic(5);
		initPlan();
	}

	public void setExItemLeiXin1()
	{
		setItemLeiXin(new List<int>());
	}

	public void setExItemLeiXin2()
	{
		setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["2"]["ItemFlag"]));
	}

	public void setExItemLeiXin3()
	{
		setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["3"]["ItemFlag"]));
	}

	public void setExItemLeiXin4()
	{
		setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["4"]["ItemFlag"]));
	}

	public void setExItemLeiXin5()
	{
		setItemLeiXin(new List<int> { 6 });
	}

	public void setExItemLeiXin6()
	{
		setItemLeiXin(new List<int> { 8 });
	}

	public void setExItemLeiXin7()
	{
		setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["6"]["ItemFlag"]));
	}

	public void setItemLeiXin(List<int> leixin)
	{
		inventoryMonstar.inventItemLeiXing = leixin;
		inventoryMonstar.MonstarLoadInventory(MonstarID);
	}

	private void Update()
	{
		if ((Object)(object)JiaoYiManager.inst == (Object)null)
		{
			updateMoney();
		}
	}
}
