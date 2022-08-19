using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000A5B RID: 2651
	public class Inventory2 : MonoBehaviour
	{
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06004A13 RID: 18963 RVA: 0x001F68C8 File Offset: 0x001F4AC8
		// (set) Token: 0x06004A12 RID: 18962 RVA: 0x001F6864 File Offset: 0x001F4A64
		public bool showTooltip
		{
			get
			{
				bool result = false;
				if (this.Tooltip.GetComponent<TooltipScale>() != null)
				{
					result = this.Tooltip.GetComponent<TooltipScale>().showTooltip;
				}
				else if (this.Tooltip.GetComponent<TooltipItem>())
				{
					result = this.Tooltip.GetComponent<TooltipItem>().showTooltip;
				}
				else if (this.Tooltip.GetComponent<TooltipSkillTab>())
				{
					result = this.Tooltip.GetComponent<TooltipSkillTab>().showTooltip;
				}
				return result;
			}
			set
			{
				GameObject tooltip = this.Tooltip;
				if (tooltip.GetComponent<TooltipScale>() != null)
				{
					tooltip.GetComponent<TooltipScale>().showTooltip = value;
					return;
				}
				if (tooltip.GetComponent<TooltipItem>())
				{
					tooltip.GetComponent<TooltipItem>().showTooltip = value;
					return;
				}
				if (tooltip.GetComponent<TooltipSkillTab>())
				{
					tooltip.GetComponent<TooltipSkillTab>().showTooltip = value;
				}
			}
		}

		// Token: 0x06004A14 RID: 18964 RVA: 0x001F6948 File Offset: 0x001F4B48
		public void Awake()
		{
			this.nowIndex = 0;
			this.datebase = jsonData.instance.gameObject.GetComponent<ItemDatebase>();
			this.draggingItem = false;
			this.dragedItem = new item();
			if (!this.shouldInit)
			{
				return;
			}
			for (int i = 0; i < (int)this.count; i++)
			{
				this.inventory.Add(new item());
			}
			if (this.ISExchengePlan)
			{
				this.InitInventoryEX();
				return;
			}
			this.InitInventory();
		}

		// Token: 0x06004A15 RID: 18965 RVA: 0x001F69C4 File Offset: 0x001F4BC4
		private void Start()
		{
			GameObject gameObject = GameObject.Find("NewUIAutoToolTips");
			this.EquipTooltip = gameObject;
			if (this.ResetToolTips)
			{
				this.Tooltip = gameObject;
			}
			this.BookTooltip = gameObject;
			this.skillBookToolTip = gameObject;
			this.DanyaoToolTip = gameObject;
			this.YaoCaoToolTip = gameObject;
			this.DanYaoToolTip = gameObject;
			this.DanLuToolTip = gameObject;
		}

		// Token: 0x06004A16 RID: 18966 RVA: 0x001F6A1C File Offset: 0x001F4C1C
		public void ZhengLi()
		{
			PlayerEx.Player.SortItem();
			this.LoadInventory();
		}

		// Token: 0x06004A17 RID: 18967 RVA: 0x001F6A30 File Offset: 0x001F4C30
		public void setItemLeiXin(List<int> leixin)
		{
			if (JiaoYiManager.inst != null && !JiaoYiManager.inst.canClick)
			{
				return;
			}
			this.inventItemLeiXing = leixin;
			if (this.selectpage != null)
			{
				this.selectpage.RestePageIndex();
			}
			this.LoadInventory();
		}

		// Token: 0x06004A18 RID: 18968 RVA: 0x001F6A80 File Offset: 0x001F4C80
		public void setMonstartLeiXin(List<int> leixin)
		{
			if (JiaoYiManager.inst != null && !JiaoYiManager.inst.canClick)
			{
				return;
			}
			this.inventItemLeiXing = leixin;
			if (this.selectpage != null)
			{
				this.selectpage.RestePageIndex();
			}
			ExchangePlan exchengePlan = Singleton.ints.exchengePlan;
			this.MonstarLoadInventory(exchengePlan.MonstarID);
		}

		// Token: 0x06004A19 RID: 18969 RVA: 0x001F6ADE File Offset: 0x001F4CDE
		public void setMonstarItemLeiXin1()
		{
			this.setMonstartLeiXin(new List<int>());
		}

		// Token: 0x06004A1A RID: 18970 RVA: 0x001F6AEB File Offset: 0x001F4CEB
		public void setMonstarItemLeiXin2()
		{
			this.setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["2"]["ItemFlag"]));
		}

		// Token: 0x06004A1B RID: 18971 RVA: 0x001F6B16 File Offset: 0x001F4D16
		public void setMonstarItemLeiXin3()
		{
			this.setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["3"]["ItemFlag"]));
		}

		// Token: 0x06004A1C RID: 18972 RVA: 0x001F6B41 File Offset: 0x001F4D41
		public void setMonstarItemLeiXin4()
		{
			this.setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["4"]["ItemFlag"]));
		}

		// Token: 0x06004A1D RID: 18973 RVA: 0x001F6B6C File Offset: 0x001F4D6C
		public void setMonstarItemLeiXin5()
		{
			this.setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["5"]["ItemFlag"]));
		}

		// Token: 0x06004A1E RID: 18974 RVA: 0x001F6B97 File Offset: 0x001F4D97
		public void setMonstarItemLeiXin6()
		{
			this.setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["6"]["ItemFlag"]));
		}

		// Token: 0x06004A1F RID: 18975 RVA: 0x001F6BC2 File Offset: 0x001F4DC2
		public void setMonstarItemLeiXin7()
		{
			this.setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["7"]["ItemFlag"]));
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x001F6BED File Offset: 0x001F4DED
		public void setItemLeiXin1()
		{
			this.setItemLeiXin(new List<int>());
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x001F6BFA File Offset: 0x001F4DFA
		public void setItemLeiXin2()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["2"]["ItemFlag"]));
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x001F6C25 File Offset: 0x001F4E25
		public void setItemLeiXin3()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["3"]["ItemFlag"]));
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x001F6C50 File Offset: 0x001F4E50
		public void setItemLeiXin4()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["4"]["ItemFlag"]));
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x001F6C7B File Offset: 0x001F4E7B
		public void setItemLeiXin5()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["5"]["ItemFlag"]));
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x001F6CA6 File Offset: 0x001F4EA6
		public void setItemLeiXin6()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["6"]["ItemFlag"]));
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x001F6CD1 File Offset: 0x001F4ED1
		public void setItemLeiXin7()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["7"]["ItemFlag"]));
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x001F6BFA File Offset: 0x001F4DFA
		public void setExItemLeiXin2()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["2"]["ItemFlag"]));
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x001F6C25 File Offset: 0x001F4E25
		public void setExItemLeiXin3()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["3"]["ItemFlag"]));
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x001F6C50 File Offset: 0x001F4E50
		public void setExItemLeiXin4()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["4"]["ItemFlag"]));
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x001F6CFC File Offset: 0x001F4EFC
		public void setExItemLeiXin5()
		{
			this.setItemLeiXin(new List<int>
			{
				6
			});
		}

		// Token: 0x06004A2B RID: 18987 RVA: 0x001F6D10 File Offset: 0x001F4F10
		public void setExItemLeiXin6()
		{
			this.setItemLeiXin(new List<int>
			{
				8
			});
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x001F6CA6 File Offset: 0x001F4EA6
		public void setExItemLeiXin7()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["6"]["ItemFlag"]));
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x001F6D24 File Offset: 0x001F4F24
		public void Update()
		{
			if (this.Temp == null && UI_Manager.inst != null)
			{
				this.Temp = UI_Manager.inst.temp.gameObject;
			}
			if (this.draggingItem || (Singleton.key != null && Singleton.key.draggingKey))
			{
				this.Temp.transform.position = UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);
				this.Temp.GetComponent<UITexture>().mainTexture = this.dragedItem.itemIcon;
				this.showTooltip = false;
			}
			if (Input.GetMouseButtonUp(0))
			{
				this.BackItem();
			}
			if ((Input.GetKeyDown(27) || Input.GetKeyDown(9)) && this.showTooltip)
			{
				this.showTooltip = false;
			}
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x001F6DF4 File Offset: 0x001F4FF4
		public void ChangeItem(ref item Item1, ref item Item2)
		{
			item item = new item();
			item = Item1;
			Item1 = Item2;
			Item2 = item;
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x001F6E11 File Offset: 0x001F5011
		public void Clear_dragedItem()
		{
			this.dragedItem = new item();
			this.draggingItem = false;
			this.Temp.GetComponent<UITexture>().mainTexture = null;
		}

		// Token: 0x06004A30 RID: 18992 RVA: 0x001F6E36 File Offset: 0x001F5036
		public void BackItem()
		{
			if (this.draggingItem)
			{
				this.inventory[this.dragedID] = this.dragedItem;
				this.Clear_dragedItem();
			}
		}

		// Token: 0x06004A31 RID: 18993 RVA: 0x001F6E60 File Offset: 0x001F5060
		private void Show()
		{
			this.showInventory = !this.showInventory;
			if (!this.showInventory)
			{
				this.showTooltip = false;
			}
			if (this.showInventory)
			{
				this.InventoryUI.transform.Find("Win").position = this.InventoryUI.transform.position;
			}
			this.InventoryUI.SetActive(this.showInventory);
			Singleton.UI.UI_Top(this.InventoryUI.transform);
		}

		// Token: 0x06004A32 RID: 18994 RVA: 0x001F6EE3 File Offset: 0x001F50E3
		private void InitInventory()
		{
			this.InitSlot("Slot", "Win/item");
			this.InventoryUI.SetActive(this.showInventory);
		}

		// Token: 0x06004A33 RID: 18995 RVA: 0x001F6F06 File Offset: 0x001F5106
		public void InitSlot(string SlotName, string ParentPatch = "Win/item")
		{
			this.InitSlot(SlotName, (int)this.count, ParentPatch);
		}

		// Token: 0x06004A34 RID: 18996 RVA: 0x001F6F18 File Offset: 0x001F5118
		public void InitSlot<T>(string SlotName, int _count, Inventory2.EventsetItemCell<T> eventcell, string ParentPatch = "Win/item") where T : ItemCell
		{
			for (int i = 0; i < _count; i++)
			{
				GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load(SlotName));
				gameObject.transform.parent = this.InventoryUI.transform.Find(ParentPatch).transform;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject.transform.parent.GetComponent<UIGrid>().repositionNow = true;
				gameObject.name = i.ToString();
				gameObject.GetComponent<T>().isPlayer = this.ISPlayer;
				gameObject.GetComponent<T>().inventory = this;
				eventcell(gameObject.GetComponent<T>());
				if (this.inventory[i].itemName != null)
				{
					gameObject.GetComponent<T>().Icon.GetComponent<UITexture>().mainTexture = this.inventory[i].itemIcon;
				}
			}
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x001F7020 File Offset: 0x001F5220
		public void InitSlot(string SlotName, int _count, string ParentPatch = "Win/item")
		{
			for (int i = 0; i < _count; i++)
			{
				GameObject gameObject;
				if (this.BaseSlotOBJ != null)
				{
					gameObject = Object.Instantiate<GameObject>(this.BaseSlotOBJ);
					gameObject.SetActive(true);
				}
				else
				{
					gameObject = (GameObject)Object.Instantiate(Resources.Load(SlotName));
				}
				gameObject.transform.parent = this.InventoryUI.transform.Find(ParentPatch).transform;
				gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				gameObject.transform.parent.GetComponent<UIGrid>().repositionNow = true;
				gameObject.name = i.ToString();
				gameObject.GetComponent<ItemCell>().isPlayer = this.ISPlayer;
				gameObject.GetComponent<ItemCell>().inventory = this;
				gameObject.GetComponent<ItemCell>().Btn1Text = this.NomelBtn1Text;
				gameObject.GetComponent<ItemCell>().AutoSetBtnText = this.AutoSetBtnText;
				if (this.inventory[i].itemName != null)
				{
					gameObject.GetComponent<ItemCell>().Icon.GetComponent<UITexture>().mainTexture = this.inventory[i].itemIcon;
				}
			}
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x001F7150 File Offset: 0x001F5350
		private void InitInventoryEX()
		{
			GameObject gameObject = this.exchengkey;
			if (gameObject == null)
			{
				if (this.ISPlayer)
				{
					gameObject = GameObject.Find("UI Root (2D)/exchangePlan/Panel/inventoryGuiEX/Win/item").gameObject;
				}
				else
				{
					gameObject = GameObject.Find("UI Root (2D)/exchangePlan/Panel/inventoryGuiAvatarEX/Win/item").gameObject;
				}
			}
			if (!this.isNewJiaoYi)
			{
				for (int i = 0; i < (int)this.count; i++)
				{
					GameObject gameObject2 = (GameObject)Object.Instantiate(Resources.Load("SlotEx"));
					if (i < 24)
					{
						gameObject2.transform.parent = this.InventoryUI.transform.Find("Win/item").transform;
					}
					else
					{
						gameObject2.transform.parent = gameObject.transform;
					}
					gameObject2.transform.localScale = new Vector3(1f, 1f, 1f);
					gameObject2.transform.parent.GetComponent<UIGrid>().repositionNow = true;
					gameObject2.name = i.ToString();
					gameObject2.GetComponent<ItemCellEX>().isPlayer = this.ISPlayer;
					gameObject2.GetComponent<ItemCellEX>().inventory = this;
					gameObject2.GetComponent<ItemCellEX>().JustShow = this.JustShow;
					gameObject2.GetComponent<ItemCellEX>().Btn1Text = this.NomelBtn1Text;
					gameObject2.GetComponent<ItemCellEX>().AutoSetBtnText = this.AutoSetBtnText;
					if (this.inventory[i].itemName != null)
					{
						gameObject2.GetComponent<ItemCellEX>().Icon.GetComponent<UITexture>().mainTexture = this.inventory[i].itemIcon;
					}
				}
			}
			this.InventoryUI.SetActive(this.showInventory);
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x001F72E8 File Offset: 0x001F54E8
		public void AddItem(int id)
		{
			int i;
			for (i = 0; i < this.inventory.Count; i++)
			{
				if (this.InventoryContains(id))
				{
					if (this.inventory[i].itemID == id && this.inventory[i].itemNum < this.inventory[i].itemMaxNum)
					{
						this.inventory[i].itemNum++;
						break;
					}
				}
				else if (this.inventory[i].itemName == null)
				{
					this.inventory[i] = this.datebase.items[id].Clone();
					break;
				}
			}
			if (i == this.inventory.Count)
			{
				for (i = 0; i < this.inventory.Count; i++)
				{
					if (this.inventory[i].itemName == null)
					{
						this.inventory[i] = this.datebase.items[id].Clone();
						return;
					}
				}
			}
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x001F7400 File Offset: 0x001F5600
		public bool is_Full(item Item, int num)
		{
			bool result = true;
			for (int i = 0; i < this.inventory.Count; i++)
			{
				if (this.inventory[i].itemID == Item.itemID)
				{
					if (Item.itemType == item.ItemType.Potion)
					{
						if (this.inventory[i].itemMaxNum - this.inventory[i].itemNum >= num)
						{
							result = false;
							break;
						}
					}
					else if (this.inventory[i].itemID == -1)
					{
						result = false;
						break;
					}
				}
				else if (this.inventory[i].itemID == -1)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x001F74A8 File Offset: 0x001F56A8
		public bool isFull(int type, string UUID)
		{
			int startIndex = 0;
			int endIndex = 24;
			if (this.isNewJiaoYi)
			{
				endIndex = 15;
			}
			if (type == 1)
			{
				startIndex = 24;
				if (this.isNewJiaoYi)
				{
					startIndex = 15;
				}
				endIndex = (int)this.count;
			}
			return this.isFull(startIndex, endIndex, UUID);
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x001F74E8 File Offset: 0x001F56E8
		public bool isFull(int startIndex, int endIndex, string UUID)
		{
			bool result = false;
			for (int i = startIndex; i < endIndex; i++)
			{
				if (this.inventory[i].itemName == null)
				{
					result = true;
					break;
				}
				if (this.inventory[i].UUID == UUID)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x06004A3B RID: 19003 RVA: 0x001F7538 File Offset: 0x001F5738
		public void exAddItem1(int addType, string uuid, int num = 1, int addChaifenTyep = 0, int AddToIndex = -1)
		{
			int startIndex;
			int fanYeCount;
			if (addType == 1)
			{
				startIndex = (int)this.FanYeCount;
				fanYeCount = (int)this.count;
			}
			else
			{
				startIndex = 0;
				fanYeCount = (int)this.FanYeCount;
			}
			this.exAddItem(startIndex, fanYeCount, uuid, num, addChaifenTyep, AddToIndex);
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x001F7570 File Offset: 0x001F5770
		public void exAddItem(int startIndex, int endIndex, string uuid, int num = 1, int addChaifenTyep = 0, int AddToIndex = -1)
		{
			int numIndex = 0;
			int num2 = (int)((startIndex == 0) ? this.FanYeCount : ((jsonData.InventoryNUM)0));
			int num3 = (int)((startIndex == 0) ? this.count : this.FanYeCount);
			for (int i = num2; i < num3; i++)
			{
				if (this.inventory[i].UUID == uuid)
				{
					numIndex = i;
					break;
				}
			}
			if (this.InventoryContains(startIndex, endIndex, uuid))
			{
				for (int j = startIndex; j < endIndex; j++)
				{
					if (this.inventory[j].UUID == uuid)
					{
						this.inventory[j].itemNum += num;
						return;
					}
				}
				return;
			}
			for (int k = startIndex; k < endIndex; k++)
			{
				if (this.inventory[k].itemName == null)
				{
					this.setInventoryIndexItem(k, numIndex, num, uuid);
					return;
				}
			}
		}

		// Token: 0x06004A3D RID: 19005 RVA: 0x001F7644 File Offset: 0x001F5844
		public void setindexItem(int i, int ItemID, int num, string uuid)
		{
			this.inventory[i] = this.datebase.items[ItemID].Clone();
			this.inventory[i].UUID = uuid;
			this.inventory[i].itemNum = num;
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x001F7698 File Offset: 0x001F5898
		public void setInventoryIndexItem(int i, int numIndex, int num, string uuid)
		{
			this.inventory[i] = this.datebase.items[this.inventory[numIndex].itemID].Clone();
			this.inventory[i].UUID = uuid;
			this.inventory[i].Seid = this.inventory[numIndex].Seid;
			this.inventory[i].itemNum = num;
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x001F7720 File Offset: 0x001F5920
		public void reduceItem1(int addType, string uuid, int num = 1)
		{
			int num2 = 24;
			if (this.isNewJiaoYi)
			{
				num2 = 15;
			}
			int startIndex;
			int endIndex;
			if (addType == 1)
			{
				startIndex = num2;
				endIndex = (int)this.count;
			}
			else
			{
				startIndex = 0;
				endIndex = num2;
			}
			this.reduceItem(startIndex, endIndex, uuid, num);
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x001F7758 File Offset: 0x001F5958
		public void reduceItem(int startIndex, int endIndex, string uuid, int num = 1)
		{
			int i = startIndex;
			while (i < endIndex)
			{
				if (this.inventory[i].UUID == uuid)
				{
					this.inventory[i].itemNum -= num;
					if (this.inventory[i].itemNum > 0)
					{
						break;
					}
					int itemNum = this.inventory[i].itemNum;
					this.inventory[i] = new item();
					if (itemNum < 0)
					{
						this.reduceItem(startIndex, endIndex, uuid, -itemNum);
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x001F77F0 File Offset: 0x001F59F0
		public void reduceItem(int index, int num)
		{
			this.inventory[index].itemNum -= num;
			if (this.inventory[index].itemNum <= 0)
			{
				int itemNum = this.inventory[index].itemNum;
				this.inventory[index] = new item();
			}
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x001F7850 File Offset: 0x001F5A50
		public void RemoveItem(int id)
		{
			Tools.instance.getPlayer().removeItem(id);
			for (int i = 0; i < this.inventory.Count; i++)
			{
				if (this.inventory[i].itemID == id)
				{
					this.inventory[i] = new item();
					return;
				}
			}
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x001F78AC File Offset: 0x001F5AAC
		private bool InventoryContains(int id)
		{
			bool flag = false;
			for (int i = 0; i < this.inventory.Count; i++)
			{
				flag = (this.inventory[i].itemID == id);
				if (flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x001F78EC File Offset: 0x001F5AEC
		private bool InventoryContains(int startindex, int endIndex, string UUID)
		{
			bool flag = false;
			for (int i = startindex; i < endIndex; i++)
			{
				flag = (this.inventory[i].UUID == UUID);
				if (flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x001F7924 File Offset: 0x001F5B24
		public static string getSkillBookDesc(JSONObject info)
		{
			string text = info["desc"].Str;
			if (info["type"].I == 3)
			{
				int num = (int)float.Parse(text);
				using (Dictionary<string, JSONObject>.Enumerator enumerator = jsonData.instance.skillJsonData.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, JSONObject> keyValuePair = enumerator.Current;
						if (keyValuePair.Value["Skill_ID"].I == num && keyValuePair.Value["Skill_Lv"].I == Tools.instance.getPlayer().getLevelType())
						{
							text = Tools.getDescByID(Tools.instance.Code64ToString(keyValuePair.Value["descr"].str), keyValuePair.Value["id"].I);
							break;
						}
					}
					return text;
				}
			}
			if (info["type"].I == 4)
			{
				int num2 = (int)float.Parse(text);
				foreach (JSONObject jsonobject in jsonData.instance.StaticSkillJsonData.list)
				{
					if (jsonobject["Skill_ID"].I == num2 && jsonobject["Skill_Lv"].I == 1)
					{
						text = Tools.instance.Code64ToString(jsonobject["descr"].str);
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x001F7ADC File Offset: 0x001F5CDC
		public static string GetSkillBookDesc(_ItemJsonData info)
		{
			string text = info.desc;
			if (info.type == 3)
			{
				int num = (int)float.Parse(text);
				int levelType = Tools.instance.getPlayer().getLevelType();
				using (List<_skillJsonData>.Enumerator enumerator = _skillJsonData.DataList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						_skillJsonData skillJsonData = enumerator.Current;
						if (skillJsonData.Skill_ID == num && skillJsonData.Skill_Lv == levelType)
						{
							text = Tools.getDescByID(skillJsonData.descr, skillJsonData.id);
							break;
						}
					}
					return text;
				}
			}
			if (info.type == 4)
			{
				int num2 = (int)float.Parse(text);
				foreach (StaticSkillJsonData staticSkillJsonData in StaticSkillJsonData.DataList)
				{
					if (staticSkillJsonData.Skill_ID == num2 && staticSkillJsonData.Skill_Lv == 1)
					{
						text = staticSkillJsonData.descr;
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x000EBA04 File Offset: 0x000E9C04
		public GameObject CreatGameObjectToParent(GameObject parent, GameObject Temp)
		{
			GameObject gameObject = Object.Instantiate<Transform>(Temp.transform).gameObject;
			gameObject.transform.SetParent(parent.transform);
			gameObject.SetActive(true);
			gameObject.transform.localScale = Vector3.one;
			return gameObject;
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x001F7BE8 File Offset: 0x001F5DE8
		public void autoSetTooltip()
		{
			this.Tooltip = Singleton.inventory.Tooltip;
			this.EquipTooltip = Singleton.inventory.EquipTooltip;
			this.BookTooltip = Singleton.inventory.BookTooltip;
			this.skillBookToolTip = Singleton.inventory.skillBookToolTip;
			this.DanyaoToolTip = Singleton.inventory.DanyaoToolTip;
			this.YaoCaoToolTip = Singleton.inventory.YaoCaoToolTip;
			this.DanYaoToolTip = Singleton.inventory.DanYaoToolTip;
			this.DanLuToolTip = Singleton.inventory.DanLuToolTip;
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x001F7C78 File Offset: 0x001F5E78
		public static int GetItemCD(item Item)
		{
			int value = EquipSeidJsonData2.DataDict[Item.itemID].value1;
			int oldCD = 1;
			if (SkillSeidJsonData29.DataDict.ContainsKey(value))
			{
				oldCD = SkillSeidJsonData29.DataDict[value].value1;
			}
			return Inventory2.GetItemCD(Item.Seid, oldCD);
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x001F7CC8 File Offset: 0x001F5EC8
		public static int GetItemCD(JSONObject Seid, int oldCD)
		{
			if (Seid == null || !Seid.HasField("SkillSeids"))
			{
				return oldCD;
			}
			return Seid["SkillSeids"].list.Find((JSONObject aa) => aa["id"].I == 29)["value1"].I;
		}

		// Token: 0x06004A4B RID: 19019 RVA: 0x001F7D2A File Offset: 0x001F5F2A
		public static string GetItemName(item Item, string baseName = "")
		{
			return Inventory2.GetItemName(Item.Seid, baseName);
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x001F7D38 File Offset: 0x001F5F38
		public static string GetItemName(JSONObject Seid, string baseName = "")
		{
			if (Seid == null || !Seid.HasField("Name"))
			{
				return baseName;
			}
			return Seid["Name"].str;
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x001F7D5C File Offset: 0x001F5F5C
		public static int GetItemQuality(item Item, int oldquality)
		{
			if (Item.Seid == null || !Item.Seid.HasField("quality"))
			{
				return oldquality;
			}
			return Item.Seid["quality"].I;
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x001F7D8F File Offset: 0x001F5F8F
		public static JSONObject GetItemAttackType(JSONObject Seid, JSONObject oldAttack)
		{
			if (Seid == null || !Seid.HasField("AttackType"))
			{
				return oldAttack;
			}
			return Seid["AttackType"];
		}

		// Token: 0x06004A4F RID: 19023 RVA: 0x001F7DAE File Offset: 0x001F5FAE
		public static string GetItemFirstDesc(JSONObject Seid, string oldAttack)
		{
			if (Seid == null || !Seid.HasField("SeidDesc"))
			{
				return oldAttack;
			}
			return Seid["SeidDesc"].Str;
		}

		// Token: 0x06004A50 RID: 19024 RVA: 0x001F7DD2 File Offset: 0x001F5FD2
		public static string GetItemDesc(JSONObject Seid, string oldAttack)
		{
			if (Seid == null || !Seid.HasField("Desc"))
			{
				return oldAttack;
			}
			return Seid["Desc"].Str;
		}

		// Token: 0x06004A51 RID: 19025 RVA: 0x001F7DF8 File Offset: 0x001F5FF8
		public void Show_Tooltip(item Item, int money = 0, int moneyPercent = 0)
		{
			try
			{
				_ItemJsonData itemJsonData = _ItemJsonData.DataDict[Item.itemID];
				JSONObject jsonobject = jsonData.instance.ItemJsonData[Item.itemID.ToString()];
				if (Item.Seid == null)
				{
					Item.Seid = new JSONObject();
				}
				string name = itemJsonData.name;
				TooltipItem component = this.Tooltip.GetComponent<TooltipItem>();
				component.Clear();
				string text = Inventory2.GetSkillBookDesc(itemJsonData);
				if (itemJsonData.type == 0)
				{
					int value = EquipSeidJsonData2.DataDict[itemJsonData.id].value1;
					JSONObject jsonobject2 = jsonData.instance.skillJsonData[value.ToString()];
					int itemCD = Inventory2.GetItemCD(Item);
					component.Label7.text = itemCD + "回合";
					string text2 = "";
					foreach (JSONObject jsonobject3 in Inventory2.GetItemAttackType(Item.Seid, jsonobject2["AttackType"]).list)
					{
						text2 += Tools.getStr("xibieFight" + jsonobject3.I);
					}
					component.Label8.text = text2;
					component.setCenterTextTitle("【冷却】", "【属性】", "");
					text = "[f28125]【主动】[-] [E0DDB4]" + text.Replace("主动：", "");
					this.showToolType = 1;
				}
				else if (itemJsonData.type != 4 && itemJsonData.type != 13 && (itemJsonData.type != 3 || !(name != "情报玉简")))
				{
					if (itemJsonData.type == 6)
					{
						Avatar player = Tools.instance.getPlayer();
						string liDanLeiXinStr = Tools.getLiDanLeiXinStr(itemJsonData.yaoZhi2);
						string liDanLeiXinStr2 = Tools.getLiDanLeiXinStr(itemJsonData.yaoZhi3);
						string liDanLeiXinStr3 = Tools.getLiDanLeiXinStr(itemJsonData.yaoZhi1);
						component.Label7.text = (player.GetHasZhuYaoShuXin(Item.itemID, itemJsonData.quality) ? liDanLeiXinStr : "未知");
						component.Label8.text = (player.GetHasFuYaoShuXin(Item.itemID, itemJsonData.quality) ? liDanLeiXinStr2 : "未知");
						component.Label9.text = (player.GetHasYaoYinShuXin(Item.itemID, itemJsonData.quality) ? liDanLeiXinStr3 : "未知");
						component.setCenterTextTitle("【主药】", "【辅药】", "【药引】");
						this.showToolType = 6;
					}
					else if (itemJsonData.type == 9 || itemJsonData.type == 14)
					{
						if (!Item.Seid.HasField("NaiJiu"))
						{
							Item.Seid = Tools.CreateItemSeid(Item.itemID);
						}
						component.setCenterTextTitle("【耐久】", "", "");
						int i = Item.Seid["NaiJiu"].I;
						int num = 100;
						if (itemJsonData.type == 14)
						{
							num = (int)jsonData.instance.LingZhouPinJie[itemJsonData.quality.ToString()]["Naijiu"];
						}
						component.Label7.text = i + "/" + num;
						this.showToolType = 9;
					}
					else if (itemJsonData.type == 5)
					{
						component.setCenterTextTitle("【耐药】", "【丹毒】", "");
						component.Label8.text = string.Concat(jsonobject["DanDu"].I);
						int jsonobject4 = Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, Item.itemID.ToString());
						int itemCanUseNum = item.GetItemCanUseNum(itemJsonData.id);
						component.Label7.text = jsonobject4 + "/" + itemCanUseNum;
						component.ShowPlayerInfo();
						this.showToolType = 5;
					}
					else
					{
						this.showToolType = 0;
					}
				}
				Regex regex = new Regex("\\{STVar=\\d*\\}");
				MatchCollection matchCollection = Regex.Matches(text, "\\{STVar=\\d*\\}");
				using (IEnumerator enumerator2 = matchCollection.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						int id;
						if (int.TryParse(((Match)enumerator2.Current).Value.Replace("{STVar=", "").Replace("}", ""), out id))
						{
							text = regex.Replace(text, GlobalValue.Get(id, "Inventory2.Show_Tooltip").ToString());
						}
					}
				}
				Regex.Matches(text, "【\\w*】");
				foreach (object obj in matchCollection)
				{
					Match match = (Match)obj;
					text = text.Replace(match.Value, "[42E395]" + match.Value + "[-]");
				}
				component.Label1.text = "[e0ddb4]" + Inventory2.GetItemFirstDesc(Item.Seid, text);
				component.Label2.text = "[bfba7d]" + Inventory2.GetItemDesc(Item.Seid, itemJsonData.desc2);
				if (UINPCJiaoHu.isDebugMode)
				{
					UILabel label = component.Label2;
					label.text = label.text + "\nUUID:" + Item.UUID;
				}
				int num2 = Inventory2.GetItemQuality(Item, itemJsonData.quality);
				List<string> tootipItemQualityColor = jsonData.instance.TootipItemQualityColor;
				string newValue = tootipItemQualityColor[num2 - 1] + Tools.getStr("shuzi" + num2) + Tools.getStr("jiecailiao");
				if (itemJsonData.type == 0 || itemJsonData.type == 1 || itemJsonData.type == 2)
				{
					num2++;
					if (Item.Seid != null && Item.Seid.HasField("qualitydesc"))
					{
						newValue = tootipItemQualityColor[num2 - 1] + Item.Seid["qualitydesc"].Str;
					}
					else
					{
						int num3 = (Item.Seid != null && Item.Seid.HasField("QPingZhi")) ? Item.Seid["QPingZhi"].I : itemJsonData.typePinJie;
						newValue = tootipItemQualityColor[num2 - 1] + ((num3 > 0) ? (Tools.getStr("shangzhongxia" + num3) + "品") : "") + Tools.getStr("EquipPingji" + (num2 - 1));
					}
				}
				else if (itemJsonData.type == 3 || itemJsonData.type == 4)
				{
					num2 *= 2;
					newValue = tootipItemQualityColor[num2 - 1] + Tools.getStr("pingjie" + itemJsonData.quality) + Tools.getStr("shangzhongxia" + itemJsonData.typePinJie);
				}
				else if (itemJsonData.type == 5 || itemJsonData.type == 9)
				{
					newValue = tootipItemQualityColor[num2 - 1] + Tools.getStr("shuzi" + num2) + Tools.getStr("pingdianyao");
				}
				else if (itemJsonData.type == 6 || itemJsonData.type == 7 || itemJsonData.type == 8)
				{
					newValue = tootipItemQualityColor[num2 - 1] + Tools.getStr("shuzi" + num2) + Tools.getStr("jiecailiao");
					if (itemJsonData.type == 8)
					{
						int wuWeiType = itemJsonData.WuWeiType;
						string text3;
						if (wuWeiType == 0)
						{
							text3 = "无";
						}
						else
						{
							text3 = LianQiWuWeiBiao.DataDict[wuWeiType].desc;
						}
						component.Label7.text = text3;
						int shuXingType = itemJsonData.ShuXingType;
						string text4;
						if (shuXingType == 0)
						{
							text4 = "无";
						}
						else
						{
							text4 = LianQiShuXinLeiBie.DataDict[shuXingType].desc;
						}
						component.Label8.text = text4;
						component.setCenterTextTitle("【种类】", "【属性】", "");
					}
				}
				component.Label3.text = Tools.getStr("pingjieCell").Replace("{X}", newValue).Replace("[333333]品级：", "");
				component.Label4.text = jsonData.instance.TootipItemNameColor[num2 - 1] + Inventory2.GetItemName(Item, itemJsonData.name);
				component.Label5.text = Tools.getStr("ItemType" + itemJsonData.type);
				if (money != 0)
				{
					int num4 = money;
					if (Item.Seid != null && Item.Seid.HasField("NaiJiu"))
					{
						num4 = (int)((float)num4 * ItemCellEX.getItemNaiJiuPrice(Item));
					}
					component.Label6.transform.parent.gameObject.SetActive(true);
					component.Label6.text = string.Concat(num4);
					if (moneyPercent > 0)
					{
						component.Label6.text = string.Format("[D55D21]{0}", num4);
					}
					else if (moneyPercent < 0)
					{
						component.Label6.text = string.Format("[75C0AE]{0}", num4);
					}
					component.ShowMoney();
				}
				else
				{
					component.Label6.transform.parent.gameObject.SetActive(false);
				}
				component.icon.mainTexture = Item.itemIcon;
				component.pingZhi.mainTexture = Item.itemPingZhi;
			}
			catch (Exception ex)
			{
				TooltipItem component2 = this.Tooltip.GetComponent<TooltipItem>();
				component2.Clear();
				component2.Label2.text = "[bfba7d]暂无说明[-]";
				Debug.LogError("物品出错" + Item.itemID.ToString());
				Debug.LogError(ex);
			}
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x00004095 File Offset: 0x00002295
		public void SaveInventory()
		{
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x001F8874 File Offset: 0x001F6A74
		public int addItemToNullInventory(int id, int num, string uuid, JSONObject Seid)
		{
			foreach (item item in this.inventory)
			{
				if (item.UUID == uuid)
				{
					if (item.itemNum >= num)
					{
						return -1;
					}
					num -= item.itemNum;
				}
			}
			if (this.ISPlayer && Tools.instance.getPlayer().FindEquipItemByUUID(uuid) != null)
			{
				return -2;
			}
			for (int i = 0; i < this.inventory.Count; i++)
			{
				if (this.inventory[i].itemID == -1)
				{
					this.inventory[i] = this.datebase.items[id].Clone();
					this.inventory[i].UUID = uuid;
					this.inventory[i].Seid = Seid;
					this.inventory[i].itemNum = num;
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06004A54 RID: 19028 RVA: 0x000B9D8C File Offset: 0x000B7F8C
		public bool isInPage(int page, int nowIndex, int OnePageMaxNum)
		{
			return nowIndex >= page * OnePageMaxNum && nowIndex < (page + 1) * OnePageMaxNum;
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x001F8990 File Offset: 0x001F6B90
		public void resteInventoryItem()
		{
			int num = this.inventory.Count;
			if (num > (int)this.FanYeCount && this.ISExchengePlan)
			{
				num = 24;
			}
			if (this.isNewJiaoYi)
			{
				num = 15;
			}
			for (int i = 0; i < num; i++)
			{
				this.inventory[i] = new item();
				this.inventory[i].itemNum = 1;
			}
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x001F89F8 File Offset: 0x001F6BF8
		public void resteAllInventoryItem()
		{
			for (int i = 0; i < this.inventory.Count; i++)
			{
				this.inventory[i] = new item();
				this.inventory[i].itemNum = 1;
			}
		}

		// Token: 0x06004A57 RID: 19031 RVA: 0x001F8A40 File Offset: 0x001F6C40
		public void loadInventory(JSONObject json)
		{
			foreach (JSONObject jsonobject in json.list)
			{
				this.addItemToNullInventory(jsonobject["ID"].I, jsonobject["Num"].I, jsonobject["UUID"].str, null);
			}
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x001F8AC4 File Offset: 0x001F6CC4
		public void loadNewNPCDropInventory(JSONObject json)
		{
			foreach (JSONObject jsonobject in json.list)
			{
				this.addItemToNullInventory(jsonobject["ID"].I, jsonobject["Num"].I, jsonobject["UUID"].str, jsonobject["seid"]);
			}
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x001F8B54 File Offset: 0x001F6D54
		public void LoadInventory()
		{
			this.resteInventoryItem();
			Avatar player = Tools.instance.getPlayer();
			new List<ITEM_INFO>();
			int num = 0;
			foreach (ITEM_INFO item_INFO in player.itemList.values)
			{
				if (player.FindEquipItemByUUID(item_INFO.uuid) == null)
				{
					if (!jsonData.instance.ItemJsonData.ContainsKey(string.Concat(item_INFO.itemId)))
					{
						Debug.LogError("找不到物品" + item_INFO.itemId);
					}
					else
					{
						JSONObject info = jsonData.instance.ItemJsonData[item_INFO.itemId.ToString()];
						if (((int)info["quality"].n == this.inventoryItemType || this.inventoryItemType == 0) && (this.inventItemLeiXing.Count == 0 || this.inventItemLeiXing.FindAll((int a) => a == (int)info["type"].n).Count > 0))
						{
							if (this.hideLearned)
							{
								if ((int)info["type"].n == 3)
								{
									int getskillID;
									if (item_INFO.itemId > jsonData.QingJiaoItemIDSegment)
									{
										getskillID = jsonData.instance.ItemsSeidJsonData[1][(item_INFO.itemId - jsonData.QingJiaoItemIDSegment).ToString()]["value1"].I;
									}
									else
									{
										getskillID = jsonData.instance.ItemsSeidJsonData[1][item_INFO.itemId.ToString()]["value1"].I;
									}
									if (Tools.instance.getPlayer().hasSkillList.Find((SkillItem aa) => aa.itemId == getskillID) != null)
									{
										continue;
									}
								}
								else if ((int)info["type"].n == 4)
								{
									int getskillID;
									if (item_INFO.itemId > jsonData.QingJiaoItemIDSegment)
									{
										getskillID = jsonData.instance.ItemsSeidJsonData[2][(item_INFO.itemId - jsonData.QingJiaoItemIDSegment).ToString()]["value1"].I;
									}
									else
									{
										getskillID = jsonData.instance.ItemsSeidJsonData[2][item_INFO.itemId.ToString()]["value1"].I;
									}
									if (Tools.instance.getPlayer().hasStaticSkillList.Find((SkillItem aa) => aa.itemId == getskillID) != null)
									{
										continue;
									}
								}
								else if ((int)info["type"].n == 10)
								{
									int id = (int)jsonData.instance.ItemsSeidJsonData[13][item_INFO.itemId.ToString()]["value1"].n;
									if (Tools.instance.getPlayer().ISStudyDanFan(id))
									{
										continue;
									}
								}
							}
							if (this.isInPage(this.nowIndex, num, (int)this.FanYeCount))
							{
								this.addItemToNullInventory(item_INFO.itemId, (int)item_INFO.itemCount, item_INFO.uuid, item_INFO.Seid);
							}
							num++;
						}
					}
				}
			}
			if (this.selectpage != null)
			{
				this.selectpage.setMaxPage(player.itemList.values.Count / (int)this.FanYeCount + 1);
			}
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x001F8F0C File Offset: 0x001F710C
		public bool HasUUIDItem(int start, int end, string uuid)
		{
			for (int i = start; i < end; i++)
			{
				if (this.inventory[i].UUID == uuid)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x001F8F44 File Offset: 0x001F7144
		public void PaiMaiMonstarLoad(int MonstarID)
		{
			this.MonstarID = MonstarID;
			this.resteInventoryItem();
			JSONObject jsonobject = jsonData.instance.AvatarBackpackJsonData[string.Concat(MonstarID)];
			List<JSONObject> list = jsonData.instance.AvatarBackpackJsonData[string.Concat(MonstarID)]["Backpack"].list.FindAll((JSONObject ccc) => (int)ccc["CanSell"].n == 1 && (int)ccc["Num"].n > 0);
			foreach (JSONObject jsonobject2 in list)
			{
				if (jsonobject2.HasField("paiMaiPlayer") && (int)jsonobject2["paiMaiPlayer"].n == 2)
				{
					this.addItemToNullInventory(jsonobject2["ItemID"].I, jsonobject2["Num"].I, jsonobject2["UUID"].str, jsonobject2["Seid"]);
				}
			}
			if (this.selectpage != null)
			{
				this.selectpage.setMaxPage(list.Count / (int)this.FanYeCount + 1);
			}
		}

		// Token: 0x06004A5C RID: 19036 RVA: 0x001F908C File Offset: 0x001F728C
		public void MonstarLoadInventory(int MonstarID)
		{
			this.MonstarID = MonstarID;
			this.resteInventoryItem();
			JSONObject jsonobject = jsonData.instance.AvatarBackpackJsonData[string.Concat(MonstarID)];
			int num = 0;
			List<JSONObject> list = jsonData.instance.AvatarBackpackJsonData[string.Concat(MonstarID)]["Backpack"].list.FindAll((JSONObject ccc) => (int)ccc["CanSell"].n == 1 && (int)ccc["Num"].n > 0);
			using (List<JSONObject>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JSONObject item = enumerator.Current;
					if ((jsonData.instance.ItemJsonData[string.Concat(item["ItemID"].I)]["quality"].I == this.inventoryItemType || this.inventoryItemType == 0) && (this.inventItemLeiXing.Count == 0 || this.inventItemLeiXing.FindAll((int a) => a == jsonData.instance.ItemJsonData[string.Concat(item["ItemID"].I)]["type"].I).Count > 0))
					{
						if (this.isInPage(this.nowIndex, num, (int)this.FanYeCount))
						{
							this.addItemToNullInventory(item["ItemID"].I, item["Num"].I, item["UUID"].str, item["Seid"]);
						}
						num++;
					}
				}
			}
			if (this.selectpage != null)
			{
				this.selectpage.setMaxPage(list.Count / (int)this.FanYeCount + 1);
			}
		}

		// Token: 0x06004A5D RID: 19037 RVA: 0x001F927C File Offset: 0x001F747C
		public bool CanClick()
		{
			return !(GameObject.Find("CanvasChoice(Clone)") != null);
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x00004095 File Offset: 0x00002295
		public void UseItem(int id)
		{
		}

		// Token: 0x06004A5F RID: 19039 RVA: 0x001F9294 File Offset: 0x001F7494
		public int GetSoltNum()
		{
			int num = 0;
			for (int i = 0; i < this.inventory.Count; i++)
			{
				if (this.inventory[i].itemID == -1)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x04004975 RID: 18805
		public jsonData.InventoryNUM count = jsonData.InventoryNUM.Max;

		// Token: 0x04004976 RID: 18806
		public jsonData.InventoryNUM FanYeCount = jsonData.InventoryNUM.Max;

		// Token: 0x04004977 RID: 18807
		public GameObject InventoryUI;

		// Token: 0x04004978 RID: 18808
		public GameObject Temp;

		// Token: 0x04004979 RID: 18809
		public List<item> inventory = new List<item>();

		// Token: 0x0400497A RID: 18810
		public ItemDatebase datebase;

		// Token: 0x0400497B RID: 18811
		private bool showInventory = true;

		// Token: 0x0400497C RID: 18812
		public bool draggingItem;

		// Token: 0x0400497D RID: 18813
		public item dragedItem;

		// Token: 0x0400497E RID: 18814
		public int dragedID;

		// Token: 0x0400497F RID: 18815
		public selectPage selectpage;

		// Token: 0x04004980 RID: 18816
		public GameObject BaseSlotOBJ;

		// Token: 0x04004981 RID: 18817
		public bool ResetToolTips = true;

		// Token: 0x04004982 RID: 18818
		public GameObject exchengkey;

		// Token: 0x04004983 RID: 18819
		private int showToolType;

		// Token: 0x04004984 RID: 18820
		public bool JustShow;

		// Token: 0x04004985 RID: 18821
		public string NomelBtn1Text = "";

		// Token: 0x04004986 RID: 18822
		public bool AutoSetBtnText;

		// Token: 0x04004987 RID: 18823
		public bool isNewJiaoYi;

		// Token: 0x04004988 RID: 18824
		public bool isPaiMai;

		// Token: 0x04004989 RID: 18825
		public GameObject Tooltip;

		// Token: 0x0400498A RID: 18826
		public GameObject EquipTooltip;

		// Token: 0x0400498B RID: 18827
		public GameObject BookTooltip;

		// Token: 0x0400498C RID: 18828
		public GameObject skillBookToolTip;

		// Token: 0x0400498D RID: 18829
		public GameObject DanyaoToolTip;

		// Token: 0x0400498E RID: 18830
		public GameObject YaoCaoToolTip;

		// Token: 0x0400498F RID: 18831
		public GameObject DanYaoToolTip;

		// Token: 0x04004990 RID: 18832
		public GameObject DanLuToolTip;

		// Token: 0x04004991 RID: 18833
		public int inventoryItemType;

		// Token: 0x04004992 RID: 18834
		public List<int> inventItemLeiXing = new List<int>();

		// Token: 0x04004993 RID: 18835
		public int nowIndex;

		// Token: 0x04004994 RID: 18836
		public bool shouldInit = true;

		// Token: 0x04004995 RID: 18837
		public bool ISExchengePlan;

		// Token: 0x04004996 RID: 18838
		public bool ISPlayer = true;

		// Token: 0x04004997 RID: 18839
		public int MonstarID;

		// Token: 0x04004998 RID: 18840
		public bool hideLearned;

		// Token: 0x02001596 RID: 5526
		// (Invoke) Token: 0x0600844C RID: 33868
		public delegate void EventsetItemCell<T>(T aa) where T : ItemCell;
	}
}
