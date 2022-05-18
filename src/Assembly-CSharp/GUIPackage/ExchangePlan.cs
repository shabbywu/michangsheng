using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

namespace GUIPackage
{
	// Token: 0x02000D4E RID: 3406
	public class ExchangePlan : MonoBehaviour
	{
		// Token: 0x060050F6 RID: 20726 RVA: 0x0003A4A3 File Offset: 0x000386A3
		private void Awake()
		{
			Singleton.ints.exchengePlan = this;
			this.init();
		}

		// Token: 0x060050F7 RID: 20727 RVA: 0x0021B910 File Offset: 0x00219B10
		public void init()
		{
			this.avatar = Tools.instance.getPlayer();
			base.gameObject.SetActive(false);
			this.playerInventory = base.transform.Find("Panel/inventoryGui").gameObject;
			this.MonstarInventory = base.transform.Find("Panel/inventoryGuiAvatar").gameObject;
			this.inventoryPlayer = base.transform.Find("Panel/Inventory2").GetComponent<Inventory2>();
			this.inventoryMonstar = base.transform.Find("Panel/Inventory3").GetComponent<Inventory2>();
			this.monstarSayText.SetActive(false);
			this.playerSayText.SetActive(false);
			if (this.type == 1)
			{
				Tools.canClickFlag = false;
			}
		}

		// Token: 0x060050F8 RID: 20728 RVA: 0x0021B9CC File Offset: 0x00219BCC
		public void showPlan()
		{
			base.transform.parent = UI_Manager.inst.gameObject.transform;
			base.transform.localPosition = Vector3.zero;
			base.transform.localScale = new Vector3(0.752f, 0.752f, 1f);
			base.gameObject.SetActive(true);
		}

		// Token: 0x060050F9 RID: 20729 RVA: 0x0021BA30 File Offset: 0x00219C30
		public void PlayerSay(string text)
		{
			this.playerSayText.SetActive(true);
			this.playerSayText.GetComponentInChildren<UILabel>().text = text;
			if (this.type == 1)
			{
				this.PlayerMoney.gameObject.SetActive(false);
				this.playerText.gameObject.SetActive(false);
			}
			base.Invoke("UnActivePlayerText", 1.5f);
		}

		// Token: 0x060050FA RID: 20730 RVA: 0x0003A4B6 File Offset: 0x000386B6
		public void UnActivePlayerText()
		{
			this.playerSayText.SetActive(false);
			if (this.type == 1)
			{
				this.PlayerMoney.gameObject.SetActive(true);
				this.playerText.gameObject.SetActive(true);
			}
		}

		// Token: 0x060050FB RID: 20731 RVA: 0x0021BA98 File Offset: 0x00219C98
		public void MonstarterSay(string text)
		{
			this.monstarSayText.SetActive(true);
			this.monstarSayText.GetComponentInChildren<UILabel>().text = text;
			if (this.type == 1)
			{
				this.MonstarMoney.gameObject.SetActive(false);
				this.MonstarText.gameObject.SetActive(false);
			}
			base.Invoke("UnActiveMonstarterText", 1.5f);
		}

		// Token: 0x060050FC RID: 20732 RVA: 0x0003A4EF File Offset: 0x000386EF
		public void UnActiveMonstarterText()
		{
			this.monstarSayText.SetActive(false);
			if (this.type == 1)
			{
				this.MonstarMoney.gameObject.SetActive(true);
				this.MonstarText.gameObject.SetActive(true);
			}
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x0003A528 File Offset: 0x00038728
		private void Start()
		{
			this.initPlan();
		}

		// Token: 0x060050FE RID: 20734 RVA: 0x0021BB00 File Offset: 0x00219D00
		public void updateMoney()
		{
			int buyMoney = this.GetBuyMoney(this.inventoryMonstar, false);
			int buyMoney2 = this.GetBuyMoney(this.inventoryPlayer, true);
			this.PlayerMoney.Set_money((int)this.avatar.money - buyMoney, false);
			this.MonstarMoney.Set_money((int)jsonData.instance.AvatarBackpackJsonData[string.Concat(this.MonstarID)]["money"].n - buyMoney2, false);
			this.PlayerMoneyPay.Set_money(buyMoney, false);
			this.MonstarMoneyPay.Set_money(buyMoney2, false);
		}

		// Token: 0x060050FF RID: 20735 RVA: 0x0021BB9C File Offset: 0x00219D9C
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
					num += item.GetJiaoYiPrice(this.MonstarID, isPlayer, true);
				}
				num2++;
			}
			return num;
		}

		// Token: 0x06005100 RID: 20736 RVA: 0x0021BC20 File Offset: 0x00219E20
		public virtual void initPlan()
		{
			this.inventoryPlayer.resteAllInventoryItem();
			this.inventoryMonstar.resteAllInventoryItem();
			this.inventoryPlayer.LoadInventory();
			this.inventoryMonstar.MonstarLoadInventory(this.MonstarID);
			this.playerText.text = Tools.getMonstarTitle(1);
			this.MonstarText.text = Tools.getMonstarTitle(this.MonstarID);
			this.MonstarFace.randomAvatar(this.MonstarID);
			try
			{
				this.SetXiHaoItem();
			}
			catch (Exception ex)
			{
				Debug.Log(ex);
			}
			UINPCJiaoHu.AllShouldHide = true;
		}

		// Token: 0x06005101 RID: 20737 RVA: 0x0021BCBC File Offset: 0x00219EBC
		public void SetXiHaoItem()
		{
			if (!jsonData.instance.AvatarBackpackJsonData[string.Concat(this.MonstarID)].HasField("XinQuType"))
			{
				jsonData.instance.MonstarCreatInterstingType(this.MonstarID);
			}
			string text = "感兴趣的物品：";
			foreach (JSONObject jsonobject in jsonData.instance.AvatarBackpackJsonData[string.Concat(this.MonstarID)]["XinQuType"].list)
			{
				string text2 = (string)jsonData.instance.AllItemLeiXin[string.Concat(jsonobject["type"].I)]["name"];
				text = string.Concat(new object[]
				{
					text,
					"\n",
					text2,
					"(+[007b06]",
					jsonobject["percent"].I,
					"[-]%)"
				});
			}
			this.daoHelp.text = text;
		}

		// Token: 0x06005102 RID: 20738 RVA: 0x0003A530 File Offset: 0x00038730
		public virtual void close()
		{
			this.inventoryPlayer.showTooltip = false;
			this.inventoryMonstar.showTooltip = false;
			Object.Destroy(base.gameObject);
			UINPCJiaoHu.AllShouldHide = false;
		}

		// Token: 0x06005103 RID: 20739 RVA: 0x0000EA5D File Offset: 0x0000CC5D
		private void OnDestroy()
		{
			Tools.canClickFlag = true;
		}

		// Token: 0x06005104 RID: 20740 RVA: 0x0021BE00 File Offset: 0x0021A000
		public void confirm()
		{
			int buyMoney = this.GetBuyMoney(this.inventoryMonstar, false);
			int buyMoney2 = this.GetBuyMoney(this.inventoryPlayer, true);
			if (!this.inventoryPlayer.isNewJiaoYi)
			{
				if ((int)this.avatar.money - buyMoney < 0)
				{
					UIPopTip.Inst.Pop("灵石不足无法交易", PopTipIconType.叹号);
					return;
				}
				if ((int)jsonData.instance.AvatarBackpackJsonData[this.MonstarID.ToString()]["money"].n - buyMoney2 < 0)
				{
					UIPopTip.Inst.Pop("灵石不足无法交易", PopTipIconType.叹号);
					return;
				}
			}
			this.avatar.money -= (ulong)((long)buyMoney);
			this.avatar.money += (ulong)((long)buyMoney2);
			jsonData.instance.AvatarBackpackJsonData[string.Concat(this.MonstarID)]["money"].n -= (float)buyMoney2;
			jsonData.instance.AvatarBackpackJsonData[string.Concat(this.MonstarID)]["money"].n += (float)buyMoney;
			int num = 0;
			int num2 = 24;
			if (this.inventoryPlayer.isNewJiaoYi)
			{
				num2 = 15;
			}
			foreach (item item in this.inventoryMonstar.inventory)
			{
				if (num >= num2 && item.itemID > 0)
				{
					Tools.instance.NewAddItem(item.itemID, item.itemNum, item.Seid, "无", false);
					jsonData.instance.MonstarRemoveItem(this.MonstarID, item.UUID, item.itemNum, false);
				}
				num++;
			}
			int num3 = 0;
			foreach (item item2 in this.inventoryPlayer.inventory)
			{
				if (num3 >= num2 && item2.itemID > 0)
				{
					jsonData.instance.MonstarAddItem(this.MonstarID, item2.UUID, item2.itemID, item2.itemNum, item2.Seid, 0);
					Tools.instance.RemoveItem(item2.UUID, item2.itemNum);
				}
				num3++;
			}
			MusicMag.instance.PlayEffectMusic(5, 1f);
			this.initPlan();
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x0003A55B File Offset: 0x0003875B
		public void setExItemLeiXin1()
		{
			this.setItemLeiXin(new List<int>());
		}

		// Token: 0x06005106 RID: 20742 RVA: 0x0003A568 File Offset: 0x00038768
		public void setExItemLeiXin2()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["2"]["ItemFlag"]));
		}

		// Token: 0x06005107 RID: 20743 RVA: 0x0003A593 File Offset: 0x00038793
		public void setExItemLeiXin3()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["3"]["ItemFlag"]));
		}

		// Token: 0x06005108 RID: 20744 RVA: 0x0003A5BE File Offset: 0x000387BE
		public void setExItemLeiXin4()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["4"]["ItemFlag"]));
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x0003A5E9 File Offset: 0x000387E9
		public void setExItemLeiXin5()
		{
			this.setItemLeiXin(new List<int>
			{
				6
			});
		}

		// Token: 0x0600510A RID: 20746 RVA: 0x0003A5FD File Offset: 0x000387FD
		public void setExItemLeiXin6()
		{
			this.setItemLeiXin(new List<int>
			{
				8
			});
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x0003A611 File Offset: 0x00038811
		public void setExItemLeiXin7()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["6"]["ItemFlag"]));
		}

		// Token: 0x0600510C RID: 20748 RVA: 0x0003A63C File Offset: 0x0003883C
		public void setItemLeiXin(List<int> leixin)
		{
			this.inventoryMonstar.inventItemLeiXing = leixin;
			this.inventoryMonstar.MonstarLoadInventory(this.MonstarID);
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x0003A65B File Offset: 0x0003885B
		private void Update()
		{
			if (JiaoYiManager.inst == null)
			{
				this.updateMoney();
			}
		}

		// Token: 0x04005214 RID: 21012
		public GameObject playerInventory;

		// Token: 0x04005215 RID: 21013
		public GameObject MonstarInventory;

		// Token: 0x04005216 RID: 21014
		public Inventory2 inventoryPlayer;

		// Token: 0x04005217 RID: 21015
		public Inventory2 inventoryMonstar;

		// Token: 0x04005218 RID: 21016
		public Money PlayerMoney;

		// Token: 0x04005219 RID: 21017
		public Money MonstarMoney;

		// Token: 0x0400521A RID: 21018
		public Money PlayerMoneyPay;

		// Token: 0x0400521B RID: 21019
		public Money MonstarMoneyPay;

		// Token: 0x0400521C RID: 21020
		public Text playerText;

		// Token: 0x0400521D RID: 21021
		public Text MonstarText;

		// Token: 0x0400521E RID: 21022
		public PlayerSetRandomFace MonstarFace;

		// Token: 0x0400521F RID: 21023
		public selectPage selectPagePlayer;

		// Token: 0x04005220 RID: 21024
		public selectPage selectPageMonstar;

		// Token: 0x04005221 RID: 21025
		public GameObject monstarSayText;

		// Token: 0x04005222 RID: 21026
		public GameObject playerSayText;

		// Token: 0x04005223 RID: 21027
		public WuDaoHelp daoHelp;

		// Token: 0x04005224 RID: 21028
		private Avatar avatar;

		// Token: 0x04005225 RID: 21029
		public int MonstarID;

		// Token: 0x04005226 RID: 21030
		public int type;

		// Token: 0x04005227 RID: 21031
		public Transform UGUITransform;
	}
}
