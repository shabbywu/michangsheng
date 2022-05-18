using System;
using GUIPackage;
using KBEngine;
using UltimateSurvival;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000632 RID: 1586
public class FightUIRoot : MonoBehaviour
{
	// Token: 0x06002762 RID: 10082 RVA: 0x00133C00 File Offset: 0x00131E00
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

	// Token: 0x06002763 RID: 10083 RVA: 0x00133CFC File Offset: 0x00131EFC
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

	// Token: 0x06002764 RID: 10084 RVA: 0x00133E64 File Offset: 0x00132064
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

	// Token: 0x06002765 RID: 10085 RVA: 0x00133F80 File Offset: 0x00132180
	public void addAwEquip(ref JSONObject addItemList, JSONObject dropinfo, string euqipName, string equipAvatarName)
	{
		if ((int)dropinfo[euqipName].n > 0 && (int)this.MonstarJsonData[equipAvatarName].n > 0 && jsonData.instance.getRandom() % 100 < (int)dropinfo[euqipName].n)
		{
			this.addTempItem(ref addItemList, (int)this.MonstarJsonData[equipAvatarName].n, 1, null);
		}
	}

	// Token: 0x06002766 RID: 10086 RVA: 0x00133FEC File Offset: 0x001321EC
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
				jsonobject3.AddField("ItemID", (int)jsonobject2["ItemID"].n);
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
					this.addTempItem(ref addItemList, (int)jsonobject4["ItemID"].n, (int)jsonobject4["Num"].n, jsonobject4["Seid"]);
					jsonobject.RemoveField(string.Concat((int)jsonobject4["index"].n));
					break;
				}
				num3++;
			}
		}
	}

	// Token: 0x06002767 RID: 10087 RVA: 0x000042DD File Offset: 0x000024DD
	public void showCaiJiItem(int textID)
	{
	}

	// Token: 0x06002768 RID: 10088 RVA: 0x001341AC File Offset: 0x001323AC
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
					Tools.instance.getPlayer().addItem((int)jsonobject3["ID"].n, (int)jsonobject3["Num"].n, jsonobject3["seid"], false);
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

	// Token: 0x06002769 RID: 10089 RVA: 0x00134534 File Offset: 0x00132734
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

	// Token: 0x0600276A RID: 10090 RVA: 0x00134588 File Offset: 0x00132788
	public void addMoney(float percent)
	{
		int num = (int)(jsonData.instance.AvatarBackpackJsonData[string.Concat(this.monstarID)]["money"].n * percent);
		this.avatar.money += (ulong)((long)num);
		this.money.Set_money(num, false);
	}

	// Token: 0x0600276B RID: 10091 RVA: 0x001345E8 File Offset: 0x001327E8
	public void AvatarAddItem(JSONObject addItemList)
	{
		foreach (JSONObject jsonobject in addItemList.list)
		{
			this.avatar.addItem((int)jsonobject["ID"].n, (int)jsonobject["Num"].n, Tools.CreateItemSeid((int)jsonobject["ID"].n), false);
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

	// Token: 0x0600276C RID: 10092 RVA: 0x0001F3A5 File Offset: 0x0001D5A5
	public void setText(int type)
	{
		this.label.text = Tools.instance.Code64ToString(jsonData.instance.DropTextJsonData[string.Concat(type)]["Text"].str);
	}

	// Token: 0x0600276D RID: 10093 RVA: 0x0001F3E5 File Offset: 0x0001D5E5
	public void setText(string text)
	{
		this.label.text = text;
	}

	// Token: 0x0600276E RID: 10094 RVA: 0x001346E4 File Offset: 0x001328E4
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

	// Token: 0x0600276F RID: 10095 RVA: 0x0001F3F3 File Offset: 0x0001D5F3
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

	// Token: 0x06002770 RID: 10096 RVA: 0x00134778 File Offset: 0x00132978
	public void setRunawayText(int type)
	{
		int staticDunSu = Tools.instance.getPlayer().getStaticDunSu();
		string newValue = (staticDunSu > 0) ? ("运起" + Tools.instance.getStaticSkillName(staticDunSu, false) + "，") : "";
		this.label.text = Tools.instance.Code64ToString(jsonData.instance.RunawayJsonData[string.Concat(type)]["Text"].str).Replace("X", string.Concat((int)jsonData.instance.RunawayJsonData[string.Concat(type)]["RunTime"].n)).Replace("运起（dunshu），", newValue);
	}

	// Token: 0x06002771 RID: 10097 RVA: 0x0001F41E File Offset: 0x0001D61E
	public void confim()
	{
		PanelMamager.CanOpenOrClose = true;
		Tools.instance.loadMapScenes(Tools.instance.FinalScene, true);
	}

	// Token: 0x06002772 RID: 10098 RVA: 0x000042DD File Offset: 0x000024DD
	private void Update()
	{
	}

	// Token: 0x04002160 RID: 8544
	public Inventory2 inventory;

	// Token: 0x04002161 RID: 8545
	public UIButton confimBtn;

	// Token: 0x04002162 RID: 8546
	private Money money;

	// Token: 0x04002163 RID: 8547
	private Avatar avatar;

	// Token: 0x04002164 RID: 8548
	private int monstarID;

	// Token: 0x04002165 RID: 8549
	private JSONObject MonstarJsonData;

	// Token: 0x04002166 RID: 8550
	private JSONObject monstarBackpack;

	// Token: 0x04002167 RID: 8551
	private GameObject slot;

	// Token: 0x04002168 RID: 8552
	private ItemDatebase datebase;

	// Token: 0x04002169 RID: 8553
	public UILabel label;

	// Token: 0x0400216A RID: 8554
	public Text TitileText1;

	// Token: 0x0400216B RID: 8555
	public Text TitileText2;

	// Token: 0x0400216C RID: 8556
	public UISprite uISprite;
}
