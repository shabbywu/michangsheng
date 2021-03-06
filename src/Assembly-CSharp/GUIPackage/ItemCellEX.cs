using System;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D4F RID: 3407
	public class ItemCellEX : ItemCell
	{
		// Token: 0x0600510F RID: 20751 RVA: 0x0021C094 File Offset: 0x0021A294
		private void Start()
		{
			this.Icon = base.transform.Find("Icon").gameObject;
			this.Num = base.transform.Find("num").gameObject;
			this.parentPlane = base.transform.parent.parent.parent.parent.parent;
			if (this.inventory == null)
			{
				if (this.isFight)
				{
					this.inventory = GameObject.Find("FightUIRoot(Clone)/Texture/Inventory2").GetComponent<Inventory2>();
					return;
				}
				if (this.isPlayer)
				{
					this.inventory = this.parentPlane.transform.Find("Panel/Inventory2").GetComponent<Inventory2>();
					this.key = this.parentPlane.transform.Find("Panel/short cutEX/Key").GetComponent<Key>();
					this.Item = this.inventory.inventory[int.Parse(base.name)];
					return;
				}
				this.inventory = this.parentPlane.transform.Find("Panel/Inventory3").GetComponent<Inventory2>();
				this.key = this.parentPlane.transform.Find("Panel/short cutEXAvatar/Key").GetComponent<Key>();
				this.Item = this.inventory.inventory[int.Parse(base.name)];
			}
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x0021C1F8 File Offset: 0x0021A3F8
		private void OnDrop(GameObject obj)
		{
			if (Input.GetMouseButtonUp(0) && !this.JustShow)
			{
				if (this.Item.itemID > 0 && jsonData.instance.ItemJsonData[this.Item.itemID.ToString()]["CanSale"].I == 1)
				{
					UIPopTip.Inst.Pop("此物品无法交易", PopTipIconType.叹号);
					return;
				}
				this.chengItemIcon();
			}
		}

		// Token: 0x06005111 RID: 20753 RVA: 0x0003A670 File Offset: 0x00038870
		public override int getItemPrice()
		{
			return this.getItemMoney(this.inventory.inventory[int.Parse(base.name)]);
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x0003A693 File Offset: 0x00038893
		public override void MobilePress()
		{
			if (this.Item.itemID == -1)
			{
				return;
			}
			base.MobilePress();
			Singleton.ToolTipsBackGround.UseAction = delegate()
			{
				if (this.Item.itemName != null && !this.inventory.draggingItem)
				{
					this.ClickChengItem();
				}
			};
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x0021C26C File Offset: 0x0021A46C
		public override void PCOnPress()
		{
			if (JiaoYiManager.inst != null && !JiaoYiManager.inst.canClick)
			{
				return;
			}
			if (this.isFight)
			{
				return;
			}
			if (this.JustShow)
			{
				return;
			}
			this.Item = this.inventory.inventory[int.Parse(base.name)];
			if (Input.GetMouseButtonDown(1) && this.Item.itemName != null && !this.inventory.draggingItem)
			{
				if (Input.GetKey(304))
				{
					Debug.Log("此处应买入卖出5个");
					ItemCellEX.moveItemCount = 5;
				}
				else if (Input.GetKey(306))
				{
					Debug.Log("此处应买入卖出全部");
					ItemCellEX.moveItemCount = -1;
				}
				else
				{
					Debug.Log("此处应买入卖出1个或数量选择");
					ItemCellEX.moveItemCount = 1;
				}
				this.ClickChengItem();
			}
			if (Input.GetMouseButtonDown(0))
			{
				this.chengItemIcon();
			}
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x0021C348 File Offset: 0x0021A548
		public void ClickChengItem()
		{
			jsonData.InventoryNUM inventoryNUM = (jsonData.InventoryNUM)int.Parse(base.name);
			this.chengItemIcon();
			if (inventoryNUM < this.inventory.FanYeCount)
			{
				ItemCellEX nullItemCell = this.getNullItemCell(1);
				if (nullItemCell != null)
				{
					nullItemCell.chengItemIcon();
				}
			}
			else
			{
				ItemCellEX nullItemCell2 = this.getNullItemCell(0);
				if (nullItemCell2 != null)
				{
					nullItemCell2.chengItemIcon();
				}
			}
			this.inventory.BackItem();
			this.inventory.showTooltip = false;
		}

		// Token: 0x06005115 RID: 20757 RVA: 0x0021C3BC File Offset: 0x0021A5BC
		public ItemCellEX getNullItemCell(int type)
		{
			int num;
			int num2;
			GameObject gameObject;
			if (type == 1)
			{
				num = (int)this.inventory.FanYeCount;
				num2 = (int)this.inventory.count;
				gameObject = this.inventory.exchengkey;
			}
			else
			{
				num = 0;
				num2 = (int)this.inventory.FanYeCount;
				gameObject = this.inventory.InventoryUI.transform.Find("Win/item").gameObject;
			}
			for (int i = 0; i < num2 - num; i++)
			{
				if (gameObject.transform.GetChild(i).GetComponent<ItemCellEX>().Item.itemID == -1)
				{
					return gameObject.transform.GetChild(i).GetComponent<ItemCellEX>();
				}
			}
			return null;
		}

		// Token: 0x06005116 RID: 20758 RVA: 0x0021C464 File Offset: 0x0021A664
		public void chengItemIcon()
		{
			if (this.Item.itemName == null)
			{
				if (this.inventory.draggingItem)
				{
					this.moveItem();
				}
				return;
			}
			if (!this.inventory.draggingItem)
			{
				this.inventory.dragedID = int.Parse(base.name);
				this.inventory.draggingItem = true;
				this.inventory.dragedItem = this.inventory.inventory[int.Parse(base.name)];
				this.inventory.inventory[int.Parse(base.name)] = new item();
				return;
			}
			this.moveItem();
			this.inventory.draggingItem = true;
		}

		// Token: 0x06005117 RID: 20759 RVA: 0x0021C520 File Offset: 0x0021A720
		public void setUI_chifen()
		{
			selectNum.instence.gameObject.GetComponent<UI_chaifen>().Item = this.inventory.dragedItem.Clone();
			selectNum.instence.gameObject.GetComponent<UI_chaifen>().inputNum.value = string.Concat(1);
		}

		// Token: 0x06005118 RID: 20760 RVA: 0x0021C578 File Offset: 0x0021A778
		public void moveItem()
		{
			if (this.inventory.isPaiMai && jsonData.instance.PaiMaiBiao[PaiMaiHang.inst.PaiMaiHangID.ToString()]["jimainum"].I < 1)
			{
				UIPopTip.Inst.Pop("本场拍卖不可寄卖", PopTipIconType.叹号);
				return;
			}
			int num = 24;
			if (this.inventory.isNewJiaoYi)
			{
				num = 15;
				if (!JiaoYiManager.inst.canClick)
				{
					return;
				}
			}
			if (int.Parse(base.name) >= num)
			{
				if ((int)jsonData.instance.ItemJsonData[this.inventory.dragedItem.itemID.ToString()]["CanSale"].n == 1)
				{
					UIPopTip.Inst.Pop("该物品无法出售", PopTipIconType.叹号);
					return;
				}
				if (this.chaifenType != 0 && this.inventory.dragedID < (int)this.inventory.FanYeCount && this.inventory.inventory[int.Parse(base.name)].itemID != -1 && this.inventory.inventory[int.Parse(base.name)].UUID != this.inventory.dragedItem.UUID)
				{
					return;
				}
				if ((this.inventory.dragedItem.itemNum <= 1 || this.inventory.dragedID >= (int)this.inventory.FanYeCount) && (this.inventory.dragedID >= (int)this.inventory.FanYeCount || !this.inventory.HasUUIDItem((int)this.inventory.FanYeCount, (int)this.inventory.count, this.inventory.dragedItem.UUID)))
				{
					this.hasDrawChanegItem();
					return;
				}
				if (!this.inventory.isNewJiaoYi)
				{
					this.setUI_chifen();
					selectNum.instence.setChoice(new EventDelegate(delegate()
					{
						UI_chaifen component = selectNum.instence.gameObject.GetComponent<UI_chaifen>();
						int num5 = int.Parse(selectNum.instence.gameObject.GetComponent<UI_chaifen>().inputNum.value);
						component.Item.itemNum = num5;
						if (this.chaifenType == 0)
						{
							if (this.CanChaifen(component.Item, num5) && this.inventory.isFull(1, component.Item.UUID))
							{
								this.inventory.exAddItem1(1, component.Item.UUID, num5, 0, -1);
								this.inventory.reduceItem1(2, component.Item.UUID, num5);
							}
							return;
						}
						int num6 = int.Parse(base.name);
						if (LianDanMag.instence.itemCells[num6 - 24].JustShow)
						{
							return;
						}
						if (this.inventory.inventory[num6].itemID == -1)
						{
							this.inventory.setInventoryIndexItem(num6, this.inventory.dragedID, num5, component.Item.UUID);
						}
						else
						{
							this.inventory.inventory[num6].itemNum += num5;
						}
						this.inventory.reduceItem1(2, component.Item.UUID, num5);
					}), null, "选择数量");
					return;
				}
				if (ItemCellEX.moveItemCount == 1)
				{
					GameObject gameObject = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("SumSelect"), Singleton.ints.exchengePlan.UGUITransform);
					gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0f);
					SumSelectManager sumSelectManager = gameObject.GetComponent<SumSelectManager>();
					sumSelectManager.Item = this.inventory.dragedItem.Clone();
					sumSelectManager.isShowMask = false;
					JiaoYiManager.inst.canClick = false;
					sumSelectManager.showSelect("", sumSelectManager.Item.itemID, (float)sumSelectManager.Item.itemNum, delegate
					{
						JiaoYiManager.inst.canClick = true;
						int num5 = (int)sumSelectManager.itemSum;
						if (num5 <= 0)
						{
							return;
						}
						sumSelectManager.Item.itemNum = num5;
						if (this.chaifenType == 0)
						{
							if (this.inventory.isFull(1, sumSelectManager.Item.UUID))
							{
								this.inventory.exAddItem1(1, sumSelectManager.Item.UUID, num5, 0, -1);
								this.inventory.reduceItem1(2, sumSelectManager.Item.UUID, num5);
							}
							JiaoYiManager.inst.updateMoney();
							return;
						}
						int num6 = int.Parse(this.name);
						if (LianDanMag.instence.itemCells[num6 - 24].JustShow)
						{
							return;
						}
						if (this.inventory.inventory[num6].itemID == -1)
						{
							this.inventory.setInventoryIndexItem(num6, this.inventory.dragedID, num5, sumSelectManager.Item.UUID);
						}
						else
						{
							this.inventory.inventory[num6].itemNum += num5;
						}
						this.inventory.reduceItem1(2, sumSelectManager.Item.UUID, num5);
					}, delegate
					{
						JiaoYiManager.inst.canClick = true;
						JiaoYiManager.inst.updateMoney();
					}, SumSelectManager.SpecialType.空);
					return;
				}
				item item = this.inventory.dragedItem.Clone();
				int num2;
				if (ItemCellEX.moveItemCount == -1)
				{
					num2 = item.itemNum;
				}
				else
				{
					num2 = ((ItemCellEX.moveItemCount < item.itemNum) ? ItemCellEX.moveItemCount : item.itemNum);
				}
				if (this.inventory.isFull(1, item.UUID))
				{
					this.inventory.BackItem();
					this.inventory.exAddItem1(1, item.UUID, num2, 0, -1);
					this.inventory.reduceItem1(2, item.UUID, num2);
				}
				JiaoYiManager.inst.updateMoney();
				return;
			}
			else
			{
				if ((this.inventory.dragedItem.itemNum <= 1 || this.inventory.dragedID < (int)this.inventory.FanYeCount) && (this.inventory.dragedID < (int)this.inventory.FanYeCount || !this.inventory.HasUUIDItem(0, (int)this.inventory.FanYeCount, this.inventory.dragedItem.UUID)))
				{
					if (this.Item.itemID != -1)
					{
						int otherAllMoney = this.getOtherAllMoney();
						int usedMoney = this.getUsedMoney();
						int num3 = this.getItemMoney(this.Item) * this.Item.itemNum;
						if (this.inventory.isNewJiaoYi)
						{
							this.exchengeItem();
						}
						else if (otherAllMoney - usedMoney - num3 < 0)
						{
							this.getMoneyText();
						}
						else
						{
							this.exchengeItem();
						}
					}
					else
					{
						this.exchengeItem();
					}
					if (JiaoYiManager.inst != null)
					{
						JiaoYiManager.inst.updateMoney();
					}
					return;
				}
				if (!this.inventory.isNewJiaoYi)
				{
					this.setUI_chifen();
					selectNum.instence.setChoice(new EventDelegate(delegate()
					{
						UI_chaifen component = selectNum.instence.gameObject.GetComponent<UI_chaifen>();
						int num5 = int.Parse(selectNum.instence.gameObject.GetComponent<UI_chaifen>().inputNum.value);
						component.Item.itemNum = num5;
						if (this.chaifenType != 0)
						{
							int.Parse(base.name);
							this.inventory.exAddItem1(2, component.Item.UUID, num5, 0, -1);
							this.inventory.reduceItem(this.inventory.dragedID, num5);
							return;
						}
						if (this.inventory.isFull(2, component.Item.UUID))
						{
							this.inventory.exAddItem1(2, component.Item.UUID, num5, 0, -1);
							this.inventory.reduceItem1(1, component.Item.UUID, num5);
						}
					}), null, "选择数量");
					return;
				}
				if (ItemCellEX.moveItemCount == 1)
				{
					GameObject gameObject2 = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("SumSelect"), Singleton.ints.exchengePlan.UGUITransform);
					JiaoYiManager.inst.canClick = false;
					gameObject2.transform.localScale = new Vector3(0.6f, 0.6f, 0f);
					SumSelectManager sumSelectManager = gameObject2.GetComponent<SumSelectManager>();
					sumSelectManager.Item = this.inventory.dragedItem.Clone();
					sumSelectManager.isShowMask = false;
					sumSelectManager.showSelect("", sumSelectManager.Item.itemID, (float)sumSelectManager.Item.itemNum, delegate
					{
						int num5 = (int)sumSelectManager.itemSum;
						JiaoYiManager.inst.canClick = true;
						if (num5 <= 0)
						{
							return;
						}
						sumSelectManager.Item.itemNum = num5;
						if (this.chaifenType != 0)
						{
							int.Parse(this.name);
							this.inventory.exAddItem1(2, sumSelectManager.Item.UUID, num5, 0, -1);
							this.inventory.reduceItem(this.inventory.dragedID, num5);
							return;
						}
						if (this.inventory.isFull(2, sumSelectManager.Item.UUID))
						{
							this.inventory.exAddItem1(2, sumSelectManager.Item.UUID, num5, 0, -1);
							this.inventory.reduceItem1(1, sumSelectManager.Item.UUID, num5);
						}
						JiaoYiManager.inst.updateMoney();
					}, delegate
					{
						JiaoYiManager.inst.canClick = true;
						JiaoYiManager.inst.updateMoney();
					}, SumSelectManager.SpecialType.空);
					return;
				}
				item item2 = this.inventory.dragedItem.Clone();
				int num4;
				if (ItemCellEX.moveItemCount == -1)
				{
					num4 = item2.itemNum;
				}
				else
				{
					num4 = ((ItemCellEX.moveItemCount < item2.itemNum) ? ItemCellEX.moveItemCount : item2.itemNum);
				}
				if (this.inventory.isFull(1, item2.UUID))
				{
					this.inventory.BackItem();
					this.inventory.exAddItem1(2, item2.UUID, num4, 0, -1);
					this.inventory.reduceItem1(1, item2.UUID, num4);
				}
				JiaoYiManager.inst.updateMoney();
				return;
			}
		}

		// Token: 0x06005119 RID: 20761 RVA: 0x0021CBE0 File Offset: 0x0021ADE0
		public bool CanChaifen(item item, int num)
		{
			return true;
		}

		// Token: 0x0600511A RID: 20762 RVA: 0x0021CBF0 File Offset: 0x0021ADF0
		public void hasDrawChanegItem()
		{
			int otherAllMoney = this.getOtherAllMoney();
			int usedMoney = this.getUsedMoney();
			int itemMoney = this.getItemMoney(this.inventory.dragedItem);
			this.WatherExCheng(otherAllMoney, usedMoney, itemMoney);
		}

		// Token: 0x0600511B RID: 20763 RVA: 0x0003A6C0 File Offset: 0x000388C0
		public void WatherExCheng(int allMoney, int useMoney, int itemMoney)
		{
			if (this.inventory.isNewJiaoYi)
			{
				this.exchengeItem();
				JiaoYiManager.inst.updateMoney();
				return;
			}
			if (allMoney - useMoney - itemMoney < 0)
			{
				this.getMoneyText();
				return;
			}
			this.exchengeItem();
		}

		// Token: 0x0600511C RID: 20764 RVA: 0x0003A6F6 File Offset: 0x000388F6
		public ExchangePlan getecplan()
		{
			if (this.parentPlane.GetComponent<PaiMaiHang>() != null)
			{
				return this.parentPlane.GetComponent<PaiMaiHang>();
			}
			return this.parentPlane.GetComponent<ExchangePlan>();
		}

		// Token: 0x0600511D RID: 20765 RVA: 0x0021CC28 File Offset: 0x0021AE28
		public string getMoneyText()
		{
			ExchangePlan exchangePlan = this.getecplan();
			string str;
			if (this.isPlayer)
			{
				int num = jsonData.instance.getRandom() % 10;
				str = Tools.getStr("exchengePlayer" + num);
				exchangePlan.MonstarterSay(str);
			}
			else
			{
				int num2 = jsonData.instance.getRandom() % 10;
				str = Tools.getStr("exchengeMonstar" + num2);
				exchangePlan.MonstarterSay(str);
			}
			return str;
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x0021CCA4 File Offset: 0x0021AEA4
		public int getOtherAllMoney()
		{
			ExchangePlan exchangePlan = this.getecplan();
			if (this.parentPlane.GetComponent<PaiMaiHang>() || this.NotCheckMoney)
			{
				return 1999999999;
			}
			int result;
			if (this.isPlayer)
			{
				result = (int)jsonData.instance.AvatarBackpackJsonData[string.Concat(exchangePlan.MonstarID)]["money"].n;
			}
			else
			{
				result = (int)Tools.instance.getPlayer().money;
			}
			return result;
		}

		// Token: 0x0600511F RID: 20767 RVA: 0x0021CD28 File Offset: 0x0021AF28
		public int getUsedMoney()
		{
			ExchangePlan exchangePlan = this.getecplan();
			if (this.parentPlane.GetComponent<PaiMaiHang>() || this.NotCheckMoney)
			{
				return 0;
			}
			int buyMoney;
			if (this.isPlayer)
			{
				buyMoney = exchangePlan.GetBuyMoney(exchangePlan.inventoryPlayer, true);
			}
			else
			{
				buyMoney = exchangePlan.GetBuyMoney(exchangePlan.inventoryMonstar, false);
			}
			return buyMoney;
		}

		// Token: 0x06005120 RID: 20768 RVA: 0x0021CD84 File Offset: 0x0021AF84
		public static float getItemNaiJiuPrice(item _item)
		{
			JSONObject jsonobject = jsonData.instance.ItemJsonData[_item.itemID.ToString()];
			float result;
			if (jsonobject["type"].I == 14 || jsonobject["type"].I == 9)
			{
				float num = 100f;
				if (jsonobject["type"].I == 14)
				{
					num = (float)jsonData.instance.LingZhouPinJie[jsonobject["quality"].I.ToString()]["Naijiu"];
				}
				result = (float)((int)_item.Seid["NaiJiu"].n) / num;
			}
			else
			{
				result = 1f;
			}
			return result;
		}

		// Token: 0x06005121 RID: 20769 RVA: 0x0021CE50 File Offset: 0x0021B050
		public int getItemMoney(item _item)
		{
			ExchangePlan exchangePlan = this.getecplan();
			int npcid = -1;
			if (exchangePlan != null)
			{
				npcid = exchangePlan.MonstarID;
			}
			return _item.GetJiaoYiPrice(npcid, this.isPlayer, false);
		}

		// Token: 0x06005122 RID: 20770 RVA: 0x0021CE84 File Offset: 0x0021B084
		public override int MoneyPercent(item _item)
		{
			ExchangePlan exchangePlan = this.getecplan();
			if (exchangePlan == null)
			{
				return 0;
			}
			return _item.GetJiaCheng(exchangePlan.MonstarID);
		}

		// Token: 0x06005123 RID: 20771 RVA: 0x0021CEB0 File Offset: 0x0021B0B0
		public void exchengeItem()
		{
			this.inventory.ChangeItem(ref this.Item, ref this.inventory.dragedItem);
			this.inventory.inventory[int.Parse(base.name)] = this.Item;
			this.inventory.inventory[this.inventory.dragedID] = this.inventory.dragedItem;
			this.inventory.Temp.GetComponent<UITexture>().mainTexture = this.inventory.dragedItem.itemIcon;
			this.inventory.draggingItem = false;
		}

		// Token: 0x06005124 RID: 20772 RVA: 0x0021CF54 File Offset: 0x0021B154
		public void Update()
		{
			try
			{
				this.Icon.GetComponent<UITexture>().mainTexture = this.inventory.inventory[int.Parse(base.name)].itemIcon;
				this.PingZhi.GetComponent<UITexture>().mainTexture = this.inventory.inventory[int.Parse(base.name)].itemPingZhi;
			}
			catch (Exception)
			{
				Debug.Log(int.Parse(base.name));
				Debug.Log(this.inventory.inventory.Count);
			}
			this.Item = this.inventory.inventory[int.Parse(base.name)];
			if (this.inventory.inventory[int.Parse(base.name)].itemNum > 1)
			{
				this.Num.GetComponent<UILabel>().text = this.inventory.inventory[int.Parse(base.name)].itemNum.ToString();
			}
			else
			{
				this.Num.GetComponent<UILabel>().text = "";
			}
			base.showYiWu();
			base.ShowName();
		}

		// Token: 0x04005228 RID: 21032
		public bool isFight;

		// Token: 0x04005229 RID: 21033
		private Key key;

		// Token: 0x0400522A RID: 21034
		private Transform parentPlane;

		// Token: 0x0400522B RID: 21035
		public bool NotCheckMoney;

		// Token: 0x0400522C RID: 21036
		public int chaifenType;

		// Token: 0x0400522D RID: 21037
		private static int moveItemCount = 1;
	}
}
