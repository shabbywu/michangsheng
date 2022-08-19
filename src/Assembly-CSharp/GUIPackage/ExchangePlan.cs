using System;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;
using UnityEngine.UI;
using YSGame;

namespace GUIPackage
{
	// Token: 0x02000A4A RID: 2634
	public class ExchangePlan : MonoBehaviour
	{
		// Token: 0x06004839 RID: 18489 RVA: 0x001E7AF4 File Offset: 0x001E5CF4
		private void Awake()
		{
			Singleton.ints.exchengePlan = this;
			this.init();
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x001E7B08 File Offset: 0x001E5D08
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

		// Token: 0x0600483B RID: 18491 RVA: 0x001E7BC4 File Offset: 0x001E5DC4
		public void showPlan()
		{
			base.transform.parent = UI_Manager.inst.gameObject.transform;
			base.transform.localPosition = Vector3.zero;
			base.transform.localScale = new Vector3(0.752f, 0.752f, 1f);
			base.gameObject.SetActive(true);
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x001E7C28 File Offset: 0x001E5E28
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

		// Token: 0x0600483D RID: 18493 RVA: 0x001E7C8D File Offset: 0x001E5E8D
		public void UnActivePlayerText()
		{
			this.playerSayText.SetActive(false);
			if (this.type == 1)
			{
				this.PlayerMoney.gameObject.SetActive(true);
				this.playerText.gameObject.SetActive(true);
			}
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x001E7CC8 File Offset: 0x001E5EC8
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

		// Token: 0x0600483F RID: 18495 RVA: 0x001E7D2D File Offset: 0x001E5F2D
		public void UnActiveMonstarterText()
		{
			this.monstarSayText.SetActive(false);
			if (this.type == 1)
			{
				this.MonstarMoney.gameObject.SetActive(true);
				this.MonstarText.gameObject.SetActive(true);
			}
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x001E7D66 File Offset: 0x001E5F66
		private void Start()
		{
			this.initPlan();
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x001E7D70 File Offset: 0x001E5F70
		public void updateMoney()
		{
			int buyMoney = this.GetBuyMoney(this.inventoryMonstar, false);
			int buyMoney2 = this.GetBuyMoney(this.inventoryPlayer, true);
			this.PlayerMoney.Set_money((int)this.avatar.money - buyMoney, false);
			this.MonstarMoney.Set_money((int)jsonData.instance.AvatarBackpackJsonData[string.Concat(this.MonstarID)]["money"].n - buyMoney2, false);
			this.PlayerMoneyPay.Set_money(buyMoney, false);
			this.MonstarMoneyPay.Set_money(buyMoney2, false);
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x001E7E0C File Offset: 0x001E600C
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

		// Token: 0x06004843 RID: 18499 RVA: 0x001E7E90 File Offset: 0x001E6090
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

		// Token: 0x06004844 RID: 18500 RVA: 0x001E7F2C File Offset: 0x001E612C
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

		// Token: 0x06004845 RID: 18501 RVA: 0x001E8070 File Offset: 0x001E6270
		public virtual void close()
		{
			this.inventoryPlayer.showTooltip = false;
			this.inventoryMonstar.showTooltip = false;
			Object.Destroy(base.gameObject);
			UINPCJiaoHu.AllShouldHide = false;
		}

		// Token: 0x06004846 RID: 18502 RVA: 0x00047BD2 File Offset: 0x00045DD2
		private void OnDestroy()
		{
			Tools.canClickFlag = true;
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x001E809C File Offset: 0x001E629C
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

		// Token: 0x06004848 RID: 18504 RVA: 0x001E8330 File Offset: 0x001E6530
		public void setExItemLeiXin1()
		{
			this.setItemLeiXin(new List<int>());
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x001E833D File Offset: 0x001E653D
		public void setExItemLeiXin2()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["2"]["ItemFlag"]));
		}

		// Token: 0x0600484A RID: 18506 RVA: 0x001E8368 File Offset: 0x001E6568
		public void setExItemLeiXin3()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["3"]["ItemFlag"]));
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x001E8393 File Offset: 0x001E6593
		public void setExItemLeiXin4()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["4"]["ItemFlag"]));
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x001E83BE File Offset: 0x001E65BE
		public void setExItemLeiXin5()
		{
			this.setItemLeiXin(new List<int>
			{
				6
			});
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x001E83D2 File Offset: 0x001E65D2
		public void setExItemLeiXin6()
		{
			this.setItemLeiXin(new List<int>
			{
				8
			});
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x001E83E6 File Offset: 0x001E65E6
		public void setExItemLeiXin7()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["6"]["ItemFlag"]));
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x001E8411 File Offset: 0x001E6611
		public void setItemLeiXin(List<int> leixin)
		{
			this.inventoryMonstar.inventItemLeiXing = leixin;
			this.inventoryMonstar.MonstarLoadInventory(this.MonstarID);
		}

		// Token: 0x06004850 RID: 18512 RVA: 0x001E8430 File Offset: 0x001E6630
		private void Update()
		{
			if (JiaoYiManager.inst == null)
			{
				this.updateMoney();
			}
		}

		// Token: 0x040048DD RID: 18653
		public GameObject playerInventory;

		// Token: 0x040048DE RID: 18654
		public GameObject MonstarInventory;

		// Token: 0x040048DF RID: 18655
		public Inventory2 inventoryPlayer;

		// Token: 0x040048E0 RID: 18656
		public Inventory2 inventoryMonstar;

		// Token: 0x040048E1 RID: 18657
		public Money PlayerMoney;

		// Token: 0x040048E2 RID: 18658
		public Money MonstarMoney;

		// Token: 0x040048E3 RID: 18659
		public Money PlayerMoneyPay;

		// Token: 0x040048E4 RID: 18660
		public Money MonstarMoneyPay;

		// Token: 0x040048E5 RID: 18661
		public Text playerText;

		// Token: 0x040048E6 RID: 18662
		public Text MonstarText;

		// Token: 0x040048E7 RID: 18663
		public PlayerSetRandomFace MonstarFace;

		// Token: 0x040048E8 RID: 18664
		public selectPage selectPagePlayer;

		// Token: 0x040048E9 RID: 18665
		public selectPage selectPageMonstar;

		// Token: 0x040048EA RID: 18666
		public GameObject monstarSayText;

		// Token: 0x040048EB RID: 18667
		public GameObject playerSayText;

		// Token: 0x040048EC RID: 18668
		public WuDaoHelp daoHelp;

		// Token: 0x040048ED RID: 18669
		private Avatar avatar;

		// Token: 0x040048EE RID: 18670
		public int MonstarID;

		// Token: 0x040048EF RID: 18671
		public int type;

		// Token: 0x040048F0 RID: 18672
		public Transform UGUITransform;
	}
}
