using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace GUIPackage;

public class ItemCellEX : ItemCell
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static UnityAction _003C_003E9__15_3;

		public static UnityAction _003C_003E9__15_5;

		internal void _003CmoveItem_003Eb__15_3()
		{
			JiaoYiManager.inst.canClick = true;
			JiaoYiManager.inst.updateMoney();
		}

		internal void _003CmoveItem_003Eb__15_5()
		{
			JiaoYiManager.inst.canClick = true;
			JiaoYiManager.inst.updateMoney();
		}
	}

	public bool isFight;

	private Key key;

	private Transform parentPlane;

	public bool NotCheckMoney;

	public int chaifenType;

	private static int moveItemCount = 1;

	private void Start()
	{
		Icon = ((Component)((Component)this).transform.Find("Icon")).gameObject;
		Num = ((Component)((Component)this).transform.Find("num")).gameObject;
		parentPlane = ((Component)this).transform.parent.parent.parent.parent.parent;
		if ((Object)(object)inventory == (Object)null)
		{
			if (isFight)
			{
				inventory = GameObject.Find("FightUIRoot(Clone)/Texture/Inventory2").GetComponent<Inventory2>();
			}
			else if (isPlayer)
			{
				inventory = ((Component)((Component)parentPlane).transform.Find("Panel/Inventory2")).GetComponent<Inventory2>();
				key = ((Component)((Component)parentPlane).transform.Find("Panel/short cutEX/Key")).GetComponent<Key>();
				Item = inventory.inventory[int.Parse(((Object)this).name)];
			}
			else
			{
				inventory = ((Component)((Component)parentPlane).transform.Find("Panel/Inventory3")).GetComponent<Inventory2>();
				key = ((Component)((Component)parentPlane).transform.Find("Panel/short cutEXAvatar/Key")).GetComponent<Key>();
				Item = inventory.inventory[int.Parse(((Object)this).name)];
			}
		}
	}

	private void OnDrop(GameObject obj)
	{
		if (Input.GetMouseButtonUp(0) && !JustShow)
		{
			if (Item.itemID > 0 && jsonData.instance.ItemJsonData[Item.itemID.ToString()]["CanSale"].I == 1)
			{
				UIPopTip.Inst.Pop("此物品无法交易");
			}
			else
			{
				chengItemIcon();
			}
		}
	}

	public override int getItemPrice()
	{
		return getItemMoney(inventory.inventory[int.Parse(((Object)this).name)]);
	}

	public override void MobilePress()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		if (Item.itemID == -1)
		{
			return;
		}
		base.MobilePress();
		Singleton.ToolTipsBackGround.UseAction = (UnityAction)delegate
		{
			if (Item.itemName != null && !inventory.draggingItem)
			{
				ClickChengItem();
			}
		};
	}

	public override void PCOnPress()
	{
		if (((Object)(object)JiaoYiManager.inst != (Object)null && !JiaoYiManager.inst.canClick) || isFight || JustShow)
		{
			return;
		}
		Item = inventory.inventory[int.Parse(((Object)this).name)];
		if (Input.GetMouseButtonDown(1) && Item.itemName != null && !inventory.draggingItem)
		{
			if (Input.GetKey((KeyCode)304))
			{
				Debug.Log((object)"此处应买入卖出5个");
				moveItemCount = 5;
			}
			else if (Input.GetKey((KeyCode)306))
			{
				Debug.Log((object)"此处应买入卖出全部");
				moveItemCount = -1;
			}
			else
			{
				Debug.Log((object)"此处应买入卖出1个或数量选择");
				moveItemCount = 1;
			}
			ClickChengItem();
		}
		if (Input.GetMouseButtonDown(0))
		{
			chengItemIcon();
		}
	}

	public void ClickChengItem()
	{
		int num = int.Parse(((Object)this).name);
		chengItemIcon();
		if (num < (int)inventory.FanYeCount)
		{
			ItemCellEX nullItemCell = getNullItemCell(1);
			if ((Object)(object)nullItemCell != (Object)null)
			{
				nullItemCell.chengItemIcon();
			}
		}
		else
		{
			ItemCellEX nullItemCell2 = getNullItemCell(0);
			if ((Object)(object)nullItemCell2 != (Object)null)
			{
				nullItemCell2.chengItemIcon();
			}
		}
		inventory.BackItem();
		inventory.showTooltip = false;
	}

	public ItemCellEX getNullItemCell(int type)
	{
		GameObject val = null;
		int num;
		int num2;
		if (type == 1)
		{
			num = (int)inventory.FanYeCount;
			num2 = (int)inventory.count;
			val = inventory.exchengkey;
		}
		else
		{
			num = 0;
			num2 = (int)inventory.FanYeCount;
			val = ((Component)inventory.InventoryUI.transform.Find("Win/item")).gameObject;
		}
		for (int i = 0; i < num2 - num; i++)
		{
			if (((Component)val.transform.GetChild(i)).GetComponent<ItemCellEX>().Item.itemID == -1)
			{
				return ((Component)val.transform.GetChild(i)).GetComponent<ItemCellEX>();
			}
		}
		return null;
	}

	public void chengItemIcon()
	{
		if (Item.itemName != null)
		{
			if (!inventory.draggingItem)
			{
				inventory.dragedID = int.Parse(((Object)this).name);
				inventory.draggingItem = true;
				inventory.dragedItem = inventory.inventory[int.Parse(((Object)this).name)];
				inventory.inventory[int.Parse(((Object)this).name)] = new item();
			}
			else
			{
				moveItem();
				inventory.draggingItem = true;
			}
		}
		else if (inventory.draggingItem)
		{
			moveItem();
		}
	}

	public void setUI_chifen()
	{
		((Component)selectNum.instence).gameObject.GetComponent<UI_chaifen>().Item = inventory.dragedItem.Clone();
		((Component)selectNum.instence).gameObject.GetComponent<UI_chaifen>().inputNum.value = string.Concat(1);
	}

	public void moveItem()
	{
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Expected O, but got Unknown
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Expected O, but got Unknown
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Expected O, but got Unknown
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Expected O, but got Unknown
		if (inventory.isPaiMai && jsonData.instance.PaiMaiBiao[PaiMaiHang.inst.PaiMaiHangID.ToString()]["jimainum"].I < 1)
		{
			UIPopTip.Inst.Pop("本场拍卖不可寄卖");
			return;
		}
		int num = 24;
		if (inventory.isNewJiaoYi)
		{
			num = 15;
			if (!JiaoYiManager.inst.canClick)
			{
				return;
			}
		}
		if (int.Parse(((Object)this).name) >= num)
		{
			if ((int)jsonData.instance.ItemJsonData[inventory.dragedItem.itemID.ToString()]["CanSale"].n == 1)
			{
				UIPopTip.Inst.Pop("该物品无法出售");
			}
			else
			{
				if (chaifenType != 0 && inventory.dragedID < (int)inventory.FanYeCount && inventory.inventory[int.Parse(((Object)this).name)].itemID != -1 && inventory.inventory[int.Parse(((Object)this).name)].UUID != inventory.dragedItem.UUID)
				{
					return;
				}
				if ((inventory.dragedItem.itemNum > 1 && inventory.dragedID < (int)inventory.FanYeCount) || (inventory.dragedID < (int)inventory.FanYeCount && inventory.HasUUIDItem((int)inventory.FanYeCount, (int)inventory.count, inventory.dragedItem.UUID)))
				{
					if (inventory.isNewJiaoYi)
					{
						if (moveItemCount == 1)
						{
							GameObject val = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("SumSelect"), Singleton.ints.exchengePlan.UGUITransform);
							val.transform.localScale = new Vector3(0.6f, 0.6f, 0f);
							SumSelectManager sumSelectManager2 = val.GetComponent<SumSelectManager>();
							sumSelectManager2.Item = inventory.dragedItem.Clone();
							sumSelectManager2.isShowMask = false;
							JiaoYiManager.inst.canClick = false;
							SumSelectManager sumSelectManager3 = sumSelectManager2;
							int itemID = sumSelectManager2.Item.itemID;
							float maxSum = sumSelectManager2.Item.itemNum;
							UnityAction val2 = delegate
							{
								JiaoYiManager.inst.canClick = true;
								int num9 = (int)sumSelectManager2.itemSum;
								if (num9 > 0)
								{
									sumSelectManager2.Item.itemNum = num9;
									if (chaifenType != 0)
									{
										int num10 = int.Parse(((Object)this).name);
										if (!LianDanMag.instence.itemCells[num10 - 24].JustShow)
										{
											if (inventory.inventory[num10].itemID == -1)
											{
												inventory.setInventoryIndexItem(num10, inventory.dragedID, num9, sumSelectManager2.Item.UUID);
											}
											else
											{
												inventory.inventory[num10].itemNum += num9;
											}
											inventory.reduceItem1(2, sumSelectManager2.Item.UUID, num9);
										}
									}
									else
									{
										if (inventory.isFull(1, sumSelectManager2.Item.UUID))
										{
											inventory.exAddItem1(1, sumSelectManager2.Item.UUID, num9);
											inventory.reduceItem1(2, sumSelectManager2.Item.UUID, num9);
										}
										JiaoYiManager.inst.updateMoney();
									}
								}
							};
							object obj = _003C_003Ec._003C_003E9__15_3;
							if (obj == null)
							{
								UnityAction val3 = delegate
								{
									JiaoYiManager.inst.canClick = true;
									JiaoYiManager.inst.updateMoney();
								};
								_003C_003Ec._003C_003E9__15_3 = val3;
								obj = (object)val3;
							}
							sumSelectManager3.showSelect("", itemID, maxSum, val2, (UnityAction)obj);
						}
						else
						{
							item item2 = inventory.dragedItem.Clone();
							int num2 = 1;
							num2 = ((moveItemCount != -1) ? ((moveItemCount < item2.itemNum) ? moveItemCount : item2.itemNum) : item2.itemNum);
							if (inventory.isFull(1, item2.UUID))
							{
								inventory.BackItem();
								inventory.exAddItem1(1, item2.UUID, num2);
								inventory.reduceItem1(2, item2.UUID, num2);
							}
							JiaoYiManager.inst.updateMoney();
						}
						return;
					}
					setUI_chifen();
					selectNum.instence.setChoice(new EventDelegate(delegate
					{
						UI_chaifen component2 = ((Component)selectNum.instence).gameObject.GetComponent<UI_chaifen>();
						int num7 = int.Parse(((Component)selectNum.instence).gameObject.GetComponent<UI_chaifen>().inputNum.value);
						component2.Item.itemNum = num7;
						if (chaifenType != 0)
						{
							int num8 = int.Parse(((Object)this).name);
							if (!LianDanMag.instence.itemCells[num8 - 24].JustShow)
							{
								if (inventory.inventory[num8].itemID == -1)
								{
									inventory.setInventoryIndexItem(num8, inventory.dragedID, num7, component2.Item.UUID);
								}
								else
								{
									inventory.inventory[num8].itemNum += num7;
								}
								inventory.reduceItem1(2, component2.Item.UUID, num7);
							}
						}
						else if (CanChaifen(component2.Item, num7) && inventory.isFull(1, component2.Item.UUID))
						{
							inventory.exAddItem1(1, component2.Item.UUID, num7);
							inventory.reduceItem1(2, component2.Item.UUID, num7);
						}
					}), null);
				}
				else
				{
					hasDrawChanegItem();
				}
			}
			return;
		}
		if ((inventory.dragedItem.itemNum > 1 && inventory.dragedID >= (int)inventory.FanYeCount) || (inventory.dragedID >= (int)inventory.FanYeCount && inventory.HasUUIDItem(0, (int)inventory.FanYeCount, inventory.dragedItem.UUID)))
		{
			if (inventory.isNewJiaoYi)
			{
				if (moveItemCount == 1)
				{
					GameObject val4 = Object.Instantiate<GameObject>(ResManager.inst.LoadPrefab("SumSelect"), Singleton.ints.exchengePlan.UGUITransform);
					JiaoYiManager.inst.canClick = false;
					val4.transform.localScale = new Vector3(0.6f, 0.6f, 0f);
					SumSelectManager sumSelectManager = val4.GetComponent<SumSelectManager>();
					sumSelectManager.Item = inventory.dragedItem.Clone();
					sumSelectManager.isShowMask = false;
					SumSelectManager sumSelectManager4 = sumSelectManager;
					int itemID2 = sumSelectManager.Item.itemID;
					float maxSum2 = sumSelectManager.Item.itemNum;
					UnityAction val5 = delegate
					{
						int num6 = (int)sumSelectManager.itemSum;
						JiaoYiManager.inst.canClick = true;
						if (num6 > 0)
						{
							sumSelectManager.Item.itemNum = num6;
							if (chaifenType != 0)
							{
								int.Parse(((Object)this).name);
								inventory.exAddItem1(2, sumSelectManager.Item.UUID, num6);
								inventory.reduceItem(inventory.dragedID, num6);
							}
							else
							{
								if (inventory.isFull(2, sumSelectManager.Item.UUID))
								{
									inventory.exAddItem1(2, sumSelectManager.Item.UUID, num6);
									inventory.reduceItem1(1, sumSelectManager.Item.UUID, num6);
								}
								JiaoYiManager.inst.updateMoney();
							}
						}
					};
					object obj2 = _003C_003Ec._003C_003E9__15_5;
					if (obj2 == null)
					{
						UnityAction val6 = delegate
						{
							JiaoYiManager.inst.canClick = true;
							JiaoYiManager.inst.updateMoney();
						};
						_003C_003Ec._003C_003E9__15_5 = val6;
						obj2 = (object)val6;
					}
					sumSelectManager4.showSelect("", itemID2, maxSum2, val5, (UnityAction)obj2);
				}
				else
				{
					item item3 = inventory.dragedItem.Clone();
					int num3 = 1;
					num3 = ((moveItemCount != -1) ? ((moveItemCount < item3.itemNum) ? moveItemCount : item3.itemNum) : item3.itemNum);
					if (inventory.isFull(1, item3.UUID))
					{
						inventory.BackItem();
						inventory.exAddItem1(2, item3.UUID, num3);
						inventory.reduceItem1(1, item3.UUID, num3);
					}
					JiaoYiManager.inst.updateMoney();
				}
				return;
			}
			setUI_chifen();
			selectNum.instence.setChoice(new EventDelegate(delegate
			{
				UI_chaifen component = ((Component)selectNum.instence).gameObject.GetComponent<UI_chaifen>();
				int num5 = int.Parse(((Component)selectNum.instence).gameObject.GetComponent<UI_chaifen>().inputNum.value);
				component.Item.itemNum = num5;
				if (chaifenType != 0)
				{
					int.Parse(((Object)this).name);
					inventory.exAddItem1(2, component.Item.UUID, num5);
					inventory.reduceItem(inventory.dragedID, num5);
				}
				else if (inventory.isFull(2, component.Item.UUID))
				{
					inventory.exAddItem1(2, component.Item.UUID, num5);
					inventory.reduceItem1(1, component.Item.UUID, num5);
				}
			}), null);
			return;
		}
		if (Item.itemID != -1)
		{
			int otherAllMoney = getOtherAllMoney();
			int usedMoney = getUsedMoney();
			int num4 = getItemMoney(Item) * Item.itemNum;
			if (inventory.isNewJiaoYi)
			{
				exchengeItem();
			}
			else if (otherAllMoney - usedMoney - num4 < 0)
			{
				getMoneyText();
			}
			else
			{
				exchengeItem();
			}
		}
		else
		{
			exchengeItem();
		}
		if ((Object)(object)JiaoYiManager.inst != (Object)null)
		{
			JiaoYiManager.inst.updateMoney();
		}
	}

	public bool CanChaifen(item item, int num)
	{
		return true;
	}

	public void hasDrawChanegItem()
	{
		int otherAllMoney = getOtherAllMoney();
		int usedMoney = getUsedMoney();
		int itemMoney = getItemMoney(inventory.dragedItem);
		WatherExCheng(otherAllMoney, usedMoney, itemMoney);
	}

	public void WatherExCheng(int allMoney, int useMoney, int itemMoney)
	{
		if (inventory.isNewJiaoYi)
		{
			exchengeItem();
			JiaoYiManager.inst.updateMoney();
		}
		else if (allMoney - useMoney - itemMoney < 0)
		{
			getMoneyText();
		}
		else
		{
			exchengeItem();
		}
	}

	public ExchangePlan getecplan()
	{
		if ((Object)(object)((Component)parentPlane).GetComponent<PaiMaiHang>() != (Object)null)
		{
			return ((Component)parentPlane).GetComponent<PaiMaiHang>();
		}
		return ((Component)parentPlane).GetComponent<ExchangePlan>();
	}

	public string getMoneyText()
	{
		ExchangePlan exchangePlan = getecplan();
		string text = "";
		if (isPlayer)
		{
			int num = jsonData.instance.getRandom() % 10;
			text = Tools.getStr("exchengePlayer" + num);
			exchangePlan.MonstarterSay(text);
		}
		else
		{
			int num2 = jsonData.instance.getRandom() % 10;
			text = Tools.getStr("exchengeMonstar" + num2);
			exchangePlan.MonstarterSay(text);
		}
		return text;
	}

	public int getOtherAllMoney()
	{
		ExchangePlan exchangePlan = getecplan();
		if (Object.op_Implicit((Object)(object)((Component)parentPlane).GetComponent<PaiMaiHang>()) || NotCheckMoney)
		{
			return 1999999999;
		}
		int num = 0;
		if (isPlayer)
		{
			return (int)jsonData.instance.AvatarBackpackJsonData[string.Concat(exchangePlan.MonstarID)]["money"].n;
		}
		return (int)Tools.instance.getPlayer().money;
	}

	public int getUsedMoney()
	{
		ExchangePlan exchangePlan = getecplan();
		if (Object.op_Implicit((Object)(object)((Component)parentPlane).GetComponent<PaiMaiHang>()) || NotCheckMoney)
		{
			return 0;
		}
		int num = 0;
		if (isPlayer)
		{
			return exchangePlan.GetBuyMoney(exchangePlan.inventoryPlayer, isPlayer: true);
		}
		return exchangePlan.GetBuyMoney(exchangePlan.inventoryMonstar, isPlayer: false);
	}

	public static float getItemNaiJiuPrice(item _item)
	{
		JSONObject jSONObject = jsonData.instance.ItemJsonData[_item.itemID.ToString()];
		float num = 0f;
		if (jSONObject["type"].I == 14 || jSONObject["type"].I == 9)
		{
			float num2 = 100f;
			if (jSONObject["type"].I == 14)
			{
				num2 = (float)jsonData.instance.LingZhouPinJie[jSONObject["quality"].I.ToString()][(object)"Naijiu"];
			}
			return (float)(int)_item.Seid["NaiJiu"].n / num2;
		}
		return 1f;
	}

	public int getItemMoney(item _item)
	{
		ExchangePlan exchangePlan = getecplan();
		int npcid = -1;
		if ((Object)(object)exchangePlan != (Object)null)
		{
			npcid = exchangePlan.MonstarID;
		}
		return _item.GetJiaoYiPrice(npcid, isPlayer);
	}

	public override int MoneyPercent(item _item)
	{
		ExchangePlan exchangePlan = getecplan();
		if ((Object)(object)exchangePlan == (Object)null)
		{
			return 0;
		}
		return _item.GetJiaCheng(exchangePlan.MonstarID);
	}

	public void exchengeItem()
	{
		inventory.ChangeItem(ref Item, ref inventory.dragedItem);
		inventory.inventory[int.Parse(((Object)this).name)] = Item;
		inventory.inventory[inventory.dragedID] = inventory.dragedItem;
		inventory.Temp.GetComponent<UITexture>().mainTexture = (Texture)(object)inventory.dragedItem.itemIcon;
		inventory.draggingItem = false;
	}

	public void Update()
	{
		try
		{
			Icon.GetComponent<UITexture>().mainTexture = (Texture)(object)inventory.inventory[int.Parse(((Object)this).name)].itemIcon;
			PingZhi.GetComponent<UITexture>().mainTexture = (Texture)(object)inventory.inventory[int.Parse(((Object)this).name)].itemPingZhi;
		}
		catch (Exception)
		{
			Debug.Log((object)int.Parse(((Object)this).name));
			Debug.Log((object)inventory.inventory.Count);
		}
		Item = inventory.inventory[int.Parse(((Object)this).name)];
		if (inventory.inventory[int.Parse(((Object)this).name)].itemNum > 1)
		{
			Num.GetComponent<UILabel>().text = inventory.inventory[int.Parse(((Object)this).name)].itemNum.ToString();
		}
		else
		{
			Num.GetComponent<UILabel>().text = "";
		}
		showYiWu();
		ShowName();
	}
}
