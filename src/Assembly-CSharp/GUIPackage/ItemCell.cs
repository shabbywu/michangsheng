using System;
using KBEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GUIPackage
{
	// Token: 0x02000D82 RID: 3458
	public class ItemCell : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600535D RID: 21341 RVA: 0x0003BA72 File Offset: 0x00039C72
		public item GetItem
		{
			get
			{
				return this.Item;
			}
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x0003BA7A File Offset: 0x00039C7A
		private void Start()
		{
			if (this.inventory == null)
			{
				this.inventory = Singleton.inventory;
			}
			this.Item = this.inventory.inventory[int.Parse(base.name)];
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x0022B8B4 File Offset: 0x00229AB4
		public void ShowName()
		{
			if (this.Item.itemID != -1 && this.KeyName != null && this.KeyObject != null)
			{
				JSONObject jsonobject = jsonData.instance.ItemJsonData[this.Item.itemID.ToString()];
				this.KeyName.text = string.Concat(new string[]
				{
					"[",
					jsonData.instance.NameColor[Inventory2.GetItemQuality(this.Item, jsonobject["quality"].I) - 1],
					"]",
					Inventory2.GetItemName(this.Item, Tools.instance.Code64ToString(jsonobject["name"].str)),
					"[-]"
				});
				this.KeyObject.SetActive(true);
				return;
			}
			if (this.KeyObject != null)
			{
				this.KeyObject.SetActive(false);
			}
		}

		// Token: 0x06005360 RID: 21344 RVA: 0x0003BAB6 File Offset: 0x00039CB6
		private void Update()
		{
			if (this.refreshCD < 0f)
			{
				this.UpdateRefresh();
				this.refreshCD = 0.2f;
				return;
			}
			this.refreshCD -= Time.deltaTime;
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x0022B9C0 File Offset: 0x00229BC0
		public void UpdateRefresh()
		{
			this.Item = this.inventory.inventory[int.Parse(base.name)];
			if (!this.isUGUI)
			{
				this.Icon.GetComponent<UITexture>().mainTexture = this.inventory.inventory[int.Parse(base.name)].itemIcon;
				if (this.Item.Seid != null && this.Item.Seid.HasField("quality"))
				{
					int i = this.Item.Seid["quality"].I;
					if (this.itemDatebase == null)
					{
						this.itemDatebase = jsonData.instance.gameObject.GetComponent<ItemDatebase>();
					}
					this.PingZhi.GetComponent<UITexture>().mainTexture = this.itemDatebase.PingZhi[i];
					if (this.PingZhiUI != null)
					{
						this.PingZhiUI.GetComponent<UI2DSprite>().sprite2D = this.itemDatebase.PingZhiUp[i];
					}
				}
				else
				{
					this.PingZhi.GetComponent<UITexture>().mainTexture = this.inventory.inventory[int.Parse(base.name)].itemPingZhi;
					if (this.PingZhiUI != null)
					{
						this.PingZhiUI.GetComponent<UI2DSprite>().sprite2D = this.inventory.inventory[int.Parse(base.name)].itemPingZhiUP;
					}
				}
				if (this.inventory.inventory[int.Parse(base.name)].itemNum > 1)
				{
					this.Num.GetComponent<UILabel>().text = this.inventory.inventory[int.Parse(base.name)].itemNum.ToString();
				}
				else
				{
					this.Num.GetComponent<UILabel>().text = "";
				}
				this.showYiWu();
			}
			else
			{
				Texture2D itemIcon = this.inventory.inventory[int.Parse(base.name)].itemIcon;
				Texture2D itemPingZhi = this.inventory.inventory[int.Parse(base.name)].itemPingZhi;
				this.Icon.GetComponent<Image>().sprite = Sprite.Create(itemIcon, new Rect(0f, 0f, (float)itemIcon.width, (float)itemIcon.height), new Vector2(0.5f, 0.5f));
				this.PingZhi.GetComponent<Image>().sprite = Sprite.Create(itemPingZhi, new Rect(0f, 0f, (float)itemPingZhi.width, (float)itemPingZhi.height), new Vector2(0.5f, 0.5f));
				if (this.inventory.inventory[int.Parse(base.name)].itemNum > 1)
				{
					this.Num.GetComponent<Text>().text = this.inventory.inventory[int.Parse(base.name)].itemNum.ToString();
				}
				else
				{
					this.Num.GetComponent<Text>().text = "";
				}
			}
			this.ShowName();
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x0022BD0C File Offset: 0x00229F0C
		public void showYiWu()
		{
			if (this.YiWu != null)
			{
				if (this.inventory.inventory[int.Parse(base.name)].itemName != null && this.inventory.inventory[int.Parse(base.name)].itemID != -1)
				{
					item item = this.inventory.inventory[int.Parse(base.name)];
					JSONObject jsonobject = jsonData.instance.ItemJsonData[item.itemID.ToString()];
					if ((int)jsonobject["type"].n == 3)
					{
						int getskillID = 0;
						try
						{
							if (item.itemID > 100000)
							{
								JSONObject jsonobject2 = jsonData.instance.ItemsSeidJsonData[1][(item.itemID - 100000).ToString()];
								getskillID = jsonobject2["value1"].I;
							}
							else
							{
								JSONObject jsonobject2 = jsonData.instance.ItemsSeidJsonData[1][item.itemID.ToString()];
								getskillID = jsonobject2["value1"].I;
							}
						}
						catch
						{
							Debug.LogError(string.Format("获取神通特性出错，请检查消耗品特性表1，物品ID{0}", item.itemID));
						}
						if (Tools.instance.getPlayer().hasSkillList.Find((SkillItem aa) => aa.itemId == getskillID) != null)
						{
							this.YiWu.SetActive(true);
						}
						else
						{
							this.YiWu.SetActive(false);
						}
					}
					else if ((int)jsonobject["type"].n == 4)
					{
						int getskillID = 0;
						try
						{
							if (item.itemID > 100000)
							{
								JSONObject jsonobject3 = jsonData.instance.ItemsSeidJsonData[2][(item.itemID - 100000).ToString()];
								getskillID = jsonobject3["value1"].I;
							}
							else
							{
								JSONObject jsonobject3 = jsonData.instance.ItemsSeidJsonData[2][item.itemID.ToString()];
								getskillID = jsonobject3["value1"].I;
							}
						}
						catch
						{
							Debug.LogError(string.Format("获取功法特性出错，请检查消耗品特性表2，物品ID{0}", item.itemID));
						}
						if (Tools.instance.getPlayer().hasStaticSkillList.Find((SkillItem aa) => aa.itemId == getskillID) != null)
						{
							this.YiWu.SetActive(true);
						}
						else
						{
							this.YiWu.SetActive(false);
						}
					}
					else if ((int)jsonobject["type"].n == 10)
					{
						int id = (int)jsonData.instance.ItemsSeidJsonData[13][string.Concat(item.itemID)]["value1"].n;
						if (Tools.instance.getPlayer().ISStudyDanFan(id))
						{
							this.YiWu.SetActive(true);
						}
						else
						{
							this.YiWu.SetActive(false);
						}
					}
					else
					{
						this.YiWu.SetActive(false);
					}
				}
				else
				{
					this.YiWu.SetActive(false);
				}
			}
			if (this.NaiYao != null)
			{
				if (this.inventory.inventory[int.Parse(base.name)].itemName != null && this.inventory.inventory[int.Parse(base.name)].itemID != -1)
				{
					item item2 = this.inventory.inventory[int.Parse(base.name)];
					if ((int)jsonData.instance.ItemJsonData[item2.itemID.ToString()]["type"].n != 5)
					{
						this.NaiYao.SetActive(false);
						return;
					}
					int jsonobject4 = Tools.getJsonobject(Tools.instance.getPlayer().NaiYaoXin, item2.itemID.ToString());
					int itemCanUseNum = item.GetItemCanUseNum(this.Item.itemID);
					if (jsonobject4 >= itemCanUseNum)
					{
						this.NaiYao.SetActive(true);
						return;
					}
					this.NaiYao.SetActive(false);
					return;
				}
				else
				{
					this.NaiYao.SetActive(false);
				}
			}
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x0003BAE9 File Offset: 0x00039CE9
		private void OnDrop(GameObject obj)
		{
			if (Input.GetMouseButtonUp(0) && !this.JustShow)
			{
				this.chengeItem();
			}
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x0003BB01 File Offset: 0x00039D01
		private void OnPress()
		{
			this.PCOnPress();
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x0022C180 File Offset: 0x0022A380
		public virtual void MobilePress()
		{
			if (this.Item.itemID == -1)
			{
				return;
			}
			if (!this.CanShowTooltips)
			{
				return;
			}
			this.PCOnHover(true);
			Singleton.ToolTipsBackGround.openTooltips();
			TooltipsBackgroundi toolTipsBackGround = Singleton.ToolTipsBackGround;
			toolTipsBackGround.CloseAction = delegate()
			{
				this.PCOnHover(false);
			};
			toolTipsBackGround.UseAction = delegate()
			{
				this.ClickUseItem();
			};
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x0022C1E0 File Offset: 0x0022A3E0
		public virtual void PCOnPress()
		{
			if (this.JustShow)
			{
				return;
			}
			this.Item = this.inventory.inventory[int.Parse(base.name)];
			if (Input.GetMouseButtonDown(1) && this.Item.itemName != null && !this.inventory.draggingItem)
			{
				this.ClickUseItem();
			}
			if (this.ISPrepare)
			{
				return;
			}
			if (Input.GetMouseButtonDown(0))
			{
				this.chengeItem();
			}
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x0022C258 File Offset: 0x0022A458
		public void ClickUseItem()
		{
			int num = (int)jsonData.instance.ItemJsonData[this.inventory.inventory[int.Parse(base.name)].itemID.ToString()]["type"].n;
			if ((num == 6 || num == 8) && (int)jsonData.instance.ItemJsonData[this.inventory.inventory[int.Parse(base.name)].itemID.ToString()]["vagueType"].n == 0)
			{
				return;
			}
			if (num == 7 || num == 9)
			{
				return;
			}
			if (num - 3 <= 1 || num == 13)
			{
				UIPopTip.Inst.Pop("需要在洞府或客栈中闭关领悟", PopTipIconType.叹号);
				return;
			}
			this.inventory.UseItem(int.Parse(base.name));
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x0022C338 File Offset: 0x0022A538
		private void chengeItem()
		{
			if (!this.inventory.CanClick())
			{
				return;
			}
			if (!Singleton.key.draggingKey)
			{
				if (this.Item.itemName != null)
				{
					if (!this.inventory.draggingItem)
					{
						this.inventory.dragedID = int.Parse(base.name);
						this.inventory.draggingItem = true;
						this.inventory.dragedItem = this.inventory.inventory[int.Parse(base.name)];
						this.inventory.inventory[int.Parse(base.name)] = new item();
						return;
					}
					if (Singleton.equip.is_draged && this.inventory.dragedItem.itemType != this.Item.itemType)
					{
						for (int i = 0; i < this.inventory.inventory.Count; i++)
						{
							if (this.inventory.inventory[i].itemID == -1)
							{
								this.inventory.inventory[i] = this.inventory.dragedItem;
								this.inventory.Clear_dragedItem();
								Singleton.equip.is_draged = false;
							}
						}
						return;
					}
					this.inventory.ChangeItem(ref this.Item, ref Singleton.inventory.dragedItem);
					this.inventory.inventory[int.Parse(base.name)] = this.Item;
					return;
				}
				else if (this.inventory.draggingItem)
				{
					this.inventory.ChangeItem(ref this.Item, ref this.inventory.dragedItem);
					this.inventory.inventory[int.Parse(base.name)] = this.Item;
					this.inventory.Temp.GetComponent<UITexture>().mainTexture = this.inventory.dragedItem.itemIcon;
					this.inventory.draggingItem = false;
					Singleton.equip.is_draged = false;
					return;
				}
			}
			else
			{
				this.inventory.Clear_dragedItem();
			}
		}

		// Token: 0x06005369 RID: 21353 RVA: 0x0022C550 File Offset: 0x0022A750
		public virtual int getItemPrice()
		{
			int num = (int)jsonData.instance.ItemJsonData[string.Concat(this.inventory.inventory[int.Parse(base.name)].itemID)]["price"].n;
			if (this.inventory.inventory[int.Parse(base.name)].Seid != null && this.inventory.inventory[int.Parse(base.name)].Seid.HasField("Money"))
			{
				num = this.inventory.inventory[int.Parse(base.name)].Seid["Money"].I;
			}
			return (int)((float)num * 0.5f);
		}

		// Token: 0x0600536A RID: 21354 RVA: 0x0003BB09 File Offset: 0x00039D09
		private void OnHover(bool isOver)
		{
			this.PCOnHover(isOver);
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x00004050 File Offset: 0x00002250
		public virtual int MoneyPercent(item a)
		{
			return 0;
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x0022C630 File Offset: 0x0022A830
		public virtual void PCOnHover(bool isOver)
		{
			if (isOver && this.inventory.inventory[int.Parse(base.name)].itemName != null)
			{
				this.inventory.Show_Tooltip(this.inventory.inventory[int.Parse(base.name)], this.getItemPrice(), this.MoneyPercent(this.inventory.inventory[int.Parse(base.name)]));
				this.inventory.showTooltip = true;
				return;
			}
			this.inventory.showTooltip = false;
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x0003BB12 File Offset: 0x00039D12
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.isUGUI)
			{
				this.OnHover(false);
			}
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x0003BB23 File Offset: 0x00039D23
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.isUGUI)
			{
				this.OnHover(true);
			}
		}

		// Token: 0x04005326 RID: 21286
		public GameObject Icon;

		// Token: 0x04005327 RID: 21287
		public GameObject Num;

		// Token: 0x04005328 RID: 21288
		public GameObject PingZhi;

		// Token: 0x04005329 RID: 21289
		public GameObject PingZhiUI;

		// Token: 0x0400532A RID: 21290
		public ItemDatebase itemDatebase;

		// Token: 0x0400532B RID: 21291
		public bool JustShow;

		// Token: 0x0400532C RID: 21292
		public bool isPlayer = true;

		// Token: 0x0400532D RID: 21293
		public bool isUGUI;

		// Token: 0x0400532E RID: 21294
		public Inventory2 inventory;

		// Token: 0x0400532F RID: 21295
		public GameObject YiWu;

		// Token: 0x04005330 RID: 21296
		public GameObject NaiYao;

		// Token: 0x04005331 RID: 21297
		public string Btn1Text = "";

		// Token: 0x04005332 RID: 21298
		public bool AutoSetBtnText;

		// Token: 0x04005333 RID: 21299
		public bool CanShowTooltips = true;

		// Token: 0x04005334 RID: 21300
		public bool CanDrawItem;

		// Token: 0x04005335 RID: 21301
		protected item Item = new item();

		// Token: 0x04005336 RID: 21302
		public bool ISPrepare;

		// Token: 0x04005337 RID: 21303
		public GameObject KeyObject;

		// Token: 0x04005338 RID: 21304
		public UILabel KeyName;

		// Token: 0x04005339 RID: 21305
		private float refreshCD;
	}
}
