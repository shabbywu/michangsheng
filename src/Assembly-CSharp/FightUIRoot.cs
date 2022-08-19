using System;
using GUIPackage;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000475 RID: 1141
public class FightUIRoot : MonoBehaviour
{
	// Token: 0x060023A9 RID: 9129 RVA: 0x000F3C54 File Offset: 0x000F1E54
	private void Awake()
	{
		this.slot = (Resources.Load("Slot") as GameObject);
		this.monstarID = Tools.instance.MonstarID;
		this.avatar = Tools.instance.getPlayer();
		this.MonstarJsonData = jsonData.instance.AvatarJsonData[string.Concat(this.monstarID)];
		this.monstarBackpack = jsonData.instance.AvatarBackpackJsonData[string.Concat(this.monstarID)]["Backpack"];
		this.datebase = jsonData.instance.transform.GetComponent<ItemDatebase>();
		this.money = base.transform.Find("Texture/inventoryGuiEX/Win/money").GetComponent<Money>();
		this.label = base.transform.Find("Texture/Label").GetComponent<UILabel>();
		this.confimBtn = base.transform.Find("Texture/close").GetComponent<UIButton>();
	}

	// Token: 0x060023AA RID: 9130 RVA: 0x000F3D50 File Offset: 0x000F1F50
	public void runAway()
	{
		this.money.gameObject.SetActive(false);
		base.transform.Find("Texture/inventoryGuiEX").gameObject.SetActive(false);
		base.transform.Find("Texture/Texture").gameObject.SetActive(false);
		this.TitileText2.text = "逃脱";
		this.TitileText1.gameObject.SetActive(false);
		this.setBG(0);
		int num = this.avatar.dunSu - (int)jsonData.instance.AvatarJsonData[string.Concat(this.monstarID)]["dunSu"].n;
		if (num > 0 && num <= jsonData.instance.RunawayJsonData.Count - 1)
		{
			this.setRunawayText(num);
			this.avatar.AddTime((int)jsonData.instance.RunawayJsonData[string.Concat(num)]["RunTime"].n, 0, 0);
			try
			{
				this.randomMapIndex((int)jsonData.instance.RunawayJsonData[string.Concat(num)]["RunDistance"].n);
				return;
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
				return;
			}
		}
		if (num > 10)
		{
			this.setRunawayText(11);
		}
	}

	// Token: 0x060023AB RID: 9131 RVA: 0x000F3EB8 File Offset: 0x000F20B8
	public void randomMapIndex(int Num)
	{
		int nowMapIndex = this.avatar.NowMapIndex;
		int i = 0;
		while (i < Num)
		{
			BaseMapCompont baseMapCompont = AllMapManage.instance.mapIndex[this.avatar.NowMapIndex];
			if (i == 0)
			{
				int num = jsonData.instance.getRandom() % baseMapCompont.nextIndex.Count;
				goto IL_73;
			}
			if (baseMapCompont.nextIndex.Count > 1)
			{
				int num = jsonData.instance.getRandom() % (baseMapCompont.nextIndex.Count - 1);
				goto IL_73;
			}
			IL_D2:
			i++;
			continue;
			IL_73:
			int num2 = 0;
			foreach (int num3 in baseMapCompont.nextIndex)
			{
				if (nowMapIndex != num3)
				{
					int num;
					if (num == num2)
					{
						nowMapIndex = this.avatar.NowMapIndex;
						this.avatar.NowMapIndex = num3;
						break;
					}
					num2++;
				}
			}
			goto IL_D2;
		}
		AllMapManage.instance.mapIndex[this.avatar.NowMapIndex].AvatarMoveToThis();
	}

	// Token: 0x060023AC RID: 9132 RVA: 0x000F3FD4 File Offset: 0x000F21D4
	public void addAwEquip(ref JSONObject addItemList, JSONObject dropinfo, string euqipName, string equipAvatarName)
	{
		if ((int)dropinfo[euqipName].n > 0 && (int)this.MonstarJsonData[equipAvatarName].n > 0 && jsonData.instance.getRandom() % 100 < (int)dropinfo[euqipName].n)
		{
			this.addTempItem(ref addItemList, (int)this.MonstarJsonData[equipAvatarName].n, 1, null);
		}
	}

	// Token: 0x060023AD RID: 9133 RVA: 0x000F4040 File Offset: 0x000F2240
	public void addBackpackDrop(ref JSONObject addItemList, float DropPercent)
	{
		int num = (int)((float)this.monstarBackpack.Count * DropPercent);
		if (num <= 0)
		{
			return;
		}
		JSONObject jsonobject = new JSONObject();
		int num2 = 0;
		foreach (JSONObject jsonobject2 in this.monstarBackpack.list)
		{
			if ((int)jsonobject2["CanDrop"].n != 0)
			{
				JSONObject jsonobject3 = new JSONObject();
				jsonobject3.AddField("ItemID", jsonobject2["ItemID"].I);
				jsonobject3.AddField("Num", (int)jsonobject2["Num"].n);
				jsonobject3.AddField("index", num2);
				jsonobject.AddField(string.Concat(num2), jsonobject3);
				num2++;
			}
		}
		for (int i = 0; i < num; i++)
		{
			int num3 = 0;
			int num4 = jsonData.instance.getRandom() % jsonobject.Count;
			foreach (JSONObject jsonobject4 in jsonobject.list)
			{
				if (num4 == num3)
				{
					this.addTempItem(ref addItemList, jsonobject4["ItemID"].I, jsonobject4["Num"].I, jsonobject4["Seid"]);
					jsonobject.RemoveField(string.Concat((int)jsonobject4["index"].n));
					break;
				}
				num3++;
			}
		}
	}

	// Token: 0x060023AE RID: 9134 RVA: 0x00004095 File Offset: 0x00002295
	public void showCaiJiItem(int textID)
	{
	}

	// Token: 0x060023AF RID: 9135 RVA: 0x000F41FC File Offset: 0x000F23FC
	public void issueReward()
	{
		UltimateSurvival.MonoSingleton<UI_Backgroud>.Instance.OpenLayer(base.gameObject, true);
		this.confimBtn.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.confim)));
		float num = (float)(Tools.instance.monstarMag.gameStartHP - this.avatar.HP) / (float)this.avatar.HP_Max * 100f;
		int num2 = (int)jsonData.instance.AvatarJsonData[this.monstarID.ToString()]["dropType"].n;
		foreach (JSONObject jsonobject in jsonData.instance.DropInfoJsonData.list)
		{
			if (num2 == (int)jsonobject["dropType"].n && RoundManager.instance != null && (float)RoundManager.instance.StaticRoundNum <= jsonobject["round"].n && num <= jsonobject["loseHp"].n)
			{
				this.setText(Tools.instance.Code64ToString(jsonobject["TextDesc"].str));
				this.setTitle(Tools.instance.Code64ToString(jsonobject["Title"].str));
				this.addMoney(jsonobject["moneydrop"].n / 100f);
				JSONObject jsonobject2 = NpcJieSuanManager.inst.npcFight.npcDrop.dropReward(jsonobject["wepen"].n / 100f, jsonobject["backpack"].n / 100f, this.monstarID);
				foreach (JSONObject jsonobject3 in jsonobject2.list)
				{
					Tools.instance.getPlayer().addItem(jsonobject3["ID"].I, jsonobject3["Num"].I, jsonobject3["seid"], false);
				}
				for (int i = 0; i < jsonobject2.Count; i++)
				{
					this.inventory.inventory.Add(new item());
				}
				this.inventory.InitSlot<ItemCell>("Slot", jsonobject2.Count, delegate(ItemCell aa)
				{
					aa.JustShow = true;
				}, "Win/item");
				this.inventory.loadNewNPCDropInventory(jsonobject2);
				if (Tools.instance.MonstarID >= 20000)
				{
					if (NpcJieSuanManager.inst.isCanJieSuan)
					{
						NpcJieSuanManager.inst.npcDeath.SetNpcDeath(2, Tools.instance.MonstarID, 0, false);
					}
					else
					{
						NpcJieSuanManager.inst.npcDeath.SetNpcDeath(2, Tools.instance.MonstarID, 0, true);
					}
					NpcJieSuanManager.inst.npcMap.RemoveNpcByList(Tools.instance.MonstarID);
					NpcJieSuanManager.inst.isUpDateNpcList = true;
					break;
				}
				Tools.instance.getPlayer().OtherAvatar.die();
				break;
			}
		}
	}

	// Token: 0x060023B0 RID: 9136 RVA: 0x000F4580 File Offset: 0x000F2780
	public void addTempItem(ref JSONObject addItemList, int ItemID, int ItemNum, JSONObject seid = null)
	{
		JSONObject jsonobject = new JSONObject();
		jsonobject.AddField("UUID", Tools.getUUID());
		jsonobject.AddField("ID", ItemID);
		jsonobject.AddField("Num", ItemNum);
		if (seid != null)
		{
			jsonobject.AddField("seid", seid);
		}
		addItemList.Add(jsonobject);
	}

	// Token: 0x060023B1 RID: 9137 RVA: 0x000F45D4 File Offset: 0x000F27D4
	public void addMoney(float percent)
	{
		int num = (int)(jsonData.instance.AvatarBackpackJsonData[string.Concat(this.monstarID)]["money"].n * percent);
		this.avatar.money += (ulong)((long)num);
		this.money.Set_money(num, false);
	}

	// Token: 0x060023B2 RID: 9138 RVA: 0x000F4634 File Offset: 0x000F2834
	public void AvatarAddItem(JSONObject addItemList)
	{
		foreach (JSONObject jsonobject in addItemList.list)
		{
			this.avatar.addItem(jsonobject["ID"].I, jsonobject["Num"].I, Tools.CreateItemSeid(jsonobject["ID"].I), false);
		}
		for (int i = 0; i < addItemList.Count; i++)
		{
			this.inventory.inventory.Add(new item());
		}
		this.inventory.InitSlot<ItemCell>("Slot", addItemList.Count, delegate(ItemCell aa)
		{
			aa.JustShow = true;
		}, "Win/item");
		this.inventory.loadInventory(addItemList);
	}

	// Token: 0x060023B3 RID: 9139 RVA: 0x000F4730 File Offset: 0x000F2930
	public void setText(int type)
	{
		this.label.text = Tools.instance.Code64ToString(jsonData.instance.DropTextJsonData[string.Concat(type)]["Text"].str);
	}

	// Token: 0x060023B4 RID: 9140 RVA: 0x000F4770 File Offset: 0x000F2970
	public void setText(string text)
	{
		this.label.text = text;
	}

	// Token: 0x060023B5 RID: 9141 RVA: 0x000F4780 File Offset: 0x000F2980
	public void setTitle(string text)
	{
		if (text == "碾压" || text == "胜利")
		{
			this.TitileText1.gameObject.SetActive(true);
			this.TitileText2.gameObject.SetActive(false);
			this.TitileText1.text = text;
			this.setBG(1);
			return;
		}
		this.TitileText1.gameObject.SetActive(false);
		this.TitileText2.gameObject.SetActive(true);
		this.TitileText2.text = text;
		this.setBG(0);
	}

	// Token: 0x060023B6 RID: 9142 RVA: 0x000F4812 File Offset: 0x000F2A12
	public void setBG(int d)
	{
		if (d == 0)
		{
			this.uISprite.spriteName = "tanchuanghui";
			return;
		}
		if (d != 1)
		{
			return;
		}
		this.uISprite.spriteName = "tanchuangliang";
	}

	// Token: 0x060023B7 RID: 9143 RVA: 0x000F4840 File Offset: 0x000F2A40
	public void setRunawayText(int type)
	{
		int staticDunSu = Tools.instance.getPlayer().getStaticDunSu();
		string newValue = (staticDunSu > 0) ? ("运起" + Tools.instance.getStaticSkillName(staticDunSu, false) + "，") : "";
		this.label.text = Tools.instance.Code64ToString(jsonData.instance.RunawayJsonData[string.Concat(type)]["Text"].str).Replace("X", string.Concat((int)jsonData.instance.RunawayJsonData[string.Concat(type)]["RunTime"].n)).Replace("运起（dunshu），", newValue);
	}

	// Token: 0x060023B8 RID: 9144 RVA: 0x000F490B File Offset: 0x000F2B0B
	public void confim()
	{
		PanelMamager.CanOpenOrClose = true;
		Tools.instance.loadMapScenes(Tools.instance.FinalScene, true);
	}

	// Token: 0x060023B9 RID: 9145 RVA: 0x00004095 File Offset: 0x00002295
	private void Update()
	{
	}

	// Token: 0x04001C88 RID: 7304
	public Inventory2 inventory;

	// Token: 0x04001C89 RID: 7305
	public UIButton confimBtn;

	// Token: 0x04001C8A RID: 7306
	private Money money;

	// Token: 0x04001C8B RID: 7307
	private Avatar avatar;

	// Token: 0x04001C8C RID: 7308
	private int monstarID;

	// Token: 0x04001C8D RID: 7309
	private JSONObject MonstarJsonData;

	// Token: 0x04001C8E RID: 7310
	private JSONObject monstarBackpack;

	// Token: 0x04001C8F RID: 7311
	private GameObject slot;

	// Token: 0x04001C90 RID: 7312
	private ItemDatebase datebase;

	// Token: 0x04001C91 RID: 7313
	public UILabel label;

	// Token: 0x04001C92 RID: 7314
	public Text TitileText1;

	// Token: 0x04001C93 RID: 7315
	public Text TitileText2;

	// Token: 0x04001C94 RID: 7316
	public UISprite uISprite;
}
