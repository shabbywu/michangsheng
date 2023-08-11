using System;
using GUIPackage;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.UI;

public class FightUIRoot : MonoBehaviour
{
	public Inventory2 inventory;

	public UIButton confimBtn;

	private Money money;

	private Avatar avatar;

	private int monstarID;

	private JSONObject MonstarJsonData;

	private JSONObject monstarBackpack;

	private GameObject slot;

	private ItemDatebase datebase;

	public UILabel label;

	public Text TitileText1;

	public Text TitileText2;

	public UISprite uISprite;

	private void Awake()
	{
		ref GameObject reference = ref slot;
		Object obj = Resources.Load("Slot");
		reference = (GameObject)(object)((obj is GameObject) ? obj : null);
		monstarID = Tools.instance.MonstarID;
		avatar = Tools.instance.getPlayer();
		MonstarJsonData = jsonData.instance.AvatarJsonData[string.Concat(monstarID)];
		monstarBackpack = jsonData.instance.AvatarBackpackJsonData[string.Concat(monstarID)]["Backpack"];
		datebase = ((Component)((Component)jsonData.instance).transform).GetComponent<ItemDatebase>();
		money = ((Component)((Component)this).transform.Find("Texture/inventoryGuiEX/Win/money")).GetComponent<Money>();
		label = ((Component)((Component)this).transform.Find("Texture/Label")).GetComponent<UILabel>();
		confimBtn = ((Component)((Component)this).transform.Find("Texture/close")).GetComponent<UIButton>();
	}

	public void runAway()
	{
		((Component)money).gameObject.SetActive(false);
		((Component)((Component)this).transform.Find("Texture/inventoryGuiEX")).gameObject.SetActive(false);
		((Component)((Component)this).transform.Find("Texture/Texture")).gameObject.SetActive(false);
		TitileText2.text = "逃脱";
		((Component)TitileText1).gameObject.SetActive(false);
		setBG(0);
		int num = avatar.dunSu - (int)jsonData.instance.AvatarJsonData[string.Concat(monstarID)]["dunSu"].n;
		if (num > 0 && num <= jsonData.instance.RunawayJsonData.Count - 1)
		{
			setRunawayText(num);
			avatar.AddTime((int)jsonData.instance.RunawayJsonData[string.Concat(num)]["RunTime"].n);
			try
			{
				randomMapIndex((int)jsonData.instance.RunawayJsonData[string.Concat(num)]["RunDistance"].n);
				return;
			}
			catch (Exception ex)
			{
				Debug.LogError((object)ex);
				return;
			}
		}
		if (num > 10)
		{
			setRunawayText(11);
		}
	}

	public void randomMapIndex(int Num)
	{
		int nowMapIndex = avatar.NowMapIndex;
		for (int i = 0; i < Num; i++)
		{
			BaseMapCompont baseMapCompont = AllMapManage.instance.mapIndex[avatar.NowMapIndex];
			int num = 0;
			if (i == 0)
			{
				num = jsonData.instance.getRandom() % baseMapCompont.nextIndex.Count;
			}
			else
			{
				if (baseMapCompont.nextIndex.Count <= 1)
				{
					continue;
				}
				num = jsonData.instance.getRandom() % (baseMapCompont.nextIndex.Count - 1);
			}
			int num2 = 0;
			foreach (int item in baseMapCompont.nextIndex)
			{
				if (nowMapIndex != item)
				{
					if (num == num2)
					{
						nowMapIndex = avatar.NowMapIndex;
						avatar.NowMapIndex = item;
						break;
					}
					num2++;
				}
			}
		}
		AllMapManage.instance.mapIndex[avatar.NowMapIndex].AvatarMoveToThis();
	}

	public void addAwEquip(ref JSONObject addItemList, JSONObject dropinfo, string euqipName, string equipAvatarName)
	{
		if ((int)dropinfo[euqipName].n > 0 && (int)MonstarJsonData[equipAvatarName].n > 0 && jsonData.instance.getRandom() % 100 < (int)dropinfo[euqipName].n)
		{
			addTempItem(ref addItemList, (int)MonstarJsonData[equipAvatarName].n, 1);
		}
	}

	public void addBackpackDrop(ref JSONObject addItemList, float DropPercent)
	{
		int num = (int)((float)monstarBackpack.Count * DropPercent);
		if (num <= 0)
		{
			return;
		}
		JSONObject jSONObject = new JSONObject();
		int num2 = 0;
		foreach (JSONObject item in monstarBackpack.list)
		{
			if ((int)item["CanDrop"].n != 0)
			{
				JSONObject jSONObject2 = new JSONObject();
				jSONObject2.AddField("ItemID", item["ItemID"].I);
				jSONObject2.AddField("Num", (int)item["Num"].n);
				jSONObject2.AddField("index", num2);
				jSONObject.AddField(string.Concat(num2), jSONObject2);
				num2++;
			}
		}
		for (int i = 0; i < num; i++)
		{
			int num3 = 0;
			int num4 = jsonData.instance.getRandom() % jSONObject.Count;
			foreach (JSONObject item2 in jSONObject.list)
			{
				if (num4 == num3)
				{
					addTempItem(ref addItemList, item2["ItemID"].I, item2["Num"].I, item2["Seid"]);
					jSONObject.RemoveField(string.Concat((int)item2["index"].n));
					break;
				}
				num3++;
			}
		}
	}

	public void showCaiJiItem(int textID)
	{
	}

	public void issueReward()
	{
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.OpenLayer(((Component)this).gameObject, _v: true);
		confimBtn.onClick.Add(new EventDelegate(confim));
		float num = (float)(Tools.instance.monstarMag.gameStartHP - avatar.HP) / (float)avatar.HP_Max * 100f;
		int num2 = (int)jsonData.instance.AvatarJsonData[monstarID.ToString()]["dropType"].n;
		foreach (JSONObject item in jsonData.instance.DropInfoJsonData.list)
		{
			if (num2 != (int)item["dropType"].n || !((Object)(object)RoundManager.instance != (Object)null) || !((float)RoundManager.instance.StaticRoundNum <= item["round"].n) || !(num <= item["loseHp"].n))
			{
				continue;
			}
			setText(Tools.instance.Code64ToString(item["TextDesc"].str));
			setTitle(Tools.instance.Code64ToString(item["Title"].str));
			addMoney(item["moneydrop"].n / 100f);
			JSONObject jSONObject = NpcJieSuanManager.inst.npcFight.npcDrop.dropReward(item["wepen"].n / 100f, item["backpack"].n / 100f, monstarID);
			foreach (JSONObject item2 in jSONObject.list)
			{
				Tools.instance.getPlayer().addItem(item2["ID"].I, item2["Num"].I, item2["seid"]);
			}
			for (int i = 0; i < jSONObject.Count; i++)
			{
				inventory.inventory.Add(new item());
			}
			inventory.InitSlot("Slot", jSONObject.Count, delegate(ItemCell aa)
			{
				aa.JustShow = true;
			});
			inventory.loadNewNPCDropInventory(jSONObject);
			if (Tools.instance.MonstarID >= 20000)
			{
				if (NpcJieSuanManager.inst.isCanJieSuan)
				{
					NpcJieSuanManager.inst.npcDeath.SetNpcDeath(2, Tools.instance.MonstarID);
				}
				else
				{
					NpcJieSuanManager.inst.npcDeath.SetNpcDeath(2, Tools.instance.MonstarID, 0, after: true);
				}
				NpcJieSuanManager.inst.npcMap.RemoveNpcByList(Tools.instance.MonstarID);
				NpcJieSuanManager.inst.isUpDateNpcList = true;
			}
			else
			{
				Tools.instance.getPlayer().OtherAvatar.die();
			}
			break;
		}
	}

	public void addTempItem(ref JSONObject addItemList, int ItemID, int ItemNum, JSONObject seid = null)
	{
		JSONObject jSONObject = new JSONObject();
		jSONObject.AddField("UUID", Tools.getUUID());
		jSONObject.AddField("ID", ItemID);
		jSONObject.AddField("Num", ItemNum);
		if (seid != null)
		{
			jSONObject.AddField("seid", seid);
		}
		addItemList.Add(jSONObject);
	}

	public void addMoney(float percent)
	{
		int num = (int)(jsonData.instance.AvatarBackpackJsonData[string.Concat(monstarID)]["money"].n * percent);
		avatar.money += (ulong)num;
		money.Set_money(num);
	}

	public void AvatarAddItem(JSONObject addItemList)
	{
		foreach (JSONObject item in addItemList.list)
		{
			avatar.addItem(item["ID"].I, item["Num"].I, Tools.CreateItemSeid(item["ID"].I));
		}
		for (int i = 0; i < addItemList.Count; i++)
		{
			inventory.inventory.Add(new item());
		}
		inventory.InitSlot("Slot", addItemList.Count, delegate(ItemCell aa)
		{
			aa.JustShow = true;
		});
		inventory.loadInventory(addItemList);
	}

	public void setText(int type)
	{
		label.text = Tools.instance.Code64ToString(jsonData.instance.DropTextJsonData[string.Concat(type)]["Text"].str);
	}

	public void setText(string text)
	{
		label.text = text;
	}

	public void setTitle(string text)
	{
		if (text == "碾压" || text == "胜利")
		{
			((Component)TitileText1).gameObject.SetActive(true);
			((Component)TitileText2).gameObject.SetActive(false);
			TitileText1.text = text;
			setBG(1);
		}
		else
		{
			((Component)TitileText1).gameObject.SetActive(false);
			((Component)TitileText2).gameObject.SetActive(true);
			TitileText2.text = text;
			setBG(0);
		}
	}

	public void setBG(int d)
	{
		switch (d)
		{
		case 0:
			uISprite.spriteName = "tanchuanghui";
			break;
		case 1:
			uISprite.spriteName = "tanchuangliang";
			break;
		}
	}

	public void setRunawayText(int type)
	{
		int staticDunSu = Tools.instance.getPlayer().getStaticDunSu();
		string newValue = ((staticDunSu > 0) ? ("运起" + Tools.instance.getStaticSkillName(staticDunSu) + "，") : "");
		label.text = Tools.instance.Code64ToString(jsonData.instance.RunawayJsonData[string.Concat(type)]["Text"].str).Replace("X", string.Concat((int)jsonData.instance.RunawayJsonData[string.Concat(type)]["RunTime"].n)).Replace("运起（dunshu），", newValue);
	}

	public void confim()
	{
		PanelMamager.CanOpenOrClose = true;
		Tools.instance.loadMapScenes(Tools.instance.FinalScene);
	}

	private void Update()
	{
	}
}
