using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using JSONClass;
using KBEngine;
using UnityEngine;

namespace GUIPackage
{
	// Token: 0x02000D7B RID: 3451
	public class Inventory2 : MonoBehaviour
	{
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060052FE RID: 21246 RVA: 0x002291A4 File Offset: 0x002273A4
		// (set) Token: 0x060052FD RID: 21245 RVA: 0x00229140 File Offset: 0x00227340
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

		// Token: 0x060052FF RID: 21247 RVA: 0x00229224 File Offset: 0x00227424
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

		// Token: 0x06005300 RID: 21248 RVA: 0x002292A0 File Offset: 0x002274A0
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

		// Token: 0x06005301 RID: 21249 RVA: 0x0003B645 File Offset: 0x00039845
		public void ZhengLi()
		{
			PlayerEx.Player.SortItem();
			this.LoadInventory();
		}

		// Token: 0x06005302 RID: 21250 RVA: 0x002292F8 File Offset: 0x002274F8
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

		// Token: 0x06005303 RID: 21251 RVA: 0x00229348 File Offset: 0x00227548
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

		// Token: 0x06005304 RID: 21252 RVA: 0x0003B657 File Offset: 0x00039857
		public void setMonstarItemLeiXin1()
		{
			this.setMonstartLeiXin(new List<int>());
		}

		// Token: 0x06005305 RID: 21253 RVA: 0x0003B664 File Offset: 0x00039864
		public void setMonstarItemLeiXin2()
		{
			this.setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["2"]["ItemFlag"]));
		}

		// Token: 0x06005306 RID: 21254 RVA: 0x0003B68F File Offset: 0x0003988F
		public void setMonstarItemLeiXin3()
		{
			this.setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["3"]["ItemFlag"]));
		}

		// Token: 0x06005307 RID: 21255 RVA: 0x0003B6BA File Offset: 0x000398BA
		public void setMonstarItemLeiXin4()
		{
			this.setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["4"]["ItemFlag"]));
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x0003B6E5 File Offset: 0x000398E5
		public void setMonstarItemLeiXin5()
		{
			this.setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["5"]["ItemFlag"]));
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x0003B710 File Offset: 0x00039910
		public void setMonstarItemLeiXin6()
		{
			this.setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["6"]["ItemFlag"]));
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x0003B73B File Offset: 0x0003993B
		public void setMonstarItemLeiXin7()
		{
			this.setMonstartLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["7"]["ItemFlag"]));
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x0003B766 File Offset: 0x00039966
		public void setItemLeiXin1()
		{
			this.setItemLeiXin(new List<int>());
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x0003B773 File Offset: 0x00039973
		public void setItemLeiXin2()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["2"]["ItemFlag"]));
		}

		// Token: 0x0600530D RID: 21261 RVA: 0x0003B79E File Offset: 0x0003999E
		public void setItemLeiXin3()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["3"]["ItemFlag"]));
		}

		// Token: 0x0600530E RID: 21262 RVA: 0x0003B7C9 File Offset: 0x000399C9
		public void setItemLeiXin4()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["4"]["ItemFlag"]));
		}

		// Token: 0x0600530F RID: 21263 RVA: 0x0003B7F4 File Offset: 0x000399F4
		public void setItemLeiXin5()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["5"]["ItemFlag"]));
		}

		// Token: 0x06005310 RID: 21264 RVA: 0x0003B81F File Offset: 0x00039A1F
		public void setItemLeiXin6()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["6"]["ItemFlag"]));
		}

		// Token: 0x06005311 RID: 21265 RVA: 0x0003B84A File Offset: 0x00039A4A
		public void setItemLeiXin7()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["7"]["ItemFlag"]));
		}

		// Token: 0x06005312 RID: 21266 RVA: 0x0003B773 File Offset: 0x00039973
		public void setExItemLeiXin2()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["2"]["ItemFlag"]));
		}

		// Token: 0x06005313 RID: 21267 RVA: 0x0003B79E File Offset: 0x0003999E
		public void setExItemLeiXin3()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["3"]["ItemFlag"]));
		}

		// Token: 0x06005314 RID: 21268 RVA: 0x0003B7C9 File Offset: 0x000399C9
		public void setExItemLeiXin4()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["4"]["ItemFlag"]));
		}

		// Token: 0x06005315 RID: 21269 RVA: 0x0003B875 File Offset: 0x00039A75
		public void setExItemLeiXin5()
		{
			this.setItemLeiXin(new List<int>
			{
				6
			});
		}

		// Token: 0x06005316 RID: 21270 RVA: 0x0003B889 File Offset: 0x00039A89
		public void setExItemLeiXin6()
		{
			this.setItemLeiXin(new List<int>
			{
				8
			});
		}

		// Token: 0x06005317 RID: 21271 RVA: 0x0003B81F File Offset: 0x00039A1F
		public void setExItemLeiXin7()
		{
			this.setItemLeiXin(Tools.JsonListToList(jsonData.instance.wupingfenlan["6"]["ItemFlag"]));
		}

		// Token: 0x06005318 RID: 21272 RVA: 0x002293A8 File Offset: 0x002275A8
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

		// Token: 0x06005319 RID: 21273 RVA: 0x00229478 File Offset: 0x00227678
		public void ChangeItem(ref item Item1, ref item Item2)
		{
			item item = new item();
			item = Item1;
			Item1 = Item2;
			Item2 = item;
		}

		// Token: 0x0600531A RID: 21274 RVA: 0x0003B89D File Offset: 0x00039A9D
		public void Clear_dragedItem()
		{
			this.dragedItem = new item();
			this.draggingItem = false;
			this.Temp.GetComponent<UITexture>().mainTexture = null;
		}

		// Token: 0x0600531B RID: 21275 RVA: 0x0003B8C2 File Offset: 0x00039AC2
		public void BackItem()
		{
			if (this.draggingItem)
			{
				this.inventory[this.dragedID] = this.dragedItem;
				this.Clear_dragedItem();
			}
		}

		// Token: 0x0600531C RID: 21276 RVA: 0x00229498 File Offset: 0x00227698
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

		// Token: 0x0600531D RID: 21277 RVA: 0x0003B8E9 File Offset: 0x00039AE9
		private void InitInventory()
		{
			this.InitSlot("Slot", "Win/item");
			this.InventoryUI.SetActive(this.showInventory);
		}

		// Token: 0x0600531E RID: 21278 RVA: 0x0003B90C File Offset: 0x00039B0C
		public void InitSlot(string SlotName, string ParentPatch = "Win/item")
		{
			this.InitSlot(SlotName, (int)this.count, ParentPatch);
		}

		// Token: 0x0600531F RID: 21279 RVA: 0x0022951C File Offset: 0x0022771C
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

		// Token: 0x06005320 RID: 21280 RVA: 0x00229624 File Offset: 0x00227824
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

		// Token: 0x06005321 RID: 21281 RVA: 0x00229754 File Offset: 0x00227954
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

		// Token: 0x06005322 RID: 21282 RVA: 0x002298EC File Offset: 0x00227AEC
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

		// Token: 0x06005323 RID: 21283 RVA: 0x00229A04 File Offset: 0x00227C04
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

		// Token: 0x06005324 RID: 21284 RVA: 0x00229AAC File Offset: 0x00227CAC
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

		// Token: 0x06005325 RID: 21285 RVA: 0x00229AEC File Offset: 0x00227CEC
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

		// Token: 0x06005326 RID: 21286 RVA: 0x00229B3C File Offset: 0x00227D3C
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

		// Token: 0x06005327 RID: 21287 RVA: 0x00229B74 File Offset: 0x00227D74
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

		// Token: 0x06005328 RID: 21288 RVA: 0x00229C48 File Offset: 0x00227E48
		public void setindexItem(int i, int ItemID, int num, string uuid)
		{
			this.inventory[i] = this.datebase.items[ItemID].Clone();
			this.inventory[i].UUID = uuid;
			this.inventory[i].itemNum = num;
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x00229C9C File Offset: 0x00227E9C
		public void setInventoryIndexItem(int i, int numIndex, int num, string uuid)
		{
			this.inventory[i] = this.datebase.items[this.inventory[numIndex].itemID].Clone();
			this.inventory[i].UUID = uuid;
			this.inventory[i].Seid = this.inventory[numIndex].Seid;
			this.inventory[i].itemNum = num;
		}

		// Token: 0x0600532A RID: 21290 RVA: 0x00229D24 File Offset: 0x00227F24
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

		// Token: 0x0600532B RID: 21291 RVA: 0x00229D5C File Offset: 0x00227F5C
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

		// Token: 0x0600532C RID: 21292 RVA: 0x00229DF4 File Offset: 0x00227FF4
		public void reduceItem(int index, int num)
		{
			this.inventory[index].itemNum -= num;
			if (this.inventory[index].itemNum <= 0)
			{
				int itemNum = this.inventory[index].itemNum;
				this.inventory[index] = new item();
			}
		}

		// Token: 0x0600532D RID: 21293 RVA: 0x00229E54 File Offset: 0x00228054
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

		// Token: 0x0600532E RID: 21294 RVA: 0x00229EB0 File Offset: 0x002280B0
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

		// Token: 0x0600532F RID: 21295 RVA: 0x00229EF0 File Offset: 0x002280F0
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

		// Token: 0x06005330 RID: 21296 RVA: 0x00229F28 File Offset: 0x00228128
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
						if ((int)keyValuePair.Value["Skill_ID"].n == num && (int)keyValuePair.Value["Skill_Lv"].n == Tools.instance.getPlayer().getLevelType())
						{
							text = Tools.getDescByID(Tools.instance.Code64ToString(keyValuePair.Value["descr"].str), (int)keyValuePair.Value["id"].n);
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
					if ((int)jsonobject["Skill_ID"].n == num2 && (int)jsonobject["Skill_Lv"].n == 1)
					{
						text = Tools.instance.Code64ToString(jsonobject["descr"].str);
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x06005331 RID: 21297 RVA: 0x0022A0E4 File Offset: 0x002282E4
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

		// Token: 0x06005332 RID: 21298 RVA: 0x0001E4F0 File Offset: 0x0001C6F0
		public GameObject CreatGameObjectToParent(GameObject parent, GameObject Temp)
		{
			GameObject gameObject = Object.Instantiate<Transform>(Temp.transform).gameObject;
			gameObject.transform.SetParent(parent.transform);
			gameObject.SetActive(true);
			gameObject.transform.localScale = Vector3.one;
			return gameObject;
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x0022A1F0 File Offset: 0x002283F0
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

		// Token: 0x06005334 RID: 21300 RVA: 0x0022A280 File Offset: 0x00228480
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

		// Token: 0x06005335 RID: 21301 RVA: 0x0022A2D0 File Offset: 0x002284D0
		public static int GetItemCD(JSONObject Seid, int oldCD)
		{
			if (Seid == null || !Seid.HasField("SkillSeids"))
			{
				return oldCD;
			}
			return Seid["SkillSeids"].list.Find((JSONObject aa) => aa["id"].I == 29)["value1"].I;
		}

		// Token: 0x06005336 RID: 21302 RVA: 0x0003B91C File Offset: 0x00039B1C
		public static string GetItemName(item Item, string baseName = "")
		{
			return Inventory2.GetItemName(Item.Seid, baseName);
		}

		// Token: 0x06005337 RID: 21303 RVA: 0x0003B92A File Offset: 0x00039B2A
		public static string GetItemName(JSONObject Seid, string baseName = "")
		{
			if (Seid == null || !Seid.HasField("Name"))
			{
				return baseName;
			}
			return Seid["Name"].str;
		}

		// Token: 0x06005338 RID: 21304 RVA: 0x0003B94E File Offset: 0x00039B4E
		public static int GetItemQuality(item Item, int oldquality)
		{
			if (Item.Seid == null || !Item.Seid.HasField("quality"))
			{
				return oldquality;
			}
			return Item.Seid["quality"].I;
		}

		// Token: 0x06005339 RID: 21305 RVA: 0x0003B981 File Offset: 0x00039B81
		public static JSONObject GetItemAttackType(JSONObject Seid, JSONObject oldAttack)
		{
			if (Seid == null || !Seid.HasField("AttackType"))
			{
				return oldAttack;
			}
			return Seid["AttackType"];
		}

		// Token: 0x0600533A RID: 21306 RVA: 0x0003B9A0 File Offset: 0x00039BA0
		public static string GetItemFirstDesc(JSONObject Seid, string oldAttack)
		{
			if (Seid == null || !Seid.HasField("SeidDesc"))
			{
				return oldAttack;
			}
			return Seid["SeidDesc"].Str;
		}

		// Token: 0x0600533B RID: 21307 RVA: 0x0003B9C4 File Offset: 0x00039BC4
		public static string GetItemDesc(JSONObject Seid, string oldAttack)
		{
			if (Seid == null || !Seid.HasField("Desc"))
			{
				return oldAttack;
			}
			return Seid["Desc"].Str;
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x0022A334 File Offset: 0x00228534
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

		// Token: 0x0600533D RID: 21309 RVA: 0x000042DD File Offset: 0x000024DD
		public void SaveInventory()
		{
		}

		// Token: 0x0600533E RID: 21310 RVA: 0x0022ADB0 File Offset: 0x00228FB0
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

		// Token: 0x0600533F RID: 21311 RVA: 0x000183D5 File Offset: 0x000165D5
		public bool isInPage(int page, int nowIndex, int OnePageMaxNum)
		{
			return nowIndex >= page * OnePageMaxNum && nowIndex < (page + 1) * OnePageMaxNum;
		}

		// Token: 0x06005340 RID: 21312 RVA: 0x0022AECC File Offset: 0x002290CC
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

		// Token: 0x06005341 RID: 21313 RVA: 0x0022AF34 File Offset: 0x00229134
		public void resteAllInventoryItem()
		{
			for (int i = 0; i < this.inventory.Count; i++)
			{
				this.inventory[i] = new item();
				this.inventory[i].itemNum = 1;
			}
		}

		// Token: 0x06005342 RID: 21314 RVA: 0x0022AF7C File Offset: 0x0022917C
		public void loadInventory(JSONObject json)
		{
			foreach (JSONObject jsonobject in json.list)
			{
				this.addItemToNullInventory((int)jsonobject["ID"].n, (int)jsonobject["Num"].n, jsonobject["UUID"].str, null);
			}
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x0022B004 File Offset: 0x00229204
		public void loadNewNPCDropInventory(JSONObject json)
		{
			foreach (JSONObject jsonobject in json.list)
			{
				this.addItemToNullInventory((int)jsonobject["ID"].n, (int)jsonobject["Num"].n, jsonobject["UUID"].str, jsonobject["seid"]);
			}
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x0022B094 File Offset: 0x00229294
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
									if (item_INFO.itemId > 100000)
									{
										getskillID = jsonData.instance.ItemsSeidJsonData[1][(item_INFO.itemId - 100000).ToString()]["value1"].I;
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
									if (item_INFO.itemId > 100000)
									{
										getskillID = jsonData.instance.ItemsSeidJsonData[2][(item_INFO.itemId - 100000).ToString()]["value1"].I;
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

		// Token: 0x06005345 RID: 21317 RVA: 0x0022B44C File Offset: 0x0022964C
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

		// Token: 0x06005346 RID: 21318 RVA: 0x0022B484 File Offset: 0x00229684
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
					this.addItemToNullInventory((int)jsonobject2["ItemID"].n, (int)jsonobject2["Num"].n, jsonobject2["UUID"].str, jsonobject2["Seid"]);
				}
			}
			if (this.selectpage != null)
			{
				this.selectpage.setMaxPage(list.Count / (int)this.FanYeCount + 1);
			}
		}

		// Token: 0x06005347 RID: 21319 RVA: 0x0022B5D0 File Offset: 0x002297D0
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
					if (((int)jsonData.instance.ItemJsonData[string.Concat((int)item["ItemID"].n)]["quality"].n == this.inventoryItemType || this.inventoryItemType == 0) && (this.inventItemLeiXing.Count == 0 || this.inventItemLeiXing.FindAll((int a) => a == (int)jsonData.instance.ItemJsonData[string.Concat((int)item["ItemID"].n)]["type"].n).Count > 0))
					{
						if (this.isInPage(this.nowIndex, num, (int)this.FanYeCount))
						{
							this.addItemToNullInventory((int)item["ItemID"].n, (int)item["Num"].n, item["UUID"].str, item["Seid"]);
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

		// Token: 0x06005348 RID: 21320 RVA: 0x0003B9E8 File Offset: 0x00039BE8
		public bool CanClick()
		{
			return !(GameObject.Find("CanvasChoice(Clone)") != null);
		}

		// Token: 0x06005349 RID: 21321 RVA: 0x000042DD File Offset: 0x000024DD
		public void UseItem(int id)
		{
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x0022B7C4 File Offset: 0x002299C4
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

		// Token: 0x040052FA RID: 21242
		public jsonData.InventoryNUM count = jsonData.InventoryNUM.Max;

		// Token: 0x040052FB RID: 21243
		public jsonData.InventoryNUM FanYeCount = jsonData.InventoryNUM.Max;

		// Token: 0x040052FC RID: 21244
		public GameObject InventoryUI;

		// Token: 0x040052FD RID: 21245
		public GameObject Temp;

		// Token: 0x040052FE RID: 21246
		public List<item> inventory = new List<item>();

		// Token: 0x040052FF RID: 21247
		public ItemDatebase datebase;

		// Token: 0x04005300 RID: 21248
		private bool showInventory = true;

		// Token: 0x04005301 RID: 21249
		public bool draggingItem;

		// Token: 0x04005302 RID: 21250
		public item dragedItem;

		// Token: 0x04005303 RID: 21251
		public int dragedID;

		// Token: 0x04005304 RID: 21252
		public selectPage selectpage;

		// Token: 0x04005305 RID: 21253
		public GameObject BaseSlotOBJ;

		// Token: 0x04005306 RID: 21254
		public bool ResetToolTips = true;

		// Token: 0x04005307 RID: 21255
		public GameObject exchengkey;

		// Token: 0x04005308 RID: 21256
		private int showToolType;

		// Token: 0x04005309 RID: 21257
		public bool JustShow;

		// Token: 0x0400530A RID: 21258
		public string NomelBtn1Text = "";

		// Token: 0x0400530B RID: 21259
		public bool AutoSetBtnText;

		// Token: 0x0400530C RID: 21260
		public bool isNewJiaoYi;

		// Token: 0x0400530D RID: 21261
		public bool isPaiMai;

		// Token: 0x0400530E RID: 21262
		public GameObject Tooltip;

		// Token: 0x0400530F RID: 21263
		public GameObject EquipTooltip;

		// Token: 0x04005310 RID: 21264
		public GameObject BookTooltip;

		// Token: 0x04005311 RID: 21265
		public GameObject skillBookToolTip;

		// Token: 0x04005312 RID: 21266
		public GameObject DanyaoToolTip;

		// Token: 0x04005313 RID: 21267
		public GameObject YaoCaoToolTip;

		// Token: 0x04005314 RID: 21268
		public GameObject DanYaoToolTip;

		// Token: 0x04005315 RID: 21269
		public GameObject DanLuToolTip;

		// Token: 0x04005316 RID: 21270
		public int inventoryItemType;

		// Token: 0x04005317 RID: 21271
		public List<int> inventItemLeiXing = new List<int>();

		// Token: 0x04005318 RID: 21272
		public int nowIndex;

		// Token: 0x04005319 RID: 21273
		public bool shouldInit = true;

		// Token: 0x0400531A RID: 21274
		public bool ISExchengePlan;

		// Token: 0x0400531B RID: 21275
		public bool ISPlayer = true;

		// Token: 0x0400531C RID: 21276
		public int MonstarID;

		// Token: 0x0400531D RID: 21277
		public bool hideLearned;

		// Token: 0x02000D7C RID: 3452
		// (Invoke) Token: 0x0600534D RID: 21325
		public delegate void EventsetItemCell<T>(T aa) where T : ItemCell;
	}
}
